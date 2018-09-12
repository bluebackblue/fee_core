using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ネットワーク。コンフィグ。
*/


/** NNetwork
*/
namespace NNetwork
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
		public static bool ASSERT_ENABLE = false;

		/** ゲームバージョン。
		*/
		public static string GAME_VERSION = "1";

		/** プレハブ名。
		*/
		public static string PREFAB_NAME_PLAYER = "Prefab/Network/player";
	}
}

