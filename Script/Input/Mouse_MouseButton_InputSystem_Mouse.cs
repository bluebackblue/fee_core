

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。マウス。
*/


/** Fee.Input
*/
#if(USE_DEF_FEE_INPUTSYSTEM)
namespace Fee.Input
{
	/** Mouse_MouseButton_InputSystem_Mouse
	*/
	class Mouse_MouseButton_InputSystem_Mouse
	{
		/** Main
		*/
		public static bool Main()
		{
			UnityEngine.InputSystem.Mouse t_device = UnityEngine.InputSystem.InputSystem.GetDevice<UnityEngine.InputSystem.Mouse>();
			if(t_device != null){

				//is_focus
				bool t_is_focus = Fee.Input.Input.GetInstance().is_focus;

				//デバイス。
				bool t_l_on = t_device.leftButton.isPressed;
				bool t_r_on = t_device.rightButton.isPressed;
				bool t_m_on = t_device.middleButton.isPressed;

				//設定。
				Fee.Input.Input.GetInstance().mouse.left.Set(t_l_on & t_is_focus);
				Fee.Input.Input.GetInstance().mouse.right.Set(t_r_on & t_is_focus);
				Fee.Input.Input.GetInstance().mouse.middle.Set(t_m_on & t_is_focus);

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.mouse_button = "Mouse_MouseButton_InputSystem_Mouse";
				}
				#endif

				return true;
			}

			return false;
		}
	}
}
#endif

