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

		/** クリップパック。
		*/
		private Dictionary<long,ClipPack> clippack;

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

			//clippack
			this.clippack = new Dictionary<long,ClipPack>();
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

		/** クリップパック。設定。
		*/
		public void SetClipPack(ClipPack a_clippack,long a_id)
		{
			if(this.clippack.ContainsKey(a_id) == false){
				//追加。
				this.clippack.Add(a_id,a_clippack);
			}else{
				//差し替え。
				this.clippack[a_id] = a_clippack;
			}
		}

		/** クリップパック。解除。
		*/
		public void UnSetClipPack(long a_id)
		{
			this.clippack.Remove(a_id);
		}

		/** クリップパック。チェック。
		*/
		public bool IsExistClipPack(long a_id)
		{
			return this.clippack.ContainsKey(a_id);
		}

		/** オーディオクリップ。取得。
		*/
		public AudioClip GetAudioClip(long a_id,int a_index)
		{
			if(this.clippack != null){
				NAudio.ClipPack t_clippack;
				if(this.clippack.TryGetValue(a_id,out t_clippack) == true){
					return t_clippack.GetAudioClip(a_index);
				}
			}
			return null;
		}

		/** クリップパック。取得。
		*/
		public ClipPack GetClipPack(long a_id)
		{
			if(this.clippack != null){
				NAudio.ClipPack t_clippack;
				if(this.clippack.TryGetValue(a_id,out t_clippack) == true){
					return t_clippack;
				}
			}
			return null;
		}

		/** 再生。
		*/
		public void PlayOneShot(long a_id,int a_index)
		{
			NAudio.ClipPack t_clippack = this.GetClipPack(a_id);
			if(t_clippack != null){
				AudioClip t_audioclip = t_clippack.GetAudioClip(a_index);
				if(t_audioclip != null){
					float t_volume = t_clippack.GetVolume(a_index);
					if(t_volume > 0.0f){
						this.myaudiosource.PlayOneShot(t_audioclip,t_volume);
					}
				}
			}
		}
	}
}

