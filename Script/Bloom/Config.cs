

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
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

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = true;

		/** マテリアル名。
		*/
		public const string MATERIAL_NAME_FIRSTDOWNSAMPLING = "Material/Bloom/BloomFirstDownSampling";
		public const string MATERIAL_NAME_DOWNSAMPLING = "Material/Bloom/BloomDownSampling";
		public const string MATERIAL_NAME_UPSAMPLING = "Material/Bloom/BloomAddUpSampling";
		public const string MATERIAL_NAME_LASTUPSAMPLING = "Material/Bloom/BloomLastAddUpSampling";

		/** 輝度抽出閾値。
		*/
		public static float DEFAULT_THRESHOLD = 0.5f;

		/** 加算強度。
		*/
		public static float DEFAULT_INTENSITY = 0.3f;

		/** デフォルト。カメラデプス。
		*/
		public static float DEFAULT_CAMERA_DEPTH = 800.2f;
	}
}

