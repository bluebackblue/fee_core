

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ブルーム。コンフィグ。
*/


/** Fee.Bloom
*/
namespace Fee.Bloom
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
		public const string SHADER_NAME_FIRSTDOWNSAMPLING = "Fee/Bloom/FirstDownSampling";
		public const string SHADER_NAME_DOWNSAMPLING = "Fee/Bloom/DownSampling";
		public const string SHADER_NAME_ADDUPSAMPLING = "Fee/Bloom/AddUpSampling";
		public const string SHADER_NAME_LASTADDUPSAMPLING = "Fee/Bloom/LastAddUpSampling";

		/** デフォルト。輝度抽出閾値。
		*/
		public static float DEFAULT_THRESHOLD = 0.5f;

		/** デフォルト。加算強度。
		*/
		public static float DEFAULT_INTENSITY = 0.3f;

		/** デフォルト。カメラデプス。
		*/
		public static float DEFAULT_CAMERA_DEPTH = 800.2f;
	}
}

