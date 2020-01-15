

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
	/** VideoClipList_Tool
	*/
	#if(UNITY_EDITOR)
	public class VideoClipList_Tool
	{
		/** ResourceItem
		*/
		public class ResourceItem
		{
			/** tag
			*/
			public string tag;

			/** path

				"Assets/xxx/yyy/zzz.png"

			*/
			public Fee.File.Path path;

			/** constructor
			*/
			public ResourceItem(string a_tag,Fee.File.Path a_assets_path)
			{
				//tag
				this.tag = a_tag;

				//path
				this.path = a_assets_path;
			}
		}

		/** 作成。
		*/
		public static UnityEngine.GameObject Create(Fee.File.Path a_output_assets_path,ResourceItem[] a_resource_list)
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

		/** 追加。
		*/
		public static UnityEngine.GameObject Add(UnityEngine.GameObject a_prefab,ResourceItem[] a_resource_list)
		{
			try{
				//videoclip_list
				VideoClipList_MonoBehaviour t_videoclip_list = a_prefab.AddComponent<VideoClipList_MonoBehaviour>();

				t_videoclip_list.tag_list = new string[a_resource_list.Length];
				t_videoclip_list.videoclip_list = new UnityEngine.Video.VideoClip[a_resource_list.Length];
				for(int ii=0;ii<t_videoclip_list.videoclip_list.Length;ii++){
					t_videoclip_list.tag_list[ii] = a_resource_list[ii].tag;
					t_videoclip_list.videoclip_list[ii] = Fee.EditorTool.Utility.LoadAsset<UnityEngine.Video.VideoClip>(a_resource_list[ii].path); 
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return a_prefab;
		}
	}
	#endif
}

