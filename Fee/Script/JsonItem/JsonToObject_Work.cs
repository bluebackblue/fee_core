

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
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
		/** 変換先。
		*/
		System.Object to_object;

		/** 変換先。
		*/
		System.Type to_type;

		/** 変換元。
		*/
		JsonItem from_jsonitem;

		/** constructor
		*/
		public JsonToObject_Work(System.Object a_object,System.Type a_type,JsonItem a_jsonitem)
		{
			this.to_object = a_object;
			this.to_type = a_type;
			this.from_jsonitem = a_jsonitem;
		}

		/** 実行。
		*/
		public void Do(System.Collections.Generic.List<JsonToObject_Work> a_work_pool)
		{
			JsonToObject_SystemObject.Convert(ref this.to_object,this.to_type,this.from_jsonitem,a_work_pool);
		}
	}
}

