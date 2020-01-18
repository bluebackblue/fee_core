

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。オブジェクト化。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonToObject_Work
	*/
	public class JsonToObject_Work
	{
		/** ModeSetList

			List。設定。

		*/
		public enum ModeSetList
		{
			/** 開始。
			*/
			Start = 0,

			/** 反映。
			*/
			Fix = 100,
		}

		/** ModeAddList

			List。追加。

		*/
		public enum ModeAddList
		{
			/** 開始。
			*/
			Start = 1,

			/** 反映。
			*/
			Fix = 101,
		}

		/** ModeAddDictionary

			Dictionary。追加。

		*/
		public enum ModeAddDictionary
		{
			/** 開始。
			*/
			Start = 2,

			/** 反映。
			*/
			Fix = 102,
		}

		/** ModeFieldInfo

			FieldInfo

		*/
		public enum ModeFieldInfo
		{
			/** 開始。
			*/
			Start = 3,

			/** 反映。
			*/
			Fix = 103,
		}

		/** モード。
		*/
		int mode;

		/** コンバート元。ＪＳＯＮ。
		*/
		private JsonItem from_jsonitem;

		/** コンバート先。インスタンス。
		*/
		private System.Type to_type;
		private System.Object to_object;
		private System.Collections.IList to_list;
		private int to_index;
		private System.Collections.IDictionary to_dictionary;
		private string to_key_string;
		private System.Reflection.FieldInfo to_fieldinfo;
		private System.Object to_parent_object;

		/** constructor

			List。設定。

		*/
		public JsonToObject_Work(ModeSetList a_mode,JsonItem a_from_listitem_json,System.Collections.IList a_to_list,int a_to_index,System.Type a_to_listitem_type)
		{
			//モード。
			this.mode = (int)a_mode;

			//設定元。
			this.from_jsonitem = a_from_listitem_json;

			//設定先。
			this.to_type = a_to_listitem_type;
			this.to_object = null;
			this.to_list = a_to_list;
			this.to_index = a_to_index;
			this.to_dictionary = null;
			this.to_key_string = null;
			this.to_fieldinfo = null;
			this.to_parent_object = null;
		}

		
		/** constructor

			List。追加。

		*/
		public JsonToObject_Work(ModeAddList a_mode,JsonItem a_from_listitem_json,System.Collections.IList a_to_list,System.Type a_to_listitem_type)
		{
			//モード。
			this.mode = (int)a_mode;

			//設定元。
			this.from_jsonitem = a_from_listitem_json;

			//設定先。
			this.to_type = a_to_listitem_type;
			this.to_object = null;
			this.to_list = a_to_list;
			this.to_index = 0;
			this.to_dictionary = null;
			this.to_key_string = null;
			this.to_fieldinfo = null;
			this.to_parent_object = null;
		}

		/** constructor

			Dictionary。追加。

		*/
		public JsonToObject_Work(ModeAddDictionary a_mode,JsonItem a_from_listitem_jsonitem,System.Collections.IDictionary a_to_dictionary,string a_to_key_string,System.Type a_to_listitem_type)
		{
			//モード。
			this.mode = (int)a_mode;

			//設定元。
			this.from_jsonitem = a_from_listitem_jsonitem;

			//設定先。
			this.to_type = a_to_listitem_type;
			this.to_object = null;
			this.to_list = null;
			this.to_index = 0;
			this.to_dictionary = a_to_dictionary;
			this.to_key_string = a_to_key_string;
			this.to_fieldinfo = null;
			this.to_parent_object = null;
		}

		/** constructor

			FieldInfo

		*/
		public JsonToObject_Work(ModeFieldInfo a_mode,JsonItem a_from_member_jsonitem,System.Reflection.FieldInfo a_to_fieldinfo,System.Object a_to_parent_object)
		{
			//モード。
			this.mode = (int)a_mode;

			//設定元。
			this.from_jsonitem = a_from_member_jsonitem;

			//設定先。
			this.to_type = a_to_fieldinfo.FieldType;
			this.to_object = null;
			this.to_list = null;
			this.to_index = 0;
			this.to_dictionary = null;
			this.to_key_string = null;
			this.to_fieldinfo = a_to_fieldinfo;
			this.to_parent_object = a_to_parent_object;
		}

		/** 実行。
		*/
		public void Do(int a_nest,System.Collections.Generic.LinkedList<JsonToObject_Work> a_work_pool)
		{
			switch(this.mode){
			case (int)ModeSetList.Start:
				{
					//List。設定。

					//インスタンス作成。
					this.to_object = JsonToObject_SystemObject.CreateInstance(this.to_type,this.from_jsonitem);

					if(this.to_type.IsClass == true){

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.to_type,this.from_jsonitem,a_nest,a_work_pool);

						//リストに設定。
						this.to_list[this.to_index] = this.to_object;
					}else{

						System.Collections.Generic.LinkedListNode<JsonToObject_Work> t_first_node = a_work_pool.First;

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.to_type,this.from_jsonitem,a_nest,a_work_pool);

						//再登録。
						this.mode = (int)ModeSetList.Fix;

						if(t_first_node != null){
							a_work_pool.AddBefore(t_first_node,this);
						}else{
							a_work_pool.AddLast(this);
						}
					}
				}break;
			case (int)ModeSetList.Fix:
				{
					//List。設定。

					//リストに設定。
					this.to_list[this.to_index] = this.to_object;
				}break;
			case (int)ModeAddList.Start:
				{
					//List。追加。

					//インスタンス作成。
					this.to_object = JsonToObject_SystemObject.CreateInstance(this.to_type,this.from_jsonitem);

					if(this.to_type.IsClass == true){

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.to_type,this.from_jsonitem,a_nest,a_work_pool);

						//リストに設定。
						this.to_list.Add(this.to_object);
					}else{

						System.Collections.Generic.LinkedListNode<JsonToObject_Work> t_first_node = a_work_pool.First;

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.to_type,this.from_jsonitem,a_nest,a_work_pool);

						//再登録。
						this.mode = (int)ModeAddList.Fix;

						if(t_first_node != null){
							a_work_pool.AddBefore(t_first_node,this);
						}else{
							a_work_pool.AddLast(this);
						}
					}
				}break;
			case (int)ModeAddList.Fix:
				{
					//List。追加。

					//リストに追加。
					this.to_list.Add(this.to_object);
				}break;
			case (int)ModeAddDictionary.Start:
				{
					//Dictionary。追加。

					//インスタンス作成。
					this.to_object = JsonToObject_SystemObject.CreateInstance(this.to_type,this.from_jsonitem);

					if(this.to_type.IsClass == true){

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.to_type,this.from_jsonitem,a_nest,a_work_pool);

						//リストに設定。
						this.to_dictionary.Add(this.to_key_string,this.to_object);
					}else{

						System.Collections.Generic.LinkedListNode<JsonToObject_Work> t_first_node = a_work_pool.First;

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.to_type,this.from_jsonitem,a_nest,a_work_pool);

						//再登録。
						this.mode = (int)ModeAddDictionary.Fix;

						if(t_first_node != null){
							a_work_pool.AddBefore(t_first_node,this);
						}else{
							a_work_pool.AddLast(this);
						}
					}
				}break;
			case (int)ModeAddDictionary.Fix:
				{
					//Dictionary。追加。

					//リストに追加。
					this.to_dictionary.Add(this.to_key_string,this.to_object);
				}break;
			case (int)ModeFieldInfo.Start:
				{
					//FieldInfo。

					//インスタンスの作成。
					this.to_object = JsonToObject_SystemObject.CreateInstance(this.to_type,this.from_jsonitem);

					if(this.to_type.IsClass == true){

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.to_type,this.from_jsonitem,a_nest,a_work_pool);

						//フィールドに設定。
						this.to_fieldinfo.SetValue(this.to_parent_object,this.to_object);
					}else{

						System.Collections.Generic.LinkedListNode<JsonToObject_Work> t_first_node = a_work_pool.First;

						//メンバーの設定。
						JsonToObject_SystemObject.Convert(ref this.to_object,this.to_type,this.from_jsonitem,a_nest,a_work_pool);

						//再登録。
						this.mode = (int)ModeFieldInfo.Fix;

						if(t_first_node != null){
							a_work_pool.AddBefore(t_first_node,this);
						}else{
							a_work_pool.AddLast(this);
						}
					}

				}break;
			case (int)ModeFieldInfo.Fix:
				{
					//FieldInfo。

					//フィールドに設定。
					this.to_fieldinfo.SetValue(this.to_parent_object,this.to_object);
				}break;
			}
		}
	}
}

