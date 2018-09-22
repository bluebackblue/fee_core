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

	アセットバンドルダウンロード
	アセットバンドル作成

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
	private NDeleter.Deleter deleter;

	/** モード。
	*/
	private int mode;	

	/** ダウンロード。
	*/
	private NDownLoad.Item download_item;

	/** クリップパック。
	*/
	private NAudio.ClipPack clippack;

	/** ステータス。
	*/
	private NRender2D.Text2D status;

	/** ボタン。キャッシュクリア。
	*/
	private NUi.Button button_cacheclear;

	/** ボタン。
	*/
	private NUi.Button button_download;





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

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//モード。
		this.mode = 0;

		//ダウンロード。
		this.download_item = null;

		//クリップパック。
		this.clippack = null;

		//ステータス。
		this.status = new NRender2D.Text2D(this.deleter,null,0);
		this.status.SetRect(100,100,0,0);
		this.status.SetText("-");

		//ボタン。
		this.button_cacheclear = new NUi.Button(this.deleter,null,0,Click_ClearAllCacheFile,-1);
		this.button_cacheclear.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_cacheclear.SetRect(100,130,150,30);
		this.button_cacheclear.SetText("キャッシュクリア");

		//ボタン。
		this.button_download = new NUi.Button(this.deleter,null,0,Click_DownLoad,-1);
		this.button_download.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_download.SetRect(300,130,150,30);
		this.button_download.SetText("ダウンロード");

		//text
		/*
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;
		this.text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text.SetRect(10,10,0,0);
		*/

		/*

		*/

		/*
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
		*/
	}

	/** クリック。
	*/
	public void Click_ClearAllCacheFile(int a_value)
	{
		//すべてのキャッシュファイル削除。
		NDownLoad.AssetBundleList.ClearAllCacheFile();
	}

	/** クリック。
	*/
	public void Click_DownLoad(int a_value)
	{
		if(this.mode == 0){
			this.mode = 1;
		}
	}

	/** Update
	*/
	private void Update()
	{
		//ダウンロード。
		NDownLoad.DownLoad.GetInstance().Main();

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		switch(this.mode){
		case 0:
			{
			}break;
		case 1:
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

				this.download_item = NDownLoad.DownLoad.GetInstance().RequestAssetBundle(t_url + "se",ASSETBUNDLE_ID_BGM,DATA_VERSION);

				this.mode++;
			}break;
		case 2:
			{
				if(this.download_item.IsBusy() == true){
					//ダウンロード中。
					this.status.SetText(this.download_item.GetProgress().ToString());
				}else{
					if(this.download_item.GetDataType() == NDownLoad.DataType.Error){
						//ダウンロード失敗。
						this.status.SetText("DataType = Error");
						this.download_item = null;
						this.mode = 0;
					}else{
						AssetBundle t_assetbundle = this.download_item.GetResultAssetBundle();
						if(t_assetbundle != null){
							GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("se");
							if(t_prefab != null){
								this.clippack = t_prefab.GetComponent<NAudio.ClipPack>();
							}
						}

						if(this.clippack == null){
							//不正なクリップパック。
							this.status.SetText("ClipPack = Error");
							this.download_item = null;
							this.mode = 0;
						}else{
							this.status.SetText("ClipPack");
							this.mode++;
						}
					}
				}
			}break;
		case 3:
			{
			}break;
		}

		/*
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
		*/
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

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/All")]
	private static void MakeAssetBundle_All()
	{
		MakeAssetBundle_StandaloneWindows();
		MakeAssetBundle_WebGL();
		MakeAssetBundle_Android();
		MakeAssetBundle_iOS();
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/StandaloneWindows")]
	private static void MakeAssetBundle_StandaloneWindows()
	{
		UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/StandaloneWindows",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.StandaloneWindows);
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/WebGL")]
	private static void MakeAssetBundle_WebGL()
	{
		UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/WebGL",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.WebGL);
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/Android")]
	private static void MakeAssetBundle_Android()
	{
		UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/Android",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.Android);
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/iOS")]
	private static void MakeAssetBundle_iOS()
	{
		UnityEditor.BuildPipeline.BuildAssetBundles("Assets/AssetBundle/iOS",UnityEditor.BuildAssetBundleOptions.None,UnityEditor.BuildTarget.iOS);
	}
	#endif
}

