

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。キー。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Key_Button_InputManager_InputKey
	*/
	class Key_Button_InputManager_InputKey
	{
		/** Main
		*/
		public static bool Main()
		{
			foreach(System.Collections.Generic.KeyValuePair<Status_Key_Type,Status_Key_Item> t_pair in Fee.Input.Input.GetInstance().key.list){
				t_pair.Value.digital.Set(UnityEngine.Input.GetKey(t_pair.Value.inputmanager_key));
			}

			//debugview
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			{
				Fee.Input.Input.GetInstance().debugview.key = "Key_Button_InputManager_InputKey";
			}
			#endif

			return true;	
		}
	}
}

