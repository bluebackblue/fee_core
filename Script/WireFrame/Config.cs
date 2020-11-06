

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ワーヤーフレーム。コンフィグ。
*/


/** Fee.WireFrame
*/
namespace Fee.WireFrame
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
		public static string SHADER_NAME_WIREFRAME = "Fee/WireFrame/WireFrame";

		/** デフォルト。
		*/
		public static float DEFAULT_LIMIT = 0.02f;
	}
}

