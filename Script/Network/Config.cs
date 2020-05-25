

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。コンフィグ。
*/


/** Fee.Network
*/
namespace Fee.Network
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

		/** ゲームバージョン。
		*/
		public static string GAME_VERSION = "1";

		/** プレイヤーステータス。送信。頻度。
		*/
		public static int DEFAULT_PLAYER_STATUS_SEND_INTERVAL = 60;
	}
}

