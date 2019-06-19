

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief モデル。アニメーションクリップツール。
*/


/** Fee.Model
*/
namespace Fee.Model
{
	/** AnimationClipTool
	*/
	#if(UNITY_EDITOR)
	public class AnimationClipTool
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

		/** Item
		*/
		public struct Item
		{
			/** tag
			*/
			public string tag;

			/** filename
			*/
			public string filename;

			/** animationclip
			*/
			public UnityEngine.AnimationClip animationclip;

			/** constructor
			*/
			public Item(string a_tag,string a_filename,UnityEngine.AnimationClip a_animationclip)
			{
				//tag
				this.tag = a_tag;

				//filename
				this.filename = a_filename;

				//animationclip
				this.animationclip = a_animationclip;
			}
		};

		/** アニメーションクリップリスト作成。ＦＢＸ。
		*/
		public static System.Collections.Generic.List<Item> CreateAnimationClipListFromFbx(PathItem[] a_list)
		{
			System.Collections.Generic.List<Item> t_anemationclip_list = new System.Collections.Generic.List<Item>();
			{
				foreach(PathItem t_item in a_list){
					if(t_item.path.StartsWith("Assets/") == true){
						System.Collections.Generic.List<string> t_file_list = Fee.EditorTool.Utility.GetFileNameList(t_item.path.Substring(7));
						foreach(string t_file_item in t_file_list){
							string t_filename = t_file_item.ToLower();
							if(System.Text.RegularExpressions.Regex.IsMatch(t_filename,"^.*\\.(fbx)$") == true){
								UnityEngine.Object[] t_object_list = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(t_item.path + t_filename);
								foreach(UnityEngine.Object t_object in t_object_list){
									UnityEngine.AnimationClip t_animationclip = t_object as UnityEngine.AnimationClip;
									if(t_animationclip != null){
										if(System.Text.RegularExpressions.Regex.IsMatch(t_animationclip.name,"^.*__preview__.*$") == true){
											UnityEngine.Debug.Log("ignore : " + t_animationclip.name);
										}else{
											t_anemationclip_list.Add(new Item(t_item.tag,t_filename,t_animationclip));
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

