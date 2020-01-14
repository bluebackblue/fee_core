

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。マテリアルリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** MaterialList_MonoBehaviour
	*/
	public class MaterialList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** material_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.Material[] material_list;
	}
}

