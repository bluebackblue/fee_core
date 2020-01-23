

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
	/** ObjectToJsonItem_WorkPool_Item
	*/
	public class ObjectToJsonItem_WorkPool_Item
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

		/** ObjectOption
		*/
		public class ObjectOption
		{
			/** Enumを文字列に変換する。
			*/
			public bool attribute_enumstring;

			/** constructor
			*/
			public ObjectOption()
			{
				this.attribute_enumstring = false;
			}
		}

		/** モード。
		*/
		private int mode;

		/** ネスト。
		*/
		private int nest;

		/** コンバート元、インスタンス。

			from_object : フィールドインフォ用の親オブジェクト / リストアイテム。

		*/
		private System.Reflection.FieldInfo from_fieldinfo;
		private System.Object from_parent_object;
		private System.Object from_object;
		private System.Type from_type;
		private ObjectToJsonItem_WorkPool_Item.ObjectOption from_objectoption;

		/** コンバート先。ＪＳＯＮ。
		*/
		private JsonItem to_jsonitem;
		private int to_index;
		private string to_key_string;

		/** constructor

			IndexArray。追加。

		*/
		public ObjectToJsonItem_WorkPool_Item(ModeAddIndexArray a_mode,int a_nest,JsonItem a_to_jsonitem,System.Object a_from_listitem_object,System.Type a_from_listitem_type,ObjectToJsonItem_WorkPool_Item.ObjectOption a_from_objectoption)
		{
			//mode
			this.mode = (int)a_mode;

			//nest
			this.nest = a_nest;

			//コンバート元、インスタンス。
			this.from_fieldinfo = null;
			this.from_parent_object = null;
			this.from_object = a_from_listitem_object;
			this.from_type = a_from_listitem_type;
			this.from_objectoption = a_from_objectoption;

			//コンバート先。ＪＳＯＮ。
			this.to_jsonitem = a_to_jsonitem;
			this.to_index = 0;
			this.to_key_string = null;
		}

		/** constructor

			IndexArray。設定。

		*/
		public ObjectToJsonItem_WorkPool_Item(ModeSetIndexArray a_mode,int a_nest,JsonItem a_to_jsonitem,int a_to_index,System.Object a_from_listitem_object,System.Type a_from_listitem_type,ObjectOption a_from_objectoption)
		{
			//mode
			this.mode = (int)a_mode;

			//nest
			this.nest = a_nest;

			//コンバート元、インスタンス。
			this.from_fieldinfo = null;
			this.from_parent_object = null;
			this.from_object = a_from_listitem_object;
			this.from_type = a_from_listitem_type;
			this.from_objectoption = a_from_objectoption;

			//コンバート先。ＪＳＯＮ。
			this.to_jsonitem = a_to_jsonitem;
			this.to_index = a_to_index;
			this.to_key_string = null;
		}

		/** constructor

			AssociativeArray。追加。

		*/
		public ObjectToJsonItem_WorkPool_Item(ModeAddAssociativeArray a_mode,int a_nest,JsonItem a_to_jsonitem,string a_to_key_string,System.Object a_from_listitem_object,System.Type a_from_listitem_type,ObjectOption a_from_objectoption)
		{
			//mode
			this.mode = (int)a_mode;

			//nest
			this.nest = a_nest;

			//コンバート元、インスタンス。
			this.from_fieldinfo = null;
			this.from_parent_object = null;
			this.from_object = a_from_listitem_object;
			this.from_type = a_from_listitem_type;
			this.from_objectoption = a_from_objectoption;

			//コンバート先。ＪＳＯＮ。
			this.to_jsonitem = a_to_jsonitem;
			this.to_index = 0;
			this.to_key_string = a_to_key_string;
		}

		/** constructor

			FieldInfo。

		*/
		public ObjectToJsonItem_WorkPool_Item(ModeFieldInfo a_mode,int a_nest,JsonItem a_to_jsonitem,System.Reflection.FieldInfo a_from_fieldinfo,System.Object a_from_parent_object)
		{
			//モード。
			this.mode = (int)a_mode;

			//ネスト。
			this.nest = a_nest;

			//コンバート元、インスタンス。
			this.from_fieldinfo = a_from_fieldinfo;
			this.from_parent_object = a_from_parent_object;
			this.from_object = null;
			this.from_type = null;
			this.from_objectoption = null;

			//コンバート先。ＪＳＯＮ。
			this.to_jsonitem = a_to_jsonitem;
			this.to_index = 0;
			this.to_key_string = null;
		}

		/** 実行。
		*/
		public void Do(ObjectToJsonItem_WorkPool a_work_pool)
		{
			switch(this.mode){
			case (int)ModeAddIndexArray.Start:
				{
					//IndexArray。追加。

					JsonItem t_jsonitem_listitem = null;

					if(this.nest < Config.CONVERTNEST_MAX){
						t_jsonitem_listitem = ObjectToJsonItem.Convert(this.from_object,this.from_type,this.from_objectoption,this.nest + 1,a_work_pool);
					}else{
						Tool.Assert(false);
					}

					this.to_jsonitem.AddItem(t_jsonitem_listitem,false);
				}break;
			case (int)ModeSetIndexArray.Start:
				{
					//IndexArray。設定。

					JsonItem t_jsonitem_listitem = null;

					if(this.nest < Config.CONVERTNEST_MAX){
						t_jsonitem_listitem = ObjectToJsonItem.Convert(this.from_object,this.from_type,this.from_objectoption,this.nest + 1,a_work_pool);
					}else{
						Tool.Assert(false);
					}

					this.to_jsonitem.SetItem(this.to_index,t_jsonitem_listitem,false);
				}break;
			case (int)ModeAddAssociativeArray.Start:
				{
					//AssociativeArray。追加。

					JsonItem t_jsonitem_member = null;

					if(this.nest < Config.CONVERTNEST_MAX){
						t_jsonitem_member = ObjectToJsonItem.Convert(this.from_object,this.from_type,this.from_objectoption,this.nest + 1,a_work_pool);
					}else{
						Tool.Assert(false);
					}

					this.to_jsonitem.SetItem(this.to_key_string,t_jsonitem_member,false);
				}break;
			case (int)ModeFieldInfo.Start:
				{
					//FieldInfo。

					//オプション設定。
					ObjectToJsonItem_WorkPool_Item.ObjectOption t_objectoption = null;

					//ＥＮＵＭの文字列化。
					if(this.from_fieldinfo.IsDefined(typeof(Fee.JsonItem.EnumString),false) == true){
						t_objectoption = new ObjectToJsonItem_WorkPool_Item.ObjectOption();
						t_objectoption.attribute_enumstring = true;
					}

					System.Object t_raw = this.from_fieldinfo.GetValue(this.from_parent_object);
					if(t_raw != null){

						JsonItem t_jsonitem_member = null;

						if(this.nest < Config.CONVERTNEST_MAX){
							t_jsonitem_member = ObjectToJsonItem.Convert(t_raw,t_raw.GetType(),t_objectoption,this.nest + 1,a_work_pool);
						}else{
							Tool.Assert(false);
						}

						this.to_jsonitem.SetItem(this.from_fieldinfo.Name,t_jsonitem_member,false);

					}else{
						//NULL処理。
					}

				}break;
			}
		}
	}
}

