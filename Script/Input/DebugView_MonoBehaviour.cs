

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。デバッグ表示。
*/


/** Fee.Input
*/
#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
namespace Fee.Input
{
	/** DebugView_MonoBehaviour
	*/
	public class DebugView_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** key
		*/
		public string key;

		/** pad
		*/
		public string pad_button;
		public string pad_stick;
		public string pad_trigger;
		public string pad_motor;

		/** mouse
		*/
		public string mouse_button;
		public string mouse_wheel;
		public string mouse_position;

		/** touch
		*/
		public string touch;
	}
}
#endif

