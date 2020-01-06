

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。バイナリコンポーネント。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Binary_Component_MonoBehaviour
	*/
	public class Binary_Component_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** data
		*/
		[UnityEngine.SerializeField,UnityEngine.HideInInspector]
		public byte[] data;

		/** CreatePrefab

		a_output_path : "Assets/xxx.prefab"

		*/
		#if(UNITY_EDITOR)
		public static void CreatePrefab(string a_output_path,byte[] a_binary)
		{
			UnityEngine.GameObject t_prefab = new UnityEngine.GameObject();
			t_prefab.name = "prefab_temp";
			{
				//バイナリコンポーネント追加。
				Binary_Component_MonoBehaviour t_binary = t_prefab.AddComponent<Binary_Component_MonoBehaviour>();
				t_binary.data = a_binary;

				//保存。
				{
					bool t_ret = false;

					#if(UNITY_5)
					{
						if(UnityEditor.PrefabUtility.CreatePrefab(a_output_path,t_prefab) != null){
							t_ret = true;
						}
					}
					#else
					{
						UnityEditor.PrefabUtility.SaveAsPrefabAsset(t_prefab,a_output_path,out t_ret);
					}
					#endif

					Tool.Assert(t_ret);
				}
			}
			UnityEngine.GameObject.DestroyImmediate(t_prefab);
		}
		#endif
	}
}

