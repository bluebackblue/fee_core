

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

		/** CULTURE
		*/
		public static System.IFormatProvider CULTURE = System.Globalization.CultureInfo.CreateSpecificCulture("ja-JP");

		/** Double To String
		*/
		public static string DOUBLE_TO_STRING_FORMAT = "{0:e16}";

		/** DOUBLE_SEPARATOR
		*/
		public static char DOUBLE_SEPARATOR = '.';

		/** String To Double

			AllowLeadingWhite  : 先行する空白文字を解析対象の文字列に使用できることを示します。
			AllowTrailingWhite : 末尾の空白文字を解析対象の文字列に使用できることを示します。 
			AllowLeadingSign   : 数値文字列に先行する符号を使用できることを示します。
			AllowDecimalPoint  : 数値文字列に小数点を使用できることを示します。
			AllowExponent      : 数値文字列に指数表記を使用できることを示します。

		*/
		public static System.Globalization.NumberStyles STRING_TO_DOBULE_NUMBERSTYLE = 
			System.Globalization.NumberStyles.AllowLeadingWhite |
			System.Globalization.NumberStyles.AllowTrailingWhite |
			System.Globalization.NumberStyles.AllowLeadingSign |
			System.Globalization.NumberStyles.AllowDecimalPoint |
			System.Globalization.NumberStyles.AllowExponent;
	}
}

