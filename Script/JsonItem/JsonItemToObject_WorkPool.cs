

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
	/** JsonItemToObject_WorkPool
	*/
	public class JsonItemToObject_WorkPool
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

		/** ModeIEnumerableParam1

			Enumerable

		*/
		public enum ModeIEnumerable
		{
			/** 開始。
			*/
			Start_Param1 = 4,
		}

		/** list
		*/
		private System.Collections.Generic.LinkedList<JsonItemToObject_WorkPool_Item> list;

		/** temp_parameter_list_1
		*/
		private System.Object[] temp_parameter_list_1;

		/** constructor
		*/
		public JsonItemToObject_WorkPool()
		{
			//list
			this.list = new System.Collections.Generic.LinkedList<JsonItemToObject_WorkPool_Item>();

			//temp_parameter_list_1
			this.temp_parameter_list_1 = new System.Object[1]{null};
		}

		/** AddFirst

			List。設定。

		*/
		public void AddFirst(ModeSetList a_mode,JsonItem a_from_listitem_json,System.Collections.IList a_to_list,int a_to_index,System.Type a_to_listitem_type)
		{
			JsonItemToObject_WorkPool_Item t_item = new JsonItemToObject_WorkPool_Item();
			{
				//モード。
				t_item.mode = (int)a_mode;

				//設定元。
				t_item.from_jsonitem = a_from_listitem_json;

				//設定先。
				t_item.to_type = a_to_listitem_type;
				t_item.to_object = null;
				t_item.to_list = a_to_list;
				t_item.to_index = a_to_index;
				t_item.to_dictionary = null;
				t_item.to_key_string = null;
				t_item.to_fieldinfo = null;
				t_item.to_parent_object = null;
				t_item.to_enumerable = null;
				t_item.to_methodinfo = null;
			}
			this.list.AddFirst(t_item);
		}
		
		/** AddFirst

			List。追加。

		*/
		public void AddFirst(ModeAddList a_mode,JsonItem a_from_listitem_json,System.Collections.IList a_to_list,System.Type a_to_listitem_type)
		{
			JsonItemToObject_WorkPool_Item t_item = new JsonItemToObject_WorkPool_Item();
			{
				//モード。
				t_item.mode = (int)a_mode;

				//設定元。
				t_item.from_jsonitem = a_from_listitem_json;

				//設定先。
				t_item.to_type = a_to_listitem_type;
				t_item.to_object = null;
				t_item.to_list = a_to_list;
				t_item.to_index = 0;
				t_item.to_dictionary = null;
				t_item.to_key_string = null;
				t_item.to_fieldinfo = null;
				t_item.to_parent_object = null;
				t_item.to_enumerable = null;
				t_item.to_methodinfo = null;
			}
			this.list.AddFirst(t_item);
		}

		/** AddFirst

			Dictionary。追加。

		*/
		public void AddFirst(ModeAddDictionary a_mode,JsonItem a_from_listitem_jsonitem,System.Collections.IDictionary a_to_dictionary,string a_to_key_string,System.Type a_to_listitem_type)
		{
			JsonItemToObject_WorkPool_Item t_item = new JsonItemToObject_WorkPool_Item();
			{
				//モード。
				t_item.mode = (int)a_mode;

				//設定元。
				t_item.from_jsonitem = a_from_listitem_jsonitem;

				//設定先。
				t_item.to_type = a_to_listitem_type;
				t_item.to_object = null;
				t_item.to_list = null;
				t_item.to_index = 0;
				t_item.to_dictionary = a_to_dictionary;
				t_item.to_key_string = a_to_key_string;
				t_item.to_fieldinfo = null;
				t_item.to_parent_object = null;
				t_item.to_enumerable = null;
				t_item.to_methodinfo = null;
			}
			this.list.AddFirst(t_item);
		}

		/** AddFirst

			FieldInfo

		*/
		public void AddFirst(ModeFieldInfo a_mode,JsonItem a_from_member_jsonitem,System.Reflection.FieldInfo a_to_fieldinfo,System.Object a_to_parent_object)
		{
			JsonItemToObject_WorkPool_Item t_item = new JsonItemToObject_WorkPool_Item();
			{
				//モード。
				t_item.mode = (int)a_mode;

				//設定元。
				t_item.from_jsonitem = a_from_member_jsonitem;

				//設定先。
				t_item.to_type = a_to_fieldinfo.FieldType;
				t_item.to_object = null;
				t_item.to_list = null;
				t_item.to_index = 0;
				t_item.to_dictionary = null;
				t_item.to_key_string = null;
				t_item.to_fieldinfo = a_to_fieldinfo;
				t_item.to_parent_object = a_to_parent_object;
				t_item.to_enumerable = null;
				t_item.to_methodinfo = null;
			}
			this.list.AddFirst(t_item);
		}

		/** AddFirst

			Enumerable

		*/
		public void AddFirst(ModeIEnumerable a_mode,JsonItem a_from_member_jsonitem,System.Collections.IEnumerable a_to_enumerable,System.Reflection.MethodInfo a_to_methodinfo,System.Type a_to_listitem_type)
		{
			JsonItemToObject_WorkPool_Item t_item = new JsonItemToObject_WorkPool_Item();
			{
				//モード。
				t_item.mode = (int)a_mode;

				//設定元。
				t_item.from_jsonitem = a_from_member_jsonitem;

				//設定先。
				t_item.to_type = a_to_listitem_type;
				t_item.to_object = null;
				t_item.to_list = null;
				t_item.to_index = 0;
				t_item.to_dictionary = null;
				t_item.to_key_string = null;
				t_item.to_fieldinfo = null;
				t_item.to_parent_object = null;
				t_item.to_enumerable = a_to_enumerable;
				t_item.to_methodinfo = a_to_methodinfo;
			}
			this.list.AddFirst(t_item);
		}

		/** 更新。
		*/
		public void Main()
		{
			while(true){
				int t_count = this.list.Count;
				if(t_count > 0){
					JsonItemToObject_WorkPool_Item t_current_work = this.list.First.Value;
					this.list.RemoveFirst();
					this.Main_Item(t_current_work);

					//たぶん無限ループ。
					if(t_count > Config.POOL_MAX){
						this.list.Clear();
						Tool.Assert(false);
					}
				}else{
					break;
				}
			}
		}

		/** 更新。
		*/
		private void Main_Item(JsonItemToObject_WorkPool_Item a_item)
		{
			switch(a_item.mode){
			case (int)ModeSetList.Start:
				{
					//List。設定。

					//JsonItemから型を決める。
					if(a_item.to_type == typeof(System.Object)){
						a_item.to_type = ValueType_ConvertToType.Get(a_item.from_jsonitem);
					}

					//インスタンス作成。
					JsonItemToObject_CreateInstance.Create(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem);

					if(a_item.to_type.IsClass == true){

						//メンバーの設定。
						JsonItemToObject.Convert(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem,this);

						//リストに設定。
						a_item.to_list[a_item.to_index] = a_item.to_object;
					}else{

						//差し込み位置。
						System.Collections.Generic.LinkedListNode<JsonItemToObject_WorkPool_Item> t_first_node = this.list.First;

						//メンバーの設定。
						JsonItemToObject.Convert(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem,this);

						//再登録。
						a_item.mode = (int)ModeSetList.Fix;

						//差し込み。
						if(t_first_node != null){
							this.list.AddBefore(t_first_node,a_item);
						}else{
							this.list.AddLast(a_item);
						}
					}
				}break;
			case (int)ModeSetList.Fix:
				{
					//List。設定。

					//リストに設定。
					try{
						a_item.to_list[a_item.to_index] = a_item.to_object;
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}					
				}break;
			case (int)ModeAddList.Start:
				{
					//List。追加。

					//JsonItemから型を決める。
					if(a_item.to_type == typeof(System.Object)){
						a_item.to_type = ValueType_ConvertToType.Get(a_item.from_jsonitem);
					}

					//インスタンス作成。
					JsonItemToObject_CreateInstance.Create(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem);

					if(a_item.to_type.IsClass == true){

						//メンバーの設定。
						JsonItemToObject.Convert(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem,this);

						//リストに設定。
						a_item.to_list.Add(a_item.to_object);
					}else{

						//差し込み位置。
						System.Collections.Generic.LinkedListNode<JsonItemToObject_WorkPool_Item> t_first_node = this.list.First;

						//メンバーの設定。
						JsonItemToObject.Convert(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem,this);

						//再登録。
						a_item.mode = (int)ModeAddList.Fix;

						//差し込み。
						if(t_first_node != null){
							this.list.AddBefore(t_first_node,a_item);
						}else{
							this.list.AddLast(a_item);
						}
					}
				}break;
			case (int)ModeAddList.Fix:
				{
					//List。追加。

					//リストに追加。
					try{
						a_item.to_list.Add(a_item.to_object);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}					
				}break;
			case (int)ModeAddDictionary.Start:
				{
					//Dictionary。追加。

					//JsonItemから型を決める。
					if(a_item.to_type == typeof(System.Object)){
						a_item.to_type = ValueType_ConvertToType.Get(a_item.from_jsonitem);
					}

					//インスタンス作成。
					JsonItemToObject_CreateInstance.Create(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem);

					if(a_item.to_type.IsClass == true){

						//メンバーの設定。
						JsonItemToObject.Convert(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem,this);

						//リストに設定。
						a_item.to_dictionary.Add(a_item.to_key_string,a_item.to_object);
					}else{

						//差し込み位置。
						System.Collections.Generic.LinkedListNode<JsonItemToObject_WorkPool_Item> t_first_node = this.list.First;

						//メンバーの設定。
						JsonItemToObject.Convert(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem,this);

						//再登録。
						a_item.mode = (int)ModeAddDictionary.Fix;

						//差し込み。
						if(t_first_node != null){
							this.list.AddBefore(t_first_node,a_item);
						}else{
							this.list.AddLast(a_item);
						}
					}
				}break;
			case (int)ModeAddDictionary.Fix:
				{
					//Dictionary。追加。

					//リストに追加。
					try{
						a_item.to_dictionary.Add(a_item.to_key_string,a_item.to_object);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}
				}break;
			case (int)ModeFieldInfo.Start:
				{
					//FieldInfo。

					//JsonItemから型を決める。
					if(a_item.to_type == typeof(System.Object)){
						a_item.to_type = ValueType_ConvertToType.Get(a_item.from_jsonitem);
					}

					//インスタンスの作成。
					JsonItemToObject_CreateInstance.Create(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem);

					if(a_item.to_type.IsClass == true){

						//メンバーの設定。
						JsonItemToObject.Convert(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem,this);

						//フィールドに設定。
						a_item.to_fieldinfo.SetValue(a_item.to_parent_object,a_item.to_object);
					}else{

						//差し込み位置。
						System.Collections.Generic.LinkedListNode<JsonItemToObject_WorkPool_Item> t_first_node = this.list.First;

						//メンバーの設定。
						JsonItemToObject.Convert(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem,this);

						//再登録。
						a_item.mode = (int)ModeFieldInfo.Fix;

						//差し込み。
						if(t_first_node != null){
							this.list.AddBefore(t_first_node,a_item);
						}else{
							this.list.AddLast(a_item);
						}
					}
				}break;
			case (int)ModeFieldInfo.Fix:
				{
					//FieldInfo。

					//フィールドに設定。
					try{
						a_item.to_fieldinfo.SetValue(a_item.to_parent_object,a_item.to_object);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}
				}break;
			case (int)ModeIEnumerable.Start_Param1:
				{
					//Generic.Stack
					//Generic.LinkedList
					//Generic.HashSet
					//Generic.Queue
					//Generic.SortedSet

					//JsonItemから型を決める。
					if(a_item.to_type == typeof(System.Object)){
						a_item.to_type = ValueType_ConvertToType.Get(a_item.from_jsonitem);
					}

					System.Type t_generic_type = Fee.ReflectionTool.Utility.GetGenericTypeDefinition(a_item.to_enumerable.GetType());

					//インスタンス作成。
					JsonItemToObject_CreateInstance.Create(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem);

					//■メンバーの設定。
					JsonItemToObject.Convert(ref a_item.to_object,a_item.to_type,a_item.from_jsonitem,this);

					//パラメータリスト。
					this.temp_parameter_list_1[0] = a_item.to_object;

					//呼び出し。
					try{
						a_item.to_methodinfo.Invoke(a_item.to_enumerable,this.temp_parameter_list_1);
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
					}
				}break;
			}
		}
	}
}

