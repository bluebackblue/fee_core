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
	private const int DATA_VERSION = 2;

	/** ASSETBUNDLE_ID_BGM
	*/
	private const long ASSETBUNDLE_ID_BGM = 0x00000001;

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
		AssetBundle_DownLoad_Start,
		AssetBundle_DownLoad_Now,

		/** サウンドプール。
		*/
		SoundPool,
		SoundPool_DownLoad_List_Start,
		SoundPool_DownLoad_List_Now,
		SoundPool_Donwload_ListItem_Start,
		SoundPool_Donwload_ListItem_Now,

		/*
		SoundPool_DownLoad_Start,
		SoundPool_DownLoad_Now,
		SoundPool_Save_Start,
		SoundPool_Save_Now,
		SoundPool_Android_Load_SoundPool,
		*/
	};

	private Mode mode;

	/** ダウンロード。
	*/
	private NDownLoad.Item download_item;

	/** セーブロード。
	*/
	private NSaveLoad.Item saveload_item;

	/** クリップパック。
	*/
	private NAudio.ClipPack clippack;

	/** バイナリ。
	*/
	private byte[] binary;

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
	private NUi.Button button_se;

	/** アンドロイドサウンドプール。
	*/
	#if(UNITY_ANDROID)
	private AndroidJavaObject android_sound_pool;
	private bool android_sound_enable;
	private int android_sound_soundid;
	private int android_sound_streamid;
	#endif
	private NJsonItem.JsonItem soundpool_jsonitem;
	private int soundpool_listitem_index;

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

		//セーブロード。インスタンス作成。
		NSaveLoad.SaveLoad.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//モード。
		this.mode = Mode.Wait;

		//ダウンロード。
		this.download_item = null;

		//セーブロード。
		this.saveload_item = null;

		//クリップパック。
		this.clippack = null;

		//バイナリ。
		this.binary = null;

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
		this.button_assetbundle.SetRect(100 + 200 * 1,130,150,30);
		this.button_assetbundle.SetText("アセットバンドル");

		//ボタン。
		this.button_se = new NUi.Button(this.deleter,null,0,Click_Se,-1);
		this.button_se.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_se.SetRect(100 + 200 * 2,130,150,30);
		this.button_se.SetText("ＳＥロード");

		#if(UNITY_ANDROID)
		{
			int STREAM_MUSIC = 3;
			this.android_sound_pool = new AndroidJavaObject("android.media.SoundPool",1,STREAM_MUSIC,0);
			this.android_sound_enable = false;
			this.android_sound_soundid = 0;
			this.android_sound_streamid = 0;
		}
		#endif
		this.soundpool_jsonitem = null;
		this.soundpool_listitem_index = 0;
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
	public void Click_Se(int a_value)
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
				this.mode = Mode.AssetBundle_DownLoad_Start;
			}break;
		case Mode.AssetBundle_DownLoad_Start:
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

				this.mode = Mode.AssetBundle_DownLoad_Now;
			}break;
		case Mode.AssetBundle_DownLoad_Now:
			{
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
								this.clippack = t_prefab.GetComponent<NAudio.ClipPack>();
							}
						}
						if(this.clippack == null){
							//不正なクリップパック。
							this.status.SetText("ClipPack = Error");
							this.download_item = null;
							this.mode = Mode.Wait;
						}else{
							this.status.SetText("ClipPack : " + this.clippack.clip_list.Length.ToString());
							this.download_item = null;

							this.clippack = null;

							this.mode = Mode.Wait;
						}
					}else{
						//ダウンロード失敗。
						this.status.SetText("DataType = Error");
						this.download_item = null;
						this.mode = Mode.Wait;
					}
				}
			}break;
		case Mode.SoundPool:
			{
				this.mode = Mode.SoundPool_DownLoad_List_Start;
			}break;
		case Mode.SoundPool_DownLoad_List_Start:
			{
				string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/Raw/se.txt";
				this.download_item = NDownLoad.DownLoad.GetInstance().Request(t_url,NDownLoad.DataType.Text);
				this.mode = Mode.SoundPool_DownLoad_List_Now;
			}break;
		case Mode.SoundPool_DownLoad_List_Now:
			{
				if(this.download_item.IsBusy() == true){
					//ダウンロード中。
					this.status.SetText(this.download_item.GetProgress().ToString());
				}else{
					if(this.download_item.GetDataType() == NDownLoad.DataType.Text){
						//ダウンロード成功。
						this.soundpool_jsonitem = new NJsonItem.JsonItem(this.download_item.GetResultText());
						this.soundpool_listitem_index = 0;
						this.download_item = null;
						this.mode = Mode.SoundPool_Donwload_ListItem_Start;
					}else{
						//ダウンロード失敗。
						this.status.SetText("DataType = Error");
						this.download_item = null;
						this.mode = Mode.Wait;
					}
				}
			}break;
		case Mode.SoundPool_Donwload_ListItem_Start:
			{
				/*
				string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/Raw/water-drop3_1.mp3";
				this.download_item = NDownLoad.DownLoad.GetInstance().Request(t_url,NDownLoad.DataType.Text);
				this.mode = Mode.SoundPool_DownLoad_List_Now;
				*/

				string t_name = null;

				if(this.soundpool_jsonitem.IsIndexArray() == true){
					if(this.soundpool_listitem_index < this.soundpool_jsonitem.GetListMax()){
						NJsonItem.JsonItem t_item = this.soundpool_jsonitem.GetItem(this.soundpool_listitem_index);
						if(t_item.IsAssociativeArray() == true){
							if(t_item.IsExistItem("name") == true){
								t_name = t_item.GetItem("name").GetStringData();
							}
						}

					}
				}

				if(t_name != null){
					Debug.Log(t_name);
					this.soundpool_listitem_index++;

					//TODO:MP3をダウンロード
					//TODO:ローカル保存。
					//TODO:サウンドプール作成
				}else{
					this.mode = Mode.Wait;
				}
			}break;

		/*
		case Mode.Se_DownLoad_Start:
			{
				string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/Raw/water-drop3_1.mp3";
				this.download_item = NDownLoad.DownLoad.GetInstance().Request(t_url,NDownLoad.DataType.Binary);
				this.mode = Mode.Se_DownLoad_Now;
			}break;
		case Mode.Se_DownLoad_Now:
			{
				if(this.download_item.IsBusy() == true){
					//ダウンロード中。
					this.status.SetText(this.download_item.GetProgress().ToString());
				}else{
					if(this.download_item.GetDataType() == NDownLoad.DataType.Error){
						//ダウンロード失敗。
						this.status.SetText("DataType = Error");
						this.download_item = null;
						this.mode = Mode.Wait;
					}else{
						this.binary = this.download_item.GetResultBinary();

						if(this.binary == null){
							//不正なバイナリ。
							this.status.SetText("Binary = Error");
							this.download_item = null;
							this.mode = Mode.Wait;
						}else{
							this.status.SetText("DataType = Binary : " + this.binary.Length.ToString());
							this.download_item = null;
							this.mode = Mode.Se_Save_Start;
						}
					}
				}
			}break;
		case Mode.Se_Save_Start:
			{
				this.saveload_item = NSaveLoad.SaveLoad.GetInstance().RequestSaveLocalBinaryFile("se_1.mp3",this.binary);
				this.mode = Mode.Se_Save_Now;
			}break;
		case Mode.Se_Save_Now:
			{
				if(this.saveload_item.IsBusy() == true){
					//セーブ中。
					this.status.SetText("SaveNow");
				}else{
					if(this.saveload_item.GetDataType() != NSaveLoad.DataType.SaveEnd){
						//セーブ失敗。
						this.status.SetText("DataType = Error");
						this.saveload_item = null;
						this.mode = Mode.Wait;
					}else{
						this.status.SetText("SaveEnd");

						this.saveload_item = null;
						this.mode = Mode.Se_Android_Load_SoundPool;
					}
				}
			}break;
		case Mode.Se_Android_Load_SoundPool:
			{
				#if(UNITY_ANDROID)
				{
					if(this.android_sound_pool != null){
						this.android_sound_enable = true;
						this.android_sound_soundid = this.android_sound_pool.Call<int>("load",Application.persistentDataPath + "/se_1.mp3",1);
					}else{
						this.android_sound_enable = false;
						this.android_sound_soundid = 0;
					}
					this.status.SetText(this.android_sound_enable.ToString() + " soundid = " + this.android_sound_soundid.ToString());
				}
				#endif

				this.mode = Mode.Wait;
			}break;
		*/
		}

		//再生。
		if(NInput.Mouse.GetInstance().left.down == true){
			#if(UNITY_ANDROID)
			if(this.android_sound_pool != null){
				if(this.android_sound_enable == true){
					int t_ret = this.android_sound_pool.Call<int>("play",this.android_sound_soundid,1.0f,1.0f,0,0,1.0f);
					this.status.SetText("id = " + this.android_sound_soundid.ToString() + " ret = " + t_ret.ToString());
				}
			}
			#endif
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
		#if(UNITY_ANDROID)
		{
			if(this.android_sound_pool != null){
				this.android_sound_pool.Dispose();
				this.android_sound_pool = null;
			}
		}
		#endif
	}

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/test11/MekeSoundPool")]
	private static void MekeSoundPool()
	{
		NJsonItem.JsonItem t_soundpool_se = new NJsonItem.JsonItem(new NJsonItem.Value_IndexArray());

		GameObject t_prefab_se = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AssetBundleData/se.prefab");
		if(t_prefab_se != null){
			NAudio.ClipPack t_clippack = t_prefab_se.GetComponent<NAudio.ClipPack>();
			if(t_clippack != null){
				for(int ii=0;ii<t_clippack.clip_list.Length;ii++){
					float t_audio_volume = 1.0f;
					string t_audio_name = "";

					if(ii<t_clippack.volume_list.Length){
						t_audio_volume = t_clippack.volume_list[ii];
					}

					if(t_clippack.clip_list[ii] != null){
						string t_asset_path = UnityEditor.AssetDatabase.GetAssetPath(t_clippack.clip_list[ii]);
						if(t_asset_path != null){
							string t_name = System.IO.Path.GetFileName(t_asset_path);
							if(t_name != null){
								t_audio_name = t_name;
							}
						}
					}

					NJsonItem.JsonItem t_item = new NJsonItem.JsonItem(new NJsonItem.Value_AssociativeArray());
					//ボリューム。
					t_item.SetItem("volume",new NJsonItem.JsonItem(new NJsonItem.Value_Float(t_audio_volume)),false);
					//名前。
					t_item.SetItem("name",new NJsonItem.JsonItem(new NJsonItem.Value_StringData(t_audio_name)),false);
					t_soundpool_se.AddItem(t_item,false);
				}
			}
		}

		//保存。
		{
			string t_json_string = t_soundpool_se.ConvertJsonString();

			string t_filename = Application.dataPath + "/AssetBundleData/se.txt";
			Debug.Log(t_filename);

			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(t_filename);

			System.IO.StreamWriter t_stream_writer = null;

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

