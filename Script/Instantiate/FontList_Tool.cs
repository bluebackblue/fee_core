

/**
 * @brief フォントリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** FontList_Tool
	*/
	#if(UNITY_EDITOR)
	public class FontList_Tool : UnityEngine.MonoBehaviour
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

		/** Create

			全部登録する。

		*/
		public static void Create(Fee.File.Path a_output_assets_path,ResourceItem[] a_resource_list)
		{
			UnityEngine.GameObject t_prefab = new UnityEngine.GameObject();
			t_prefab.name = "prefab_temp";
			try{
				//font_list
				FontList_MonoBehaviour t_font_list = t_prefab.AddComponent<FontList_MonoBehaviour>();

				t_font_list.tag_list = new string[a_resource_list.Length];
				t_font_list.font_list = new UnityEngine.Font[a_resource_list.Length];
				for(int ii=0;ii<t_font_list.font_list.Length;ii++){
					t_font_list.tag_list[ii] = a_resource_list[ii].tag;
					t_font_list.font_list[ii] = Fee.EditorTool.Utility.LoadAsset<UnityEngine.Font>(a_resource_list[ii].path); 
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

