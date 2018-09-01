using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。コンフィグ。
*/


/** NInput
*/
namespace NInput
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

		/** ドラッグ判定閾値。4方向。
		*/
		public static float DRAG_DIR4_DOT = 0.5f;

		/** ドラッグアップ判定閾値。
		*/
		public static float DRAGUP_LENGTH_SCALE = 1.5f;
		public static float DRAGUP_LENGTH_MIN = 9.0f;

		/** ドラッグオン判定閾値。
		*/
		public static float DRAGON_LENGTH_MIN = 20.0f;

		/** マウス。ドラッグ時間。最大。
		*/
		public static int MOUSE_DRAGTIME_MAX = 9999;

		/** マウス。ホイール。
		*/
		public static string MOUSE_INPUTNAME_WHEEL = "Mouse ScrollWheel";

		/** ジョイスティック。方向６。
		*/
		public static string JOY_INPUTNAME_AXIS6 = "JoyAxis6";

		/** ジョイスティック。方向７。
		*/
		public static string JOY_INPUTNAME_AXIS7 = "JoyAxis7";

		/** ジョイスティック。方向８。
		*/
		public static string JOY_INPUTNAME_AXIS8 = "JoyAxis8";

		/** ジョイスティック。ボタン０。
		*/
		public static string JOY_INPUTNAME_BUTTON0 = "JoyButton0";

		/** ジョイスティック。ボタン１。
		*/
		public static string JOY_INPUTNAME_BUTTON1 = "JoyButton1";

		/** ジョイスティック。ボタン２。
		*/
		public static string JOY_INPUTNAME_BUTTON2 = "JoyButton2";

		/** ジョイスティック。ボタン３。
		*/
		public static string JOY_INPUTNAME_BUTTON3 = "JoyButton3";

		/** キー。左。
		*/
		public static KeyCode KEY_LEFT = KeyCode.A;

		/** キー。右。
		*/
		public static KeyCode KEY_RIGHT = KeyCode.D;

		/** キー。上。
		*/
		public static KeyCode KEY_UP = KeyCode.W;

		/** キー。下。
		*/
		public static KeyCode KEY_DOWN = KeyCode.S;
	}
}

