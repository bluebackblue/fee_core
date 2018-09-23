using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。バンク。
*/


/** NAudio
*/
namespace NAudio
{
	/** Bank
	*/
	public class Bank
	{
		/** pack_audioclip
		*/
		public Pack_AudioClip pack_audioclip;

		/** constructor
		*/
		public Bank(Pack_AudioClip a_pack)
		{
			this.pack_audioclip = a_pack;
		}

		/** ボリューム取得。
		*/
		public float GetVolume(int a_index)
		{
			if((0<=a_index)&&(a_index<this.pack_audioclip.volume_list.Length)){
				return this.pack_audioclip.volume_list[a_index];
			}

			return 1.0f;
		}

		/** 個数。取得。
		*/
		public int GetCount()
		{
			return this.pack_audioclip.clip_list.Length;
		}

		/** オーディオクリップ。取得。
		*/
		public AudioClip GetAudioClip(int a_index)
		{
			if((0<=a_index)&&(a_index<this.pack_audioclip.clip_list.Length)){
				return this.pack_audioclip.clip_list[a_index];
			}

			return null;
		}
	}
}

