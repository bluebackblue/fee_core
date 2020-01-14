

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。オーディオクリップ。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** AudioClipList_MonoBehaviour
	*/
	public class AudioClipList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** audioclip_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.AudioClip[] audioclip_list;
	}
}

