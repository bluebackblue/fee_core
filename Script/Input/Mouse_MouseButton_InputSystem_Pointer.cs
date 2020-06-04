

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
	/** Mouse_MouseButton_InputSystem_Pointer
	*/
	class Mouse_MouseButton_InputSystem_Pointer
	{
		/** Main
		*/
		public static bool Main()
		{
			UnityEngine.InputSystem.Pointer t_device = UnityEngine.InputSystem.Pointer.current;
			//UnityEngine.InputSystem.Pointer t_device = UnityEngine.InputSystem.InputSystem.GetDevice<UnityEngine.InputSystem.Pointer>();
			if(t_device != null){

				//is_focus
				bool t_is_focus = Fee.Input.Input.GetInstance().is_focus;

				//デバイス。
				bool t_l_on = t_device.press.isPressed;

				//設定。
				Fee.Input.Input.GetInstance().mouse.left.Set(t_l_on & t_is_focus);
				Fee.Input.Input.GetInstance().mouse.right.Set(false & t_is_focus);
				Fee.Input.Input.GetInstance().mouse.middle.Set(false & t_is_focus);

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.mouse_button = "Mouse_MouseButton_InputSystem_Pointer";
				}
				#endif

				return true;
			}
			return false;
		}
	}
}
#endif

