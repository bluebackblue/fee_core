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
	/** ToJson_Work
	*/
	public class ToJson_Work
	{
		/** additem
		*/
		public System.Object additem_object;
		public int additem_index;

		/** setitem
		*/
		public System.Object setitem_object;
		public string setitem_key;

		/** to_jsonitem
		*/
		public JsonItem to_jsonitem;

		/** constructor
		*/
		public ToJson_Work(System.Object a_object,int a_index,JsonItem a_to_jsonitem)
		{
			this.additem_object = a_object;
			this.additem_index = a_index;
			this.to_jsonitem = a_to_jsonitem;

			this.setitem_object = null;
			this.setitem_key = null;
		}

		/** constructor
		*/
		public ToJson_Work(System.Object a_object,string a_key,JsonItem a_to_jsonitem)
		{
			this.setitem_object = a_object;
			this.setitem_key = a_key;
			this.to_jsonitem = a_to_jsonitem;

			this.additem_object = null;
			this.additem_index = -1;
		}
	}
}

