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
		/** additem
		*/
		private System.Object additem_object;
		private int additem_index;

		/** setitem
		*/
		private System.Object setitem_object;
		private string setitem_key;

		/** to_jsonitem
		*/
		private JsonItem to_jsonitem;

		/** constructor
		*/
		public ObjectToJson_Work(System.Object a_object,int a_index,JsonItem a_to_jsonitem)
		{
			this.additem_object = a_object;
			this.additem_index = a_index;
			this.to_jsonitem = a_to_jsonitem;

			this.setitem_object = null;
			this.setitem_key = null;
		}

		/** constructor
		*/
		public ObjectToJson_Work(System.Object a_object,string a_key,JsonItem a_to_jsonitem)
		{
			this.setitem_object = a_object;
			this.setitem_key = a_key;
			this.to_jsonitem = a_to_jsonitem;

			this.additem_object = null;
			this.additem_index = -1;
		}

		/** 実行。
		*/
		public void Do(List<ObjectToJson_Work> a_work_pool)
		{
			if(this.additem_object != null){
				JsonItem t_jsonitem_member = ObjectToJson.Convert(this.additem_object,a_work_pool);
				if(t_jsonitem_member != null){
					this.to_jsonitem.AddItem(t_jsonitem_member,false);
				}else{
					//nullの場合は追加しない。
				}
			}else if(this.setitem_object != null){
				JsonItem t_jsonitem_member = ObjectToJson.Convert(this.setitem_object,a_work_pool);
				if(t_jsonitem_member != null){
					this.to_jsonitem.SetItem(this.setitem_key,t_jsonitem_member,false);
				}else{
					//nullの場合は追加しない。
				}
			}else{
				//nullの場合は追加しない。
			}
		}
	}
}

