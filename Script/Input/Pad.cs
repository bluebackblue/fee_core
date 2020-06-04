

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
	/** Pad
	*/
	public class Pad
	{
		/** status
		*/
		public Status_Pad[] status;

		/** inputsystem
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		private Pad_InputSystem inputsystem;
		#endif

		/** constructor
		*/
		public Pad()
		{
			//status
			this.status = new Status_Pad[Config.PAD_MAX];
			for(int ii=0;ii<this.status.Length;ii++){
				//リセット。
				this.status[ii].Reset();

				//有効。
				this.status[ii].enable = true;

				//パッドインデックス。
				this.status[ii].pad_index = ii;

				//パッドタイプ。
				this.status[ii].pad_type = Config.DEFAULT_INPUTMANAGER_PADTYPE;
			}

			//inputsystem
			#if(USE_DEF_FEE_INPUTSYSTEM)
			this.inputsystem = new Pad_InputSystem();
			#endif
		}

		/** 削除。
		*/
		public void Delete()
		{
			#if(USE_DEF_FEE_INPUTSYSTEM)
			this.inputsystem.Delete();
			#endif
		}

		/** GetInputSystemDevice
		*/
		#if(USE_DEF_FEE_INPUTSYSTEM)
		public UnityEngine.InputSystem.Gamepad GetInputSystemDevice(int a_index)
		{
			return this.inputsystem.GetDevice(a_index);
		}
		#endif

		/** 更新。デジタルボタン。
		*/
		private void Main_DigitalButton(int a_index)
		{
			//インプットシステム。ゲームパッド。パッドデジタルボタン。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADDIGITALBUTTON == true){
				if(Pad_Button_InputSystem_GamePad.Main(a_index) == true){
					return;
				}
			}
			#endif

			//インプットマネージャ。インプットネーム。パッドデジタルボタン。
			if(Config.USE_INPUTMANAGER_INPUTNAME_PADDIGITALBUTTON == true){
				if(Pad_Button_InputManager_InputName.Main(a_index) == true){
					return;
				}
			}
		}

		/** 更新。スティック。
		*/
		private void Main_Stick(int a_index)
		{
			//インプットシステム。ゲームパッド。パッドスティック。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADSTICK == true){
				if(Pad_Stick_InputSystem_GamePad.Main(a_index) == true){
					return;
				}
			}
			#endif

			//インプットマネージャ。インプットネーム。パッドスティック。
			if(Config.USE_INPUTMANAGER_INPUTNAME_PADSTICK == true){
				if(Pad_Stick_InputManager_InputName.Main(a_index) == true){
					return;
				}
			}
		}

		/** 更新。トリガー。
		*/
		private void Main_Trigger(int a_index)
		{
			//インプットシステム。ゲームパッド。パッドトリガー。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADTRIGGER == true){
				if(Pad_Trigger_InputSystem_GamePad.Main(a_index) == true){
					return;
				}
			}
			#endif

			//インプットマネージャ。インプットネーム。パッドトリガー。
			if(Config.USE_INPUTMANAGER_INPUTNAME_PADTRIGGER == true){
				if(Pad_Trigger_InputManager_InputName.Main(a_index) == true){
					return;
				}
			}
		}

		/** 更新。モータ。
		*/
		private void Main_Motor(int a_index)
		{
			//インプットシステム。ゲームパッド。パッドモーター。
			#if(USE_DEF_FEE_INPUTSYSTEM)
			if(Config.USE_INPUTSYSTEM_GAMEPAD_PADMOTOR == true){
				if(Pad_Motor_InputSystem_GamePad.Main(a_index) == true){
					return;
				}
			}
			#endif
		}

		/** 更新。
		*/
		public void Main()
		{
			/** デバイスリスト。更新。
			*/
			#if(USE_DEF_FEE_INPUTSYSTEM)
			this.inputsystem.UpdateDeviceList();
			#endif

			for(int ii=0;ii<this.status.Length;ii++){
				if(this.status[ii].enable == true){

					//デジタルボタン。更新。
					this.Main_DigitalButton(ii);

					//トリガー。更新。
					this.Main_Trigger(ii);

					//スティック。更新。
					this.Main_Stick(ii);
				}
			}

			//更新。
			for(int ii=0;ii<this.status.Length;ii++){
				if(this.status[ii].enable == true){

					//デジタルボタン。
					this.status[ii].left.Main();
					this.status[ii].right.Main();
					this.status[ii].up.Main();
					this.status[ii].down.Main();
					this.status[ii].enter.Main();
					this.status[ii].escape.Main();
					this.status[ii].sub1.Main();
					this.status[ii].sub2.Main();
					this.status[ii].left_menu.Main();
					this.status[ii].right_menu.Main();

					//アナログスティック。
					this.status[ii].l_stick.Main();
					this.status[ii].r_stick.Main();
					this.status[ii].l_stick_button.Main();
					this.status[ii].r_stick_button.Main();

					//トリガーボタン。
					this.status[ii].l_trigger_1.Main();
					this.status[ii].r_trigger_1.Main();
					this.status[ii].l_trigger_2.Main();
					this.status[ii].r_trigger_2.Main();

					//モータ。
					this.status[ii].motor_low.Main(1);
					this.status[ii].motor_high.Main(1);
				}
			}

			for(int ii=0;ii<this.status.Length;ii++){
				if(this.status[ii].enable == true){

					//モーター。更新。
					this.Main_Motor(ii);
				}
			}
		}
	}
}

