

/**
 * @brief プレハブリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** PrefabList_Tool
	*/
	#if(UNITY_EDITOR)
	public class PrefabList_Tool : UnityEngine.MonoBehaviour
	{
		/** ResourceItem
		*/
		public class ResourceItem
		{
			/** tag
			*/
			public string tag;

			/** path

				"Assets/xxx/yyy/zzz.prefab"

			*/
			public Fee.File.Path path;

			/** constructor
			*/
			public ResourceItem(string a_tag,Fee.File.Path a_path)
			{
				//tag
				this.tag = a_tag;

				//path
				this.path = a_path;
			}
		}

		/** CreatePrefab

			全部登録する。

		*/
		public static void CreatePrefab(Fee.File.Path a_output_path,ResourceItem[] a_resource_list)
		{
			UnityEngine.GameObject t_prefab = new UnityEngine.GameObject();
			t_prefab.name = "prefab_temp";
			try{
				//prefab_list
				PrefabList_MonoBehaviour t_prefab_list = t_prefab.AddComponent<PrefabList_MonoBehaviour>();

				t_prefab_list.tag_list = new string[a_resource_list.Length];
				t_prefab_list.prefab_list = new UnityEngine.GameObject[a_resource_list.Length];
				for(int ii=0;ii<t_prefab_list.prefab_list.Length;ii++){
					t_prefab_list.tag_list[ii] = a_resource_list[ii].tag;
					t_prefab_list.prefab_list[ii] = Fee.EditorTool.Utility.LoadAsset<UnityEngine.GameObject>(a_resource_list[ii].path); 
				}

				//SavePrefab
				Fee.EditorTool.Utility.SavePrefab(t_prefab,a_output_path);
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
			UnityEngine.GameObject.DestroyImmediate(t_prefab);
		}
	}
	#endif
}

