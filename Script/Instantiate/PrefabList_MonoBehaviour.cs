

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
	/** PrefabList_MonoBehaviour
	*/
	public class PrefabList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** prefab_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.GameObject[] prefab_list;
	}
}

