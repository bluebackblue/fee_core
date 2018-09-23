using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。オブジェクト化。
*/


/** NJsonItem
*/
namespace NJsonItem
{
	/** JsonToObject_Work
	*/
	public class JsonToObject_Work
	{
		/** 変換先。
		*/
		System.Object to_object;

		/** 変換元。
		*/
		JsonItem from_jsonitem;

		/** constructor
		*/
		public JsonToObject_Work(System.Object a_object,JsonItem a_jsonitem)
		{
			this.to_object = a_object;
			this.from_jsonitem = a_jsonitem;
		}

		/** 実行。
		*/
		public void Do(List<JsonToObject_Work> a_work_pool)
		{
			JsonToObject_SystemObject.Convert(ref this.to_object,this.from_jsonitem,a_work_pool);
		}
	}
}

