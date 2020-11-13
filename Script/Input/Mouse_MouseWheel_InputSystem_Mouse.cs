

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
	/** Mouse_MouseWheel_InputSystem_Mouse
	*/
	class Mouse_MouseWheel_InputSystem_Mouse
	{
		/** Main
		*/
		public static bool Main()
		{
			UnityEngine.InputSystem.Mouse t_device = UnityEngine.InputSystem.Mouse.current;
			if(t_device != null){

				//is_focus
				bool t_is_focus = Fee.Input.Input.GetInstance().is_focus;

				//デバイス。
				int t_x = (int)t_device.scroll.ReadValue().x;
				int t_y = (int)t_device.scroll.ReadValue().y;

				//設定。
				if(t_is_focus == true){
					Fee.Input.Input.GetInstance().mouse.mouse_wheel.Set(t_x,t_y);
				}else{
					Fee.Input.Input.GetInstance().mouse.mouse_wheel.Set(0,0);
				}

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.mouse_wheel = "Mouse_MouseWheel_InputSystem_Mouse";
				}
				#endif

				return true;
			}

			return false;
		}
	}
}
#endif

