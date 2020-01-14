

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。オーディオボリューム。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** AudioVolumeList
	*/
	public class AudioVolumeList
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<string,float> list;

		/** constructor
		*/
		public AudioVolumeList(AudioVolumeList_MonoBehaviour a_list)
		{
			this.list = new System.Collections.Generic.Dictionary<string,float>();
			for(int ii=0;ii<a_list.tag_list.Length;ii++){
				this.list.Add(a_list.tag_list[ii],a_list.audiovolume_list[ii]);
			}
		}

		/** GetAudioVolume
		*/
		public float GetAudioVolume(string a_tag)
		{
			float t_audiovolume;
			if(this.list.TryGetValue(a_tag,out t_audiovolume) == true){
				return t_audiovolume;
			}
			return 1.0f;
		}
	}
}

