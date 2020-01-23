

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
	/** JsonItemToObject_WorkPool_Item
	*/
	public class JsonItemToObject_WorkPool_Item
	{
		/** モード。
		*/
		public int mode;

		/** コンバート元。ＪＳＯＮ。
		*/
		public JsonItem from_jsonitem;

		/** コンバート先。インスタンス。
		*/
		public System.Type to_type;
		public System.Object to_object;
		public System.Collections.IList to_list;
		public int to_index;
		public System.Collections.IDictionary to_dictionary;
		public string to_key_string;
		public System.Reflection.FieldInfo to_fieldinfo;
		public System.Object to_parent_object;
		public System.Collections.IEnumerable to_enumerable;
		public System.Reflection.MethodInfo to_methodinfo;
	}
}

