

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

		/** デバッグリスロー。
		*/
		public static bool DEBUGRETHROW_ENABLE = false;

		/** COLOR_NORMAL
		*/
		public static UnityEngine.Color COLOR_FRAME_BASE = new UnityEngine.Color(0.5f,0.5f,0.5f,1.0f);

		/** COLOR_NORMAL
		*/
		public static UnityEngine.Color COLOR_FRAME = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
		
		/** COLOR_UPDATE
		*/
		public static UnityEngine.Color COLOR_UPDATE = new UnityEngine.Color(1.0f,0.0f,0.0f,1.0f);

		/** COLOR_FIXEDUPDATE
		*/
		public static UnityEngine.Color COLOR_FIXEDUPDATE = new UnityEngine.Color(1.0f,1.0f,0.0f,1.0f);

		/** COLOR_LATEUPDATE
		*/
		public static UnityEngine.Color COLOR_LATEUPDATE = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);

		/** COLOR_ONPRERENDER
		*/
		public static UnityEngine.Color COLOR_ONPRERENDER = new UnityEngine.Color(0.0f,0.0f,1.0f,1.0f);

		/** COLOR_ONPOSTRENDER
		*/
		public static UnityEngine.Color COLOR_ONPOSTRENDER = new UnityEngine.Color(0.0f,1.0f,0.0f,1.0f);

		/** MATERIAL_PATH
		*/
		public static string MATERIAL_PATH = "Material/PerformanceCounter/Sprite";
	}
}

