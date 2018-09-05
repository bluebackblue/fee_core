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
	/** MonoBehaviour_AudioSource_Bgm
	*/
	public class MonoBehaviour_AudioSource_Bgm : MonoBehaviour
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
		private AudioSource[] myaudiosource;
		private float[] myaudiosource_volume;
		private int[] myaudiosource_index;
		private float[] myaudiosource_time;

		/** クリップパック。
		*/
		private ClipPack clippack;

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

		/** 初期化。
		*/
		public void Initialize(Volume a_volume_master,Volume a_volume_bgm)
		{
			//volume_master
			this.volume_master = a_volume_master;

			//volume_se
			this.volume_bgm = a_volume_bgm;

			//myaudiosource
			this.myaudiosource = this.GetComponents<AudioSource>();
			for(int ii=0;ii<this.myaudiosource.Length;ii++){
				this.myaudiosource[ii].playOnAwake = false;
				this.myaudiosource[ii].loop = true;
				this.myaudiosource[ii].volume = this.volume_master.GetVolume() * this.volume_bgm.GetVolume();
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
				float t_volume = 0.0f;
				if(this.clippack != null){
					t_volume = this.clippack.GetVolume(ii);
				}
				this.myaudiosource[ii].volume = this.myaudiosource_volume[ii] * this.volume_master.GetVolume() * this.volume_bgm.GetVolume() * t_volume;
			}
		}

		/** クリップパック。設定。
		*/
		public void SetClipPack(ClipPack a_clippack)
		{
			this.clippack = a_clippack;
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
			if(this.clippack != null){
				if(this.clippack.clip_list != null){
					return this.clippack.clip_list.Length;
				}
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
			switch(this.mode){
			case Mode.Wait:
				{
					if(this.request_index >= 0){
						//再生。
						this.myaudiosource[0].clip = this.clippack.GetAudioClip(this.request_index);
						this.myaudiosource[0].Play();
						this.myaudiosource_time[0] = 0.0f;

						//ボリューム。
						this.myaudiosource_volume[0] = 1.0f;
						this.UpdateVolume();

						this.play_index = this.request_index;
						this.mode = Mode.Play0;

						this.loopcount = 0;
						this.playposition = 0.0f;
					}
				}break;
			case Mode.Play0:
				{
					if(this.play_index != this.request_index){
						//クロスフェード開始。

						this.fadetime = 0.0f;

						//ボリューム。
						if(Config.BGM_PLAY_FADEIN == true){
							this.myaudiosource_volume[1] = 0.0f;
						}else{
							this.myaudiosource_volume[1] = 1.0f;
						}
						this.UpdateVolume();

						//再生。
						if(this.request_index >= 0){
							this.myaudiosource[1].clip = this.clippack.GetAudioClip(this.request_index);
							this.myaudiosource[1].Play();
							this.myaudiosource_time[1] = 0.0f;
						}

						this.play_index = this.request_index;
						this.mode = Mode.Cross0To1;

						this.loopcount = 0;
						this.playposition = 0.0f;
					}else{
						//再生中。
						float t_old = this.myaudiosource_time[0];
						this.myaudiosource_time[0] = this.myaudiosource[0].time;
						this.playposition = this.myaudiosource_time[0];

						if(t_old > this.myaudiosource_time[0]){
							Tool.Log("Bgm","loop : " + t_old.ToString() + " : " + this.myaudiosource_time[0].ToString());
							this.loopcount++;
						}
					}
				}break;
			case Mode.Play1:
				{
					if(this.play_index != this.request_index){
						//クロスフェード開始。

						this.fadetime = 0.0f;

						//ボリューム。
						if(Config.BGM_PLAY_FADEIN == true){
							this.myaudiosource_volume[0] = 0.0f;
						}else{
							this.myaudiosource_volume[0] = 1.0f;
						}
						this.UpdateVolume();

						//再生。
						if(this.request_index >= 0){
							this.myaudiosource[0].clip = this.clippack.GetAudioClip(this.request_index);
							this.myaudiosource[0].Play();
							this.myaudiosource_time[0] = 0.0f;
						}

						this.play_index = this.request_index;
						this.mode = Mode.Cross1To0;

						this.loopcount = 0;
						this.playposition = 0.0f;
					}else{
						//再生中。
						float t_old = this.myaudiosource_time[1];
						this.myaudiosource_time[1] = this.myaudiosource[1].time;
						this.playposition = this.myaudiosource_time[1];

						if(t_old > this.myaudiosource_time[1]){
							Tool.Log("Bgm","loop : " + t_old.ToString() + " : " + this.myaudiosource_time[1].ToString());
							this.loopcount++;
						}
					}
				}break;
			case Mode.Cross0To1:
				{
					this.playposition = this.myaudiosource[1].time;

					if(Config.BGM_PLAY_FADEIN == true){
						this.fadetime += Config.BGM_CROSSFADE_SPEED;
					}else{
						this.fadetime = 1.0f;
					}

					if(this.fadetime < 1.0f){
						//ボリューム。
						this.myaudiosource_volume[1] = this.fadetime;
						this.myaudiosource_volume[0] = 1.0f - this.fadetime;
						this.UpdateVolume();

						//再生位置。
						this.myaudiosource_time[1] = this.myaudiosource[1].time;
						
					}else{
						//ボリューム。
						this.myaudiosource_volume[1] = 1.0f;
						this.myaudiosource_volume[0] = 0.0f;
						this.UpdateVolume();

						//停止。
						this.myaudiosource[0].Stop();
						this.myaudiosource[0].clip = null;
						this.myaudiosource[0].time = 0.0f;
						this.myaudiosource_time[0] = 0.0f;

						if(this.play_index < 0){
							this.mode = Mode.Wait;
						}else{
							this.mode = Mode.Play1;
						}
					}
				}break;
			case Mode.Cross1To0:
				{
					this.playposition = this.myaudiosource[0].time;

					this.fadetime += Config.BGM_CROSSFADE_SPEED;

					if(Config.BGM_PLAY_FADEIN == true){
						this.fadetime += Config.BGM_CROSSFADE_SPEED;
					}else{
						this.fadetime = 1.0f;
					}

					if(this.fadetime < 1.0f){
						//ボリューム。
						this.myaudiosource_volume[0] = this.fadetime;
						this.myaudiosource_volume[1] = 1.0f - this.fadetime;
						this.UpdateVolume();

						//再生位置。
						this.myaudiosource_time[0] = this.myaudiosource[0].time;

					}else{
						//ボリューム。
						this.myaudiosource_volume[0] = 1.0f;
						this.myaudiosource_volume[1] = 0.0f;
						this.UpdateVolume();

						//停止。
						this.myaudiosource[1].Stop();
						this.myaudiosource[1].clip = null;
						this.myaudiosource[1].time = 0.0f;
						this.myaudiosource_time[1] = 0.0f;

						if(this.play_index < 0){
							this.mode = Mode.Wait;
						}else{
							this.mode = Mode.Play0;
						}
					}
				}break;
			}
		}
	}
}

