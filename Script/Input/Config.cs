

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。コンフィグ。
*/


/** Fee.Input
*/
namespace Fee.Input
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
		public static int MOUSE_EDITOR_OFFSET_Y = 0;
		#endif

		/** インプットシステム。左。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_L_LEFT = UnityEngine.InputSystem.Key.A;
		#endif

		/** インプットシステム。右。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_L_RIGHT = UnityEngine.InputSystem.Key.D;
		#endif

		/** インプットシステム。上。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_L_UP = UnityEngine.InputSystem.Key.W;
		#endif

		/** インプットシステム。下。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_L_DOWN = UnityEngine.InputSystem.Key.S;
		#endif

		/** インプットシステム。左。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_R_LEFT = UnityEngine.InputSystem.Key.LeftArrow;
		#endif

		/** インプットシステム。右。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_R_RIGHT = UnityEngine.InputSystem.Key.RightArrow;
		#endif

		/** インプットシステム。上。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_R_UP = UnityEngine.InputSystem.Key.UpArrow;
		#endif

		/** インプットシステム。下。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_R_DOWN = UnityEngine.InputSystem.Key.DownArrow;
		#endif

		/** インプットシステム。エンター。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_ENTER = UnityEngine.InputSystem.Key.Enter;
		#endif

		/** インプットシステム。エスケープ。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_ESCAPE = UnityEngine.InputSystem.Key.Escape;
		#endif

		/** インプットシステム。サブ１。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_SUB1 = UnityEngine.InputSystem.Key.LeftShift;
		#endif

		/** インプットシステム。サブ２。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_SUB2 = UnityEngine.InputSystem.Key.LeftCtrl;
		#endif

		/** インプットシステム。左メニュー。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_LEFT_MENU = UnityEngine.InputSystem.Key.Space;
		#endif

		/** インプットシステム。右メニュー。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_RIGHT_MENU = UnityEngine.InputSystem.Key.Backspace;
		#endif


		/** インプットシステム。左メニュー。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_LEFT_TRIGGER_BUTTON = UnityEngine.InputSystem.Key.L;
		#endif

		/** インプットシステム。右メニュー。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine.InputSystem.Key INPUTSYSTEM_RIGHT_TRIGGER_BUTTON = UnityEngine.InputSystem.Key.R;
		#endif




		/** インプットマネージャ。マウスホイール。
		*/
		public static string INPUTMANAGER_MOUSEWHEEL = "Mouse ScrollWheel";

		/** インプットマネージャ。トリガー。
		*/
		public static string[] INPUTMANAGER_LT1 = new string[]{
			"type0_left_trigger1_button",
			"type1_left_trigger1_button"
		};
		public static string[] INPUTMANAGER_RT1 = new string[]{
			"type0_right_trigger1_button",
			"type1_right_trigger1_button"
		};
		public static string[] INPUTMANAGER_LT2 = new string[]{
			"type0_left_trigger2_axis",
			"type1_left_trigger2_axis"
		};
		public static string[] INPUTMANAGER_RT2 = new string[]{
			"type0_right_trigger2_axis",
			"type1_right_trigger2_axis"
		};

		/** インプットマネージャ。ボタン。
		*/
		public static string[] INPUTMANAGER_LEFT = new string[]{
			"type0_left",
			"type1_left"
		};
		public static string[] INPUTMANAGER_RIGHT = new string[]{
			"type0_right",
			"type1_right"
		};
		public static string[] INPUTMANAGER_UP = new string[]{
			"type0_up",
			"type1_up"
		};
		public static string[] INPUTMANAGER_DOWN = new string[]{
			"type0_down",
			"type1_down"
		};
		public static string[] INPUTMANAGER_ENTER = new string[]{
			"type0_enter",
			"type1_enter"
		};
		public static string[] INPUTMANAGER_ESCAPE = new string[]{
			"type0_escape",
			"type1_escape"
		};
		public static string[] INPUTMANAGER_SUB1 = new string[]{
			"type0_sub1",
			"type1_sub1"
		};
		public static string[] INPUTMANAGER_SUB2 = new string[]{
			"type0_sub2",
			"type1_sub2"
		};
		public static string[] INPUTMANAGER_LMENU = new string[]{
			"type0_left_menu",
			"type1_left_menu"
		};
		public static string[] INPUTMANAGER_RMENU = new string[]{
			"type0_right_menu",
			"type1_right_menu"
		};

		/** インプットマネージャ。ティック。
		*/
		public static string[] INPUTMANAGER_LSX = new string[]{
			"type0_left_stick_axis_x",
			"type1_left_stick_axis_x"
		};
		public static string[] INPUTMANAGER_LSY = new string[]{
			"type0_left_stick_axis_y",
			"type1_left_stick_axis_y"
		};
		public static string[] INPUTMANAGER_RSX = new string[]{
			"type0_right_stick_axis_x",
			"type1_right_stick_axis_x"
		};
		public static string[] INPUTMANAGER_RSY = new string[]{
			"type0_right_stick_axis_y",
			"type1_right_stick_axis_y"
		};
		public static string[] INPUTMANAGER_LSB = new string[]{
			"type0_left_stick_button",
			"type1_left_stick_button"
		};
		public static string[] INPUTMANAGER_RSB = new string[]{
			"type0_right_stick_button",
			"type1_right_stick_button"
		};
	}
}

