

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
	/** Pad_Stick_InputManager_InputName
	*/
	class Pad_Stick_InputManager_InputName
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
				float t_l_x = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LSX.GetItem(t_pad_index,t_pad_type));
				float t_l_y = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LSY.GetItem(t_pad_index,t_pad_type));
				float t_r_x = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RSX.GetItem(t_pad_index,t_pad_type));
				float t_r_y = UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RSY.GetItem(t_pad_index,t_pad_type));
				bool t_l_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_LSB.GetItem(t_pad_index,t_pad_type));
				bool t_r_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_RSB.GetItem(t_pad_index,t_pad_type));

				//設定。
				if(t_is_focus == true){
					Fee.Input.Input.GetInstance().pad.status[a_index].l_stick.Set(t_l_x,t_l_y);
					Fee.Input.Input.GetInstance().pad.status[a_index].r_stick.Set(t_r_x,t_r_y);
				}else{
					Fee.Input.Input.GetInstance().pad.status[a_index].l_stick.Set(0.0f,0.0f);
					Fee.Input.Input.GetInstance().pad.status[a_index].r_stick.Set(0.0f,0.0f);
				}
				Fee.Input.Input.GetInstance().pad.status[a_index].l_stick_button.Set(t_l_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].r_stick_button.Set(t_r_on & t_is_focus);

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.pad_stick = "Pad_Stick_InputManager_InputName";
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

