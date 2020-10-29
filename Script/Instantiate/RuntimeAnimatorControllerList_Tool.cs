

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。ランタイムアニメーションコントローラ。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** RuntimeAnimatorControllerList_Tool
	*/
	#if(UNITY_EDITOR)
	public class RuntimeAnimatorControllerList_Tool
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
				//runtimeanimationcontroller_list
				RuntimeAnimatorControllerList_MonoBehaviour t_runtimeanimationcontroller_list = a_prefab.AddComponent<RuntimeAnimatorControllerList_MonoBehaviour>();

				t_runtimeanimationcontroller_list.tag_list = new string[a_resource_list.Length];
				t_runtimeanimationcontroller_list.runtimeanimatorcontroller_list = new UnityEngine.RuntimeAnimatorController[a_resource_list.Length];
				for(int ii=0;ii<t_runtimeanimationcontroller_list.runtimeanimatorcontroller_list.Length;ii++){
					t_runtimeanimationcontroller_list.tag_list[ii] = a_resource_list[ii].tag;
					t_runtimeanimationcontroller_list.runtimeanimatorcontroller_list[ii] = Fee.EditorTool.AssetTool.LoadAsset<UnityEngine.RuntimeAnimatorController>(a_resource_list[ii].path); 
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}

			return a_prefab;
		}
	}
	#endif
}

