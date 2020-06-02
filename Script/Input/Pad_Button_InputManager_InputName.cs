

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
	/** Pad_Button_InputManager_InputName
	*/
	class Pad_Button_InputManager_InputName
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

				bool t_left_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_LEFT.GetItem(t_pad_index,t_pad_type)) < -0.5f) ? true : false;
				bool t_right_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_RIGHT.GetItem(t_pad_index,t_pad_type)) > 0.5f) ? true : false;
				bool t_up_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_UP.GetItem(t_pad_index,t_pad_type)) > 0.5f) ? true : false;
				bool t_down_on = (UnityEngine.Input.GetAxis(Config.INPUTMANAGER_DOWN.GetItem(t_pad_index,t_pad_type)) < -0.5f) ? true : false;

				bool t_enter_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_ENTER.GetItem(t_pad_index,t_pad_type));
				bool t_escape_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_ESCAPE.GetItem(t_pad_index,t_pad_type));
				bool t_sub1_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_SUB1.GetItem(t_pad_index,t_pad_type));
				bool t_sub2_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_SUB2.GetItem(t_pad_index,t_pad_type));
				bool t_left_menu_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_LMENU.GetItem(t_pad_index,t_pad_type));
				bool t_right_menu_on = UnityEngine.Input.GetButton(Config.INPUTMANAGER_RMENU.GetItem(t_pad_index,t_pad_type));

				//設定。
				Fee.Input.Input.GetInstance().pad.status[a_index].left.Set(t_left_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].right.Set(t_right_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].up.Set(t_up_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].down.Set(t_down_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].enter.Set(t_enter_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].escape.Set(t_escape_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].sub1.Set(t_sub1_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].sub2.Set(t_sub2_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].left_menu.Set(t_left_menu_on & t_is_focus);
				Fee.Input.Input.GetInstance().pad.status[a_index].right_menu.Set(t_right_menu_on & t_is_focus);

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.pad_button = "Pad_Button_InputManager_InputName";
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

