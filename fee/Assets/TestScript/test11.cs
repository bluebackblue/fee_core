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

	アセットバンドル作成
	アセットバンドルダウンロード

*/
public class test11 : main_base
{
	/** DATA_VERSION
	*/
	private const int DATA_VERSION = 3;

	/** ASSETBUNDLE_ID_BGM
	*/
	private const long ASSETBUNDLE_ID_BGM = 0x00000001;

	/** SE_ID
	*/
	private const long SE_ID = 0x00000001;

	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** Mode
	*/
	private enum Mode
	{
		/** 待ち。
		*/
		Wait,

		/** アセットバンドル。
		*/
		AssetBundle,
		AssetBundle_Start,
		AssetBundle_Now,
		AssetBundle_Fix,

		/** サウンドプール。
		*/
		SoundPool,
		SoundPool_Start,
		SoundPool_Now,
		SoundPool_Fix,
	};

	private Mode mode;

	/** ダウンロード。
	*/
	private NDownLoad.Item download_item;

	/** パック。
	*/
	private NAudio.Pack_AudioClip pack_audioclip;

	/** パック。
	*/
	private NAudio.Pack_SoundPool pack_soundpool;

	/** ステータス。
	*/
	private NRender2D.Text2D status;

	/** ボタン。キャッシュクリア。
	*/
	private NUi.Button button_cacheclear;

	/** ボタン。
	*/
	private NUi.Button button_assetbundle;

	/** ボタン。
	*/
	private NUi.Button button_soundpool;

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
		NAudio.Config.LOG_ENABLE = true;
		NAudio.Audio.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//セーブロード。インスタンス作成。
		NSaveLoad.SaveLoad.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//モード。
		this.mode = Mode.Wait;

		//ダウンロード。
		this.download_item = null;

		//パック。
		this.pack_audioclip = null;

		//パック。
		this.pack_soundpool = null;

		//ステータス。
		this.status = new NRender2D.Text2D(this.deleter,null,0);
		this.status.SetRect(100,100,0,0);
		this.status.SetText("-");

		//ボタン。
		this.button_cacheclear = new NUi.Button(this.deleter,null,0,Click_ClearAllCacheFile,-1);
		this.button_cacheclear.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_cacheclear.SetRect(100 + 200 * 0,130,150,30);
		this.button_cacheclear.SetText("キャッシュクリア");

		//ボタン。
		this.button_assetbundle = new NUi.Button(this.deleter,null,0,Click_AssetBundle,-1);
		this.button_assetbundle.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_assetbundle.SetRect(100 + 210 * 1,130,200,30);
		this.button_assetbundle.SetText("AssetBundleロード");

		//ボタン。
		this.button_soundpool = new NUi.Button(this.deleter,null,0,Click_SoundPool,-1);
		this.button_soundpool.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_soundpool.SetRect(100 + 210 * 2,130,200,30);
		this.button_soundpool.SetText("SoundPoolロード");
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
	public void Click_AssetBundle(int a_value)
	{
		if(this.mode == Mode.Wait){
			this.mode = Mode.AssetBundle;
		}
	}

	/** クリック。
	*/
	public void Click_SoundPool(int a_value)
	{
		if(this.mode == Mode.Wait){
			this.mode = Mode.SoundPool;
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

		//セーブロード。
		NSaveLoad.Config.LOG_ENABLE = true;
		NSaveLoad.SaveLoad.GetInstance().Main();

		switch(this.mode){
		case Mode.Wait:
			{
			}break;
		case Mode.AssetBundle:
			{
				//アセットバンドル。

				this.mode = Mode.AssetBundle_Start;
			}break;
		case Mode.AssetBundle_Start:
			{
				//アセットバンドル。

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

				this.mode = Mode.AssetBundle_Now;
			}break;
		case Mode.AssetBundle_Now:
			{
				//アセットバンドル。

				if(this.download_item.IsBusy() == true){
					//ダウンロード中。
					this.status.SetText(this.download_item.GetProgress().ToString());
				}else{
					if(this.download_item.GetDataType() == NDownLoad.DataType.AssetBundle){
						//ダウンロード成功。
						AssetBundle t_assetbundle = this.download_item.GetResultAssetBundle();
						if(t_assetbundle != null){
							GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("se");
							if(t_prefab != null){
								this.pack_audioclip = t_prefab.GetComponent<NAudio.Pack_AudioClip>();
							}
						}
						if(this.pack_audioclip == null){
							//不正なクリップパック。
							this.status.SetText("Error : " + this.mode.ToString());
							this.download_item = null;
							this.mode = Mode.Wait;
						}else{
							this.download_item = null;
							this.mode = Mode.AssetBundle_Fix;
						}
					}else{
						//ダウンロード失敗。
						this.status.SetText("Error : " + this.download_item.GetResultErrorString());
						this.download_item = null;
						this.mode = Mode.Wait;
					}
				}
			}break;
		case Mode.AssetBundle_Fix:
			{
				this.status.SetText("Success");

				NAudio.Audio.GetInstance().LoadSe(this.pack_audioclip,SE_ID);
				this.pack_audioclip = null;
				this.mode = Mode.Wait;
			}break;
		case Mode.SoundPool:
			{
				//サウンドプール。

				this.mode = Mode.SoundPool_Start;
			}break;
		case Mode.SoundPool_Start:
			{
				//サウンドプール。

				string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/Raw/se.txt";
				this.download_item = NDownLoad.DownLoad.GetInstance().RequestSoundPool(t_url,DATA_VERSION);
				this.mode = Mode.SoundPool_Now;
			}break;
		case Mode.SoundPool_Now:
			{
				//サウンドプール。

				if(this.download_item.IsBusy() == true){
					//ダウンロード中。
					this.status.SetText(this.download_item.GetProgress().ToString());
				}else{
					if(this.download_item.GetDataType() == NDownLoad.DataType.SoundPool){
						//ダウンロード成功。
						this.pack_soundpool = this.download_item.GetResultSoundPool();
						if(this.pack_soundpool == null){
							//不正なサウンドプール。
							this.status.SetText("Error : " + this.mode.ToString());
							this.download_item = null;
							this.mode = Mode.Wait;
						}else{
							this.download_item = null;
							this.mode = Mode.SoundPool_Fix;
						}
					}else{
						//ダウンロード失敗。
						this.status.SetText("Error : " + this.download_item.GetResultErrorString());
						this.download_item = null;
						this.mode = Mode.Wait;
					}
				}
			}break;
		case Mode.SoundPool_Fix:
			{
				this.status.SetText("Success");

				NAudio.Audio.GetInstance().LoadSe(this.pack_soundpool,SE_ID);
				this.pack_soundpool = null;
				this.mode = Mode.Wait;
			}break;
		}

		//再生。
		if(NInput.Mouse.GetInstance().left.down == true){
			NAudio.Audio.GetInstance().PlaySe(SE_ID,0);
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
		this.deleter.DeleteAll();
	}

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/test11/MekeSoundPool")]
	private static void MekeSoundPool()
	{
		string t_assetbundle_name = "se";

		NAudio.Pack_SoundPool t_pack_soundpool = new NAudio.Pack_SoundPool();
		GameObject t_prefab_se = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AssetBundleData/" + t_assetbundle_name + ".prefab");
		if(t_prefab_se != null){
			NAudio.Pack_AudioClip t_pack_audioclip = t_prefab_se.GetComponent<NAudio.Pack_AudioClip>();
			if(t_pack_audioclip != null){
				for(int ii=0;ii<t_pack_audioclip.audioclip_list.Count;ii++){

					//volume
					float t_audio_volume = 1.0f;
					if(ii<t_pack_audioclip.volume_list.Count){
						t_audio_volume = t_pack_audioclip.volume_list[ii];
					}

					//name
					string t_audio_name = "";
					if(t_pack_audioclip.audioclip_list[ii] != null){
						string t_asset_path = UnityEditor.AssetDatabase.GetAssetPath(t_pack_audioclip.audioclip_list[ii]);
						if(t_asset_path != null){
							string t_name = System.IO.Path.GetFileName(t_asset_path);
							if(t_name != null){
								t_audio_name = t_name;
							}
						}
					}

					//volume
					t_pack_soundpool.name_list.Add(t_audio_name);

					//name
					t_pack_soundpool.volume_list.Add(t_audio_volume);
				}
			}

			t_pack_soundpool.data_hash = t_pack_soundpool.GetHashCode();
		}

		//保存。
		{
			NJsonItem.JsonItem t_jsonitem = NJsonItem.ObjectToJson.Convert(t_pack_soundpool);
			string t_json_string = t_jsonitem.ConvertJsonString();

			string t_filename = Application.dataPath + "/AssetBundle/Raw/" + t_assetbundle_name + ".txt";
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(t_filename);
			System.IO.StreamWriter t_stream_writer = null;

			//open
			try{
				t_stream_writer = t_fileinfo.CreateText();
			}catch(System.Exception){
			}

			//write
			if(t_stream_writer != null){
				t_stream_writer.Write(t_json_string);
				t_stream_writer.Flush();
			}

			//close
			if(t_stream_writer != null){
				t_stream_writer.Close();
			}

			UnityEditor.AssetDatabase.Refresh();
		}
	}
	#endif

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

