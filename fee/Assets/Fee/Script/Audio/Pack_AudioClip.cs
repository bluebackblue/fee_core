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
		/** clip_list
		*/
		public AudioClip[] clip_list;

		/** volume_list
		*/
		public float[] volume_list;
	}
}

