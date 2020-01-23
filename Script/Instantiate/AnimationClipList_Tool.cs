

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
	/** AnimationClipList_Tool
	*/
	#if(UNITY_EDITOR)
	public class AnimationClipList_Tool
	{
		/** ResourceItem_Directory
		*/
		public class ResourceItem_Directory
		{
			/** prefix
			*/
			public string prefix;

			/** path

				"Assets/xxx/yyy/zzz/"

			*/
			public Fee.File.Path path;

			/** constructor
			*/
			public ResourceItem_Directory(string a_prefix,Fee.File.Path a_assets_path)
			{
				//prefix
				this.prefix = a_prefix;

				//path
				this.path = a_assets_path;
			}
		}

		/** ResourceItem
		*/
		public class ResourceItem
		{
			/** tag
			*/
			public string tag;

			/** path

				"Assets/xxx/yyy/zzz.fbx"

			*/
			public Fee.File.Path path;

			/** clipanimation_name
			*/
			public string clipanimation_name;

			/** constructor
			*/
			public ResourceItem(string a_tag,Fee.File.Path a_assets_path,string a_clipanimation_name)
			{
				//tag
				this.tag = a_tag;

				//path
				this.path = a_assets_path;

				//clipanimation_name
				this.clipanimation_name = a_clipanimation_name;
			}
		}

		/** FindItem
		*/
		private class FindItem
		{
			/** path
			*/
			public Fee.File.Path path;

			/** filename
			*/
			public UnityEngine.AnimationClip animationclip;

			/** constructor
			*/
			public FindItem(Fee.File.Path a_path,UnityEngine.AnimationClip a_animationclip)
			{
				//path
				this.path = a_path;

				//animationclip
				this.animationclip = a_animationclip;
			}
		}

		/** 作成。

			全部追加する。

		*/
		public static UnityEngine.GameObject Create(Fee.File.Path a_output_assets_path,ResourceItem_Directory[] a_resource_list)
		{
			UnityEngine.GameObject t_prefab = new UnityEngine.GameObject();
			t_prefab.name = "prefab_temp";
			try{
				//追加。
				Add(t_prefab,a_resource_list);

				//新規作成。
				Fee.EditorTool.Utility.SavePrefab(t_prefab,a_output_assets_path);
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
			UnityEngine.GameObject.DestroyImmediate(t_prefab);

			//ロード。
			UnityEngine.GameObject t_gameobject = Fee.EditorTool.Utility.LoadAsset<UnityEngine.GameObject>(a_output_assets_path);
			Tool.Assert(t_gameobject != null);
			return t_gameobject;
		}

		/** 作成。

			指定したものを追加する。

		*/
		public static UnityEngine.GameObject Create(Fee.File.Path a_output_assets_path,ResourceItem[] a_resource_list)
		{
			UnityEngine.GameObject t_prefab = new UnityEngine.GameObject();
			t_prefab.name = "prefab_temp";
			{
				//追加。
				Add(t_prefab,a_resource_list);

				//新規作成。
				Fee.EditorTool.Utility.SavePrefab(t_prefab,a_output_assets_path);
			}
			UnityEngine.GameObject.DestroyImmediate(t_prefab);

			//ロード。
			UnityEngine.GameObject t_gameobject = Fee.EditorTool.Utility.LoadAsset<UnityEngine.GameObject>(a_output_assets_path);
			Tool.Assert(t_gameobject != null);
			return t_gameobject;
		}

		/** 追加。

			全部追加する。

		*/
		public static UnityEngine.GameObject Add(UnityEngine.GameObject a_prefab,ResourceItem_Directory[] a_resource_list)
		{
			try{
				//animationclip_list
				AnimationClipList_MonoBehaviour t_animationclip_list = a_prefab.AddComponent<AnimationClipList_MonoBehaviour>();

				System.Collections.Generic.List<System.Tuple<string,FindItem>> t_list = new System.Collections.Generic.List<System.Tuple<string,FindItem>>();

				foreach(ResourceItem_Directory t_resouce_item in a_resource_list){
					System.Collections.Generic.List<FindItem> t_list_find = new System.Collections.Generic.List<FindItem>();
					AnimationClipList_Tool.FindAnimationClip_Directory(t_resouce_item.path,t_list_find);
					foreach(FindItem t_finditem in t_list_find){
						//タグの自動生成。
						string t_tag = t_resouce_item.prefix + ":" + t_finditem.path.GetFileName() + ":" + t_finditem.animationclip.name;
						t_list.Add(new System.Tuple<string,FindItem>(t_tag,t_finditem));
					}
				}

				t_animationclip_list.tag_list = new string[t_list.Count];
				t_animationclip_list.animationclip_list = new UnityEngine.AnimationClip[t_list.Count];
				for(int ii=0;ii<t_animationclip_list.animationclip_list.Length;ii++){
					t_animationclip_list.tag_list[ii] = t_list[ii].Item1;
					t_animationclip_list.animationclip_list[ii] = t_list[ii].Item2.animationclip;
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return a_prefab;
		}

		/** 追加。

			指定したものを追加する。

		*/
		public static UnityEngine.GameObject Add(UnityEngine.GameObject a_prefab,ResourceItem[] a_resource_list)
		{
			try{
				//animationclip_list
				AnimationClipList_MonoBehaviour t_animationclip_list = a_prefab.AddComponent<AnimationClipList_MonoBehaviour>();

				System.Collections.Generic.List<System.Tuple<string,FindItem>> t_list = new System.Collections.Generic.List<System.Tuple<string,FindItem>>();

				foreach(ResourceItem t_resouce_item in a_resource_list){
					System.Collections.Generic.List<FindItem> t_list_find = new System.Collections.Generic.List<FindItem>();
					AnimationClipList_Tool.FindAnimationClip(t_resouce_item.path,t_list_find);
					foreach(FindItem t_finditem in t_list_find){
						if(t_finditem.animationclip.name == t_resouce_item.clipanimation_name){
							t_list.Add(new System.Tuple<string,FindItem>(t_resouce_item.tag,t_finditem));
							break;
						}					
					}
				}

				t_animationclip_list.tag_list = new string[t_list.Count];
				t_animationclip_list.animationclip_list = new UnityEngine.AnimationClip[t_list.Count];
				for(int ii=0;ii<t_list.Count;ii++){
					t_animationclip_list.tag_list[ii] = t_list[ii].Item1;
					t_animationclip_list.animationclip_list[ii] = t_list[ii].Item2.animationclip;
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return a_prefab;
		}

		/** FindAnimationClip
		*/
		private static void FindAnimationClip_Directory(Fee.File.Path a_path,System.Collections.Generic.List<FindItem> a_out_list)
		{
			//ディレクトリ内のファイルを列挙。
			System.Collections.Generic.List<string> t_name_list = Fee.EditorTool.Utility.CreateFileNameList(a_path);

			foreach(string t_file_name_item in t_name_list){
				if(System.Text.RegularExpressions.Regex.IsMatch(t_file_name_item,"^.*\\.(fbx)$") == true){
					FindAnimationClip(new File.Path(a_path.GetNormalizePath() + "/" + t_file_name_item),a_out_list);
				}
			}

		}

		/** FindAnimationClip
		*/
		private static void FindAnimationClip(Fee.File.Path a_path,System.Collections.Generic.List<FindItem> a_out_list)
		{
			UnityEngine.Object[] t_object_list = Fee.EditorTool.Utility.LoadAllAsset(a_path);
			foreach(UnityEngine.Object t_object in t_object_list){
				UnityEngine.AnimationClip t_animationclip = t_object as UnityEngine.AnimationClip;
				if(t_animationclip != null){
					if(System.Text.RegularExpressions.Regex.IsMatch(t_animationclip.name,"^.*__preview__.*$") == true){
						//UnityEngine.Debug.Log("ignore : " + t_animationclip.name);
					}else{
						a_out_list.Add(new FindItem(a_path,t_animationclip));
					}
				}
			}
		}
	}
	#endif
}

