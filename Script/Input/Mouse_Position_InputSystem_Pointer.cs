

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
	/** Mouse_Position_InputSystem_Pointer
	*/
	class Mouse_Position_InputSystem_Pointer
	{
		/** Main
		*/
		public static bool Main()
		{
			UnityEngine.InputSystem.Pointer t_device = UnityEngine.InputSystem.InputSystem.GetDevice<UnityEngine.InputSystem.Pointer>();
			if(t_device != null){

				//デバイス。
				int t_x;
				int t_y;
				{
					int t_pos_x = (int)t_device.position.x.ReadValue();
					int t_pos_y = (int)(UnityEngine.Screen.height - t_device.position.y.ReadValue());

					//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
					Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen(t_pos_x,t_pos_y,out t_x,out t_y);
				}

				//設定。
				Fee.Input.Input.GetInstance().mouse.cursor.Set(t_x,t_y);

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.mouse_position = "Mouse_Position_InputSystem_Pointer";
				}
				#endif

				return true;
			}
			return false;
		}
	}
}
#endif

