

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。パッド。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Pad_Trigger_InputManager_InputName
	*/
	class Pad_Trigger_InputManager_InputName
	{
		/** Main
		*/
		public static bool Main(int a_index)
		{
			try{
				//is_focus
				bool t_is_focus = Fee.Input.Input.GetInstance().is_focus;

				//pad_index
				int t_pad_index = Fee.Input.Input.GetInstance().pad.status[a_index].pad_index;

				//pad_type
				Pad_InputManagerItemName.PadType t_pad_type = Fee.Input.Input.GetInstance().pad.status[a_index].pad_type;

				//デバイス。
				bool t_l_1 = UnityEngine.Input.GetButton(Config.INPUTMANAGER_LT1.GetItem(t_pad_index,t_pad_type));
				bool t_r_1 = UnityEngine.Input.GetButton(Config.INPUTMANAGER_RT1.GetItem(t_pad_index,t_pad_type));
				float t_l_2 = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LT2.GetItem(t_pad_index,t_pad_type));
				float t_r_2 = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RT2.GetItem(t_pad_index,t_pad_type));

				if(t_l_2 < 0.0f){
					t_l_2 = 0.0f;
				}

				if(t_r_2 < 0.0f){
					t_r_2 = 0.0f;
				}

				//設定。
				Fee.Input.Input.GetInstance().pad.status[a_index].l_trigger_1.Set(t_l_1 & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].r_trigger_1.Set(t_r_1 & t_is_focus);
				if(t_is_focus == true){
					Fee.Input.Input.GetInstance().pad.status[a_index].l_trigger_2.Set(t_l_2);
					Fee.Input.Input.GetInstance().pad.status[a_index].r_trigger_2.Set(t_r_2);
				}else{
					Fee.Input.Input.GetInstance().pad.status[a_index].l_trigger_2.Set(0.0f);
					Fee.Input.Input.GetInstance().pad.status[a_index].r_trigger_2.Set(0.0f);
				}

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.pad_trigger = "Pad_Trigger_InputManager_InputName";
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

