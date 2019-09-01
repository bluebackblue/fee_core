

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
				UnityEngine.Experimental.LowLevel.PlayerLoopSystem t_player_loop = UnityEngine.Experimental.LowLevel.PlayerLoop.GetDefaultPlayerLoop();
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
				UnityEngine.Experimental.LowLevel.PlayerLoop.SetPlayerLoop(a_item.raw);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

