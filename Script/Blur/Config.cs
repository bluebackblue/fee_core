

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

		/** リスロー。
		*/
		public static bool RETHROW_ENABLE = false;

		/** マテリアル名。
		*/
		public static string MATERIAL_NAME_BLURX = "Material/Blur/BlurX";
		public static string MATERIAL_NAME_BLURY = "Material/Blur/BlurY";

		/** デフォルト。カメラデプス。
		*/
		public static float DEFAULT_CAMERA_DEPTH = 800.5f;
	}
}

