

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。アニメーションクリップ。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** AnimationClipList_MonoBehaviour
	*/
	public class AnimationClipList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** animationclip_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.AnimationClip[] animationclip_list;
	}
}

