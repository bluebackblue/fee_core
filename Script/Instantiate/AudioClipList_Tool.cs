

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
	/** AudioClipList_Tool
	*/
	#if(UNITY_EDITOR)
	public class AudioClipList_Tool
	{
		/** ResourceItem
		*/
		public class ResourceItem
		{
			/** tag
			*/
			public string tag;

			/** path

				"Assets/xxx/yyy/zzz.tty"

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

		/** 追加。
		*/
		public static UnityEngine.GameObject Add(UnityEngine.GameObject a_prefab,ResourceItem[] a_resource_list)
		{
			try{
				//audioclip_list
				AudioClipList_MonoBehaviour t_audioclip_list = a_prefab.AddComponent<AudioClipList_MonoBehaviour>();

				t_audioclip_list.tag_list = new string[a_resource_list.Length];
				t_audioclip_list.audioclip_list = new UnityEngine.AudioClip[a_resource_list.Length];
				for(int ii=0;ii<t_audioclip_list.audioclip_list.Length;ii++){
					t_audioclip_list.tag_list[ii] = a_resource_list[ii].tag;
					t_audioclip_list.audioclip_list[ii] = Fee.EditorTool.Utility.LoadAsset<UnityEngine.AudioClip>(a_resource_list[ii].path); 
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return a_prefab;
		}
	}
	#endif
}

