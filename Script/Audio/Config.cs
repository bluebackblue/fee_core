

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。コンフィグ。
*/


/** Fee.Audio
*/
namespace Fee.Audio
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




		/** デフォルトボリューム。マスター。
		*/
		public static float DEFAULT_VOLUME_MASTER = 0.7f;

		/** デフォルトボリューム。ＳＥ。
		*/
		public static float DEFAULT_VOLUME_SE = 0.7f;

		/** デフォルトボリューム。ＢＧＭ。
		*/
		public static float DEFAULT_VOLUME_BGM = 0.7f;

		/** クロスフェード速度。
		*/
		public static float BGM_CROSSFADE_SPEED = 0.02f;

		/** ＢＧＭ再生。フェードイン。
		*/
		public static bool BGM_PLAY_FADEIN = true;
	}
}

