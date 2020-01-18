

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。ＪＳＯＮ化。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** ObjectToJson_Work
	*/
	public class ObjectToJson_Work
	{
		/** ModeIndexArray
		*/
		public enum ModeIndexArray
		{
			Value = 0,
		}

		/** AssociativeArray
		*/
		public enum AssociativeArray
		{
			Value = 1,
		}

		/** mode
		*/
		private int mode;

		/** nest
		*/
		private int nest;

		/** from_object
		*/
		private System.Object from_object;

		/** to_jsonitem
		*/
		private JsonItem to_jsonitem;
		private int to_index;
		private string to_key;

		/** ObjectOption
		*/
		public class ObjectOption
		{
			/** Enumを文字列に変換する。
			*/
			public bool attribute_enumstring = false;
		}

		/** object_option
		*/
		private ObjectOption object_option;

		/** constructor
		*/
		public ObjectToJson_Work(ModeIndexArray a_mode,System.Object a_object,ObjectOption a_object_option,int a_index,JsonItem a_to_jsonitem,int a_nest)
		{
			this.mode = (int)a_mode;

			this.from_object = a_object;
			this.to_jsonitem = a_to_jsonitem;
			this.to_index = a_index;
			this.to_key = null;

			this.object_option = a_object_option;

			this.nest = a_nest;
		}

		/** constructor
		*/
		public ObjectToJson_Work(AssociativeArray a_mode,System.Object a_object,ObjectOption a_object_option,string a_key,JsonItem a_to_jsonitem,int a_nest)
		{
			this.mode = (int)a_mode;

			this.from_object = a_object;
			this.to_jsonitem = a_to_jsonitem;
			this.to_index = -1;
			this.to_key = a_key;

			this.object_option = a_object_option;

			this.nest = a_nest;
		}

		/** 実行。
		*/
		public void Do(System.Collections.Generic.List<ObjectToJson_Work> a_work_pool)
		{
			switch(this.mode){
			case (int)ModeIndexArray.Value:
				{
					JsonItem t_jsonitem_member = null;

					if(this.nest < Config.CONVERTNEST_MAX){
						t_jsonitem_member = ObjectToJson_SystemObject.Convert(this.from_object,this.object_option,this.nest + 1,a_work_pool);
					}else{
						Tool.Assert(false);
					}

					this.to_jsonitem.SetItem(this.to_index,t_jsonitem_member,false);
				}break;
			case (int)AssociativeArray.Value:
				{
					JsonItem t_jsonitem_member = null;

					if(this.nest < Config.CONVERTNEST_MAX){
						t_jsonitem_member = ObjectToJson_SystemObject.Convert(this.from_object,this.object_option,this.nest + 1,a_work_pool);
					}else{
						Tool.Assert(false);
					}

					this.to_jsonitem.SetItem(this.to_key,t_jsonitem_member,false);
				}break;
			}
		}
	}
}

