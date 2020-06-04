

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
	/** Pad_Motor_InputSystem_GamePad
	*/
	class Pad_Motor_InputSystem_GamePad
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

				float t_value_low = Fee.Input.Input.GetInstance().pad.status[a_index].motor_low.GetValue();
				float t_value_high = Fee.Input.Input.GetInstance().pad.status[a_index].motor_high.GetValue();			
				float t_raw_value_low = Fee.Input.Input.GetInstance().pad.status[a_index].motor_low.GetRawValue();
				float t_raw_value_high = Fee.Input.Input.GetInstance().pad.status[a_index].motor_high.GetRawValue();

				{
					Fee.Input.Input.GetInstance().pad.status[a_index].motor_low.SetRawValue(t_value_low);
					Fee.Input.Input.GetInstance().pad.status[a_index].motor_high.SetRawValue(t_value_high);
					t_device.SetMotorSpeeds(t_value_low,t_value_high);
				}

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.pad_motor = "Pad_Motor_InputSystem_GamePad";
				}
				#endif

				return true;
			}
			return false;
		}
	}
}
#endif

