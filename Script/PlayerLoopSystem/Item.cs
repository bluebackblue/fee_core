

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プレイヤーループシステム。アイテム。
*/


/** Fee.PlayerLoopSystem
*/
namespace Fee.PlayerLoopSystem
{
	/** Item
	*/
	public class Item
	{
		/** raw
		*/
		public UnityEngine.Experimental.LowLevel.PlayerLoopSystem raw;

		/** constructor
		*/
		public Item(in UnityEngine.Experimental.LowLevel.PlayerLoopSystem a_raw)
		{
			this.raw = a_raw;
		}

		/** 削除。
		*/
		public void Remove(System.Collections.Generic.List<System.Type> a_ignore_list)
		{
			for(int ii=0;ii<this.raw.subSystemList.Length;ii++){

				bool t_change = false;

				for(int jj=0;jj<this.raw.subSystemList[ii].subSystemList.Length;jj++){
					if(a_ignore_list.Contains(this.raw.subSystemList[ii].subSystemList[jj].type) == true){
						t_change = true;
					}
				}

				if(t_change == true){
					System.Collections.Generic.List<UnityEngine.Experimental.LowLevel.PlayerLoopSystem> t_new_list = new System.Collections.Generic.List<UnityEngine.Experimental.LowLevel.PlayerLoopSystem>(this.raw.subSystemList[ii].subSystemList);

					t_new_list.RemoveAll(
						(UnityEngine.Experimental.LowLevel.PlayerLoopSystem a_item) => {
							return a_ignore_list.Contains(a_item.type);
						}
					);

					this.raw.subSystemList[ii].subSystemList = t_new_list.ToArray();


					//ログ。
					{
						UnityEngine.Debug.Log("---------" + this.raw.subSystemList[ii].type.Name + "---------");
						foreach(var t_item in t_new_list){
							UnityEngine.Debug.Log(t_item.type.Name);
						}
					}
				}
			}
		}
	}
}


