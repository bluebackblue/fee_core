

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

		/** EXECUTIONORDER
		*/
		public const int EXECUTIONORDER_FIRST = -1000;
		public const int EXECUTIONORDER_LAST = 1000;

		/** BAR_FRAME_TIME

			１フレームの時間。

		*/
		public static float BAR_FRAME_TIME = 1.0f / 60;

		/** BAR_FRAME_LENGTH

			１フレームの長さ。

		*/
		public static float BAR_FRAME_LENGTH = 0.25f;

		/** カメラデプス。
		*/
		public static float CAMERADEPTH_FIRST = -100.0f;
		public static float CAMERADEPTH_LAST = 100.0f;

		/** COLOR_FRAME_BASE
		*/
		public static UnityEngine.Color COLOR_FRAME_BASE = new UnityEngine.Color(0.5f,0.5f,0.5f,1.0f);

		/** COLOR_FRAME
		*/
		public static UnityEngine.Color COLOR_FRAME = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
		
		/** COLOR_FIXEDUPDATE
		*/
		public static UnityEngine.Color COLOR_FIXEDUPDATE = new UnityEngine.Color(1.0f,0.0f,0.0f,1.0f);

		/** COLOR_UPDATE
		*/
		public static UnityEngine.Color COLOR_UPDATE = new UnityEngine.Color(0.0f,1.0f,0.0f,1.0f);

		/** COLOR_LATEUPDATE
		*/
		public static UnityEngine.Color COLOR_LATEUPDATE = new UnityEngine.Color(0.0f,0.0f,1.0f,1.0f);

		/** COLOR_RENDER
		*/
		public static UnityEngine.Color COLOR_RENDER = new UnityEngine.Color(1.0f,1.0f,0.0f,1.0f);

		/** MATERIAL_PATH
		*/
		public static string MATERIAL_PATH = "Material/PerformanceCounter/Sprite";
	}
}

