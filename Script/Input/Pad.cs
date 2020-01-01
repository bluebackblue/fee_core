

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。パッド。
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

		/** is_focus
		*/
		public bool is_focus;

		/** paddevice_list
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		private System.Collections.Generic.List<UnityEngine_InputSystem.Gamepad> paddevice_list;
		#endif

		/** pad_status
		*/
		public Pad_Status[] pad_status;

		/** [シングルトン]constructor
		*/
		private Pad()
		{
			//is_focus
			this.is_focus = false;

			//paddevice_list
			#if(USE_DEF_FEE_INPUTSYSTEM)
			this.paddevice_list = new System.Collections.Generic.List<UnityEngine_InputSystem.Gamepad>();
			#endif

			//pad_status
			this.pad_status = new Pad_Status[Config.PAD_MAX];

			for(int ii=0;ii<this.pad_status.Length;ii++){
				//リセット。
				this.pad_status[ii].Reset();

				//devicename
				this.pad_status[ii].devicename = "";

				//有効。
				this.pad_status[ii].enable = true;

				//パッドインデックス。
				this.pad_status[ii].pad_index = ii;

				//パッドタイプ。
				#if((!UNITY_EDITOR)&&(UNITY_WEBGL))
				this.pad_status[ii].pad_type = Pad_InputManagerItemName.PadType.Type_X;
				#elif((!UNITY_EDITOR)&&(UNITY_WEBGL))
				this.pad_status[ii].pad_type = Pad_InputManagerItemName.PadType.Type_A;
				#else
				this.pad_status[ii].pad_type = Pad_InputManagerItemName.PadType.Type_P;
				#endif
			}
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				foreach(UnityEngine_InputSystem.InputDevice t_device in UnityEngine_InputSystem.InputSystem.devices){
					UnityEngine_InputSystem.Gamepad t_gamepad = t_device as UnityEngine_InputSystem.Gamepad;
					if(t_gamepad != null){
						t_gamepad.SetMotorSpeeds(0.0f,0.0f);
					}
				}
			}
			#endif
		}

		/** 更新。インプットシステム。デバイスリスト。
		*/
		private void Main_InputSystem_UpdateDeviceList()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				this.paddevice_list.Clear();

				foreach(UnityEngine_InputSystem.InputDevice t_device in UnityEngine_InputSystem.InputSystem.devices){
					UnityEngine_InputSystem.Gamepad t_gamepad = t_device as UnityEngine_InputSystem.Gamepad;
					if(t_gamepad != null){
						this.paddevice_list.Add(t_gamepad);
					}
				}

				this.paddevice_list.Sort((UnityEngine_InputSystem.Gamepad a_test,UnityEngine_InputSystem.Gamepad a_target) => {
					int t_ret = a_test.deviceId - a_target.deviceId;
					if(t_ret == 0){
						t_ret = a_test.GetHashCode() - a_target.GetHashCode();
					}
					return t_ret;
				});
			}
			#endif
		}

		/** 取得。インプットシステム。パッドデバイス。
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		private UnityEngine_InputSystem.Gamepad GetPadDevice(int a_index)
		{
			if(a_index < this.paddevice_list.Count){
				return this.paddevice_list[a_index];
			}
			return null;
		}
		#endif

		/** 更新。インプットシステム。ゲームパッド。パッドデジタルボタン。
		*/
		private bool Main_InputSystem_GamePad_PadDigitalButton(ref Pad_Status a_pad_status)
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Gamepad t_gamepad = this.GetPadDevice(a_pad_status.pad_index);
				if(t_gamepad != null){
					//デバイス。
					bool t_left_on = t_gamepad.dpad.left.isPressed;
					bool t_right_on = t_gamepad.dpad.right.isPressed;
					bool t_up_on = t_gamepad.dpad.up.isPressed;
					bool t_down_on = t_gamepad.dpad.down.isPressed;
					bool t_enter_on = t_gamepad.buttonEast.isPressed;
					bool t_escape_on = t_gamepad.buttonSouth.isPressed;
					bool t_sub1_on = t_gamepad.buttonNorth.isPressed;
					bool t_sub2_on = t_gamepad.buttonWest.isPressed;
					bool t_left_menu_on = t_gamepad.selectButton.isPressed;
					bool t_right_menu_on = t_gamepad.startButton.isPressed;

					//設定。
					a_pad_status.left.Set(t_left_on & this.is_focus);
					a_pad_status.right.Set(t_right_on & this.is_focus);
					a_pad_status.up.Set(t_up_on & this.is_focus);
					a_pad_status.down.Set(t_down_on & this.is_focus);
					a_pad_status.enter.Set(t_enter_on & this.is_focus);
					a_pad_status.escape.Set(t_escape_on & this.is_focus);
					a_pad_status.sub1.Set(t_sub1_on & this.is_focus);
					a_pad_status.sub2.Set(t_sub2_on & this.is_focus);
					a_pad_status.left_menu.Set(t_left_menu_on & this.is_focus);
					a_pad_status.right_menu.Set(t_right_menu_on & this.is_focus);

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャ。インプットネーム。パッドデジタルボタン。
		*/
		#if(true)
		private bool Main_InputManager_InputName_PadDigitalButton(ref Pad_Status a_pad_status)
		{
			bool t_left_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LEFT.GetItem(a_pad_status.pad_index,a_pad_status.pad_type)) < -0.5f) ? true : false;
			bool t_right_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RIGHT.GetItem(a_pad_status.pad_index,a_pad_status.pad_type)) > 0.5f) ? true : false;
			bool t_up_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_UP.GetItem(a_pad_status.pad_index,a_pad_status.pad_type)) > 0.5f) ? true : false;
			bool t_down_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_DOWN.GetItem(a_pad_status.pad_index,a_pad_status.pad_type)) < -0.5f) ? true : false;

			bool t_enter_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_ENTER.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			bool t_escape_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_ESCAPE.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			bool t_sub1_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_SUB1.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			bool t_sub2_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_SUB2.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			bool t_left_menu_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_LMENU.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			bool t_right_menu_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_RMENU.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));

			//設定。
			a_pad_status.left.Set(t_left_on & this.is_focus);
			a_pad_status.right.Set(t_right_on & this.is_focus);
			a_pad_status.up.Set(t_up_on & this.is_focus);
			a_pad_status.down.Set(t_down_on & this.is_focus);
			a_pad_status.enter.Set(t_enter_on & this.is_focus);
			a_pad_status.escape.Set(t_escape_on & this.is_focus);
			a_pad_status.sub1.Set(t_sub1_on & this.is_focus);
			a_pad_status.sub2.Set(t_sub2_on & this.is_focus);
			a_pad_status.left_menu.Set(t_left_menu_on & this.is_focus);
			a_pad_status.right_menu.Set(t_right_menu_on & this.is_focus);

			return true;
		}
		#endif

		/** 更新。インプットシステム。ゲームパッド。パッドスティック。
		*/
		private bool Main_InputSystem_GamePad_PadStick(ref Pad_Status a_pad_status)
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Gamepad t_gamepad = this.GetPadDevice(a_pad_status.pad_index);
				if(t_gamepad != null){
					//デバイス。
					float t_l_x = t_gamepad.leftStick.x.ReadValue();
					float t_l_y = t_gamepad.leftStick.y.ReadValue();
					float t_r_x = t_gamepad.rightStick.x.ReadValue();
					float t_r_y = t_gamepad.rightStick.y.ReadValue();
					bool t_l_on = t_gamepad.leftStickButton.isPressed;
					bool t_r_on = t_gamepad.rightStickButton.isPressed;

					//設定。
					if(this.is_focus == true){
						a_pad_status.l_stick.Set(t_l_x,t_l_y);
						a_pad_status.r_stick.Set(t_r_x,t_r_y);
					}else{
						a_pad_status.l_stick.Set(0.0f,0.0f);
						a_pad_status.r_stick.Set(0.0f,0.0f);
					}
					a_pad_status.l_stick_button.Set(t_l_on & this.is_focus);
					a_pad_status.r_stick_button.Set(t_r_on & this.is_focus);

					//devicename
					a_pad_status.devicename = t_gamepad.displayName;

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャ。インプットネーム。パッドスティック。
		*/
		private bool Main_InputManager_InputName_PadStick(ref Pad_Status a_pad_status)
		{
			//デバイス。
			float t_l_x = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LSX.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			float t_l_y = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LSY.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			float t_r_x = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RSX.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			float t_r_y = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RSY.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			bool t_l_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_LSB.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			bool t_r_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_RSB.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));

			//設定。
			if(this.is_focus == true){
				a_pad_status.l_stick.Set(t_l_x,t_l_y);
				a_pad_status.r_stick.Set(t_r_x,t_r_y);
			}else{
				a_pad_status.l_stick.Set(0.0f,0.0f);
				a_pad_status.r_stick.Set(0.0f,0.0f);
			}
			a_pad_status.l_stick_button.Set(t_l_on & this.is_focus);
			a_pad_status.r_stick_button.Set(t_r_on & this.is_focus);

			//devicename
			a_pad_status.devicename = "inputname";

			return true;
		}

		/** 更新。インプットシステム。ゲームパッド。パッドトリガー。
		*/
		private bool Main_InputSystem_GamePad_PadTrigger(ref Pad_Status a_pad_status)
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Gamepad t_gamepad = this.GetPadDevice(a_pad_status.pad_index);
				if(t_gamepad != null){
					//デバイス。
					bool t_l_1 = t_gamepad.leftShoulder.isPressed;
					bool t_r_1 = t_gamepad.rightShoulder.isPressed;
					float t_l_2 = t_gamepad.leftTrigger.ReadValue();
					float t_r_2 = t_gamepad.rightTrigger.ReadValue();

					//設定。
					a_pad_status.l_trigger_1.Set(t_l_1 & this.is_focus);
					a_pad_status.r_trigger_1.Set(t_r_1 & this.is_focus);
					if(this.is_focus == true){
						a_pad_status.l_trigger_2.Set(t_l_2);
						a_pad_status.r_trigger_2.Set(t_r_2);
					}else{
						a_pad_status.l_trigger_2.Set(0.0f);
						a_pad_status.r_trigger_2.Set(0.0f);
					}

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。インプットマネージャ。インプットネーム。パッドトリガー。
		*/
		private bool Main_InputManager_InputName_PadTrigger(ref Pad_Status a_pad_status)
		{
			//デバイス。
			bool t_l_1 = UnityEngine.Input.GetButton(Config.INPUTMANAGER_LT1.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			bool t_r_1 = UnityEngine.Input.GetButton(Config.INPUTMANAGER_RT1.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			float t_l_2 = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LT2.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));
			float t_r_2 = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RT2.GetItem(a_pad_status.pad_index,a_pad_status.pad_type));

			if(t_l_2 < 0.0f){
				t_l_2 = 0.0f;
			}

			if(t_r_2 < 0.0f){
				t_r_2 = 0.0f;
			}

			//設定。
			a_pad_status.l_trigger_1.Set(t_l_1 & this.is_focus);
			a_pad_status.r_trigger_1.Set(t_r_1 & this.is_focus);
			if(this.is_focus == true){
				a_pad_status.l_trigger_2.Set(t_l_2);
				a_pad_status.r_trigger_2.Set(t_r_2);
			}else{
				a_pad_status.l_trigger_2.Set(0.0f);
				a_pad_status.r_trigger_2.Set(0.0f);
			}

			return true;
		}

		/** 更新。インプットシステム。ゲームパッド。パッドモーター。
		*/
		private bool Main_InputSystem_GamePad_PadMotor(ref Pad_Status a_pad_status)
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			{
				UnityEngine_InputSystem.Gamepad t_gamepad = this.GetPadDevice(a_pad_status.pad_index);
				if(t_gamepad != null){
					float t_value_low = a_pad_status.moter_low.GetValue();
					float t_value_high = a_pad_status.moter_high.GetValue();			
					float t_raw_value_low = a_pad_status.moter_low.GetRawValue();
					float t_raw_value_high = a_pad_status.moter_high.GetRawValue();

					{
						a_pad_status.moter_low.SetRawValue(t_value_low);
						a_pad_status.moter_high.SetRawValue(t_value_high);
						t_gamepad.SetMotorSpeeds(t_value_low,t_value_high);
					}

					return true;
				}
			}
			#endif

			return false;
		}

		/** 更新。デジタルボタン。
		*/
		private void Main_DigitalButton(ref Pad_Status a_pad_status)
		{
			//インプットシステム。ゲームパッド。パッドデジタルボタン。
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON == true){
				if(this.Main_InputSystem_GamePad_PadDigitalButton(ref a_pad_status) == true){
					return;
				}
			}

			//インプットマネージャ。インプットネーム。パッドデジタルボタン。
			if(Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON == true){
				if(this.Main_InputManager_InputName_PadDigitalButton(ref a_pad_status) == true){
					return;
				}
			}
		}

		/** 更新。スティック。
		*/
		private void Main_Stick(ref Pad_Status a_pad_status)
		{
			//インプットシステム。ゲームパッド。パッドスティック。
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK == true){
				if(this.Main_InputSystem_GamePad_PadStick(ref a_pad_status) == true){
					return;
				}
			}

			//インプットマネージャ。インプットネーム。パッドスティック。
			if(Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK == true){
				if(this.Main_InputManager_InputName_PadStick(ref a_pad_status) == true){
					return;
				}
			}
		}

		/** 更新。トリガー。
		*/
		private void Main_Trigger(ref Pad_Status a_pad_status)
		{
			//インプットシステム。ゲームパッド。パッドトリガー。
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER == true){
				if(this.Main_InputSystem_GamePad_PadTrigger(ref a_pad_status) == true){
					return;
				}
			}

			//インプットマネージャ。インプットネーム。パッドトリガー。
			if(Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER == true){
				if(this.Main_InputManager_InputName_PadTrigger(ref a_pad_status) == true){
					return;
				}
			}
		}

		/** 更新。モータ。
		*/
		private void Main_Moter(ref Pad_Status a_pad_status)
		{
			//インプットシステム。ゲームパッド。パッドモーター。
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR == true){
				if(this.Main_InputSystem_GamePad_PadMotor(ref a_pad_status) == true){
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

				/** 更新。インプットシステム。デバイスリスト。
				*/
				this.Main_InputSystem_UpdateDeviceList();

				for(int ii=0;ii<this.pad_status.Length;ii++){
					if(this.pad_status[ii].enable == true){

						//デジタルボタン。更新。
						this.Main_DigitalButton(ref this.pad_status[ii]);

						//トリガー。更新。
						this.Main_Trigger(ref this.pad_status[ii]);

						//スティック。更新。
						this.Main_Stick(ref this.pad_status[ii]);
					}
				}

				//更新。
				for(int ii=0;ii<this.pad_status.Length;ii++){
					if(this.pad_status[ii].enable == true){

						//デジタルボタン。
						this.pad_status[ii].left.Main();
						this.pad_status[ii].right.Main();
						this.pad_status[ii].up.Main();
						this.pad_status[ii].down.Main();
						this.pad_status[ii].enter.Main();
						this.pad_status[ii].escape.Main();
						this.pad_status[ii].sub1.Main();
						this.pad_status[ii].sub2.Main();
						this.pad_status[ii].left_menu.Main();
						this.pad_status[ii].right_menu.Main();

						//アナログスティック。
						this.pad_status[ii].l_stick.Main();
						this.pad_status[ii].r_stick.Main();
						this.pad_status[ii].l_stick_button.Main();
						this.pad_status[ii].r_stick_button.Main();

						//トリガーボタン。
						this.pad_status[ii].l_trigger_1.Main();
						this.pad_status[ii].r_trigger_1.Main();
						this.pad_status[ii].l_trigger_2.Main();
						this.pad_status[ii].r_trigger_2.Main();

						//モータ。
						this.pad_status[ii].moter_low.Main(1);
						this.pad_status[ii].moter_high.Main(1);
					}
				}

				for(int ii=0;ii<this.pad_status.Length;ii++){
					if(this.pad_status[ii].enable == true){

						//モーター。更新。
						this.Main_Moter(ref this.pad_status[ii]);
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

