

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。マテリアルリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** MaterialList_Tool
	*/
	#if(UNITY_EDITOR)
	public class MaterialList_Tool
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
				//materialclip_list
				MaterialList_MonoBehaviour t_material_list = a_prefab.AddComponent<MaterialList_MonoBehaviour>();

				t_material_list.tag_list = new string[a_resource_list.Length];
				t_material_list.material_list = new UnityEngine.Material[a_resource_list.Length];
				for(int ii=0;ii<t_material_list.material_list.Length;ii++){
					t_material_list.tag_list[ii] = a_resource_list[ii].tag;
					t_material_list.material_list[ii] = Fee.EditorTool.Utility.LoadAsset<UnityEngine.Material>(a_resource_list[ii].path); 
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return a_prefab;
		}
	}
	#endif
}

