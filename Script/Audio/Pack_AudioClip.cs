

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。パック。
*/


/** Fee.Audio
*/
namespace Fee.Audio
{
	/** Pack_AudioClip

	アセットバンドル化するためのプレハブ。

	*/
	public class Pack_AudioClip : UnityEngine.MonoBehaviour
	{
		/** audioclip_list
		*/
		public System.Collections.Generic.List<UnityEngine.AudioClip> audioclip_list;

		/** volume_list
		*/
		public System.Collections.Generic.List<float> volume_list;

		/** constructor
		*/
		public Pack_AudioClip()
		{
			//audioclip_list
			this.audioclip_list = new System.Collections.Generic.List<UnityEngine.AudioClip>();

			//volume_list
			this.volume_list = new System.Collections.Generic.List<float>();
		}
	}
}

