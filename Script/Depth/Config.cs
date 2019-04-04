

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 深度。コンフィグ。
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

		/** マテリアル名。
		*/
		public const string MATERIAL_NAME_DEPTHVIEW = "Material/Depth/DepthView";

		/** デフォルト。カメラデプス。
		*/
		public static float DEFAULT_CAMERA_DEPTH = 800.3f;
	}
}

