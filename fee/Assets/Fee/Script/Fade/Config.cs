using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief フェード。コンフィグ。
*/


/** NFade
*/
namespace NFade
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

		/** デフォルト。アニメ。スピード。
		*/
		public static float DEFAULT_ANIME_SPEED = 0.01f;

		/** デフォルト。アニメ。色。
		*/
		public static Color DEFAULT_ANIME_COLOR = new Color(0.0f,0.0f,0.0f,0.0f);
	}
}

