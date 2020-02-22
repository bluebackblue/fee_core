

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。コンフィグ。
*/


/** Fee.File
*/
namespace Fee.File
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

		/** USE_ASYNC
		*/
		#if(UNITY_5)
		public static bool USE_ASYNC = false;
		#elif(UNITY_WEBGL)
		public static bool USE_ASYNC = false;
		#else
		public static bool USE_ASYNC = true;
		#endif

		/** RESPONSECODE_KEY
		*/
		public static string RESPONSECODE_KEY = "ResponseCode";
	}
}

