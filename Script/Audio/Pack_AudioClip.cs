using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。パック。
*/


/** NAudio
*/
namespace NAudio
{
	/** Pack_AudioClip
	*/
	public class Pack_AudioClip : MonoBehaviour
	{
		/** audioclip_list
		*/
		public List<AudioClip> audioclip_list;

		/** volume_list
		*/
		public List<float> volume_list;

		/** constructor
		*/
		public Pack_AudioClip()
		{
			//audioclip_list
			this.audioclip_list = new List<AudioClip>();

			//volume_list
			this.volume_list = new List<float>();
		}
	}
}

