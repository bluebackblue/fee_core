

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
	/** UnityEngine_InputSystem
	*/
	#if(USE_DEF_FEE_INPUTSYSTEM)
		#if((UNITY_2018_3)||(UNITY_2018_4))
			using UnityEngine_InputSystem = UnityEngine.Experimental.Input;
		#else
			using UnityEngine_InputSystem = UnityEngine.InputSystem;
		#endif
	#endif

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

		/** パッドタイプ。
		*/
		public enum PadType
		{
			Type0 = 0,
			Type1,

			Max,
		}
		private PadType padtype;

		/** is_focus
		*/
		public bool is_focus;

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
		public Digital_Button l_trigger_1;
		public Digital_Button r_trigger_1;
		public Analog_Button l_trigger_2;
		public Analog_Button r_trigger_2;

		/** モーター。
		*/
		public Moter_Speed moter_low;
		public Moter_Speed moter_high;

		/** [シングルトン]constructor
		*/
		private Pad()
		{
			//パッドタイプ。
			#if((!UNITY_EDITOR)&&(UNITY_WEBGL))
			this.padtype = PadType.Type1;
			#else
			this.padtype = PadType.Type0;
			#endif

			//is_focus
			this.is_focus = false;

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
			this.l_trigger_1.Reset();
			this.r_trigger_1.Reset();
			this.l_trigger_2.Reset();
			this.r_trigger_2.Reset();

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
				UnityEngine_InputSystem.Gamepad t_gamepad_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Gamepad>();
				if(t_gamepad_current != null){
					t_gamepad_current.SetMotorSpeeds(0.0f,0.0f);
				}
			}
			#endif
		}

		/** パッドタイプ。設定。
		*/
		public void SetPadType(PadType a_padtype)
		{
			this.padtype = a_padtype;
		}

		/** パッドタイプ。取得。
		*/
		public PadType GetPadType()
		{
			return this.padtype;
		}

		/** 更新。インプットシステム。ゲームパッド。パッドデジタルボタン。
		*/
		private bool Main_InputSystem_GamePad_PadDigitalButton()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Gamepad t_gamepad_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Gamepad>();
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
					this.left.Set(t_left_on & this.is_focus);
					this.right.Set(t_right_on & this.is_focus);
					this.up.Set(t_up_on & this.is_focus);
					this.down.Set(t_down_on & this.is_focus);
					this.enter.Set(t_enter_on & this.is_focus);
					this.escape.Set(t_escape_on & this.is_focus);
					this.sub1.Set(t_sub1_on & this.is_focus);
					this.sub2.Set(t_sub2_on & this.is_focus);
					this.left_menu.Set(t_left_menu_on & this.is_focus);
					this.right_menu.Set(t_right_menu_on & this.is_focus);

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャ。インプットネーム。パッドデジタルボタン。
		*/
		#if(true)
		private bool Main_InputManager_InputName_PadDigitalButton()
		{
			bool t_left_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LEFT[(int)this.padtype]) < -0.5f) ? true : false;
			bool t_right_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RIGHT[(int)this.padtype]) > 0.5f) ? true : false;
			bool t_up_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_UP[(int)this.padtype]) > 0.5f) ? true : false;
			bool t_down_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_DOWN[(int)this.padtype]) < -0.5f) ? true : false;

			bool t_enter_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_ENTER[(int)this.padtype]);
			bool t_escape_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_ESCAPE[(int)this.padtype]);
			bool t_sub1_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_SUB1[(int)this.padtype]);
			bool t_sub2_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_SUB2[(int)this.padtype]);
			bool t_left_menu_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_LMENU[(int)this.padtype]);
			bool t_right_menu_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_RMENU[(int)this.padtype]);

			//設定。
			this.left.Set(t_left_on & this.is_focus);
			this.right.Set(t_right_on & this.is_focus);
			this.up.Set(t_up_on & this.is_focus);
			this.down.Set(t_down_on & this.is_focus);
			this.enter.Set(t_enter_on & this.is_focus);
			this.escape.Set(t_escape_on & this.is_focus);
			this.sub1.Set(t_sub1_on & this.is_focus);
			this.sub2.Set(t_sub2_on & this.is_focus);
			this.left_menu.Set(t_left_menu_on & this.is_focus);
			this.right_menu.Set(t_right_menu_on & this.is_focus);

			return true;
		}
		#endif

		/** 更新。インプットシステム。ゲームパッド。パッドスティック。
		*/
		private bool Main_InputSystem_GamePad_PadStick()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Gamepad t_gamepad_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Gamepad>();
				if(t_gamepad_current != null){
					//デバイス。
					float t_l_x = t_gamepad_current.leftStick.x.ReadValue();
					float t_l_y = t_gamepad_current.leftStick.y.ReadValue();
					float t_r_x = t_gamepad_current.rightStick.x.ReadValue();
					float t_r_y = t_gamepad_current.rightStick.y.ReadValue();
					bool t_l_on = t_gamepad_current.leftStickButton.isPressed;
					bool t_r_on = t_gamepad_current.rightStickButton.isPressed;

					//設定。
					if(this.is_focus == true){
						this.left_stick.Set(t_l_x,t_l_y);
						this.right_stick.Set(t_r_x,t_r_y);
					}else{
						this.left_stick.Set(0.0f,0.0f);
						this.right_stick.Set(0.0f,0.0f);
					}
					this.left_stick_button.Set(t_l_on & this.is_focus);
					this.right_stick_button.Set(t_r_on & this.is_focus);
					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャ。インプットネーム。パッドスティック。
		*/
		private bool Main_InputManager_InputName_PadStick()
		{
			//デバイス。
			float t_l_x = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LSX[(int)this.padtype]);
			float t_l_y = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LSY[(int)this.padtype]);
			float t_r_x = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RSX[(int)this.padtype]);
			float t_r_y = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RSY[(int)this.padtype]);
			bool t_l_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_LSB[(int)this.padtype]);
			bool t_r_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_RSB[(int)this.padtype]);

			//設定。
			if(this.is_focus == true){
				this.left_stick.Set(t_l_x,t_l_y);
				this.right_stick.Set(t_r_x,t_r_y);
			}else{
				this.left_stick.Set(0.0f,0.0f);
				this.right_stick.Set(0.0f,0.0f);
			}
			this.left_stick_button.Set(t_l_on & this.is_focus);
			this.right_stick_button.Set(t_r_on & this.is_focus);
			return true;
		}

		/** 更新。インプットシステム。ゲームパッド。パッドトリガー。
		*/
		private bool Main_InputSystem_GamePad_PadTrigger()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Gamepad t_gamepad_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Gamepad>();
				if(t_gamepad_current != null){
					//デバイス。
					bool t_l_1 = t_gamepad_current.leftShoulder.isPressed;
					bool t_r_1 = t_gamepad_current.rightShoulder.isPressed;
					float t_l_2 = t_gamepad_current.leftTrigger.ReadValue();
					float t_r_2 = t_gamepad_current.rightTrigger.ReadValue();

					//設定。
					this.l_trigger_1.Set(t_l_1 & this.is_focus);
					this.r_trigger_1.Set(t_r_1 & this.is_focus);
					if(this.is_focus == true){
						this.l_trigger_2.Set(t_l_2);
						this.r_trigger_2.Set(t_r_2);
					}else{
						this.l_trigger_2.Set(0.0f);
						this.r_trigger_2.Set(0.0f);
					}

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャ。インプットネーム。パッドトリガー。
		*/
		private bool Main_InputManager_InputName_PadTrigger()
		{
			//デバイス。
			bool t_l_1 = UnityEngine.Input.GetButton(Config.INPUTMANAGER_LT1[(int)this.padtype]);
			bool t_r_1 = UnityEngine.Input.GetButton(Config.INPUTMANAGER_RT1[(int)this.padtype]);
			float t_l_2 = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LT2[(int)this.padtype]);
			float t_r_2 = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RT2[(int)this.padtype]);

			if(t_l_2 < 0.0f){
				t_l_2 = 0.0f;
			}

			if(t_r_2 < 0.0f){
				t_r_2 = 0.0f;
			}

			//設定。
			this.l_trigger_1.Set(t_l_1 & this.is_focus);
			this.r_trigger_1.Set(t_r_1 & this.is_focus);
			if(this.is_focus == true){
				this.l_trigger_2.Set(t_l_2);
				this.r_trigger_2.Set(t_r_2);
			}else{
				this.l_trigger_2.Set(0.0f);
				this.r_trigger_2.Set(0.0f);
			}

			return true;
		}

		/** 更新。インプットシステム。ゲームパッド。パッドモーター。
		*/
		private bool Main_InputSystem_GamePad_PadMotor()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Gamepad t_gamepad_current = UnityEngine_InputSystem.InputSystem.GetDevice<UnityEngine_InputSystem.Gamepad>();
				if(t_gamepad_current != null){
					float t_value_low = this.moter_low.GetValue();
					float t_value_high = this.moter_high.GetValue();			
					float t_raw_value_low = this.moter_low.GetRawValue();
					float t_raw_value_high = this.moter_high.GetRawValue();

					{
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
			//インプットシステム。ゲームパッド。パッドデジタルボタン。
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON == true){
				if(this.Main_InputSystem_GamePad_PadDigitalButton() == true){
					return;
				}
			}

			//インプットマネージャ。インプットネーム。パッドデジタルボタン。
			if(Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON == true){
				if(this.Main_InputManager_InputName_PadDigitalButton() == true){
					return;
				}
			}
		}

		/** 更新。スティック。
		*/
		private void Main_Stick()
		{
			//インプットシステム。ゲームパッド。パッドスティック。
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK == true){
				if(this.Main_InputSystem_GamePad_PadStick() == true){
					return;
				}
			}

			//インプットマネージャ。インプットネーム。パッドスティック。
			if(Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK == true){
				if(this.Main_InputManager_InputName_PadStick() == true){
					return;
				}
			}
		}

		/** 更新。トリガー。
		*/
		private void Main_Trigger()
		{
			//インプットシステム。ゲームパッド。パッドトリガー。
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER == true){
				if(this.Main_InputSystem_GamePad_PadTrigger() == true){
					return;
				}
			}

			//インプットマネージャ。インプットネーム。パッドトリガー。
			if(Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER == true){
				if(this.Main_InputManager_InputName_PadTrigger() == true){
					return;
				}
			}
		}

		/** 更新。モータ。
		*/
		private void Main_Moter()
		{
			//インプットシステム。ゲームパッド。パッドモーター。
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR == true){
				if(this.Main_InputSystem_GamePad_PadMotor() == true){
					return;
				}
			}
		}

		/** 更新。
		*/
		public void Main(bool a_is_focus)
		{
			//is_focus
			this.is_focus = a_is_focus;

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
					this.l_trigger_1.Main();
					this.r_trigger_1.Main();
					this.l_trigger_2.Main();
					this.r_trigger_2.Main();

					//モータ。
					this.moter_low.Main(1);
					this.moter_high.Main(1);
				}

				//モーター。更新。
				this.Main_Moter();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
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

