

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
	/** Volume
	*/
	public class Volume
	{
		/** parent
		*/
		private Volume parent;

		/** volume
		*/
		private float volume;

		/** constructor
		*/
		public Volume(Volume a_parent,float a_volume)
		{
			//parent
			this.parent = a_parent;

			//volume
			this.volume = a_volume;
		}

		/** ボリューム。取得。
		*/
		public float GetVolume()
		{
			return this.volume;
		}

		/** ボリューム。設定。
		*/
		public void SetVolume(float a_volume)
		{
			this.volume = a_volume;
		}

		/** オーディオソースボリューム。計算。
		*/
		public float CalcAudioSourceVolume()
		{
			if(this.parent == null){
				return this.volume;
			}else{
				float t_volume = this.parent.CalcAudioSourceVolume();
				return t_volume * this.volume;
			}
		}
	}
}

