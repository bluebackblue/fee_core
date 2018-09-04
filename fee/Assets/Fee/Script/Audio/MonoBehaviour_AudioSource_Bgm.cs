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

		/** クリップパック。
		*/
		private ClipPack clippack;

		/** TODO
		*/
		private float myvolume;

		/** リクエストインデックス。
		*/
		private int request_index;

		/** 再生中インデックス。
		*/
		private int play_index;

		/** time
		*/
		private int time;

		/** mode
		*/
		private Mode mode;

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
				this.myaudiosource[ii].volume = this.volume_master.GetVolume() * this.volume_bgm.GetVolume();
			}

			this.myvolume = 0.0f;

			this.request_index = -1;

			this.play_index = -1;

			this.mode = Mode.Wait;

			this.time = 0;
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
			this.myvolume = this.volume_master.GetVolume() * this.volume_bgm.GetVolume();
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

		/** 更新。
		*/
		public void Update()
		{
			switch(this.mode){
			case Mode.Wait:
				{
					if(this.request_index >= 0){
						//再生。
						this.myaudiosource[0].clip = this.clippack.clip_list[this.request_index];
						this.myaudiosource[0].Play();

						this.play_index = this.request_index;

						this.mode = Mode.Play0;
					}
				}break;
			case Mode.Play0:
				{
					if(this.play_index != this.request_index){
						//クロスフェード開始。

						this.time = 0;

						this.myaudiosource[1].clip = this.clippack.clip_list[this.request_index];
						this.myaudiosource[1].volume = 0.0f;
						this.myaudiosource[1].Play();

						this.play_index = this.request_index;
						this.mode = Mode.Cross0To1;
					}
				}break;
			case Mode.Play1:
				{
					if(this.play_index != this.request_index){
						//クロスフェード開始。

						this.time = 0;

						this.myaudiosource[0].clip = this.clippack.clip_list[this.request_index];
						this.myaudiosource[0].volume = 0.0f;
						this.myaudiosource[0].Play();

						this.play_index = this.request_index;
						this.mode = Mode.Cross1To0;
					}
				}break;
			case Mode.Cross0To1:
				{
					this.time++;

					if(this.time < 60){
						float t_per = (float)this.time / 60;

						float t_volume = this.volume_bgm.GetVolume() * this.volume_master.GetVolume();

						this.myaudiosource[1].volume = t_per * t_volume;
						this.myaudiosource[0].volume = (1.0f - t_per) * t_volume;
					}else{
						this.myaudiosource[0].Stop();
						this.myaudiosource[0].clip = null;

						this.mode = Mode.Play1;
					}
				}break;
			case Mode.Cross1To0:
				{
					this.time++;

					if(this.time < 60){
						float t_per = (float)this.time / 60;

						float t_volume = this.volume_bgm.GetVolume() * this.volume_master.GetVolume();

						this.myaudiosource[0].volume = t_per * t_volume;
						this.myaudiosource[1].volume = (1.0f - t_per) * t_volume;
					}else{
						this.myaudiosource[1].Stop();
						this.myaudiosource[1].clip = null;

						this.mode = Mode.Play0;
					}
				}break;
			}
		}
	}
}

