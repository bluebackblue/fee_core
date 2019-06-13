

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。コンフィグ。
*/


/** Fee.Ui
*/
namespace Fee.Ui
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




		/** デフォルト。コーナーサイズ。
		*/
		public static int DEFAULT_BUTTON_CORNER_SIZE = 10;

		/** デフォルト。コーナーサイズ。
		*/
		public static int DEFAULT_SLIDER_BG_CORNER_SIZE = 10;

		/** デフォルト。コーナーサイズ。
		*/
		public static int DEFAULT_SLIDER_BUTTON_CORNER_SIZE = 10;

		/** デフォルト。ウィンドウレイヤーインデックス。開始。
		*/
		public static int DEFAULT_WINDOW_LAYER_INDEX_START = 1;

		/** ボタンテクスチャー。フィルターモード。チェック。
		*/
		public static bool BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE = true;

		/** ボタンテクスチャー。ミップマップ。チェック。
		*/
		public static bool BUTTONTEXTURE_CHECK_MIPMAPCOUNT_ENABLE = true;
	}
}

