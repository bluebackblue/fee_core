

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
		public static JsonItem ObjectToJsonItem<Type>(Type a_instance)
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

		/** オブジェクト => Json文字列。
		*/
		public static string ObjectToJsonString<Type>(Type a_instance)
		{
			#if(USE_DEF_FEE_UTF8JSON)
			{
				return ObjectToJsonString_Utf8Json(a_instance);
			}
			#else
			{
				return ObjectToJsonString_Fee(a_instance);
			}
			#endif
		}

		/** Json文字列 => オブジェクト。
		*/
		public static Type JsonStringToObject<Type>(string a_jsonstring)
		{
			#if(USE_DEF_FEE_UTF8JSON)
			{
				return JsonStringToObject_Utf8Json<Type>(a_jsonstring);
			}
			#else
			{
				return JsonStringToObject_Fee<Type>(a_jsonstring);
			}
			#endif
		}
	
		/** Fee。オブジェクト => Json文字列。
		*/
		public static string ObjectToJsonString_Fee<Type>(Type a_instance)
		{
			if(a_instance != null){
				JsonItem t_jsonitem = ObjectToJson_SystemObject.Convert(a_instance,null);
				return t_jsonitem.ConvertJsonString();
			}
			return null;
		}

		/** Fee。Json文字列 => オブジェクト。
		*/
		public static Type JsonStringToObject_Fee<Type>(string a_jsonstring)
		{
			if(a_jsonstring != null){
				JsonItem t_jsonitem = new JsonItem(a_jsonstring);
				return t_jsonitem.ConvertObject<Type>();
			}
			return default(Type);
		}

		/** Utf8Json。オブジェクト => Json文字列。
		*/
		#if(USE_DEF_FEE_UTF8JSON)
		public static string ObjectToJsonString_Utf8Json<Type>(Type a_instance)
		{
			if(a_instance != null){
				return Utf8Json.JsonSerializer.ToJsonString(a_instance);
			}
			return null;
		}
		#endif

		/** Utf8Json。Json文字列 => オブジェクト。
		*/
		#if(USE_DEF_FEE_UTF8JSON)
		public static Type JsonStringToObject_Utf8Json<Type>(string a_jsonstring)
		{
			if(a_jsonstring != null){
				return Utf8Json.JsonSerializer.Deserialize<Type>(a_jsonstring);
			}
			return default(Type);
		}
		#endif
	}
}

