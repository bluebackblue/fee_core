

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 雲。コンフィグ。
*/


/** Fee.Cloud
*/
namespace Fee.Cloud
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
		public static string SHADER_NAME_VOLUMECLOUD = "Fee/Cloud/VolumeCloud";

		/** デフォルト。強度。
		*/
		public static float DEFAULT_POWER = 1.0f;

		/** デフォルト。ノイズスケール。
		*/
		public static float DEFAULT_NOISESCALE = 5.0f;

		/** デフォルト。スケール。
		*/
		public static float DEFAULT_INV_SCALE = 1.3f;

		/** デフォルト。ノイズオフセット。
		*/
		public static UnityEngine.Vector3 DEFAULT_NOISEOFFSET = new UnityEngine.Vector3(0.0f,0.0f,0.0f);

		/** デフォルト。色。
		*/
		public static UnityEngine.Color DEFAULT_COLOR = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
	}
}

