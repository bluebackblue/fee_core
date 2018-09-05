using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief オーディオ。
*/


/** NAudio
*/
namespace NAudio
{
	/** ClipPack
	*/
	public class ClipPack : MonoBehaviour
	{
		/** clip_list
		*/
		public AudioClip[] clip_list;

		/** volume_list
		*/
		public float[] volume_list;

		/** リスト数。取得。
		*/
		public int GetListMax()
		{
			if(this.clip_list != null){
				return this.clip_list.Length;
			}
			return 0;
		}

		/** オーディオクリップ。取得。
		*/
		public AudioClip GetAudioClip(int a_index)
		{
			if(this.clip_list != null){
				if((0 <= a_index)&&(a_index < this.volume_list.Length)){
					return this.clip_list[a_index];
				}
			}
			return null;
		}

		/** ボリューム。取得。
		*/
		public float GetVolume(int a_index)
		{
			if(this.volume_list != null){
				if((0 <= a_index)&&(a_index < this.volume_list.Length)){
					return this.volume_list[a_index];
				}
			}
			return 1.0f;
		}
	}
}

