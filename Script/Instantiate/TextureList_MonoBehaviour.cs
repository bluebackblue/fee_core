

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。テクスチャーリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** TextureList_MonoBehaviour
	*/
	public class TextureList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** texture_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.Texture2D[] texture_list;
	}
}

