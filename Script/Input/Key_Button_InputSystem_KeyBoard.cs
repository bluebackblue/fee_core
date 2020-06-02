

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。キー。
*/


/** Fee.Input
*/
#if(USE_DEF_FEE_INPUTSYSTEM)
namespace Fee.Input
{
	/** Key_Button_InputSystem_KeyBoard
	*/
	class Key_Button_InputSystem_KeyBoard
	{
		/** Main
		*/
		public static bool Main()
		{
			UnityEngine.InputSystem.Keyboard t_device = UnityEngine.InputSystem.InputSystem.GetDevice<UnityEngine.InputSystem.Keyboard>();
			if(t_device != null){
				foreach(System.Collections.Generic.KeyValuePair<Status_Key_Type,Status_Key_Item> t_pair in Fee.Input.Input.GetInstance().key.list){
					t_pair.Value.digital.Set(t_device[t_pair.Value.inputsystem_key].isPressed);
				}

				//debugview
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
				{
					Fee.Input.Input.GetInstance().debugview.key = "Key_Button_InputSystem_KeyBoard";
				}
				#endif

				return true;
			}
			return false;
		}
	}
}
#endif

