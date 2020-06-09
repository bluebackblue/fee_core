

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief オーディオ。ＳＥ。
*/


/** Fee.Audio
*/
namespace Fee.Audio
{
	/** Se
	*/
	public class Se
	{
		/** ボリューム。
		*/
		private Volume volume;

		/** audiosource
		*/
		private System.Collections.Generic.List<UnityAudioSource> audiosource;

		/** ゲームオブジェクト。
		*/
		private UnityEngine.GameObject gameobject;

		/** バンクリスト。
		*/
		private System.Collections.Generic.Dictionary<long,Bank> bank_list;

		/** ロード。
		*/
		private System.Collections.Generic.List<Bank> load_worklist;

		/** アンロード。
		*/
		private System.Collections.Generic.List<Bank> unload_worklist;

		/** 初期化。
		*/
		public Se(Volume a_volume_master)
		{
			//audiosource
			this.audiosource = new System.Collections.Generic.List<UnityAudioSource>();

			//volume
			this.volume = new Volume(a_volume_master,Config.DEFAULT_VOLUME_SE);

			//ゲームオブジェクト。
			{
				this.gameobject = new UnityEngine.GameObject();
				this.gameobject.name = "Se";
				this.audiosource.Add(new UnityAudioSource(this.gameobject.AddComponent<UnityEngine.AudioSource>(),this.volume));

				UnityEngine.GameObject.DontDestroyOnLoad(this.gameobject);
			}

			//設定。
			for(int ii=0;ii<this.audiosource.Count;ii++){
				this.audiosource[ii].SetOperationVolume(1.0f);
			}
			
			//bank_list
			this.bank_list = new System.Collections.Generic.Dictionary<long,Bank>();

			//load
			this.load_worklist = new System.Collections.Generic.List<Bank>();

			//unload
			this.unload_worklist = new System.Collections.Generic.List<Bank>();
		}

		/** 削除。
		*/
		public void Delete()
		{
			UnityEngine.GameObject.Destroy(this.gameobject);
		}

		/** ボリューム。設定。
		*/
		public void SetVolume(float a_volume)
		{
			this.volume.SetVolume(a_volume);
		}

		/** ボリューム。取得。
		*/
		public float GetVolume()
		{
			return this.volume.GetVolume();
		}

		/** ボリューム。更新。
		*/
		public void ApplyVolume()
		{
			for(int ii=0;ii<this.audiosource.Count;ii++){
				this.audiosource[ii].ApplyVolume();
			}
		}

		/** SetBank
		*/
		public void SetBank(Bank a_bank,long a_id)
		{
			if(a_bank != null){
				Bank t_bank_new = a_bank;

				Bank t_bank_old;
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
			}else{
				Tool.Assert(false);
			}
		}

		/** バンク。解除。
		*/
		public void UnSetBank(long a_id)
		{
			Bank t_bank_old;
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
			Bank t_bank;
			if(this.bank_list != null){
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
			Fee.Audio.Bank t_bank = this.GetBank(a_id);
			if(t_bank != null){

				float t_data_volume = 0.0f;
				UnityEngine.AudioClip t_audioclip = null;
				string t_name = null;

				t_bank.GetAudioClip(a_index,out t_audioclip,out t_data_volume);
				if(t_audioclip != null){
					this.audiosource[0].SetAudioClip(t_audioclip);
					this.audiosource[0].SetDataVolume(t_data_volume);
					this.audiosource[0].ApplyVolume();

					this.audiosource[0].PlayOneShot();
				}else{
					t_bank.GetSoundPool(a_index,out t_name,out t_data_volume);
					if(t_name != null){
						Fee.SoundPool.SoundPool.GetInstance().GetPlayer().Play(t_name,this.volume.CalcAudioSourceVolume() * t_data_volume);
					}
				}
			}
		}

		/** 更新。
		*/
		public void Main()
		{
			if(this.load_worklist.Count > 0){
				int t_index = this.load_worklist.Count - 1;

				//ロード。
				if(this.load_worklist[t_index].LoadMain() == true){
					this.load_worklist.RemoveAt(t_index);
				}
			}else if(this.unload_worklist.Count > 0){
				int t_index = this.unload_worklist.Count - 1;

				//アンロード。
				if(this.unload_worklist[t_index].UnloadMain() == true){
					this.unload_worklist.RemoveAt(t_index);
				}
			}
		}
	}
}

