

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief オーディオ。オーディオソース。
*/


/** Fee.Audio
*/
namespace Fee.Audio
{
	/** Bgm_AudioSource_MonoBehaviour
	*/
	public class Bgm_AudioSource_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** Mode
		*/
		private enum Mode
		{
			/** 停止中。
			*/
			Wait,

			/** 再生中。
			*/
			Play0,
			Play1,

			/** クロスフェード中。
			*/
			Cross0To1,
			Cross1To0,
		};

		/** ボリューム。マスター。
		*/
		private Volume volume_master;

		/** ボリューム。ＢＧＭ。
		*/
		private Volume volume_bgm;

		/** オーディオソース。
		*/
		private UnityEngine.AudioSource[] myaudiosource;
		private float[] myaudiosource_data_volume;
		private float[] myaudiosource_volume;
		private int[] myaudiosource_index;
		private float[] myaudiosource_time;

		/** バンク。
		*/
		private Bank bank;

		/** リクエストインデックス。
		*/
		private int request_index;

		/** 再生中インデックス。
		*/
		private int play_index;

		/** fadetime
		*/
		private float fadetime;

		/** mode
		*/
		private Mode mode;

		/** ループ数。
		*/
		private int loopcount;

		/** 再生位置。
		*/
		private float playposition;

		/** ロード。
		*/
		private Bank load_work;

		/** アンロード。
		*/
		private Bank unload_work;

		/** 初期化。
		*/
		public void Initialize(Volume a_volume_master,Volume a_volume_bgm)
		{
			//volume_master
			this.volume_master = a_volume_master;

			//volume_se
			this.volume_bgm = a_volume_bgm;

			//myaudiosource
			this.myaudiosource = this.GetComponents<UnityEngine.AudioSource>();
			for(int ii=0;ii<this.myaudiosource.Length;ii++){
				this.myaudiosource[ii].playOnAwake = false;
				this.myaudiosource[ii].loop = true;
				this.myaudiosource[ii].volume = this.volume_master.GetVolume() * this.volume_bgm.GetVolume();
			}

			//myaudiosource_data_volume
			this.myaudiosource_data_volume = new float[this.myaudiosource.Length];
			for(int ii=0;ii<this.myaudiosource_data_volume.Length;ii++){
				this.myaudiosource_data_volume[ii] = 0.0f;
			}

			//myaudiosource_volume
			this.myaudiosource_volume = new float[this.myaudiosource.Length];
			for(int ii=0;ii<this.myaudiosource_volume.Length;ii++){
				this.myaudiosource_volume[ii] = 0.0f;
			}

			//myaudiosource_index
			this.myaudiosource_index = new int[this.myaudiosource.Length];
			for(int ii=0;ii<this.myaudiosource_volume.Length;ii++){
				this.myaudiosource_index[ii] = -1;
			}

			//myaudiosource_time
			this.myaudiosource_time = new float[this.myaudiosource.Length];
			for(int ii=0;ii<this.myaudiosource_time.Length;ii++){
				this.myaudiosource_time[ii] = 0.0f;
			}

			//request_index
			this.request_index = -1;

			//play_index
			this.play_index = -1;

			//fadetime
			this.fadetime = 0.0f;

			//mode
			this.mode = Mode.Wait;

			//loopcount
			this.loopcount = 0;

			//再生位置。
			this.playposition = 0.0f;

			//load
			this.load_work = null;

			//unload
			this.unload_work = null;
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
			for(int ii=0;ii<this.myaudiosource_volume.Length;ii++){
				//フェード * マスター * ＢＧＭ * データ。
				this.myaudiosource[ii].volume = this.myaudiosource_volume[ii] * this.volume_master.GetVolume() * this.volume_bgm.GetVolume() * this.myaudiosource_data_volume[ii];
			}
		}

		/** バンク。設定。
		*/
		public bool SetBank(Bank a_bank)
		{
			if(this.load_work == null){
				this.load_work = a_bank;

				return true;
			}

			return false;
		}

		/** バンク。解除。
		*/
		public bool UnSetBank()
		{
			if(this.unload_work == null){
				this.unload_work = this.bank;
				this.bank = null;

				return true;
			}

			return false;
		}

		/** 再生。
		*/
		public void PlayBgm(int a_index)
		{
			this.request_index = a_index;
		}

		/** ＢＧＭ数。取得。
		*/
		public int GetBgmMax()
		{
			if(this.bank != null){
				return this.bank.GetCount();
			}
			return 0;
		}

		/** ループ数。取得。
		*/
		public int GetLoopCount()
		{
			return this.loopcount;
		}

		/** 再生位置。取得。
		*/
		public float GetPlayPosition()
		{
			return this.playposition;
		}

		/** 更新。
		*/
		public void Update()
		{
			int t_request_index = this.request_index;

			if(this.load_work != null){
				//ロード中。

				//ストリーミング再生。
				//this.load_work.LoadMain();
				//t_request_index = -1;

				this.bank = this.load_work;
				this.load_work = null;
			}

			if(this.unload_work != null){
				//アンロード中。

				t_request_index = -1;

				if(this.mode == Mode.Wait){
					if(this.unload_work.UnloadMain() == true){
						this.unload_work = null;
					}
				}
			}

			switch(this.mode){
			case Mode.Wait:
				{
					if(t_request_index >= 0){
						if(this.bank != null){

							//オーディオクリップ。
							UnityEngine.AudioClip t_audioclip = null;
							float t_volume = 0.0f;
							if(this.bank != null){
								this.bank.GetAudioClip(t_request_index,out t_audioclip,out t_volume);
							}

							//再生。
							this.myaudiosource_data_volume[0] = t_volume;
							this.myaudiosource[0].clip = t_audioclip;
							this.myaudiosource[0].Play();
							this.myaudiosource_time[0] = 0.0f;

							//ボリューム。
							this.myaudiosource_volume[0] = 1.0f;
							this.UpdateVolume();

							this.play_index = t_request_index;
							this.mode = Mode.Play0;

							this.loopcount = 0;
							this.playposition = 0.0f;

						}
					}
				}break;
			case Mode.Play0:
			case Mode.Play1:
				{
					int t_index;
					int t_index_next;
					if(this.mode == Mode.Play0){
						t_index = 0;
						t_index_next = 1;
					}else{
						t_index = 1;
						t_index_next = 0;
					}

					if(this.play_index != t_request_index){
						//クロスフェード開始。

						this.fadetime = 0.0f;

						//ボリューム。
						if(Config.BGM_PLAY_FADEIN == true){
							this.myaudiosource_volume[t_index_next] = 0.0f;
						}else{
							this.myaudiosource_volume[t_index_next] = 1.0f;
						}
						this.UpdateVolume();

						//再生。
						if(t_request_index >= 0){

							//オーディオクリップ。
							UnityEngine.AudioClip t_audioclip = null;
							float t_volume = 0.0f;
							if(this.bank != null){
								this.bank.GetAudioClip(t_request_index,out t_audioclip,out t_volume);
							}

							this.myaudiosource_data_volume[t_index_next] = t_volume;
							this.myaudiosource[t_index_next].clip = t_audioclip;
							this.myaudiosource[t_index_next].Play();
							this.myaudiosource_time[t_index_next] = 0.0f;
						}

						this.play_index = t_request_index;

						if(this.mode == Mode.Play0){
							this.mode = Mode.Cross0To1;
						}else{
							this.mode = Mode.Cross1To0;
						}

						this.loopcount = 0;
						this.playposition = 0.0f;
					}else{
						//再生中。
						float t_old = this.myaudiosource_time[t_index];
						this.myaudiosource_time[t_index] = this.myaudiosource[t_index].time;
						this.playposition = this.myaudiosource_time[t_index];

						if(t_old > this.myaudiosource_time[t_index]){
							Tool.Log("Bgm","loop : " + t_old.ToString() + " : " + this.myaudiosource_time[t_index].ToString());
							this.loopcount++;
						}
					}
				}break;
			case Mode.Cross0To1:
			case Mode.Cross1To0:
				{
					int t_index;
					int t_index_next;
					if(this.mode == Mode.Cross0To1){
						t_index = 0;
						t_index_next = 1;
					}else{
						t_index = 1;
						t_index_next = 0;
					}

					this.playposition = this.myaudiosource[t_index_next].time;

					if(Config.BGM_PLAY_FADEIN == true){
						this.fadetime += Config.BGM_CROSSFADE_SPEED;
					}else{
						this.fadetime = 1.0f;
					}

					if(this.fadetime < 1.0f){
						//ボリューム。
						this.myaudiosource_volume[t_index_next] = this.fadetime;
						this.myaudiosource_volume[t_index] = 1.0f - this.fadetime;
						this.UpdateVolume();

						//再生位置。
						this.myaudiosource_time[t_index_next] = this.myaudiosource[t_index_next].time;
						
					}else{
						//ボリューム。
						this.myaudiosource_volume[t_index_next] = 1.0f;
						this.myaudiosource_volume[t_index] = 0.0f;
						this.UpdateVolume();

						//停止。
						this.myaudiosource[t_index].Stop();
						this.myaudiosource[t_index].clip = null;
						this.myaudiosource[t_index].time = 0.0f;
						this.myaudiosource_time[t_index] = 0.0f;

						if(this.play_index < 0){
							this.mode = Mode.Wait;
						}else if(this.mode == Mode.Cross0To1){
							this.mode = Mode.Play1;
						}else{
							this.mode = Mode.Play0;
						}
					}
				}break;
			}
		}
	}
}

