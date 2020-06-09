

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief オーディオ。ＢＧＭ。
*/


/** Fee.Audio
*/
namespace Fee.Audio
{
	/** Bgm
	*/
	public class Bgm
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

		/** ボリューム。
		*/
		private Volume volume;

		/** audiosource
		*/
		private System.Collections.Generic.List<UnityAudioSource> audiosource;

		/** ゲームオブジェクト。
		*/
		private UnityEngine.GameObject gameobject;

		/** ステータス。
		*/
		private class Status
		{
			public float time;

			/** constructor
			*/
			public Status()
			{
				this.time = 0.0f;
			}
		}
		private System.Collections.Generic.List<Status> status_list;

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

		/** constructor
		*/
		public Bgm(Volume a_volume_master)
		{
			//audiosource
			this.audiosource = new System.Collections.Generic.List<UnityAudioSource>();

			//volume
			this.volume = new Volume(a_volume_master,Config.DEFAULT_VOLUME_BGM);

			//ゲームオブジェクト。
			{
				this.gameobject = new UnityEngine.GameObject();
				this.gameobject.name = "Bgm";
				this.audiosource.Add(new UnityAudioSource(this.gameobject.AddComponent<UnityEngine.AudioSource>(),this.volume));
				this.audiosource.Add(new UnityAudioSource(this.gameobject.AddComponent<UnityEngine.AudioSource>(),this.volume));

				UnityEngine.GameObject.DontDestroyOnLoad(this.gameobject);
			}
			
			//設定。
			for(int ii=0;ii<this.audiosource.Count;ii++){
				this.audiosource[ii].SetLoopFlag(true);
			}

			//status_list
			this.status_list = new System.Collections.Generic.List<Status>();
			this.status_list.Add(new Status());
			this.status_list.Add(new Status());

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

		/** 再生インデックス。取得。
		*/
		public int GetPlayIndex()
		{
			return this.request_index;
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
		public void Main()
		{
			int t_request_index = this.request_index;

			if(this.load_work != null){
				//ロード中。

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
							float t_data_volume = 0.0f;
							if(this.bank != null){
								this.bank.GetAudioClip(t_request_index,out t_audioclip,out t_data_volume);
							}

							//再生。
							this.status_list[0].time = 0.0f;

							//設定。
							this.audiosource[0].SetDataVolume(t_data_volume);
							this.audiosource[0].SetOperationVolume(1.0f);
							this.audiosource[0].SetAudioClip(t_audioclip);

							this.audiosource[0].ApplyVolume();
							this.audiosource[0].PlayDirect();

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
							this.audiosource[t_index_next].SetOperationVolume(0.0f);
							this.audiosource[t_index_next].ApplyVolume();
						}else{
							this.audiosource[t_index_next].SetOperationVolume(1.0f);
							this.audiosource[t_index_next].ApplyVolume();
						}

						//再生。
						if(t_request_index >= 0){

							//オーディオクリップ。
							UnityEngine.AudioClip t_audioclip = null;
							float t_data_volume = 0.0f;
							if(this.bank != null){
								this.bank.GetAudioClip(t_request_index,out t_audioclip,out t_data_volume);
							}

							//設定。
							this.audiosource[t_index_next].SetDataVolume(t_data_volume);
							this.audiosource[t_index_next].SetAudioClip(t_audioclip);
							this.audiosource[t_index_next].PlayDirect();

							this.status_list[t_index_next].time = 0.0f;
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

						float t_old_time = this.status_list[t_index].time;
						this.status_list[t_index].time = this.audiosource[t_index].GetTime();
						this.playposition = this.status_list[t_index].time;

						if(t_old_time > this.status_list[t_index].time){
							Tool.Log("Bgm","loop : " + t_old_time.ToString() + " : " + this.status_list[t_index].time.ToString());
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

					this.playposition = this.audiosource[t_index_next].GetTime();

					if(Config.BGM_PLAY_FADEIN == true){
						this.fadetime += Config.BGM_CROSSFADE_SPEED;
					}else{
						this.fadetime = 1.0f;
					}

					if(this.fadetime < 1.0f){
						//ボリューム。
						this.audiosource[t_index_next].SetOperationVolume(this.fadetime);
						this.audiosource[t_index_next].ApplyVolume();
						this.audiosource[t_index].SetOperationVolume(1.0f - this.fadetime);
						this.audiosource[t_index].ApplyVolume();

						//再生位置。
						this.status_list[t_index_next].time = this.audiosource[t_index_next].GetTime();
						
					}else{
						//ボリューム。
						this.audiosource[t_index_next].SetOperationVolume(1.0f);
						this.audiosource[t_index_next].ApplyVolume();
						this.audiosource[t_index].SetOperationVolume(0.0f);
						this.audiosource[t_index].ApplyVolume();

						//停止。
						this.status_list[t_index].time = 0.0f;

						this.audiosource[t_index].Stop();
						this.audiosource[t_index].SetAudioClip(null);
						this.audiosource[t_index].SetTime(0.0f);

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

