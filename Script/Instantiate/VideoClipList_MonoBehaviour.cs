

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。ビデオクリップリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** VideoClipList_MonoBehaviour
	*/
	public class VideoClipList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** videoclip_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.Video.VideoClip[] videoclip_list;
	}
}

