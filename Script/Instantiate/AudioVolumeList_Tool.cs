

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
	/** AudioVolumeList_Tool
	*/
	#if(UNITY_EDITOR)
	public class AudioVolumeList_Tool
	{
		/** ResourceItem
		*/
		public class ResourceItem
		{
			/** tag
			*/
			public string tag;

			/** volume

				"Assets/xxx/yyy/zzz.tty"

			*/
			public float volume;

			/** constructor
			*/
			public ResourceItem(string a_tag,float a_volume)
			{
				//tag
				this.tag = a_tag;

				//volume
				this.volume = a_volume;
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
				//audiovolume_list
				AudioVolumeList_MonoBehaviour t_audiovolume_list = a_prefab.AddComponent<AudioVolumeList_MonoBehaviour>();

				t_audiovolume_list.tag_list = new string[a_resource_list.Length];
				t_audiovolume_list.audiovolume_list = new float[a_resource_list.Length];
				for(int ii=0;ii<t_audiovolume_list.audiovolume_list.Length;ii++){
					t_audiovolume_list.tag_list[ii] = a_resource_list[ii].tag;
					t_audiovolume_list.audiovolume_list[ii] = a_resource_list[ii].volume; 
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return a_prefab;
		}
	}
	#endif
}

