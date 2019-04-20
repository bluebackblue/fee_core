

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief モデル。アニメーションクリップリスト。
*/


/** Fee.Model
*/
namespace Fee.Model
{
	/** AnimationClipList
	*/
	#if(UNITY_EDITOR)
	public class AnimationClipList
	{
		/** PathItem
		*/
		public class PathItem
		{
			/**  tag
			*/
			public string tag;

			/** path
			*/
			public string path;

			/** constructor
			*/
			public PathItem(string a_tag,string a_path)
			{
				//tag
				this.tag = a_tag;

				//path
				this.path = a_path;
			}
		}

		/** AnimationItem
		*/
		public struct AnimationItem
		{
			/** tag
			*/
			public string tag;

			/** animationclip
			*/
			public UnityEngine.AnimationClip animationclip;

			/** constructor
			*/
			public AnimationItem(string a_tag,UnityEngine.AnimationClip a_animationclip)
			{
				//tag
				this.tag = a_tag;

				//animationclip
				this.animationclip = a_animationclip;
			}
		};

		/** アニメーションクリップリスト作成。ＦＢＸ。
		*/
		public static System.Collections.Generic.List<AnimationItem> CreateAnimationClipListFromFbx(PathItem[] a_list)
		{
			System.Collections.Generic.List<AnimationItem> t_anemationclip_list = new System.Collections.Generic.List<AnimationItem>();
			{
				foreach(PathItem t_item in a_list){
					if(t_item.path.StartsWith("Assets/") == true){
						System.Collections.Generic.List<string> t_file_list = Fee.EditorTool.Utility.GetFileNameList(t_item.path.Substring(7));
						foreach(string t_filename in t_file_list){
							if(System.Text.RegularExpressions.Regex.IsMatch(t_filename,"^*\\.(f|F)(b|B)|(x|X)$") == true){
								UnityEngine.Object[] t_object_list = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(t_item.path + t_filename);
								foreach(UnityEngine.Object t_object in t_object_list){
									UnityEngine.AnimationClip t_animationclip = t_object as UnityEngine.AnimationClip;
									if(t_animationclip != null){
										if(t_animationclip.name != "__preview__Take 001"){
											t_anemationclip_list.Add(new AnimationItem(t_item.tag,t_animationclip));
										}
									}
								}
							}
						}
					}
				}
			}
			return t_anemationclip_list;
		}
	}
	#endif
}

