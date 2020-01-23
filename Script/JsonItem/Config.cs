

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。コンフィグ。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** Config
	*/
	public class Config
	{
		/** ログ。
		*/
		public static bool LOG_ENABLE = false;

		/** ログエラー。
		*/
		public static bool LOGERROR_ENABLE = true;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = true;

		/** リスロー。
		*/
		public static bool RETHROW_ENABLE = false;

		/** プール数。最大値。
		*/
		public static int POOL_MAX = 1024;

		/** コンバートネスト。最大値。
		*/
		public static int CONVERTNEST_MAX = 32;

		/** CULTURE
		*/
		public static System.IFormatProvider CULTURE = System.Globalization.CultureInfo.CreateSpecificCulture("ja-JP");

		/** Floating To String
		*/
		public static string FLOATING_TO_STRING_FORMAT = "{0:e16}";

		/** FLOATING_SEPARATOR
		*/
		public static char FLOATING_SEPARATOR = '.';

		/** デフォルトオプション。
		*/
		public static ConvertToJsonStringOption DEFAULT_CONVERTTOJSONSTRING_OPTION = ConvertToJsonStringOption.None;
	}
}

