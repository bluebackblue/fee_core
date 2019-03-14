

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
	/** JsonToObject
	*/
	public class JsonToObject<Type>
	{
		/** Convert
		*/
		public static Type Convert(JsonItem a_jsonitem)
		{
			System.Type t_type = typeof(Type);
			System.Object t_object = JsonToObject_SystemObject.CreateInstance(t_type,a_jsonitem);
			JsonToObject_SystemObject.Convert(ref t_object,t_type,a_jsonitem);
			return (Type)System.Convert.ChangeType(t_object,t_type);
		}
	}
}

