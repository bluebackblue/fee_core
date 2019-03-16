

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。パッド。
*/


//The variable "" is assigned but its value is never used.
#pragma warning disable 0219


/** Fee.Input
*/
namespace Fee.Input
{
	/** Pad
	*/
	public class Pad
	{
		/** [シングルトン]s_instance
		*/
		private static Pad s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Pad();
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Pad GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** デジタルボタン。
		*/
		public Digital_Button left;
		public Digital_Button right;
		public Digital_Button up;
		public Digital_Button down;
		public Digital_Button enter;
		public Digital_Button escape;
		public Digital_Button sub1;
		public Digital_Button sub2;
		public Digital_Button left_menu;
		public Digital_Button right_menu;

		/** アナログスティック。
		*/
		public Analog_Stick left_stick;
		public Analog_Stick right_stick;	
		public Digital_Button left_stick_button;
		public Digital_Button right_stick_button;

		/** トリガーボタン。
		*/
		public Digital_Button left_trigger1_button;
		public Digital_Button right_trigger1_button;
		public Analog_Button left_trigger2_button;
		public Analog_Button right_trigger2_button;

		/** モーター。
		*/
		public Moter_Speed moter_low;
		public Moter_Speed moter_high;

		/** [シングルトン]constructor
		*/
		private Pad()
		{
			//デジタルボタン。
			this.left.Reset();
			this.right.Reset();
			this.up.Reset();
			this.down.Reset();
			this.enter.Reset();
			this.escape.Reset();
			this.sub1.Reset();
			this.sub2.Reset();
			this.left_menu.Reset();
			this.right_menu.Reset();

			//アナログスティック。
			this.left_stick.Reset();
			this.right_stick.Reset();
			this.left_stick_button.Reset();
			this.right_stick_button.Reset();

			//トリガーボタン。
			this.left_trigger1_button.Reset();
			this.right_trigger1_button.Reset();
			this.left_trigger2_button.Reset();
			this.right_trigger2_button.Reset();

			//モーター。
			this.moter_low.Reset();
			this.moter_high.Reset();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Gamepad t_gamepad_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Gamepad>();
				if(t_gamepad_current != null){
					t_gamepad_current.SetMotorSpeeds(0.0f,0.0f);
				}
			}
			#endif
		}

		/** 更新。インプットシステムゲームパッド。デジタルボタン。
		*/
		private bool Main_InputSystemGamePad_DigitalButton()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Gamepad t_gamepad_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Gamepad>();
				if(t_gamepad_current != null){
					//デバイス。
					bool t_left_on = t_gamepad_current.dpad.left.isPressed;
					bool t_right_on = t_gamepad_current.dpad.right.isPressed;
					bool t_up_on = t_gamepad_current.dpad.up.isPressed;
					bool t_down_on = t_gamepad_current.dpad.down.isPressed;
					bool t_enter_on = t_gamepad_current.buttonEast.isPressed;
					bool t_escape_on = t_gamepad_current.buttonSouth.isPressed;
					bool t_sub1_on = t_gamepad_current.buttonNorth.isPressed;
					bool t_sub2_on = t_gamepad_current.buttonWest.isPressed;
					bool t_left_menu_on = t_gamepad_current.selectButton.isPressed;
					bool t_right_menu_on = t_gamepad_current.startButton.isPressed;

					//設定。
					this.left.Set(t_left_on);
					this.right.Set(t_right_on);
					this.up.Set(t_up_on);
					this.down.Set(t_down_on);
					this.enter.Set(t_enter_on);
					this.escape.Set(t_escape_on);
					this.sub1.Set(t_sub1_on);
					this.sub2.Set(t_sub2_on);
					this.left_menu.Set(t_left_menu_on);
					this.right_menu.Set(t_right_menu_on);

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャゲームパッド。デジタルボタン。
		*/
		#if(true)
		private bool Main_InputManagerGamePad_DigitalButton()
		{
			bool t_left_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_LEFT) < -0.5f) ? true : false;
			bool t_right_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_RIGHT) > 0.5f) ? true : false;
			bool t_up_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_UP) > 0.5f) ? true : false;
			bool t_down_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_DOWN) < -0.5f) ? true : false;
			bool t_enter_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_ENTER);
			bool t_escape_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_ESCAPE);
			bool t_sub1_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_SUB1);
			bool t_sub2_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_SUB2);
			bool t_left_menu_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_LMENU);
			bool t_right_menu_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_RMENU);

			//設定。
			this.left.Set(t_left_on);
			this.right.Set(t_right_on);
			this.up.Set(t_up_on);
			this.down.Set(t_down_on);
			this.enter.Set(t_enter_on);
			this.escape.Set(t_escape_on);
			this.sub1.Set(t_sub1_on);
			this.sub2.Set(t_sub2_on);
			this.left_menu.Set(t_left_menu_on);
			this.right_menu.Set(t_right_menu_on);

			return true;
		}
		#endif

		/** 更新。インプットシステムゲームパッド。スティック。
		*/
		private bool Main_InputSystemGamePad_Stick()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Gamepad t_gamepad_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Gamepad>();
				if(t_gamepad_current != null){
					//デバイス。
					float t_l_x = t_gamepad_current.leftStick.x.ReadValue();
					float t_l_y = t_gamepad_current.leftStick.y.ReadValue();
					float t_r_x = t_gamepad_current.rightStick.x.ReadValue();
					float t_r_y = t_gamepad_current.rightStick.y.ReadValue();
					bool t_l_on = t_gamepad_current.leftStickButton.isPressed;
					bool t_r_on = t_gamepad_current.rightStickButton.isPressed;

					//設定。
					this.left_stick.Set(t_l_x,t_l_y);
					this.right_stick.Set(t_r_x,t_r_y);
					this.left_stick_button.Set(t_l_on);
					this.right_stick_button.Set(t_r_on);

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャゲームパッド。スティック。
		*/
		private bool Main_InputManagerGamePad_Stick()
		{
			//デバイス。
			float t_l_x = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_LSX);
			float t_l_y = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_LSY);
			float t_r_x = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_RSX);
			float t_r_y = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_RSY);
			bool t_l_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_LSB);
			bool t_r_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_RSB);

			//設定。
			this.left_stick.Set(t_l_x,t_l_y);
			this.right_stick.Set(t_r_x,t_r_y);
			this.left_stick_button.Set(t_l_on);
			this.right_stick_button.Set(t_r_on);

			return true;
		}

		/** 更新。インプットシステムゲームパッド。トリガー。
		*/
		private bool Main_InputSystemGamePad_Trigger()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Gamepad t_gamepad_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Gamepad>();
				if(t_gamepad_current != null){
					//デバイス。
					bool t_l_1 = t_gamepad_current.leftShoulder.isPressed;
					bool t_r_1 = t_gamepad_current.rightShoulder.isPressed;
					float t_l_2 = t_gamepad_current.leftTrigger.ReadValue();
					float t_r_2 = t_gamepad_current.rightTrigger.ReadValue();

					//設定。
					this.left_trigger1_button.Set(t_l_1);
					this.right_trigger1_button.Set(t_r_1);
					this.left_trigger2_button.Set(t_l_2);
					this.right_trigger2_button.Set(t_r_2);

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャゲームパッド。トリガー。
		*/
		private bool Main_InputManagerGamePad_Trigger()
		{
			//デバイス。
			bool t_l_1 = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_LT1);
			bool t_r_1 = UnityEngine.Input.GetButton(Config.INPUTMANAGER_KEYNAME_RT1);
			float t_l_2 = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_LT2);
			float t_r_2 = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_KEYNAME_RT2);

			if(t_l_2 < 0.0f){
				t_l_2 = 0.0f;
			}

			if(t_r_2 < 0.0f){
				t_r_2 = 0.0f;
			}

			//設定。
			this.left_trigger1_button.Set(t_l_1);
			this.right_trigger1_button.Set(t_r_1);
			this.left_trigger2_button.Set(t_l_2);
			this.right_trigger2_button.Set(t_r_2);

			return true;
		}

		/** 更新。インプットシステムゲームパッド。モータ。
		*/
		private bool Main_InputSystemGamePad_Motor()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine.Experimental.Input.Gamepad t_gamepad_current = UnityEngine.Experimental.Input.InputSystem.GetDevice<UnityEngine.Experimental.Input.Gamepad>();
				if(t_gamepad_current != null){
					float t_value_low = this.moter_low.GetValue();
					float t_value_high = this.moter_high.GetValue();			
					float t_raw_value_low = this.moter_low.GetRawValue();
					float t_raw_value_high = this.moter_high.GetRawValue();

					if((t_value_low != t_raw_value_low)||(t_value_high != t_raw_value_high)){
						this.moter_low.SetRawValue(t_value_low);
						this.moter_high.SetRawValue(t_value_high);
						t_gamepad_current.SetMotorSpeeds(t_value_low,t_value_high);
					}

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。デジタルボタン。
		*/
		private void Main_DigitalButton()
		{
			//インプットシステムゲームパッド。デジタルボタン。
			if(Config.USE_INPUTSYSTEM_GAMEPAD == true){
				if(this.Main_InputSystemGamePad_DigitalButton() == true){
					return;
				}
			}

			//インプットマネージャゲームパッド。デジタルボタン。
			if(Config.USE_INPUTMANAGER_GAMEPAD == true){
				if(this.Main_InputManagerGamePad_DigitalButton() == true){
					return;
				}
			}
		}

		/** 更新。スティック。
		*/
		private void Main_Stick()
		{
			//インプットシステムゲームパッド。スティック。
			if(Config.USE_INPUTSYSTEM_GAMEPAD == true){
				if(this.Main_InputSystemGamePad_Stick() == true){
					return;
				}
			}

			//インプットマネージャゲームパッド。スティック。
			if(Config.USE_INPUTMANAGER_GAMEPAD == true){
				if(this.Main_InputManagerGamePad_Stick() == true){
					return;
				}
			}
		}

		/** 更新。トリガー。
		*/
		private void Main_Trigger()
		{
			//インプットシステムゲームパッド。トリガー。
			if(Config.USE_INPUTSYSTEM_GAMEPAD == true){
				if(this.Main_InputSystemGamePad_Trigger() == true){
					return;
				}
			}

			//インプットマネージャゲームパッド。トリガー。
			if(Config.USE_INPUTMANAGER_GAMEPAD == true){
				if(this.Main_InputManagerGamePad_Trigger() == true){
					return;
				}
			}
		}

		/** 更新。モータ。
		*/
		private void Main_Moter()
		{
			//インプットシステムゲームパッド。モータ。
			if(Config.USE_INPUTSYSTEM_GAMEPAD == true){
				if(this.Main_InputSystemGamePad_Motor() == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//デジタルボタン。更新。
				this.Main_DigitalButton();

				//トリガー。更新。
				this.Main_Trigger();

				//スティック。更新。
				this.Main_Stick();

				//更新。
				{
					//デジタルボタン。
					this.left.Main();
					this.right.Main();
					this.up.Main();
					this.down.Main();
					this.enter.Main();
					this.escape.Main();
					this.sub1.Main();
					this.sub2.Main();
					this.left_menu.Main();
					this.right_menu.Main();

					//アナログスティック。
					this.left_stick.Main();
					this.right_stick.Main();
					this.left_stick_button.Main();
					this.right_stick_button.Main();

					//トリガーボタン。
					this.left_trigger1_button.Main();
					this.right_trigger1_button.Main();
					this.left_trigger2_button.Main();
					this.right_trigger2_button.Main();

					//モータ。
					this.moter_low.Main(1);
					this.moter_high.Main(1);
				}

				//モーター。更新。
				this.Main_Moter();
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}

		/** 移動チェック。ダウン時。
		*/
		public Dir4Type DownMoveCheck()
		{
			if(this.up.down == true){
				return Dir4Type.Up;
			}else if(this.down.down == true){
				return Dir4Type.Down;
			}else if(this.left.down == true){
				return Dir4Type.Left;
			}else if(this.right.down == true){
				return Dir4Type.Right;
			}

			return Dir4Type.None;
		}

		/** 移動チェック。オン時。
		*/
		public Dir4Type OnMoveCheck()
		{
			if(this.up.on == true){
				return Dir4Type.Up;
			}else if(this.down.on == true){
				return Dir4Type.Down;
			}else if(this.left.on == true){
				return Dir4Type.Left;
			}else if(this.right.on == true){
				return Dir4Type.Right;
			}

			return Dir4Type.None;
		}
	}
}

