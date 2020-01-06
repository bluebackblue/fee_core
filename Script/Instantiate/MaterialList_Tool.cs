

/**
 * @brief マテリアルリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** MaterialList_Tool
	*/
	#if(UNITY_EDITOR)
	public class MaterialList_Tool : UnityEngine.MonoBehaviour
	{
		/** ResourceItem
		*/
		public class ResourceItem
		{
			/** tag
			*/
			public string tag;

			/** path

				"Assets/xxx/yyy/zzz.material"

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

		/** CreateMaterial

			全部登録する。

		*/
		public static void CreatePrefab(Fee.File.Path a_output_path,ResourceItem[] a_resource_list)
		{
			UnityEngine.GameObject t_prefab = new UnityEngine.GameObject();
			t_prefab.name = "prefab_temp";
			try{
				//materialclip_list
				MaterialList_MonoBehaviour t_material_list = t_prefab.AddComponent<MaterialList_MonoBehaviour>();

				t_material_list.tag_list = new string[a_resource_list.Length];
				t_material_list.material_list = new UnityEngine.Material[a_resource_list.Length];
				for(int ii=0;ii<t_material_list.material_list.Length;ii++){
					t_material_list.tag_list[ii] = a_resource_list[ii].tag;
					t_material_list.material_list[ii] = Fee.EditorTool.Utility.LoadAsset<UnityEngine.Material>(a_resource_list[ii].path); 
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

