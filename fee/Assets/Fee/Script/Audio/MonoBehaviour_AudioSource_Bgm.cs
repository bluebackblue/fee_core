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

			//request_index
			this.request_index = -1;

			//play_index
			this.play_index = -1;

			//fadetime
			this.fadetime = 0.0f;

			//mode
			this.mode = Mode.Wait;
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

						//ボリューム。
						this.myaudiosource_volume[0] = 1.0f;
						this.UpdateVolume();

						this.play_index = this.request_index;
						this.mode = Mode.Play0;
					}
				}break;
			case Mode.Play0:
				{
					if(this.play_index != this.request_index){
						//クロスフェード開始。

						this.fadetime = 0.0f;

						//再生。
						this.myaudiosource[1].clip = this.clippack.GetAudioClip(this.request_index);
						this.myaudiosource[1].volume = 0.0f;
						this.myaudiosource[1].Play();

						//ボリューム。
						this.myaudiosource_volume[1] = 0.0f;

						this.play_index = this.request_index;
						this.mode = Mode.Cross0To1;
					}
				}break;
			case Mode.Play1:
				{
					if(this.play_index != this.request_index){
						//クロスフェード開始。

						this.fadetime = 0.0f;

						//再生。
						this.myaudiosource[0].clip = this.clippack.GetAudioClip(this.request_index);
						this.myaudiosource[0].volume = 0.0f;
						this.myaudiosource[0].Play();

						//ボリューム。
						this.myaudiosource_volume[0] = 0.0f;

						this.play_index = this.request_index;
						this.mode = Mode.Cross1To0;
					}
				}break;
			case Mode.Cross0To1:
				{
					this.fadetime += Config.BGM_CROSSFADE_SPEED;
					if(this.fadetime < 1.0f){

						//ボリューム。
						this.myaudiosource_volume[1] = this.fadetime;
						this.myaudiosource_volume[0] = 1.0f - this.fadetime;
						this.UpdateVolume();
						
					}else{

						//ボリューム。
						this.myaudiosource_volume[1] = 1.0f;
						this.myaudiosource_volume[0] = 0.0f;
						this.UpdateVolume();

						Debug.Log(this.myaudiosource[1].volume.ToString());

						//停止。
						this.myaudiosource[0].Stop();
						this.myaudiosource[0].clip = null;

						this.mode = Mode.Play1;
					}
				}break;
			case Mode.Cross1To0:
				{
					this.fadetime += Config.BGM_CROSSFADE_SPEED;
					if(this.fadetime < 1.0f){

						//ボリューム。
						this.myaudiosource_volume[0] = this.fadetime;
						this.myaudiosource_volume[1] = 1.0f - this.fadetime;
						this.UpdateVolume();
						
					}else{

						//ボリューム。
						this.myaudiosource_volume[0] = 1.0f;
						this.myaudiosource_volume[1] = 0.0f;
						this.UpdateVolume();

						Debug.Log(this.myaudiosource[0].volume.ToString());

						//停止。
						this.myaudiosource[1].Stop();
						this.myaudiosource[1].clip = null;

						this.mode = Mode.Play0;
					}
				}break;
			}
		}
	}
}

