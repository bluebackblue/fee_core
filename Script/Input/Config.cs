

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
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

		/** デフォルト。連打間隔。
		*/
		public static int DEFAULT_RAPID_TIME_MAX = 6;

		/** インプットシステム。マウス。マウス位置。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
			#if((!UNITY_EDITOR)&&(UNITY_ANDROID))
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = false;
			#else
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = true;
			#endif
		#else
			//both
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = true;
			#elif(UNITY_ANDROID)
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = false;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = false;
			#else
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEPOSITION = true;
			#endif
		#endif

		/** インプットシステム。ポインター。マウス位置。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = true;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = false;
			#elif(UNITY_ANDROID)
			public static bool USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = false;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = false;
			#else
			public static bool USE_INPUTSYSTEM_POINTER_MOUSEPOSITION = false;
			#endif
		#endif

		/** インプットマネージャ。インプットマウス。マウス位置。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = true;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = false;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = true;
			#else
			public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEPOSITION = true;
			#endif
		#endif

		/** インプットシステム。マウス。マウスボタン。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
			#if((!UNITY_EDITOR)&&(UNITY_ANDROID))
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = false;
			#else
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = true;
			#endif
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = true;
			#elif(UNITY_ANDROID)
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = false;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = false;
			#else
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEBUTTON = true;
			#endif
		#endif

		/** インプットシステム。ポインター。マウスボタン。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = true;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = false;
			#elif(UNITY_ANDROID)
			public static bool USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = false;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = false;
			#else
			public static bool USE_INPUTSYSTEM_POINTER_MOUSEBUTTON = false;
			#endif
		#endif

		/** インプットマネージャ。インプットマウス。マウスボタン。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = true;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = false;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = true;
			#else
			public static bool USE_INPUTMANAGER_INPUTMOUSE_MOUSEBUTTON = true;
			#endif
		#endif

		/** インプットシステム。マウス。マウスホイール。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL= false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL= true;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL= true;
			#elif(UNITY_ANDROID)
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL = false;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL = false;
			#else
			public static bool USE_INPUTSYSTEM_MOUSE_MOUSEWHEEL = true;
			#endif
		#endif

		/** インプットマネージャ。インプットネーム。マウスホイール。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL = true;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL = false;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL = true;
			#else
			public static bool USE_INPUTMANAGER_INPUTNAME_MOUSEWHEEL = true;
			#endif
		#endif

		/** インプットシステム。ゲームパッド。パッドデジタルボタン。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON = true;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON = true;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON = false;
			#else
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON = true;
			#endif
		#endif

		/** インプットマネージャ。インプットネーム。パッドデジタルボタン。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON = true;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON = false;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON = true;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON = true;
			#else
			public static bool USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON = true;
			#endif
		#endif

		/** インプットシステム。ゲームパッド。パッドスティック。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADSTICK = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADSTICK = true;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADSTICK = true;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADSTICK = false;
			#else
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADSTICK = true;
			#endif
		#endif

		/** インプットマネージャ。インプットネーム。パッドスティック。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTNAME_PADSTICK = true;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTNAME_PADSTICK = false;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTMANAGER_INPUTNAME_PADSTICK = true;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTMANAGER_INPUTNAME_PADSTICK = true;
			#else
			public static bool USE_INPUTMANAGER_INPUTNAME_PADSTICK = true;
			#endif
		#endif

		/** インプットシステム。ゲームパッド。パッドトリガー。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER = true;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER = true;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER = false;
			#else
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER = true;
			#endif
		#endif

		/** インプットマネージャ。インプットネーム。パッドトリガー。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTNAME_PADTRIGGER = true;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTNAME_PADTRIGGER = false;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTMANAGER_INPUTNAME_PADTRIGGER = true;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTMANAGER_INPUTNAME_PADTRIGGER = true;
			#else
			public static bool USE_INPUTMANAGER_INPUTNAME_PADTRIGGER = true;
			#endif
		#endif

		/** インプットシステム。ゲームパッド。パッドモーター。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADMOTOR = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_GAMEPAD_PADMOTOR = true;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADMOTOR = true;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADMOTOR = false;
			#else
			public static bool USE_INPUTSYSTEM_GAMEPAD_PADMOTOR = true;
			#endif
		#endif

		/** インプットシステム。キーボード。キー。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_KEYBOARD_KEY = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_KEYBOARD_KEY = true;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_KEYBOARD_KEY = true;
			#else
			public static bool USE_INPUTSYSTEM_KEYBOARD_KEY = true;
			#endif
		#endif

		/** インプットマネージャ。ゲットキー。キー。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_GETKEY_KEY = true;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_GETKEY_KEY = false;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTMANAGER_GETKEY_KEY = true;
			#else
			public static bool USE_INPUTMANAGER_GETKEY_KEY = true;
			#endif
		#endif

		/** インプットシステム。マウス。タッチ。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_MOUSE_TOUCH = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
			#if((!UNITY_EDITOR)&&(UNITY_ANDROID))
			public static bool USE_INPUTSYSTEM_MOUSE_TOUCH = false;
			#else
			public static bool USE_INPUTSYSTEM_MOUSE_TOUCH = true;
			#endif
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_MOUSE_TOUCH = false;
			#elif(UNITY_ANDROID)
			public static bool USE_INPUTSYSTEM_MOUSE_TOUCH = true;
			#else
			public static bool USE_INPUTSYSTEM_MOUSE_TOUCH = false;
			#endif
		#endif

		/** インプットマネージャ。インプットマウス。タッチ。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTMOUSE_TOUCH = true;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTMOUSE_TOUCH = false;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTMANAGER_INPUTMOUSE_TOUCH = true;
			#elif(UNITY_WEBGL)
			public static bool USE_INPUTMANAGER_INPUTMOUSE_TOUCH = true;
			#else
			public static bool USE_INPUTMANAGER_INPUTMOUSE_TOUCH = true;
			#endif
		#endif

		/** インプットシステム。タッチスクリーン。タッチ。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH = false;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH = true;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH = false;
			#elif(UNITY_ANDROID)
			public static bool USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH = false;
			#else
			public static bool USE_INPUTSYSTEM_TOUCHSCREEN_TOUCH = false;
			#endif
		#endif

		/** インプットマネージャ。インプットタッチ。タッチ。
		*/
		#if((ENABLE_LEGACY_INPUT_MANAGER)&&(!ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTTOUCH_TOUCH = true;
		#elif((!ENABLE_LEGACY_INPUT_MANAGER)&&(ENABLE_INPUT_SYSTEM))
		public static bool USE_INPUTMANAGER_INPUTTOUCH_TOUCH = false;
		#else
			#if(UNITY_EDITOR)
			public static bool USE_INPUTMANAGER_INPUTTOUCH_TOUCH = false;
			#elif(UNITY_ANDROID)
			public static bool USE_INPUTMANAGER_INPUTTOUCH_TOUCH = true;
			#else
			public static bool USE_INPUTMANAGER_INPUTTOUCH_TOUCH = false;
			#endif
		#endif

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

		/** インプットマネージャ。マウスホイール。
		*/
		public static string INPUTMANAGER_MOUSEWHEEL = "Mouse ScrollWheel";

		/** PAD_MAX
		*/
		public static int PAD_MAX = 2;
		
		/** インプットマネージャ。トリガー。
		*/
		public static Pad_InputManagerItemName INPUTMANAGER_LT1 = new Pad_InputManagerItemName(
			"pad_0_type_p_left_trigger1_button",
			"pad_0_type_x_left_trigger1_button",
			"pad_0_type_a_left_trigger1_button",
			"pad_1_type_p_left_trigger1_button",
			"pad_1_type_x_left_trigger1_button",
			"pad_1_type_a_left_trigger1_button"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_RT1 = new Pad_InputManagerItemName(
			"pad_0_type_p_right_trigger1_button",
			"pad_0_type_x_right_trigger1_button",
			"pad_0_type_a_right_trigger1_button",
			"pad_1_type_p_right_trigger1_button",
			"pad_1_type_x_right_trigger1_button",
			"pad_1_type_a_right_trigger1_button"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_LT2 = new Pad_InputManagerItemName(
			"pad_0_type_p_left_trigger2_axis",
			"pad_0_type_x_left_trigger2_axis",
			"pad_0_type_a_left_trigger2_axis",
			"pad_1_type_p_left_trigger2_axis",
			"pad_1_type_x_left_trigger2_axis",
			"pad_1_type_a_left_trigger2_axis"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_RT2 = new Pad_InputManagerItemName(
			"pad_0_type_p_right_trigger2_axis",
			"pad_0_type_x_right_trigger2_axis",
			"pad_0_type_a_right_trigger2_axis",
			"pad_1_type_p_right_trigger2_axis",
			"pad_1_type_x_right_trigger2_axis",
			"pad_1_type_a_right_trigger2_axis"
		);

		/** インプットマネージャ。ボタン。
		*/
		public static Pad_InputManagerItemName INPUTMANAGER_LEFT = new Pad_InputManagerItemName(
			"pad_0_type_p_left",
			"pad_0_type_x_left",
			"pad_0_type_a_left",
			"pad_1_type_p_left",
			"pad_1_type_x_left",
			"pad_1_type_a_left"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_RIGHT = new Pad_InputManagerItemName(
			"pad_0_type_p_right",
			"pad_0_type_x_right",
			"pad_0_type_a_right",
			"pad_1_type_p_right",
			"pad_1_type_x_right",
			"pad_1_type_a_right"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_UP = new Pad_InputManagerItemName(
			"pad_0_type_p_up",
			"pad_0_type_x_up",
			"pad_0_type_a_up",
			"pad_1_type_p_up",
			"pad_1_type_x_up",
			"pad_1_type_a_up"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_DOWN = new Pad_InputManagerItemName(
			"pad_0_type_p_down",
			"pad_0_type_x_down",
			"pad_0_type_a_down",
			"pad_1_type_p_down",
			"pad_1_type_x_down",
			"pad_1_type_a_down"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_ENTER = new Pad_InputManagerItemName(
			"pad_0_type_p_enter",
			"pad_0_type_x_enter",
			"pad_0_type_a_enter",
			"pad_1_type_p_enter",
			"pad_1_type_x_enter",
			"pad_1_type_a_enter"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_ESCAPE = new Pad_InputManagerItemName(
			"pad_0_type_p_escape",
			"pad_0_type_x_escape",
			"pad_0_type_a_escape",
			"pad_1_type_p_escape",
			"pad_1_type_x_escape",
			"pad_1_type_a_escape"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_SUB1 = new Pad_InputManagerItemName(
			"pad_0_type_p_sub1",
			"pad_0_type_x_sub1",
			"pad_0_type_a_sub1",
			"pad_1_type_p_sub1",
			"pad_1_type_x_sub1",
			"pad_1_type_a_sub1"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_SUB2 = new Pad_InputManagerItemName(
			"pad_0_type_p_sub2",
			"pad_0_type_x_sub2",
			"pad_0_type_a_sub2",
			"pad_1_type_p_sub2",
			"pad_1_type_x_sub2",
			"pad_1_type_a_sub2"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_LMENU = new Pad_InputManagerItemName(
			"pad_0_type_p_left_menu",
			"pad_0_type_x_left_menu",
			"pad_0_type_a_left_menu",
			"pad_1_type_p_left_menu",
			"pad_1_type_x_left_menu",
			"pad_1_type_a_left_menu"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_RMENU = new Pad_InputManagerItemName(
			"pad_0_type_p_right_menu",
			"pad_0_type_x_right_menu",
			"pad_0_type_a_right_menu",
			"pad_1_type_p_right_menu",
			"pad_1_type_x_right_menu",
			"pad_1_type_a_right_menu"
		);

		/** インプットマネージャ。ティック。
		*/
		public static Pad_InputManagerItemName INPUTMANAGER_LSX = new Pad_InputManagerItemName(
			"pad_0_type_p_left_stick_axis_x",
			"pad_0_type_x_left_stick_axis_x",
			"pad_0_type_a_left_stick_axis_x",
			"pad_1_type_p_left_stick_axis_x",
			"pad_1_type_x_left_stick_axis_x",
			"pad_1_type_a_left_stick_axis_x"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_LSY = new Pad_InputManagerItemName(
			"pad_0_type_p_left_stick_axis_y",
			"pad_0_type_x_left_stick_axis_y",
			"pad_0_type_a_left_stick_axis_y",
			"pad_1_type_p_left_stick_axis_y",
			"pad_1_type_x_left_stick_axis_y",
			"pad_1_type_a_left_stick_axis_y"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_RSX = new Pad_InputManagerItemName(
			"pad_0_type_p_right_stick_axis_x",
			"pad_0_type_x_right_stick_axis_x",
			"pad_0_type_a_right_stick_axis_x",
			"pad_1_type_p_right_stick_axis_x",
			"pad_1_type_x_right_stick_axis_x",
			"pad_1_type_a_right_stick_axis_x"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_RSY = new Pad_InputManagerItemName(
			"pad_0_type_p_right_stick_axis_y",
			"pad_0_type_x_right_stick_axis_y",
			"pad_0_type_a_right_stick_axis_y",
			"pad_1_type_p_right_stick_axis_y",
			"pad_1_type_x_right_stick_axis_y",
			"pad_1_type_a_right_stick_axis_y"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_LSB = new Pad_InputManagerItemName(
			"pad_0_type_p_left_stick_button",
			"pad_0_type_x_left_stick_button",
			"pad_0_type_a_left_stick_button",
			"pad_1_type_p_left_stick_button",
			"pad_1_type_x_left_stick_button",
			"pad_1_type_a_left_stick_button"
		);
		public static Pad_InputManagerItemName INPUTMANAGER_RSB = new Pad_InputManagerItemName(
			"pad_0_type_p_right_stick_button",
			"pad_0_type_x_right_stick_button",
			"pad_0_type_a_right_stick_button",
			"pad_1_type_p_right_stick_button",
			"pad_1_type_x_right_stick_button",
			"pad_1_type_a_right_stick_button"
		);




		#if(false)

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

		#endif






	}
}

