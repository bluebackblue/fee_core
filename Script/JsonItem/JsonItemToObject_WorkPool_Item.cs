

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
		public JsonItem from_value_jsonitem;
		public JsonItem from_key_jsonitem;

		/** コンバート先。インスタンス。
		*/
		public System.Type to_value_type;
		public System.Type to_key_type;
		public System.Object to_value_object;
		public System.Object to_key_object;
		public System.Collections.IList to_list;
		public int to_index;
		public System.Collections.IDictionary to_dictionary;
		public System.Reflection.FieldInfo to_fieldinfo;
		public System.Object to_parent_object;
		public System.Collections.IEnumerable to_enumerable;
		public System.Reflection.MethodInfo to_methodinfo;
	}
}

