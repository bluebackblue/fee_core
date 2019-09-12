

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エクセル。コンフィグ。
*/


/** Fee.Excel
*/
namespace Fee.Excel
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

		/** 終端検索範囲。
		*/
		public static int END_SEARCH_WIDTH = 1000;
		public static int END_SEARCH_HEIGHT = 1000;

		/** COMMAND
		*/
		public static string COMMAND_PARAM_ROOT = "[root]";
		public static string COMMAND_PARAM_TYPE = "[param_type]";
		public static string COMMAND_PARAM_NAME = "[param_name]";
		public static string COMMAND_PARAM_END = "[end]";
		public static string COMMAND_ON = "*";
		
		/** PARAMTYPE
		*/
		public const string PARAMTYPE_STRING = "string";
		public const string PARAMTYPE_INT = "int";
		public const string PARAMTYPE_FLOAT = "float";
		public const string PARAMTYPE_COMMENT = "-comment";
	}
}

