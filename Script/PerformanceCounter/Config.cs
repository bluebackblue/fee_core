

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パフォーマンスカウンター。コンフィグ。
*/


/** Fee.PerformanceCounter
*/
namespace Fee.PerformanceCounter
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

		/** COLOR_NORMAL
		*/
		public static UnityEngine.Color COLOR_NORMAL = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
		
		/** COLOR_OVER
		*/
		public static UnityEngine.Color COLOR_OVER = new UnityEngine.Color(1.0f,0.0f,0.0f,1.0f);

		/** ログプレフィックス。
		*/
		public static string LOG_TAGNAME_STRING = "-------- PerformanceCounter --------";
		
	}
}

