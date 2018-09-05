using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** test11
*/
public class test11 : main_base
{
	/** DATA_VERSION
	*/
	private const int DATA_VERSION = 2;

	/** ASSETBUNDLE_ID_BGM
	*/
	private const long ASSETBUNDLE_ID_BGM = 0x00000001;

	/** download_bgm
	*/
	NDownLoad.Item download_bgm;

	

	/** Start
	*/
	private void Start()
	{
		//ダウンロード。インスタンス作成。
		NDownLoad.Config.LOG_ENABLE = true;
		NDownLoad.DownLoad.CreateInstance();

		//オーディオ。インスタンス作成。
		NAudio.Audio.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//アセットバンドル。破棄。
		AssetBundle.UnloadAllAssetBundles(false);

		//ダウンロードリクエスト。
		{
			#if(UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN)
			string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/StandaloneWindows/bgm";
			#elif(UNITY_WEBGL)
			string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/WebGL/bgm";
			#elif(UNITY_ANDROID)
			string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/Android/bgm";
			#elif(UNITY_IOS)
			string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/iOS/bgm";
			#endif

			this.download_bgm = NDownLoad.DownLoad.GetInstance().RequestAssetBundle(t_url,ASSETBUNDLE_ID_BGM,DATA_VERSION);
		}
	}

	/** Update
	*/
	private void Update()
	{
		//ダウンロード。
		NDownLoad.DownLoad.GetInstance().Main();

		//オーディオ。
		//NAudio.Audio.GetInstance()

		if(this.download_bgm != null){
			if(this.download_bgm.IsBusy() == false){

				AssetBundle t_assetbundle = this.download_bgm.GetResultAssetBundle();
				if(t_assetbundle != null){
					GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("bgm");
					if(t_prefab != null){
						NAudio.ClipPack t_cippack = t_prefab.GetComponent<NAudio.ClipPack>();
						if(t_cippack != null){
							NAudio.Audio.GetInstance().LoadBgm(t_cippack);
							NAudio.Audio.GetInstance().PlayBgm(0);
						}
					}
				}

				this.download_bgm = null;
			}
		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
	}

	#if UNITY_EDITOR
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/StandaloneWindows")]
	private static void MakeAssetBundle_StandaloneWindows()
	{
		UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/StandaloneWindows",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.StandaloneWindows);
	}
	#endif

	#if UNITY_EDITOR
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/WebGL")]
	private static void MakeAssetBundle_WebGL()
	{
		UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/WebGL",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.WebGL);
	}
	#endif

	#if UNITY_EDITOR
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/Android")]
	private static void MakeAssetBundle_Android()
	{
		UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/Android",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.Android);
	}
	#endif

	#if UNITY_EDITOR
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/iOS")]
	private static void MakeAssetBundle_iOS()
	{
		UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/iOS",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.iOS);
	}
	#endif
}

