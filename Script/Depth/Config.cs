

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief デプス。コンフィグ。
*/


/** Fee.Depth
*/
namespace Fee.Depth
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
		public static string SHADER_NAME_DEPTHTEXTURE = "Fee/Depth/DepthTexture";
		//public static string SHADER_NAME_CAMERADEPTHTEXTURE = "Fee/Depth/CameraDepthTexture";

		/** デフォルト。ブレンド率。
		*/
		public static float DEFAULT_BLENDRATE = 0.5f;
	}
}

