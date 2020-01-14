

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。プレハブリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** PrefabList
	*/
	public class PrefabList
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.GameObject> list;

		/** constructor
		*/
		public PrefabList(PrefabList_MonoBehaviour a_list)
		{
			this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.GameObject>();
			for(int ii=0;ii<a_list.tag_list.Length;ii++){
				this.list.Add(a_list.tag_list[ii],a_list.prefab_list[ii]);
			}
		}

		/** GetPrefab
		*/
		public UnityEngine.GameObject GetPrefab(string a_tag)
		{
			UnityEngine.GameObject t_prefab;
			if(this.list.TryGetValue(a_tag,out t_prefab) == true){
				return t_prefab;
			}
			return null;
		}
	}
}

