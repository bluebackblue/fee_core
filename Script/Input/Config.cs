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
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_MOUSE = true;
		#elif(UNITY_ANDROID)
		public static bool USE_INPUTSYSTEM_MOUSE = false;
		#else
		public static bool USE_INPUTSYSTEM_MOUSE = true;
		#endif

		/** インプットシステム。ポインター。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_POINTER = true;
		#elif(UNITY_ANDROID)
		public static bool USE_INPUTSYSTEM_POINTER = true;
		#else
		public static bool USE_INPUTSYSTEM_POINTER = true;
		#endif

		/** インプットシステム。ゲームパッド。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_GAMEPAD = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTSYSTEM_GAMEPAD = false;
		#else
		public static bool USE_INPUTSYSTEM_GAMEPAD = true;
		#endif

		/** インプットシステム。タッチスクリーン。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_TOUCHSCREEN = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTSYSTEM_TOUCHSCREEN = false;
		#else
		public static bool USE_INPUTSYSTEM_TOUCHSCREEN = true;
		#endif

		/** インプットシステム。キー。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_KEY = true;
		#else
		public static bool USE_INPUTSYSTEM_KEY = true;
		#endif

		/** インプットマネージャ。マウス。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_MOUSE = true;
		#else
		public static bool USE_INPUTMANAGER_MOUSE = true;
		#endif

		/** インプットマネージャ。ゲームパッド。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_GAMEPAD = true;
		#else
		public static bool USE_INPUTMANAGER_GAMEPAD = true;
		#endif

		/** インプットマネージャ。タッチ。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_TOUCH = false;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTMANAGER_TOUCH = false;
		#else
		public static bool USE_INPUTMANAGER_TOUCH = true;
		#endif

		/** インプットマネージャ。キー。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_KEY = true;
		#else
		public static bool USE_INPUTMANAGER_KEY = true;
		#endif

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

		/** マウス。エディター。オフセットＸ。
		*/
		#if(UNITY_EDITOR)
		public static int MOUSE_EDITOR_OFFSET_X = 0;
		#endif

		/** マウス。エディター。オフセットＹ。
		*/
		#if(UNITY_EDITOR)
		public static int MOUSE_EDITOR_OFFSET_Y = 5;
		#endif

		/** キー。左。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_LEFT = UnityEngine.Experimental.Input.Key.A;
		#endif

		/** キー。右。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_RIGHT = UnityEngine.Experimental.Input.Key.D;
		#endif

		/** キー。上。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_UP = UnityEngine.Experimental.Input.Key.W;
		#endif

		/** キー。下。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_DOWN = UnityEngine.Experimental.Input.Key.S;
		#endif

		/** キー。エンター。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_ENTER = UnityEngine.Experimental.Input.Key.Enter;
		#endif

		/** キー。エスケープ。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_ESCAPE = UnityEngine.Experimental.Input.Key.Escape;
		#endif

		/** キー。サブ１。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_SUB1 = UnityEngine.Experimental.Input.Key.LeftShift;
		#endif

		/** キー。サブ２。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_SUB2 = UnityEngine.Experimental.Input.Key.LeftCtrl;
		#endif

		/** キー。左メニュー。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_LEFT_MENU = UnityEngine.Experimental.Input.Key.Space;
		#endif

		/** キー。右メニュー。
		*/
		#if(USE_DEF_INPUTSYSTEM)
		public static UnityEngine.Experimental.Input.Key KEY_RIGHT_MENU = UnityEngine.Experimental.Input.Key.Backspace;
		#endif
	}
}

