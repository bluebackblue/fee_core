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

		/** バンクリスト。
		*/
		private Dictionary<long,Bank> bank_list;

		/** ロード。
		*/
		private List<Bank> load_worklist;

		/** アンロード。
		*/
		private List<Bank> unload_worklist;

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

			//bank_list
			this.bank_list = new Dictionary<long,Bank>();

			//load
			this.load_worklist = new List<Bank>();

			//unload
			this.unload_worklist = new List<Bank>();
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

		/** SetPack
		*/
		public void SetPack(Pack_AudioClip a_pack,long a_id)
		{
			Bank t_bank_new = new Bank(a_pack);

			Bank t_bank_old = null;
			if(this.bank_list.TryGetValue(a_id,out t_bank_old) == true){
				//差し替え。

				//アンロード。
				if(t_bank_old != null){
					this.unload_worklist.Add(t_bank_old);
				}

				//差し替え。
				this.bank_list[a_id] = t_bank_new;
			}else{
				//追加。
				this.bank_list.Add(a_id,t_bank_new);
			}

			//ロード。
			this.load_worklist.Add(t_bank_new);
		}

		/** SetPack
		*/
		public void SetPack(Pack_SoundPool a_pack,long a_id)
		{
			Bank t_bank_new = new Bank(a_pack);

			Bank t_bank_old = null;
			if(this.bank_list.TryGetValue(a_id,out t_bank_old) == true){
				//差し替え。

				//アンロード。
				if(t_bank_old != null){
					this.unload_worklist.Add(t_bank_old);
				}

				//差し替え。
				this.bank_list[a_id] = t_bank_new;
			}else{
				//追加。
				this.bank_list.Add(a_id,t_bank_new);
			}

			//ロード。
			this.load_worklist.Add(t_bank_new);
		}

		/** バンク。解除。
		*/
		public void UnSetBank(long a_id)
		{
			Bank t_bank_old = null;
			if(this.bank_list.TryGetValue(a_id,out t_bank_old) == true){
				//削除。
				this.bank_list.Remove(a_id);
			}

			//アンロード。
			if(t_bank_old != null){
				this.unload_worklist.Add(t_bank_old);
			}
		}

		/** バンク。チェック。
		*/
		public bool IsExistBank(long a_id)
		{
			return this.bank_list.ContainsKey(a_id);
		}

		/** バンク。取得。
		*/
		public Bank GetBank(long a_id)
		{
			if(this.bank_list != null){
				NAudio.Bank t_bank;
				if(this.bank_list.TryGetValue(a_id,out t_bank) == true){
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

				float t_volume = 0.0f;
				AudioClip t_audioclip = null;
				string t_name = null;

				t_bank.GetAudioClip(a_index,out t_audioclip,out t_volume);
				if(t_audioclip != null){
					this.myaudiosource.PlayOneShot(t_audioclip,this.volume_master.GetVolume() * this.volume_se.GetVolume() * t_volume);
				}else{
					t_bank.GetSoundPool(a_index,out t_name,out t_volume);
					if(t_name != null){
						NAudio.Audio.GetInstance().GetSoundPool().Play(t_name,this.volume_master.GetVolume() * this.volume_se.GetVolume() * t_volume);
					}
				}
			}
		}

		/** 更新。
		*/
		public void Update()
		{
			if(this.load_worklist.Count > 0){
				//ロード。
				if(this.load_worklist[0].LoadMain() == true){
					this.load_worklist.RemoveAt(0);
				}
			}else if(this.unload_worklist.Count > 0){
				//アンロード。
				if(this.unload_worklist[0].UnloadMain() == true){
					this.unload_worklist.RemoveAt(0);
				}
			}
		}
	}
}

