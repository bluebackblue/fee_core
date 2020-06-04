

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
	/** Pad_Button_InputSystem_GamePad
	*/
	class Pad_Button_InputSystem_GamePad
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
				bool t_left_on = t_device.dpad.left.isPressed;
				bool t_right_on = t_device.dpad.right.isPressed;
				bool t_up_on = t_device.dpad.up.isPressed;
				bool t_down_on = t_device.dpad.down.isPressed;
				bool t_enter_on = t_device.buttonEast.isPressed;
				bool t_escape_on = t_device.buttonSouth.isPressed;
				bool t_sub1_on = t_device.buttonNorth.isPressed;
				bool t_sub2_on = t_device.buttonWest.isPressed;
				bool t_left_menu_on = t_device.selectButton.isPressed;
				bool t_right_menu_on = t_device.startButton.isPressed;

				//設定。
				Fee.Input.Input.GetInstance().pad.status[a_index].left.Set(t_left_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].right.Set(t_right_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].up.Set(t_up_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].down.Set(t_down_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].enter.Set(t_enter_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].escape.Set(t_escape_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].sub1.Set(t_sub1_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].sub2.Set(t_sub2_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].left_menu.Set(t_left_menu_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].right_menu.Set(t_right_menu_on & t_is_focus);

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.pad_button = "Pad_Button_InputSystem_GamePad";
				}
				#endif

				return true;
			}
			return false;
		}
	}
}
#endif

