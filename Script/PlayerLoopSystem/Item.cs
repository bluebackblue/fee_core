

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
	/** UnityEngine_PlayerLoopSystem
	*/
	#if((UNITY_2018)||(UNITY_2019_2))
	using UnityEngine_PlayerLoopSystem = UnityEngine.Experimental.LowLevel;
	#else
	using UnityEngine_PlayerLoopSystem = UnityEngine.LowLevel;
	#endif

	/** Item
	*/
	public class Item
	{
		/** raw
		*/
		public UnityEngine_PlayerLoopSystem.PlayerLoopSystem raw;

		/** constructor
		*/
		public Item(in UnityEngine_PlayerLoopSystem.PlayerLoopSystem a_raw)
		{
			this.raw = a_raw;
		}

		/** CreateTypeList
		*/
		public System.Collections.Generic.List<System.Type> CreateTypeList()
		{
			System.Collections.Generic.List<System.Type> t_list = new System.Collections.Generic.List<System.Type>();

			for(int ii=0;ii<this.raw.subSystemList.Length;ii++){
				for(int jj=0;jj<this.raw.subSystemList[ii].subSystemList.Length;jj++){
					t_list.Add(this.raw.subSystemList[ii].subSystemList[jj].type);
				}
			}

			return t_list;
		}

		/** 削除。
		*/
		public void RemoveFromType(System.Collections.Generic.List<System.Type> a_type_list)
		{
			for(int ii=0;ii<this.raw.subSystemList.Length;ii++){

				bool t_change = false;

				for(int jj=0;jj<this.raw.subSystemList[ii].subSystemList.Length;jj++){
					if(a_type_list.Contains(this.raw.subSystemList[ii].subSystemList[jj].type) == true){
						t_change = true;
					}
				}

				if(t_change == true){
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_new_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList[ii].subSystemList);

					t_new_list.RemoveAll(
						(UnityEngine_PlayerLoopSystem.PlayerLoopSystem a_item) => {
							if(a_type_list.Contains(a_item.type) == true){
								Tool.Log("PlayerLoopSystemRemove.",a_item.type.Name);
								return true;
							}
							return false;
						}
					);

					this.raw.subSystemList[ii].subSystemList = t_new_list.ToArray();
				}
			}
		}

		/** 削除。
		*/
		public void RemoveFromType(System.Type a_type)
		{
			for(int ii=0;ii<this.raw.subSystemList.Length;ii++){

				bool t_change = false;

				for(int jj=0;jj<this.raw.subSystemList[ii].subSystemList.Length;jj++){
					if(a_type == this.raw.subSystemList[ii].subSystemList[jj].type){
						t_change = true;
					}
				}

				if(t_change == true){
					System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem> t_new_list = new System.Collections.Generic.List<UnityEngine_PlayerLoopSystem.PlayerLoopSystem>(this.raw.subSystemList[ii].subSystemList);

					t_new_list.RemoveAll(
						(UnityEngine_PlayerLoopSystem.PlayerLoopSystem a_item) => {
							if(a_type == a_item.type){
								Tool.Log("PlayerLoopSystemRemove.",a_item.type.Name);
								return true;
							}
							return false;
						}
					);

					this.raw.subSystemList[ii].subSystemList = t_new_list.ToArray();
				}
			}
		}
	}
}

