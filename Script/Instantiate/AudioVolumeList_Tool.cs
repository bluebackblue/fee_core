

/**
 * @brief オーディオボリューム。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** AudioVolumeList_Tool
	*/
	#if(UNITY_EDITOR)
	public class AudioVolumeList_Tool : UnityEngine.MonoBehaviour
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

		/** Create

			全部登録する。

		*/
		public static void Create(Fee.File.Path a_output_assets_path,ResourceItem[] a_resource_list)
		{
			UnityEngine.GameObject t_prefab = new UnityEngine.GameObject();
			t_prefab.name = "prefab_temp";
			try{
				//audiovolume_list
				AudioVolumeList_MonoBehaviour t_audiovolume_list = t_prefab.AddComponent<AudioVolumeList_MonoBehaviour>();

				t_audiovolume_list.tag_list = new string[a_resource_list.Length];
				t_audiovolume_list.audiovolume_list = new float[a_resource_list.Length];
				for(int ii=0;ii<t_audiovolume_list.audiovolume_list.Length;ii++){
					t_audiovolume_list.tag_list[ii] = a_resource_list[ii].tag;
					t_audiovolume_list.audiovolume_list[ii] = a_resource_list[ii].volume; 
				}

				//SavePrefab
				Fee.EditorTool.Utility.SavePrefab(t_prefab,a_output_assets_path);
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
			UnityEngine.GameObject.DestroyImmediate(t_prefab);
		}
	}
	#endif
}

