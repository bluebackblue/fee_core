

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フェード。コンフィグ。
*/


/** Fee.Fade
*/
namespace Fee.Fade
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

		/** デフォルト。アニメ。スピード。
		*/
		public static float DEFAULT_ANIME_SPEED = 0.01f;

		/** デフォルト。アニメ。色。
		*/
		public static UnityEngine.Color DEFAULT_ANIME_COLOR = UnityEngine.Color.clear;
	}
}

