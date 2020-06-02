

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。マウス。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Mouse_MouseButton_InputManager_InputMouse
	*/
	class Mouse_MouseButton_InputManager_InputMouse
	{
		/** Main
		*/
		public static bool Main()
		{
			try{
				//デバイス。
				bool t_l_on = UnityEngine.Input.GetMouseButton(0);
				bool t_r_on = UnityEngine.Input.GetMouseButton(1);
				bool t_m_on = UnityEngine.Input.GetMouseButton(2);

				//is_focus
				bool t_is_focus = Fee.Input.Input.GetInstance().is_focus;

				//設定。
				Fee.Input.Input.GetInstance().mouse.left.Set(t_l_on & t_is_focus);
				Fee.Input.Input.GetInstance().mouse.right.Set(t_r_on & t_is_focus);
				Fee.Input.Input.GetInstance().mouse.middle.Set(t_m_on & t_is_focus);

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.mouse_button = "Mouse_MouseButton_InputManager_InputMouse";
				}
				#endif

				return true;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}			
			return false;
		}
	}
}

