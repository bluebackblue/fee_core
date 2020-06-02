

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。タッチ。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Touch_Phase_InputManager_InputTouch
	*/
	class Touch_Phase_InputManager_InputTouch
	{
		/** Main
		*/
		public static bool Main()
		{
			try{
				//リスト作成。
				if(Fee.Input.Input.GetInstance().touch.device_item_list.Length < UnityEngine.Input.touchCount){
					Fee.Input.Input.GetInstance().touch.device_item_list = new Touch.Touch_Device_Item[UnityEngine.Input.touchCount];
				}

				//list
				Touch.Touch_Device_Item[] t_list = Fee.Input.Input.GetInstance().touch.device_item_list;
				int t_list_count = 0;

				for(int ii=0;ii<UnityEngine.Input.touchCount;ii++){
					UnityEngine.Touch t_touch = UnityEngine.Input.GetTouch(ii);

					switch(t_touch.phase){
					case UnityEngine.TouchPhase.Began:
					case UnityEngine.TouchPhase.Moved:
					case UnityEngine.TouchPhase.Stationary:
						{
							//デバイス。
							int t_x;
							int t_y;
							{
								int t_pos_x = (int)t_touch.position.x;
								int t_pos_y = UnityEngine.Screen.height - (int)t_touch.position.y;

								//（ＧＵＩスクリーン座標）=>（仮想スクリーン座標）。
								Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen(t_pos_x,t_pos_y,out t_x,out t_y);
							}

							//位置。
							t_list[t_list_count].x = t_x;
							t_list[t_list_count].y = t_y;

							//フェーズ。
							if(t_touch.phase == UnityEngine.TouchPhase.Began){
								t_list[t_list_count].phasetype = Touch_Phase.PhaseType.Began;
							}else if(t_touch.phase == UnityEngine.TouchPhase.Moved){
								t_list[t_list_count].phasetype = Touch_Phase.PhaseType.Moved;
							}else if(t_touch.phase == UnityEngine.TouchPhase.Stationary){
								t_list[t_list_count].phasetype = Touch_Phase.PhaseType.Stationary;
							}else{
								t_list[t_list_count].phasetype = Touch_Phase.PhaseType.None;
							}

							//フラグ。
							t_list[t_list_count].link = false;

							//追加情報。
							t_list[t_list_count].touch_raw_id = Touch.INVALID_TOUCH_RAW_ID;

							t_list_count++;
						}break;
					}
				}

				Fee.Input.Input.GetInstance().touch.device_item_list_count = t_list_count;

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.touch = "Touch_Phase_InputManager_InputTouch";
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

