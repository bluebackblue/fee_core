

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。アニメーションクリップ。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** AnimationClipList
	*/
	public class AnimationClipList
	{
		/** list
		*/
		public System.Collections.Generic.Dictionary<string,UnityEngine.AnimationClip> list;

		/** constructor
		*/
		public AnimationClipList(AnimationClipList_MonoBehaviour a_list)
		{
			this.list = new System.Collections.Generic.Dictionary<string,UnityEngine.AnimationClip>();
			for(int ii=0;ii<a_list.tag_list.Length;ii++){
				this.list.Add(a_list.tag_list[ii],a_list.animationclip_list[ii]);
			}
		}

		/** GetAnimationClip
		*/
		public UnityEngine.AnimationClip GetAnimationClip(string a_tag)
		{
			UnityEngine.AnimationClip t_animationclip;
			if(this.list.TryGetValue(a_tag,out t_animationclip) == true){
				return t_animationclip;
			}
			return null;
		}
	}
}

