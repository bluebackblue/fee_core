

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief オーディオ。
*/


/** Fee.Audio
*/
namespace Fee.Audio
{
	/** UnityAudioSource
	*/
	public class UnityAudioSource
	{
		/** raw
		*/
		public UnityEngine.AudioSource raw;

		/** volume
		*/
		public Volume volume;

		/** ボリューム。データ。
		*/
		public float volume_data;

		/** ボリューム。
		*/
		public float volume_operation;

		/** constructor
		*/
		public UnityAudioSource(UnityEngine.AudioSource a_audiosoucne,Volume a_volume)
		{
			//raw
			this.raw = a_audiosoucne;

			//volume
			this.volume = a_volume;

			//volume_data
			this.volume_data = 0.0f;

			//volume_oparation
			this.volume_operation = 1.0f;

			if(this.raw != null){
				this.raw.playOnAwake = false;
				this.raw.loop = false;
				this.raw.volume = 0.0f;
			}
		}

		/** SetLoopFlag
		*/
		public void SetLoopFlag(bool a_flag)
		{
			this.raw.loop = a_flag;
		}

		/** SetDataVolume
		*/
		public void SetDataVolume(float a_volume)
		{
			this.volume_data = a_volume;
		}

		/** SetOperationVolume
		*/
		public void SetOperationVolume(float a_volume)
		{
			this.volume_operation = a_volume;
		}

		/** ApplyVolume
		*/
		public void ApplyVolume()
		{
			this.raw.volume = this.volume.CalcAudioSourceVolume() * this.volume_data * this.volume_operation;
		}

		/** SetAudioClip
		*/
		public void SetAudioClip(UnityEngine.AudioClip a_audioclip)
		{
			this.raw.clip = a_audioclip;
		}

		/** 再生。
		*/
		public void PlayDirect()
		{
			this.raw.Play();
		}

		/** 停止。
		*/
		public void Stop()
		{
			this.raw.Stop();
		}

		/** 時間。取得。
		*/
		public float GetTime()
		{
			return this.raw.time;
		}

		/** 時間。設定。
		*/
		public void SetTime(float a_time)
		{
			this.raw.time = a_time;
		}

		/** 再生。
		*/
		public void PlayOneShot()
		{
			this.raw.PlayOneShot(this.raw.clip,1.0f);
		}
	}
}

