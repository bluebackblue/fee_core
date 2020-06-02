

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
	/** Pad_Trigger_InputSystem_GamePad
	*/
	class Pad_Trigger_InputSystem_GamePad
	{
		/** Main
		*/
		public static bool Main(int a_index)
		{
			//is_focus
			bool t_is_focus = Fee.Input.Input.GetInstance().is_focus;

			//pad_index
			int t_pad_index = Fee.Input.Input.GetInstance().pad.status[a_index].pad_index;

			UnityEngine.InputSystem.Gamepad t_device = Fee.Input.Input.GetInstance().pad.GetDevice(t_pad_index);
			if(t_device != null){
				//デバイス。
				bool t_l_1 = t_device.leftShoulder.isPressed;
				bool t_r_1 = t_device.rightShoulder.isPressed;
				float t_l_2 = t_device.leftTrigger.ReadValue();
				float t_r_2 = t_device.rightTrigger.ReadValue();

				//設定。
				Fee.Input.Input.GetInstance().pad.status[a_index].l_trigger_1.Set(t_l_1 & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].r_trigger_1.Set(t_r_1 & t_is_focus);
				if(t_is_focus == true){
					Fee.Input.Input.GetInstance().pad.status[a_index].l_trigger_2.Set(t_l_2);
					Fee.Input.Input.GetInstance().pad.status[a_index].r_trigger_2.Set(t_r_2);
				}else{
					Fee.Input.Input.GetInstance().pad.status[a_index].l_trigger_2.Set(0.0f);
					Fee.Input.Input.GetInstance().pad.status[a_index].r_trigger_2.Set(0.0f);
				}

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.pad_trigger = "Pad_Trigger_InputSystem_GamePad";
				}
				#endif

				return true;
			}

			return false;
		}
	}
}
#endif

