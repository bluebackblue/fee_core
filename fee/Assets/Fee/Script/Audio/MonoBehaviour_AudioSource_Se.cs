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
	/** MonoBehaviour_AudioSource_Se
	*/
	public class MonoBehaviour_AudioSource_Se : MonoBehaviour
	{
		/** ボリューム。マスター。
		*/
		private Volume volume_master;

		/** ボリューム。ＳＥ。
		*/
		private Volume volume_se;

		/** オーディオソース。
		*/
		private AudioSource myaudiosource;

		/** 初期化。
		*/
		public void Initialize(Volume a_volume_master,Volume a_volume_se)
		{
			//volume_master
			this.volume_master = a_volume_master;

			//volume_se
			this.volume_se = a_volume_se;

			//myaudiosource
			this.myaudiosource = this.GetComponent<AudioSource>();
			this.myaudiosource.playOnAwake = false;
			this.myaudiosource.volume = this.volume_master.GetVolume() * this.volume_se.GetVolume();
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** ボリューム更新。
		*/
		public void UpdateVolume()
		{
			this.myaudiosource.volume = this.volume_master.GetVolume() * this.volume_se.GetVolume();
		}

		/** 再生。
		*/
		public void PlayOneShot(AudioClip a_audioclip)
		{
			this.myaudiosource.PlayOneShot(a_audioclip);
		}
	}
}

