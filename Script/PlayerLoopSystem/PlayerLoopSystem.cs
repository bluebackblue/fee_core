

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

	/** PlayerLoopSystem
	*/
	public class PlayerLoopSystem
	{
		/** アイテム。作成。
		*/
		public static Item CreateItem()
		{
			Item t_item = null;

			try{
				UnityEngine_PlayerLoopSystem.PlayerLoopSystem t_player_loop = UnityEngine_PlayerLoopSystem.PlayerLoop.GetDefaultPlayerLoop();
				t_item = new Item(t_player_loop);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_item;
		}

		/** アイテム。適応。
		*/
		public static void ApplyItem(Item a_item)
		{
			try{
				UnityEngine_PlayerLoopSystem.PlayerLoop.SetPlayerLoop(a_item.raw);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

