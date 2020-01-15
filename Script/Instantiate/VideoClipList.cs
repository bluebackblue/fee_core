

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。ビデオクリップリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** VideoClipList
	*/
	public class VideoClipList
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.Video.VideoClip> list;

		/** constructor
		*/
		public VideoClipList(VideoClipList_MonoBehaviour a_list)
		{
			this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.Video.VideoClip>();
			for(int ii=0;ii<a_list.tag_list.Length;ii++){
				this.list.Add(a_list.tag_list[ii],a_list.videoclip_list[ii]);
			}
		}

		/** GetVideoClip
		*/
		public UnityEngine.Video.VideoClip GetVideoClip(string a_tag)
		{
			UnityEngine.Video.VideoClip t_videoclip;
			if(this.list.TryGetValue(a_tag,out t_videoclip) == true){
				return t_videoclip;
			}
			return null;
		}
	}
}

