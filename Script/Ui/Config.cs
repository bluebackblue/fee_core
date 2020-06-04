

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
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

		/** デバッグリスロー。
		*/
		public static bool DEBUGRETHROW_ENABLE = false;

		/** プレイヤーループ。追加先
		*/
		public static System.Type PLAYERLOOP_TARGETTYPE = typeof(Fee.EventPlate.PlayerLoopSystemType.Fee_EventPlate_Main);
		
		/** プレイヤーループ。追加方法。
		*/
		public static Fee.PlayerLoopSystem.AddType PLAYERLOOP_ADDTYPE = Fee.PlayerLoopSystem.AddType.AddAfter;

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

		/** ボタンテクスチャ。フィルターモード。チェック。
		*/
		public static bool BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE = true;

		/** ボタンテクスチャ。ミップマップ。チェック。
		*/
		public static bool BUTTONTEXTURE_CHECK_MIPMAPCOUNT_ENABLE = true;

		/** ドラッグキャンセル。ドラッグ距離閾値。
		*/
		public static float DRAGCANCEL_THRESHOLD = 2.0f;
	}
}

