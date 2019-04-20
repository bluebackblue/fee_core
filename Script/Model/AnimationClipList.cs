

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
	class AnimationClipList
	{
		/** アニメーションクリップリスト作成。ＦＢＸ。
		*/
		public static System.Collections.Generic.List<UnityEngine.AnimationClip> CreateAnimationClipListFromFbx(string[] a_path_list)
		{
			System.Collections.Generic.List<UnityEngine.AnimationClip> t_anemationclip_list = new System.Collections.Generic.List<UnityEngine.AnimationClip>();
			{
				foreach(string t_path in a_path_list){
					if(t_path.StartsWith("Assets/") == true){
						string t_path_relative = t_path.Substring(7);
						System.Collections.Generic.List<string> t_file_list = Fee.EditorTool.Utility.GetFileNameList(t_path_relative);
						foreach(string t_filename in t_file_list){
							if(System.Text.RegularExpressions.Regex.IsMatch(t_filename,"^*\\.(f|F)(b|B)|(x|X)$") == true){
								UnityEngine.Object[] t_object_list = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(t_path + t_filename);
								foreach(UnityEngine.Object t_object in t_object_list){
									UnityEngine.AnimationClip t_animationclip = t_object as UnityEngine.AnimationClip;
									if(t_animationclip != null){
										if(t_animationclip.name != "__preview__Take 001"){
											t_anemationclip_list.Add(t_animationclip);
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

