

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
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

		/** リスロー。
		*/
		public static bool RETHROW_ENABLE = false;




		/** ダウンロードサウンドプール。キャッシュ。
		*/
		public static bool USE_DOWNLOAD_SOUNDPOOL_CACHE = true;

		/** ロードストリーミングアセットサウンドプール。キャッシュ。
		*/
		public static bool USE_LOADSTREAMINGASSETS_SOUNDPOOL_CACHE = true;
	}
}

