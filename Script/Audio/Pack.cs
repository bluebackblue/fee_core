

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief オーディオ。パック。
*/


/** Fee.Audio
*/
namespace Fee.Audio
{
	/** Pack
	*/
	public class Pack
	{
		/** audioclip_list
		*/
		public System.Collections.Generic.List<UnityEngine.AudioClip> audioclip_list;

		/** volume_list
		*/
		public System.Collections.Generic.List<float> volume_list;

		/** constructor
		*/
		public Pack()
		{
			//audioclip_list
			this.audioclip_list = new System.Collections.Generic.List<UnityEngine.AudioClip>();

			//volume_list
			this.volume_list = new System.Collections.Generic.List<float>();
		}
	}
}

