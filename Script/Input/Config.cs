

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
	/** UnityEngine_InputSystem
	*/
	#if(USE_DEF_FEE_INPUTSYSTEM)
		#if((UNITY_2018_3)||(UNITY_2018_4))
			using UnityEngine_InputSystem = UnityEngine.Experimental.Input;
		#else
			using UnityEngine_InputSystem = UnityEngine.InputSystem;
		#endif
	#endif

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




		/** インプットシステム。マウス。マウス位置。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = true;
		#elif(UNITY_ANDROID)
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = false;
		#else
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = true;
		#endif

		/** インプットシステム。ポインター。マウス位置。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = true;
		#elif(UNITY_ANDROID)
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = true;
		#else
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = true;
		#endif

		/** インプットマネージャ。インプットマウス。マウス位置。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = true;
		#else
		public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = true;
		#endif




		/** インプットシステム。マウス。マウスボタン。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = true;
		#elif(UNITY_ANDROID)
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = false;
		#else
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = true;
		#endif

		/** インプットシステム。ポインター。マウスボタン。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = true;
		#elif(UNITY_ANDROID)
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = true;
		#else
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = true;
		#endif

		/** インプットマネージャ。インプットマウス。マウスボタン。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = true;
		#else
		public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = true;
		#endif




		/** インプットシステム。マウス。マウスホイール。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL= true;
		#elif(UNITY_ANDROID)
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL = false;
		#else
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL = true;
		#endif

		/** インプットマネージャ。インプットネーム。マウスホイール。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL = true;
		#else
		public static bool USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL = true;
		#endif




		/** インプットシステム。ゲームパッド。パッドデジタルボタン。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON = false;
		#else
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON = true;
		#endif

		/** インプットマネージャ。インプットネーム。パッドデジタルボタン。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON = false;
		#else
		public static bool USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON = true;
		#endif





		/** インプットシステム。ゲームパッド。パッドスティック。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADSTICK = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADSTICK = false;
		#else
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADSTICK = true;
		#endif

		/** インプットマネージャ。インプットネーム。パッドスティック。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_INPUTNAME_PADSTICK = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTMANAGER_INPUTNAME_PADSTICK = false;
		#else
		public static bool USE_INPUTMANAGER_INPUTNAME_PADSTICK = true;
		#endif





		/** インプットシステム。ゲームパッド。パッドトリガー。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER = false;
		#else
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER = true;
		#endif

		/** インプットマネージャ。インプットネーム。パッドトリガー。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_INPUTNAME_PADTRIGGER = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTMANAGER_INPUTNAME_PADTRIGGER = false;
		#else
		public static bool USE_INPUTMANAGER_INPUTNAME_PADTRIGGER = true;
		#endif





		/** インプットシステム。ゲームパッド。パッドモーター。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADMOTOR = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADMOTOR = false;
		#else
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADMOTOR = true;
		#endif




		/** インプットシステム。キーボード。キー。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_KEYBOARD_KEY = true;
		#else
		public static bool USE_INPUTSYSTEM_KEYBOARD_KEY = true;
		#endif

		/** インプットマネージャ。ゲットキー。キー。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_GETKEY_KEY = true;
		#else
		public static bool USE_INPUTMANAGER_GETKEY_KEY = true;
		#endif





		/** インプットシステム。タッチスクリーン。タッチ。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH = true;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH = false;
		#else
		public static bool USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH = true;
		#endif

		/** インプットマネージャ。インプットタッチ。タッチ。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_INPUTTOUCH_TOUCH = false;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTMANAGER_INPUTTOUCH_TOUCH = false;
		#else
		public static bool USE_INPUTMANAGER_INPUTTOUCH_TOUCH = true;
		#endif

		/** インプットマネージャ。インプットマウス。タッチ。
		*/
		#if(UNITY_EDITOR)
		public static bool USE_INPUTMANAGER_INPUTMOUSE_TOUCH = false;
		#elif(UNITY_WEBGL)
		public static bool USE_INPUTMANAGER_INPUTMOUSE_TOUCH = false;
		#else
		public static bool USE_INPUTMANAGER_INPUTMOUSE_TOUCH = true;
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

		
		
		/** インプットシステム。左。（デジタル）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_LEFT = UnityEngine_InputSystem.Key.LeftArrow;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_LEFT = UnityEngine.KeyCode.LeftArrow;

		/** インプットシステム。右。（デジタル）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_RIGHT = UnityEngine_InputSystem.Key.RightArrow;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_RIGHT = UnityEngine.KeyCode.RightArrow;

		/** インプットシステム。上。（デジタル）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_UP = UnityEngine_InputSystem.Key.UpArrow;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_UP = UnityEngine.KeyCode.UpArrow;

		/** インプットシステム。下。（デジタル）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_DOWN = UnityEngine_InputSystem.Key.DownArrow;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_DOWN = UnityEngine.KeyCode.DownArrow;

		
		
		/** インプットシステム。左。（左アナログ）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_L_LEFT = UnityEngine_InputSystem.Key.A;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_L_LEFT = UnityEngine.KeyCode.A;

		/** インプットシステム。右。（左アナログ）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_L_RIGHT = UnityEngine_InputSystem.Key.D;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_L_RIGHT = UnityEngine.KeyCode.D;

		/** インプットシステム。上。（左アナログ）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_L_UP = UnityEngine_InputSystem.Key.W;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_L_UP = UnityEngine.KeyCode.W;

		/** インプットシステム。下。（左アナログ）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_L_DOWN = UnityEngine_InputSystem.Key.S;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_L_DOWN = UnityEngine.KeyCode.S;



		/** インプットシステム。左。（右アナログ）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_R_LEFT = UnityEngine_InputSystem.Key.J;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_R_LEFT = UnityEngine.KeyCode.J;

		/** インプットシステム。右。（右アナログ）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_R_RIGHT = UnityEngine_InputSystem.Key.L;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_R_RIGHT = UnityEngine.KeyCode.L;

		/** インプットシステム。上。（右アナログ）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_R_UP = UnityEngine_InputSystem.Key.I;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_R_UP = UnityEngine.KeyCode.I;

		/** インプットシステム。下。（右アナログ）。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_R_DOWN = UnityEngine_InputSystem.Key.K;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_R_DOWN = UnityEngine.KeyCode.K;



		/** インプットシステム。エンター。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_ENTER = UnityEngine_InputSystem.Key.Enter;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_ENTER = UnityEngine.KeyCode.Return;

		/** インプットシステム。エスケープ。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_ESCAPE = UnityEngine_InputSystem.Key.Escape;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_ESCAPE = UnityEngine.KeyCode.Escape;

		/** インプットシステム。サブ１。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_SUB1 = UnityEngine_InputSystem.Key.LeftShift;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_SUB1 = UnityEngine.KeyCode.LeftShift;

		/** インプットシステム。サブ２。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_SUB2 = UnityEngine_InputSystem.Key.LeftCtrl;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_SUB2 = UnityEngine.KeyCode.LeftControl;



		/** インプットシステム。左メニュー。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_LEFT_MENU = UnityEngine_InputSystem.Key.Space;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_LEFT_MENU = UnityEngine.KeyCode.Space;

		/** インプットシステム。右メニュー。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_RIGHT_MENU = UnityEngine_InputSystem.Key.Backspace;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_RIGHT_MENU = UnityEngine.KeyCode.Backspace;



		/** インプットシステム。左トリガー１。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_L_TRIGGER_1 = UnityEngine_InputSystem.Key.Z;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_L_TRIGGER_1 = UnityEngine.KeyCode.Z;

		/** インプットシステム。右トリガー１。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_R_TRIGGER_1 = UnityEngine_InputSystem.Key.X;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_R_TRIGGER_1 = UnityEngine.KeyCode.X;

		/** インプットシステム。左トリガー２。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_L_TRIGGER_2 = UnityEngine_InputSystem.Key.C;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_L_TRIGGER_2 = UnityEngine.KeyCode.C;

		/** インプットシステム。右トリガー２。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public static UnityEngine_InputSystem.Key INPUTSYSTEM_B_R_TRIGGER_2 = UnityEngine_InputSystem.Key.V;
		#endif
		public static UnityEngine.KeyCode INPUTMANAGER_B_R_TRIGGER_2 = UnityEngine.KeyCode.V;



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

