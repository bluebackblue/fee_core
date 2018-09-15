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

	アセットバンドル

*/
public class test11 : main_base
{
	/** DATA_VERSION
	*/
	private const int DATA_VERSION = 2;

	/** ASSETBUNDLE_ID_BGM
	*/
	private const long ASSETBUNDLE_ID_BGM = 0x00000001;

	/** 削除管理。
	*/
	NDeleter.Deleter deleter;

	/** text
	*/
	NRender2D.Text2D text;

	/** download_bgm_a
	*/
	NDownLoad.Item download_bgm_a;
	NDownLoad.Item download_bgm_b;

	/** sprite
	*/
	NRender2D.Sprite2D sprite;

	/** Start
	*/
	private void Start()
	{
		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//ダウンロード。インスタンス作成。
		NDownLoad.Config.LOG_ENABLE = true;
		NDownLoad.DownLoad.CreateInstance();

		//オーディオ。インスタンス作成。
		NAudio.Audio.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//text
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;
		this.text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text.SetRect(10,10,0,0);

		//sprite
		{
			int t_w = 100;
			int t_h = 100;
			int t_x = (NRender2D.Render2D.VIRTUAL_W - t_w) / 2;
			int t_y = (NRender2D.Render2D.VIRTUAL_H - t_h) / 2;
			this.sprite = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
			this.sprite.SetRect(t_x,t_y,t_w,t_h);
			this.sprite.SetRotate(true);
			this.sprite.SetCenter(t_x + 50,t_y + 50);
		}

		//すべてのキャッシュファイル削除。
		NDownLoad.AssetBundleList.ClearAllCacheFile();

		//ダウンロードリクエスト。
		{
			string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/";
	
			#if((UNITY_STANDALONE_WIN)||(UNITY_EDITOR_WIN))
			t_url += "StandaloneWindows/";
			#elif(UNITY_WEBGL)
			t_url += "WebGL/";
			#elif(UNITY_ANDROID)
			t_url += "Android/";
			#elif(UNITY_IOS)
			t_url += "iOS/";
			#else
			t_url += "StandaloneWindows/";
			#endif

			this.download_bgm_a = NDownLoad.DownLoad.GetInstance().RequestAssetBundle(t_url + "bgm",ASSETBUNDLE_ID_BGM,DATA_VERSION);
			this.download_bgm_b = NDownLoad.DownLoad.GetInstance().RequestAssetBundle(t_url + "bgm",ASSETBUNDLE_ID_BGM,DATA_VERSION);
		}
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		Quaternion t_q = this.sprite.GetQuaternion();
		t_q = Quaternion.AngleAxis(360 * UnityEngine.Time.fixedDeltaTime,new Vector3(0.0f,0.0f,1.0f)) * t_q;
		this.sprite.SetQuaternion(ref t_q);
	}

	/** Update
	*/
	private void Update()
	{
		//ダウンロード。
		NDownLoad.DownLoad.GetInstance().Main();

		if(this.download_bgm_a != null){
			if(this.download_bgm_a.IsBusy() == false){
				this.text.SetText("Download : End");

				//エラーチェック。
				if(this.download_bgm_a.GetDataType() == NDownLoad.DataType.Error){
					this.text.SetText("Error : " + this.download_bgm_a.GetResultErrorString());
				}else{
					//ロード、再生。
					AssetBundle t_assetbundle = this.download_bgm_a.GetResultAssetBundle();
					if(t_assetbundle != null){
						GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("bgm");
						if(t_prefab != null){
							NAudio.ClipPack t_cippack = t_prefab.GetComponent<NAudio.ClipPack>();
							if(t_cippack != null){
								NAudio.Audio.GetInstance().LoadBgm(t_cippack);
								NAudio.Audio.GetInstance().PlayBgm(0);
								this.text.SetText("PlayBgm : 0");
							}else{
								this.text.SetText("ClipPack : null");
							}
						}else{
							this.text.SetText("LoadAsset : bgm : null");
						}
					}else{
						this.text.SetText("AssetBundle : null");
					}
				}

				this.download_bgm_a = null;
			}else{
				//ダウンロード中。
				this.text.SetText("Download : " + this.download_bgm_a.GetProgress().ToString());
			}
		}

		if(this.download_bgm_b != null){
			if(this.download_bgm_b.IsBusy() == false){
				this.download_bgm_b = null;
			}
		}
	}

	/** 削除前。
	*/
	public override bool PreDestroy(bool a_first)
	{
		return true;
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
	}

	#if UNITY_EDITOR
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/All")]
	private static void MakeAssetBundle_All()
	{
		MakeAssetBundle_StandaloneWindows();
		MakeAssetBundle_WebGL();
		MakeAssetBundle_Android();
		MakeAssetBundle_iOS();
	}
	#endif

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

