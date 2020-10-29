

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

			a_assets_path	: アセットフォルダからの相対パス。

		*/
		#if(UNITY_EDITOR)
		public static void CreatePrefab(Fee.File.Path a_assets_path,byte[] a_binary)
		{
			UnityEngine.GameObject t_prefab = new UnityEngine.GameObject("prefab_temp");
			{
				//コンポーネント追加。
				Binary_Component_MonoBehaviour t_monobehaviour = t_prefab.AddComponent<Binary_Component_MonoBehaviour>();
				t_monobehaviour.data = a_binary;

				//保存。
				Fee.EditorTool.AssetTool.SavePrefab(t_prefab,a_assets_path);
			}
			UnityEngine.GameObject.DestroyImmediate(t_prefab);
		}
		#endif
	}
}

