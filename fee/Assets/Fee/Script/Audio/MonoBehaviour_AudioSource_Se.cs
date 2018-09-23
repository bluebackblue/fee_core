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

		/** バンク。
		*/
		private Dictionary<long,Bank> bank;

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

			//bank
			this.bank = new Dictionary<long,Bank>();
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

		/** バンク。設定。
		*/
		public void SetBank(Bank a_bank,long a_id)
		{
			if(this.bank.ContainsKey(a_id) == false){
				//追加。
				this.bank.Add(a_id,a_bank);
			}else{
				//差し替え。
				this.bank[a_id] = a_bank;
			}
		}

		/** バンク。解除。
		*/
		public void UnSetBank(long a_id)
		{
			this.bank.Remove(a_id);
		}

		/** バンク。チェック。
		*/
		public bool IsExistBank(long a_id)
		{
			return this.bank.ContainsKey(a_id);
		}

		/** バンク。取得。
		*/
		public Bank GetBank(long a_id)
		{
			if(this.bank != null){
				NAudio.Bank t_bank;
				if(this.bank.TryGetValue(a_id,out t_bank) == true){
					return t_bank;
				}
			}
			return null;
		}

		/** 再生。
		*/
		public void PlayOneShot(long a_id,int a_index)
		{
			NAudio.Bank t_bank = this.GetBank(a_id);
			if(t_bank != null){

				//TODO:
				AudioClip t_audioclip = t_bank.GetAudioClip(a_index);
				if(t_audioclip != null){
					float t_volume = t_bank.GetVolume(a_index);
					if(t_volume > 0.0f){
						this.myaudiosource.PlayOneShot(t_audioclip,t_volume);
					}
				}
			}
		}
	}
}

#if(false)
{
	if(this.android_sound_pool != null){
		this.android_sound_enable = true;
		this.android_sound_soundid = this.android_sound_pool.Call<int>("load",Application.persistentDataPath + "/se_1.mp3",1);
	}else{
		this.android_sound_enable = false;
		this.android_sound_soundid = 0;
	}
	this.status.SetText(this.android_sound_enable.ToString() + " soundid = " + this.android_sound_soundid.ToString());
}
#endif


