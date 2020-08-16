

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プレイヤーループシステム。
*/


/** Fee.PlayerLoopSystem
*/
namespace Fee.PlayerLoopSystem
{
	/** UnityEngine_PlayerLoopSystem
	*/
	#if((UNITY_2018)||(UNITY_2019_2))
	using UnityEngine_PlayerLoopSystem = UnityEngine.Experimental.LowLevel;
	#else
	using UnityEngine_PlayerLoopSystem = UnityEngine.LowLevel;
	#endif

	/** CallBack
	*/
	public interface CallBack
	{
		/** [Fee.PlayerLoopSystem.CallBack]Get
		*/
		UnityEngine_PlayerLoopSystem.PlayerLoopSystem Get();

		/** [Fee.PlayerLoopSystem.CallBack]Set
		*/
		void Set(in UnityEngine_PlayerLoopSystem.PlayerLoopSystem a_playerloopsystem);
	}
}

