

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プレイヤーループシステム。デバッグ表示。
*/


/** Fee.PlayerLoopSystem
*/
#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
namespace Fee.PlayerLoopSystem
{
	/** DebugView_MonoBehaviour
	*/
	public class DebugView_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** list
		*/
		public System.Collections.Generic.List<string> list;
	}
}
#endif

