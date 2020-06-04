

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。タッチ。
*/


/** Fee.Input
*/
#if(USE_DEF_FEE_INPUTSYSTEM)
namespace Fee.Input
{
	/** Touch_Phase_InputSystem_Mouse
	*/
	class Touch_Phase_InputSystem_Mouse
	{
		/** Main
		*/
		public static bool Main()
		{
			UnityEngine.InputSystem.Mouse t_device = UnityEngine.InputSystem.Mouse.current;
			//UnityEngine.InputSystem.Mouse t_device = UnityEngine.InputSystem.InputSystem.GetDevice<UnityEngine.InputSystem.Mouse>();
			if(t_device != null){

				//リスト作成。
				if(Fee.Input.Input.GetInstance().touch.device_item_list.Length < 1){
					Fee.Input.Input.GetInstance().touch.device_item_list = new Touch.Touch_Device_Item[1];
				}

				Touch.Touch_Device_Item[] t_list = Fee.Input.Input.GetInstance().touch.device_item_list;
				int t_list_count = 0;

				if(t_device.leftButton.isPressed == true){
					//デバイス。
					int t_x;
					int t_y;
					{
						int t_pos_x = (int)t_device.position.x.ReadValue();
						int t_pos_y = (int)(UnityEngine.Screen.height - t_device.position.y.ReadValue());

						//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
						Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen(t_pos_x,t_pos_y,out t_x,out t_y);
					}

					//位置。
					t_list[t_list_count].x = t_x;
					t_list[t_list_count].y = t_y;

					//フェーズ。
					t_list[t_list_count].phasetype = Touch_Phase.PhaseType.Moved;

					//フラグ。
					t_list[t_list_count].link = false;

					//追加情報。
					t_list[t_list_count].touch_raw_id = Touch.INVALID_TOUCH_RAW_ID;

					t_list_count++;
				}

				Fee.Input.Input.GetInstance().touch.device_item_list_count = t_list_count;

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.touch = "Touch_Phase_InputSystem_Mouse";
				}
				#endif

				return true;
			}

			return false;
		}
	}
}
#endif

