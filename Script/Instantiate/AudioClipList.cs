

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。オーディオクリップ。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** AudioClipList
	*/
	public class AudioClipList
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.AudioClip> list;

		/** constructor
		*/
		public AudioClipList(AudioClipList_MonoBehaviour a_list)
		{
			this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.AudioClip>();
			for(int ii=0;ii<a_list.tag_list.Length;ii++){
				this.list.Add(a_list.tag_list[ii],a_list.audioclip_list[ii]);
			}
		}

		/** GetAudioClip
		*/
		public UnityEngine.AudioClip GetAudioClip(string a_tag)
		{
			UnityEngine.AudioClip t_audioclip;
			if(this.list.TryGetValue(a_tag,out t_audioclip) == true){
				return t_audioclip;
			}
			return null;
		}
	}
}

