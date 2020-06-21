

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
	/** Touch_Phase_InputSystem_TouchScreen
	*/
	class Touch_Phase_InputSystem_TouchScreen
	{
		/** Main
		*/
		public static bool Main()
		{
			UnityEngine.InputSystem.Touchscreen t_device = UnityEngine.InputSystem.Touchscreen.current;
			//UnityEngine.InputSystem.Touchscreen t_device = UnityEngine.InputSystem.InputSystem.GetDevice<UnityEngine.InputSystem.Touchscreen>();
			if(t_device != null){

				//TODO:バグ対策。一番最後に発行されたタッチＩＤを取得する。
				int t_last_touchid = -1;
				{
					for(int ii=0;ii<t_device.touches.Count;ii++){
						int t_touch_id = t_device.touches[ii].touchId.ReadValue();
						if((t_touch_id >= t_last_touchid)||(t_last_touchid == -1)){
							t_last_touchid = t_touch_id;
						}
					}
				}

				//リスト作成。
				if(Fee.Input.Input.GetInstance().touch.device_item_list.Length < t_device.touches.Count){
					Fee.Input.Input.GetInstance().touch.device_item_list = new Touch.Touch_Device_Item[t_device.touches.Count];
				}

				//list
				Touch.Touch_Device_Item[] t_list = Fee.Input.Input.GetInstance().touch.device_item_list;
				int t_list_count = 0;

				for(int ii=0;ii<t_device.touches.Count;ii++){
					//デバイス。
					UnityEngine.InputSystem.Controls.TouchControl t_touch = t_device.touches[ii];

					int t_x;
					int t_y;
					{
						int t_pos_x = (int)t_touch.position.x.ReadValue();

						#if((UNITY_STANDALONE_WIN)||(UNITY_EDITOR_WIN)||(UNITY_WEBGL))
						int t_pos_y = (int)t_touch.position.y.ReadValue();
						#else
						int t_pos_y = (int)(UnityEngine.Screen.height - t_touch.position.y.ReadValue());
						#endif

						//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
						Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen(t_pos_x,t_pos_y,out t_x,out t_y);
					}

					UnityEngine.InputSystem.TouchPhase t_touch_phase = t_touch.phase.ReadValue();
					int t_touch_id = t_touch.touchId.ReadValue();

					{
						//位置。
						t_list[t_list_count].x = t_x;
						t_list[t_list_count].y = t_y;

						bool t_enable = false;

						//フェーズ。
						switch(t_touch_phase){
						case UnityEngine.InputSystem.TouchPhase.Began:
							{
								t_list[t_list_count].phasetype = Touch_Phase.PhaseType.Began;
								t_enable = true;
							}break;
						case UnityEngine.InputSystem.TouchPhase.Moved:
							{
								t_list[t_list_count].phasetype = Touch_Phase.PhaseType.Moved;
								t_enable = true;
							}break;
						case UnityEngine.InputSystem.TouchPhase.Stationary:
							{
								t_list[t_list_count].phasetype = Touch_Phase.PhaseType.Stationary;
								t_enable = true;
							}break;
						case UnityEngine.InputSystem.TouchPhase.Ended:
						case UnityEngine.InputSystem.TouchPhase.Canceled:
						case UnityEngine.InputSystem.TouchPhase.None:
						default:
							{
							}break;
						}

						//TODO:バグ対策。タッチＩＤが古いものは除外。
						if(t_touch_id < t_last_touchid - 100){
							t_enable = false;
						}

						if(t_enable == true){
							//フラグ。
							t_list[t_list_count].link = false;

							//追加情報。
							t_list[t_list_count].touch_raw_id = t_touch_id;

							t_list_count++;
						}
					}
				}

				Fee.Input.Input.GetInstance().touch.device_item_list_count = t_list_count;

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.touch = "Touch_Phase_InputSystem_TouchScreen";
				}
				#endif

				return true;
			}
			return false;
		}
	}
}
#endif

