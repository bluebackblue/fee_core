

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。コンフィグ。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
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

		/** ロードＵＲＬサウンドプール。キャッシュ。
		*/
		public static bool USE_LOADURL_SOUNDPOOL_CACHE = true;

		/** ロードストリーミングアセットサウンドプール。キャッシュ。
		*/
		public static bool USE_LOADSTREAMINGASSETS_SOUNDPOOL_CACHE = true;
	}
}

