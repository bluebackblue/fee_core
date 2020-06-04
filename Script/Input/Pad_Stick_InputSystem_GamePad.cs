

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。パッド。
*/


/** Fee.Input
*/
#if(USE_DEF_FEE_INPUTSYSTEM)
namespace Fee.Input
{
	/** Pad_Stick_InputSystem_GamePad
	*/
	class Pad_Stick_InputSystem_GamePad
	{
		/** Main
		*/
		public static bool Main(int a_index)
		{
			//is_focus
			bool t_is_focus = Fee.Input.Input.GetInstance().is_focus;

			//pad_index
			int t_pad_index = Fee.Input.Input.GetInstance().pad.status[a_index].pad_index;

			UnityEngine.InputSystem.Gamepad t_device = Fee.Input.Input.GetInstance().pad.GetInputSystemDevice(t_pad_index);
			if(t_device != null){

				//デバイス。
				float t_l_x = t_device.leftStick.x.ReadValue();
				float t_l_y = t_device.leftStick.y.ReadValue();
				float t_r_x = t_device.rightStick.x.ReadValue();
				float t_r_y = t_device.rightStick.y.ReadValue();
				bool t_l_on = t_device.leftStickButton.isPressed;
				bool t_r_on = t_device.rightStickButton.isPressed;

				//設定。
				if(t_is_focus== true){
					Fee.Input.Input.GetInstance().pad.status[a_index].l_stick.Set(t_l_x,t_l_y);
					Fee.Input.Input.GetInstance().pad.status[a_index].r_stick.Set(t_r_x,t_r_y);
				}else{
					Fee.Input.Input.GetInstance().pad.status[a_index].l_stick.Set(0.0f,0.0f);
					Fee.Input.Input.GetInstance().pad.status[a_index].r_stick.Set(0.0f,0.0f);
				}
				Fee.Input.Input.GetInstance().pad.status[a_index].l_stick_button.Set(t_l_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].r_stick_button.Set(t_r_on & t_is_focus);

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.pad_stick = "Pad_Stick_InputSystem_GamePad";
				}
				#endif

				return true;
			}

			return false;
		}
	}
}
#endif

