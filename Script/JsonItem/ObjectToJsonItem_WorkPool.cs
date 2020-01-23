

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。JsonItem化。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** ObjectToJsonItem_WorkPool
	*/
	public class ObjectToJsonItem_WorkPool
	{
		/** ModeAddIndexArray

			IndexArray。追加。

		*/
		public enum ModeAddIndexArray
		{
			/** 開始。
			*/
			Start = 0,
		}

		/** ModeSetIndexArray

			IndexArray。設定。

		*/
		public enum ModeSetIndexArray
		{
			/** 開始。
			*/
			Start = 1,
		}

		/** ModeAddAssociativeArray

			AssociativeArray。追加。

		*/
		public enum ModeAddAssociativeArray
		{
			/** 開始。
			*/
			Start = 2,
		}

		/** ModeFieldInfo

			FieldInfo。

		*/
		public enum ModeFieldInfo
		{
			/** 開始。
			*/
			Start = 3,
		}

		/** list
		*/
		private System.Collections.Generic.List<ObjectToJsonItem_WorkPool_Item> list;

		/** constructor
		*/
		public ObjectToJsonItem_WorkPool()
		{
			this.list = new System.Collections.Generic.List<ObjectToJsonItem_WorkPool_Item>();
		}

		/** Add

			IndexArray。追加。

		*/
		public void Add(ModeAddIndexArray a_mode,JsonItem a_to_jsonitem,System.Object a_from_listitem_object,System.Type a_from_listitem_type,ConvertToJsonItemOption a_from_option,int a_nest)
		{
			ObjectToJsonItem_WorkPool_Item t_item = new ObjectToJsonItem_WorkPool_Item();
			{
				//mode
				t_item.mode = (int)a_mode;

				//nest
				t_item.nest = a_nest;

				//コンバート元、インスタンス。
				t_item.from_fieldinfo = null;
				t_item.from_parent_object = null;
				t_item.from_object = a_from_listitem_object;
				t_item.from_type = a_from_listitem_type;
				t_item.from_option = a_from_option;

				//コンバート先。ＪＳＯＮ。
				t_item.to_jsonitem = a_to_jsonitem;
				t_item.to_index = 0;
				t_item.to_key_string = null;
			}
			this.list.Add(t_item);
		}

		/** Add

			IndexArray。設定。

		*/
		public void Add(ModeSetIndexArray a_mode,JsonItem a_to_jsonitem,int a_to_index,System.Object a_from_listitem_object,System.Type a_from_listitem_type,ConvertToJsonItemOption a_from_option,int a_nest)
		{
			ObjectToJsonItem_WorkPool_Item t_item = new ObjectToJsonItem_WorkPool_Item();
			{
				//mode
				t_item.mode = (int)a_mode;

				//nest
				t_item.nest = a_nest;

				//コンバート元、インスタンス。
				t_item.from_fieldinfo = null;
				t_item.from_parent_object = null;
				t_item.from_object = a_from_listitem_object;
				t_item.from_type = a_from_listitem_type;
				t_item.from_option = a_from_option;

				//コンバート先。ＪＳＯＮ。
				t_item.to_jsonitem = a_to_jsonitem;
				t_item.to_index = a_to_index;
				t_item.to_key_string = null;
			}
			this.list.Add(t_item);
		}

		/** Add

			AssociativeArray。追加。

		*/
		public void Add(ModeAddAssociativeArray a_mode,JsonItem a_to_jsonitem,string a_to_key_string,System.Object a_from_listitem_object,System.Type a_from_listitem_type,ConvertToJsonItemOption a_from_option,int a_nest)
		{
			ObjectToJsonItem_WorkPool_Item t_item = new ObjectToJsonItem_WorkPool_Item();
			{
				//mode
				t_item.mode = (int)a_mode;

				//nest
				t_item.nest = a_nest;

				//コンバート元、インスタンス。
				t_item.from_fieldinfo = null;
				t_item.from_parent_object = null;
				t_item.from_object = a_from_listitem_object;
				t_item.from_type = a_from_listitem_type;
				t_item.from_option = a_from_option;

				//コンバート先。ＪＳＯＮ。
				t_item.to_jsonitem = a_to_jsonitem;
				t_item.to_index = 0;
				t_item.to_key_string = a_to_key_string;
			}
			this.list.Add(t_item);
		}

		/** Add

			FieldInfo。

		*/
		public void Add(ModeFieldInfo a_mode,JsonItem a_to_jsonitem,System.Reflection.FieldInfo a_from_fieldinfo,System.Object a_from_parent_object,int a_nest)
		{
			ObjectToJsonItem_WorkPool_Item t_item = new ObjectToJsonItem_WorkPool_Item();
			{
				//モード。
				t_item.mode = (int)a_mode;

				//ネスト。
				t_item.nest = a_nest;

				//コンバート元、インスタンス。
				t_item.from_fieldinfo = a_from_fieldinfo;
				t_item.from_parent_object = a_from_parent_object;
				t_item.from_object = null;
				t_item.from_type = null;
				t_item.from_option = ConvertToJsonItemOption.None;

				//コンバート先。ＪＳＯＮ。
				t_item.to_jsonitem = a_to_jsonitem;
				t_item.to_index = 0;
				t_item.to_key_string = null;
			}
			this.list.Add(t_item);
		}

		/** 更新。
		*/
		public void Main()
		{
			while(true){
				int t_count = this.list.Count;
				if(t_count > 0){
					ObjectToJsonItem_WorkPool_Item t_current_work = this.list[t_count - 1];
					this.list.RemoveAt(t_count - 1);
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
		private void Main_Item(ObjectToJsonItem_WorkPool_Item a_item)
		{
			switch(a_item.mode){
			case (int)ModeAddIndexArray.Start:
				{
					//IndexArray。追加。

					JsonItem t_jsonitem_listitem = null;

					if(a_item.nest < Config.CONVERTNEST_MAX){
						t_jsonitem_listitem = ObjectToJsonItem.Convert(a_item.from_object,a_item.from_type,a_item.from_option,this,a_item.nest + 1);
					}else{
						Tool.Assert(false);
					}

					a_item.to_jsonitem.AddItem(t_jsonitem_listitem,false);
				}break;
			case (int)ModeSetIndexArray.Start:
				{
					//IndexArray。設定。

					JsonItem t_jsonitem_listitem = null;

					if(a_item.nest < Config.CONVERTNEST_MAX){
						t_jsonitem_listitem = ObjectToJsonItem.Convert(a_item.from_object,a_item.from_type,a_item.from_option,this,a_item.nest + 1);
					}else{
						Tool.Assert(false);
					}

					a_item.to_jsonitem.SetItem(a_item.to_index,t_jsonitem_listitem,false);
				}break;
			case (int)ModeAddAssociativeArray.Start:
				{
					//AssociativeArray。追加。

					JsonItem t_jsonitem_member = null;

					if(a_item.nest < Config.CONVERTNEST_MAX){
						t_jsonitem_member = ObjectToJsonItem.Convert(a_item.from_object,a_item.from_type,a_item.from_option,this,a_item.nest + 1);
					}else{
						Tool.Assert(false);
					}

					a_item.to_jsonitem.SetItem(a_item.to_key_string,t_jsonitem_member,false);
				}break;
			case (int)ModeFieldInfo.Start:
				{
					//FieldInfo。

					//ＥＮＵＭの文字列化。
					if(a_item.from_fieldinfo.IsDefined(typeof(Fee.JsonItem.EnumString),false) == true){
						a_item.from_option = ConvertToJsonItemOption.EnumString;
					}else{
						a_item.from_option = ConvertToJsonItemOption.None;
					}

					System.Object t_raw = a_item.from_fieldinfo.GetValue(a_item.from_parent_object);
					if(t_raw != null){

						JsonItem t_jsonitem_member = null;

						if(a_item.nest < Config.CONVERTNEST_MAX){
							t_jsonitem_member = ObjectToJsonItem.Convert(t_raw,t_raw.GetType(),a_item.from_option,this,a_item.nest + 1);
						}else{
							Tool.Assert(false);
						}

						a_item.to_jsonitem.SetItem(a_item.from_fieldinfo.Name,t_jsonitem_member,false);

					}else{
						//NULL処理。
					}

				}break;
			}
		}

	}
}

