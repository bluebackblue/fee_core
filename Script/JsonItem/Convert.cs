

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。コンバート。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** Convert
	*/
	public class Convert
	{
		/** オブジェクト => JsonItem。
		*/
		public static JsonItem ObjectToJsonItem(System.Object a_instance)
		{
			return ObjectToJson_SystemObject.Convert(a_instance,null);
		}

		/** JsonItem => オブジェクト。
		*/
		public static Type JsonItemToObject<Type>(JsonItem a_jsonitem)
		{
			if(a_jsonitem != null){
				return a_jsonitem.ConvertObject<Type>();
			}else{
				return default(Type);
			}
		}

		/** オブジェクト => Json文字列。
		*/
		public static string ObjectToJsonString(System.Object a_instance)
		{
			if(a_instance != null){
				JsonItem t_jsonitem = ObjectToJson_SystemObject.Convert(a_instance,null);
				return t_jsonitem.ConvertJsonString();
			}

			return null;
		}

		/** JsonItem => Json文字列。
		*/
		public static string JsonItemToJsonString(JsonItem a_jsonitem)
		{
			if(a_jsonitem != null){
				return a_jsonitem.ConvertJsonString();
			}else{
				return null;
			}
		}

		/** Json文字列 => JsonItem。
		*/
		public static JsonItem JsonStringToJsonItem(string a_jsonstring)
		{
			return new JsonItem(a_jsonstring);
		}

		/** Json文字列 => オブジェクト。
		*/
		public static Type JsonStringToObject<Type>(string a_jsonstring)
		{
			if(a_jsonstring != null){
				JsonItem t_jsonitem = new JsonItem(a_jsonstring);
				return t_jsonitem.ConvertObject<Type>();
			}
			return default(Type);
		}
	}
}

