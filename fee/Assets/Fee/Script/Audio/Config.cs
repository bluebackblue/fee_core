using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。コンフィグ。
*/


/** NAudio
*/
namespace NAudio
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

		/** デフォルトボリューム。マスター。
		*/
		public static float DEFAULT_VOLUME_MASTER = 0.8f;

		/** デフォルトボリューム。ＳＥ。
		*/
		public static float DEFAULT_VOLUME_SE = 0.8f;

		/** デフォルトボリューム。ＢＧＭ。
		*/
		public static float DEFAULT_VOLUME_BGM = 0.8f;
	}
}

