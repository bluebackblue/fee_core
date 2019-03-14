using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。ＪＳＯＮ化。
*/


/** NJsonItem
*/
namespace NJsonItem
{
	/** ObjectToJson_Work
	*/
	public class ObjectToJson_Work
	{
		/** from_object
		*/
		private System.Object from_object;

		/** to_jsonitem
		*/
		private JsonItem to_jsonitem;
		private int to_index;
		private string to_key;

		/** additem
		*/
		private bool mode_add;

		/** setitem
		*/
		private bool mode_set;

		/** constructor
		*/
		public ObjectToJson_Work(System.Object a_object,int a_index,JsonItem a_to_jsonitem)
		{
			this.from_object = a_object;
			this.to_jsonitem = a_to_jsonitem;
			this.to_index = a_index;
			this.to_key = null;

			this.mode_add = true;
			this.mode_set = false;
		}

		/** constructor
		*/
		public ObjectToJson_Work(System.Object a_object,string a_key,JsonItem a_to_jsonitem)
		{
			this.from_object = a_object;
			this.to_jsonitem = a_to_jsonitem;
			this.to_index = -1;
			this.to_key = a_key;

			this.mode_add = false;
			this.mode_set = true;
		}

		/** 実行。
		*/
		public void Do(List<ObjectToJson_Work> a_work_pool)
		{
			JsonItem t_jsonitem_member = ObjectToJson.Convert(this.from_object,a_work_pool);

			if(this.mode_add == true){
				this.to_jsonitem.SetItem(this.to_index,t_jsonitem_member,false);
			}else if(this.mode_set == true){
				this.to_jsonitem.SetItem(this.to_key,t_jsonitem_member,false);
			}
		}
	}
}

