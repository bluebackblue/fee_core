

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。オーディオボリューム。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** AudioVolumeList_MonoBehaviour
	*/
	public class AudioVolumeList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** audiovolume_list
		*/
		[UnityEngine.SerializeField]
		public float[] audiovolume_list;
	}
}

