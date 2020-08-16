

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief グラフィック。コンピュートシェーダ。
*/


/** Fee.Graphic
*/
namespace Fee.Graphic
{
	/** ComputeShader_MonoBehaviour
	*/
	public class ComputeShader_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** computeshader
		*/
		public UnityEngine.ComputeShader computeshader;

		/** CreatePrefab

			a_assets_path	: アセットフォルダからの相対パス。

		*/
		#if(UNITY_EDITOR)
		public static void CreatePrefab(Fee.File.Path a_assets_path,UnityEngine.ComputeShader a_computeshader)
		{
			UnityEngine.GameObject t_prefab = new UnityEngine.GameObject("prefab_temp");
			{
				//コンポーネント追加。
				ComputeShader_MonoBehaviour t_monobehaviour = t_prefab.AddComponent<ComputeShader_MonoBehaviour>();
				t_monobehaviour.computeshader = a_computeshader;

				//保存。
				Fee.EditorTool.Utility.SavePrefab(t_prefab,a_assets_path);
			}
			UnityEngine.GameObject.DestroyImmediate(t_prefab);
		}
		#endif
	}
}

