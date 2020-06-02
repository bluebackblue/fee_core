

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
	/** Mouse_MouseWheel_InputManager_InputMouse
	*/
	class Mouse_MouseWheel_InputManager_InputMouse
	{
		/** Main
		*/
		public static bool Main()
		{
			try{
				bool t_is_focus = Fee.Input.Input.GetInstance().is_focus;

				//デバイス。
				float t_wheel = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_MOUSEWHEEL);

				//設定。
				if(t_is_focus == true){
					if(t_wheel > 0.0f){
						Fee.Input.Input.GetInstance().mouse.mouse_wheel.Set(0,20);
					}else if(t_wheel < 0.0f){
						Fee.Input.Input.GetInstance().mouse.mouse_wheel.Set(0,-20);
					}else{
						Fee.Input.Input.GetInstance().mouse.mouse_wheel.Set(0,0);
					}
				}else{
					Fee.Input.Input.GetInstance().mouse.mouse_wheel.Set(0,0);
				}

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.mouse_wheel = "Mouse_MouseWheel_InputManager_InputMouse";
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

