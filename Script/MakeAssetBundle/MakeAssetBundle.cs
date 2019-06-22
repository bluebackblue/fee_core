

/**
 * Copyright (c) OpenWorld
 * Released under the MIT License
 * https://gitlab.com/blueback28/openworld/blob/master/license/LICENSE.txt
 * @brief アセットバンドル作成。
*/


/** System
*/
namespace System
{
	/** MakeAssetBundle
	*/
	#if(false)
	public class MakeAssetBundle
	{
		/** s_directoryname
		*/
		private const string s_directoryname = "AssetBundle_OutPut";

		/** フォルダ作成。
		*/
		private static void MakeAssetBundle_MakeFolder(string a_platformname)
		{
			//Assets/AssetBundle_OutPut
			{
				string t_path = UnityEngine.Application.dataPath + "/" + s_directoryname;
				if(System.IO.Directory.Exists(t_path) == false){
					System.IO.Directory.CreateDirectory(t_path);
					UnityEditor.AssetDatabase.Refresh();
				}
			}

			//Assets/AssetBundle_OutPut/xxxx
			{
				string t_path = UnityEngine.Application.dataPath + "/" + s_directoryname + "/" + a_platformname;
				if(System.IO.Directory.Exists(t_path) == false){
					System.IO.Directory.CreateDirectory(t_path);
					UnityEditor.AssetDatabase.Refresh();
				}
			}
		}

		/** 作成。
		*/
		#if(USE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/MakeAssetBundle/All")]
		private static void MenuItem_MakeAssetBundle_All()
		{
			MakeAssetBundle_StandaloneWindows();
			MakeAssetBundle_WebGL();
			MakeAssetBundle_Android();
			MakeAssetBundle_iOS();
		}
		#endif

		/** スタンドアローンウィンドウズ。
		*/
		#if(USE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/MakeAssetBundle/StandaloneWindows")]
		private static void MenuItem_MakeAssetBundle_StandaloneWindows()
		{
			MakeAssetBundle_StandaloneWindows();
		}
		#endif

		/** WebGL。
		*/
		#if(USE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/MakeAssetBundle/WebGL")]
		private static void MenuItem_MakeAssetBundle_WebGL()
		{
			MakeAssetBundle_WebGL();
		}
		#endif

		/** アンドロイド。
		*/
		#if(USE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/MakeAssetBundle/Android")]
		private static void MenuItem_MakeAssetBundle_Android()
		{
			MakeAssetBundle_Android();
		}
		#endif

		/** iOS。
		*/
		#if(USE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/MakeAssetBundle/iOS")]
		private static void MenuItem_MakeAssetBundle_iOS()
		{
			MakeAssetBundle_iOS();
		}
		#endif

		/** スタンドアローンウィンドウズ。
		*/
		private static void MakeAssetBundle_StandaloneWindows()
		{
			MakeAssetBundle_MakeFolder("StandaloneWindows");

			if(UnityEditor.BuildPipeline.IsBuildTargetSupported(UnityEditor.BuildTargetGroup.Standalone,UnityEditor.BuildTarget.StandaloneWindows) == true){
				UnityEngine.Debug.Log("StandaloneWindows::start");
				UnityEditor.BuildPipeline.BuildAssetBundles("Assets/" + s_directoryname + "/StandaloneWindows",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.StandaloneWindows);
				UnityEngine.Debug.Log("StandaloneWindows::end");
			}
		}

		/** WebGL。
		*/
		private static void MakeAssetBundle_WebGL()
		{
			MakeAssetBundle_MakeFolder("WebGL");

			if(UnityEditor.BuildPipeline.IsBuildTargetSupported(UnityEditor.BuildTargetGroup.WebGL,UnityEditor.BuildTarget.WebGL) == true){
				UnityEngine.Debug.Log("WebGL::start");
				UnityEditor.BuildPipeline.BuildAssetBundles("Assets/" + s_directoryname + "/WebGL",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.WebGL);
				UnityEngine.Debug.Log("WebGL::end");
			}
		}

		/** アンドロイド。
		*/
		private static void MakeAssetBundle_Android()
		{
			MakeAssetBundle_MakeFolder("Android");

			if(UnityEditor.BuildPipeline.IsBuildTargetSupported(UnityEditor.BuildTargetGroup.Android,UnityEditor.BuildTarget.Android) == true){
				UnityEngine.Debug.Log("Android::start");
				UnityEditor.BuildPipeline.BuildAssetBundles("Assets/" + s_directoryname + "/Android",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.Android);
				UnityEngine.Debug.Log("Android::end");
			}
		}

		/** iOS。
		*/
		private static void MakeAssetBundle_iOS()
		{
			MakeAssetBundle_MakeFolder("iOS");

			if(UnityEditor.BuildPipeline.IsBuildTargetSupported(UnityEditor.BuildTargetGroup.iOS,UnityEditor.BuildTarget.iOS) == true){
				UnityEngine.Debug.Log("iOS::start");
				UnityEditor.BuildPipeline.BuildAssetBundles("Assets/" + s_directoryname + "/iOS",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.iOS);
				UnityEngine.Debug.Log("iOS::end");
			}
		}
	}
	#endif
}

