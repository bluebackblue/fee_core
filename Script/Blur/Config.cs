

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ブラー。コンフィグ。
*/


/** Fee.Blur
*/
namespace Fee.Blur
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

		/** シェーダー名。
		*/
		public static string SHADER_NAME_BLURX = "Fee/Blur/BlurX";
		public static string SHADER_NAME_BLURY = "Fee/Blur/BlurY";

		/** デフォルト。ブレンド率。
		*/
		public static float DEFAULT_BLENDRATE = 1.0f;
	}
}

