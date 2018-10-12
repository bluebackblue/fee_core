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
		public static bool ASSERT_ENABLE = true;

		/** インプットシステム。マウス。
		*/
		public static bool USE_INPUTSYSTEM_MOUSE = true;

		/** インプットシステム。ポインター。
		*/
		public static bool USE_INPUTSYSTEM_POINTER = true;

		/** インプットマネージャ。マウス。
		*/
		public static bool USE_INPUTMANAGER_MOUSE = true;

		/** インプットシステム。ゲームパッド。
		*/
		#if(UNITY_WEBGL)
		public static bool USE_INPUTSYSTEM_GAMEPAD = false;
		#else
		public static bool USE_INPUTSYSTEM_GAMEPAD = true;
		#endif

		/** インプットマネージャ。ゲームパッド。
		*/
		public static bool USE_INPUTMANAGER_GAMEPAD = true;

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

		/** アナログボタン。オンからオフになる閾値。
		*/
		public static float ANALOG_BUTTON_VALUE_OFF = 0.4f;

		/** アナログボタン。オフからオンになる閾値。
		*/
		public static float ANALOG_BUTTON_VALUE_ON = 0.6f;

		/** キー。左。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_LEFT = UnityEngine.Experimental.Input.Key.A;

		/** キー。右。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_RIGHT = UnityEngine.Experimental.Input.Key.D;

		/** キー。上。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_UP = UnityEngine.Experimental.Input.Key.W;

		/** キー。下。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_DOWN = UnityEngine.Experimental.Input.Key.S;

		/** キー。エンター。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_ENTER = UnityEngine.Experimental.Input.Key.Enter;

		/** キー。エスケープ。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_ESCAPE = UnityEngine.Experimental.Input.Key.Escape;

		/** キー。サブ１。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_SUB1 = UnityEngine.Experimental.Input.Key.LeftShift;

		/** キー。サブ２。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_SUB2 = UnityEngine.Experimental.Input.Key.LeftCtrl;

		/** キー。左メニュー。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_LEFT_MENU = UnityEngine.Experimental.Input.Key.Space;

		/** キー。右メニュー。
		*/
		public static UnityEngine.Experimental.Input.Key KEY_RIGHT_MENU = UnityEngine.Experimental.Input.Key.Backspace;
	}
}

