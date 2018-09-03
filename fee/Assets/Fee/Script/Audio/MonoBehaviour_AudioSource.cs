using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。オーディオソース。
*/


/** NAudio
*/
namespace NAudio
{
	/** MonoBehaviour_AudioSource
	*/
	public class MonoBehaviour_AudioSource : MonoBehaviour
	{
		/** オーディオソース。
		*/
		private AudioSource myaudiosource;

		/** 初期化。
		*/
		public void Initialize()
		{
			this.myaudiosource = this.GetComponent<AudioSource>();
		}

		/**
		*/
		public void Delete()
		{
		}

		/** 再生。
		*/
		public void PlayOneShot(AudioClip a_audioclip)
		{
			this.myaudiosource.PlayOneShot(a_audioclip);
		}
	}
}

