

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

		/** リスロー。
		*/
		public static bool RETHROW_ENABLE = false;




		/** ゲームバージョン。
		*/
		public static string GAME_VERSION = "1";

		/** プレハブ名。
		*/
		public static string PREFAB_NAME_PLAYER = "Prefab/Network/Player";
	}
}

