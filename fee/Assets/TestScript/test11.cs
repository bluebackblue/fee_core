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

	オーディオクリップパックのアセットバンドル作成
	サウンドプールパックの作成
	オーディオクリップパックのアセットバンドルロード
	サウンドプールパックのオード
*/
public class test11 : main_base
{
	/** DATA_VERSION
	*/
	private const int DATA_VERSION = 3;

	/** ASSETBUNDLE_ID_BGM
	*/
	private const long ASSETBUNDLE_ID_BGM = 0x00000001;

	/** ASSETBUNDLE_ID_SE
	*/
	private const long ASSETBUNDLE_ID_SE = 0x00000002;

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

		/** Start
		*/
		Start,

		/** Now
		*/
		Now,

		/** Fix
		*/
		Fix,
	};

	private Mode mode;

	/** soundpool_flag
	*/
	private bool soundpool_flag;

	/** ダウンロード。
	*/
	private NDownLoad.Item download_item_se;

	/** ダウンロード。
	*/
	private NDownLoad.Item download_item_bgm;

	/** オーディオクリップパック。
	*/
	private NAudio.Pack_AudioClip pack_audioclip;

	/** サウンドプールパック。
	*/
	private NAudio.Pack_SoundPool pack_soundpool;

	/** ステータス。
	*/
	private NRender2D.Text2D status;

	/** ステータス。
	*/
	private NRender2D.Text2D status_2;

	/** ボタン。アンロード。
	*/
	private NUi.Button button_unload;

	/** ボタン。
	*/
	private NUi.Button button_assetbundle;

	/** ボタン。
	*/
	private NUi.Button button_soundpool;

	/** ボタン。
	*/
	private NUi.Button button_bgm;

	/** スライダー。
	*/
	private NUi.Slider slider_master;

	/** スライダー。
	*/
	private NUi.Slider slider_bgm;

	/** slider_se
	*/
	private NUi.Slider slider_se;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.Config.LOG_ENABLE = true;
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//ダウンロード。インスタンス作成。
		NDownLoad.Config.LOG_ENABLE = true;
		NDownLoad.Config.SOUNDPOOL_CHECK_DATAVERSION = false;
		NDownLoad.Config.SOUNDPOOL_CHECL_DATAHASH = false;
		NDownLoad.DownLoad.CreateInstance();

		//セーブロード。
		NSaveLoad.Config.LOG_ENABLE = true;
		NSaveLoad.SaveLoad.CreateInstance();

		//オーディオ。インスタンス作成。
		NAudio.Config.LOG_ENABLE = true;
		NAudio.Audio.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Config.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//フォント。
		Font t_font = Resources.Load<Font>("mplus-1p-medium");
		if(t_font != null){
			NRender2D.Render2D.GetInstance().SetDefaultFont(t_font);
		}

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

		//モード。
		this.mode = Mode.Wait;

		//soundpool_flag
		this.soundpool_flag = false;

		//ダウンロード。
		this.download_item_se = null;

		//ダウンロード。
		this.download_item_bgm = null;

		//パック。
		this.pack_audioclip = null;

		//パック。
		this.pack_soundpool = null;

		//ステータス。
		this.status = new NRender2D.Text2D(this.deleter,null,0);
		this.status.SetRect(100,50,0,0);
		this.status.SetText("-");

		//ステータス。
		this.status_2 = new NRender2D.Text2D(this.deleter,null,0);
		this.status_2.SetRect(100,100,0,0);
		this.status_2.SetText("-");

		int t_xx = 0;

		//ボタン。
		this.button_unload = new NUi.Button(this.deleter,null,0,this.CallBack_Click_Unload,-1);
		this.button_unload.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_unload.SetRect(t_xx,130,170,30);
		this.button_unload.SetText("アンロード");

		t_xx += 210;

		//ボタン。
		this.button_assetbundle = new NUi.Button(this.deleter,null,0,this.CallBack_Click_AssetBundle,-1);
		this.button_assetbundle.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_assetbundle.SetRect(t_xx,130,170,30);
		this.button_assetbundle.SetText("AssetBundleロード");

		t_xx += 210;

		//ボタン。
		this.button_soundpool = new NUi.Button(this.deleter,null,0,this.CallBack_Click_SoundPool,-1);
		this.button_soundpool.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_soundpool.SetRect(t_xx,130,170,30);
		this.button_soundpool.SetText("SoundPoolロード");

		t_xx += 210;

		//ボタン。
		this.button_bgm = new NUi.Button(this.deleter,null,0,this.CallBack_Click_Bgm,-1);
		this.button_bgm.SetTexture(Resources.Load<Texture2D>("button"));
		this.button_bgm.SetRect(t_xx,130,170,30);
		this.button_bgm.SetText("ＢＧＭ");

		int t_yy = 300;

		//スライダー。
		this.slider_master = new NUi.Slider(this.deleter,null,0,this.CallBack_Change_Master,0);
		this.slider_master.SetRect(100,t_yy,400,40);
		this.slider_master.SetValue(0.0f);
		this.slider_master.SetButtonTexture(Resources.Load<Texture2D>("button"));
		this.slider_master.SetButtonSize(10,80);
		this.slider_master.SetButtonTextureCornerSize(2);
		this.slider_master.SetTexture(Resources.Load<Texture2D>("slider"));

		t_yy += 60;

		//スライダー。
		this.slider_bgm = new NUi.Slider(this.deleter,null,0,this.CallBack_Change_Bgm,0);
		this.slider_bgm.SetRect(100,t_yy,400,40);
		this.slider_bgm.SetValue(0.0f);
		this.slider_bgm.SetButtonTexture(Resources.Load<Texture2D>("button"));
		this.slider_bgm.SetButtonSize(10,80);
		this.slider_bgm.SetButtonTextureCornerSize(2);
		this.slider_bgm.SetTexture(Resources.Load<Texture2D>("slider"));

		t_yy += 60;

		//スライダー。
		this.slider_se = new NUi.Slider(this.deleter,null,0,this.CallBack_Change_Se,0);
		this.slider_se.SetRect(100,t_yy,400,40);
		this.slider_se.SetValue(0.0f);
		this.slider_se.SetButtonTexture(Resources.Load<Texture2D>("button"));
		this.slider_se.SetButtonSize(10,80);
		this.slider_se.SetButtonTextureCornerSize(2);
		this.slider_se.SetTexture(Resources.Load<Texture2D>("slider"));

		//値設定。
		this.slider_master.SetValue(NAudio.Audio.GetInstance().GetMasterVolume());
		this.slider_bgm.SetValue(NAudio.Audio.GetInstance().GetBgmVolume());
		this.slider_se.SetValue(NAudio.Audio.GetInstance().GetSeVolume());
	}

	/** [Slider_Base]コールバック。変更。
	*/
	private void CallBack_Change_Master(int a_id,float a_value)
	{
		NAudio.Audio.GetInstance().SetMasterVolume(a_value);

		if(a_value == 0.0f){
			this.slider_bgm.SetLock(true);
			this.slider_se.SetLock(true);
		}else{
			this.slider_bgm.SetLock(false);
			this.slider_se.SetLock(false);
		}
	}

	/** [Slider_Base]コールバック。変更。
	*/
	private void CallBack_Change_Bgm(int a_id,float a_value)
	{
		NAudio.Audio.GetInstance().SetBgmVolume(a_value);
	}

	/** [Slider_Base]コールバック。変更。
	*/
	private void CallBack_Change_Se(int a_id,float a_value)
	{
		NAudio.Audio.GetInstance().SetSeVolume(a_value);
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click_Unload(int a_id)
	{
		//ＳＥのアンロード。
		NAudio.Audio.GetInstance().UnLoadSe(SE_ID);

		//アセットバンドルのアンロード。
		NDownLoad.DownLoad.GetInstance().GetAssetBundleList().UnloadAssetBundle(ASSETBUNDLE_ID_SE);
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click_AssetBundle(int a_id)
	{
		if(this.mode == Mode.Wait){
			this.soundpool_flag = false;
			this.mode = Mode.Start;
		}
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click_SoundPool(int a_id)
	{
		if(this.mode == Mode.Wait){
			this.soundpool_flag = true;
			this.mode = Mode.Start;
		}
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click_Bgm(int a_id)
	{
		if(this.download_item_bgm == null){
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

			this.download_item_bgm = NDownLoad.DownLoad.GetInstance().RequestAssetBundle(t_url + "bgm",ASSETBUNDLE_ID_BGM,DATA_VERSION);
		}
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//ダウンロード。
		NDownLoad.DownLoad.GetInstance().Main();

		//セーブロード。
		NSaveLoad.SaveLoad.GetInstance().Main();

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＢＧＭ。
		if(this.download_item_bgm != null){
			if(this.download_item_bgm.IsBusy() == true){
				//ダウンロード中。
				this.status.SetText("bgm : " + this.download_item_bgm.GetResultProgress().ToString());
			}else{
				if(this.download_item_bgm.GetResultDataType() == NDownLoad.DataType.AssetBundle){
					//ダウンロード成功。アセットバンドル。
					AssetBundle t_assetbundle = this.download_item_bgm.GetResultAssetBundle();
					if(t_assetbundle != null){
						GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("bgm");
						if(t_prefab != null){
							NAudio.Pack_AudioClip t_pack_audioclip = t_prefab.GetComponent<NAudio.Pack_AudioClip>();
							if(t_pack_audioclip != null){
								NAudio.Audio.GetInstance().LoadBgm(t_pack_audioclip);
								NAudio.Audio.GetInstance().PlayBgm(0);
							}
						}
					}
				}
				this.download_item_bgm = null;

				this.status.SetText("bgm : end");
			}
		}

		switch(this.mode){
		case Mode.Wait:
			{
			}break;
		case Mode.Start:
			{
				string t_name = "se";

				if(this.soundpool_flag == true){
					string t_url = "http://bbbproject.sakura.ne.jp/www/project_webgl/fee/AssetBundle/Raw/" + t_name + ".txt";
					this.download_item_se = NDownLoad.DownLoad.GetInstance().RequestSoundPool(t_url,DATA_VERSION);
				}else{
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

					this.download_item_se = NDownLoad.DownLoad.GetInstance().RequestAssetBundle(t_url + "se",ASSETBUNDLE_ID_SE,DATA_VERSION);
				}

				this.mode = Mode.Now;
			}break;
		case Mode.Now:
			{
				if(this.download_item_se.IsBusy() == true){
					//ダウンロード中。
					this.status.SetText("se : " + this.download_item_se.GetResultProgress().ToString());
				}else{
					if(this.download_item_se.GetResultDataType() == NDownLoad.DataType.SoundPool){
						//ダウンロード成功。サウンドプール。

						this.pack_soundpool = this.download_item_se.GetResultSoundPool();
						if(this.pack_soundpool == null){
							//不正なサウンドプールパック。
							this.status.SetText("Error : " + this.mode.ToString());
							this.download_item_se = null;
							this.mode = Mode.Wait;
						}else{
							this.download_item_se = null;
							this.mode = Mode.Fix;
						}
					}else if(this.download_item_se.GetResultDataType() == NDownLoad.DataType.AssetBundle){
						//ダウンロード成功。アセットバンドル。

						AssetBundle t_assetbundle = this.download_item_se.GetResultAssetBundle();
						if(t_assetbundle != null){
							GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("se");
							if(t_prefab != null){
								this.pack_audioclip = t_prefab.GetComponent<NAudio.Pack_AudioClip>();
							}
						}
						if(this.pack_audioclip == null){
							//不正なオーディオクリップパック。
							this.status.SetText("Error : " + this.mode.ToString());
							this.download_item_se = null;
							this.mode = Mode.Wait;
						}else{
							this.download_item_se = null;
							this.mode = Mode.Fix;
						}
					}else{
						//ダウンロード失敗。
						this.status.SetText("Error : " + this.download_item_se.GetResultErrorString());
						this.download_item_se = null;
						this.mode = Mode.Wait;
					}
				}
			}break;
		case Mode.Fix:
			{
				this.status.SetText("Success");

				if(this.soundpool_flag == true){
					NAudio.Audio.GetInstance().LoadSe(this.pack_soundpool,SE_ID);
				}else{
					NAudio.Audio.GetInstance().LoadSe(this.pack_audioclip,SE_ID);
				}

				this.pack_audioclip = null;
				this.pack_soundpool = null;
				this.mode = Mode.Wait;
			}break;
		}

		//再生。
		if(NInput.Mouse.GetInstance().InRectCheck(ref NRender2D.Render2D.VIRTUAL_RECT_MAX)){
			if(NInput.Mouse.GetInstance().left.down == true){
				NAudio.Audio.GetInstance().PlaySe(SE_ID,0);
			}
		}

		this.status_2.SetText("soundpool = " + NAudio.Audio.GetInstance().GetSoundPoolCount().ToString() + " assetbundle = " + NDownLoad.DownLoad.GetInstance().GetAssetBundleCount().ToString());
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

	/** ファイルコピー。
	*/
	#if(UNITY_EDITOR)
	private static void CopyFile(string a_filename,string a_path_from,string a_path_to)
	{
		byte[] t_binary = null;

		{
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_path_from + "/" + a_filename);
			t_binary = new byte[t_fileinfo.Length];

			System.IO.FileStream t_filestream_read = null;

			try{
				//open
				t_filestream_read = t_fileinfo.OpenRead();

				//read
				if(t_filestream_read != null){
					t_filestream_read.Read(t_binary,0,t_binary.Length);
				}
			}catch(System.Exception){
				t_binary = null;

				Debug.Assert(false);
			}

			//close
			if(t_filestream_read != null){
				t_filestream_read.Close();
				t_filestream_read = null;
			}
		}

		if(t_binary != null){
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_path_to + "/" + a_filename);
			System.IO.FileStream t_filestream_write = null;

			try{
				//open
				t_filestream_write = t_fileinfo.OpenWrite();

				//write
				if(t_filestream_write != null){
					t_filestream_write.Write(t_binary,0,t_binary.Length);
				}
			}catch(System.Exception){
				Debug.Assert(false);
			}

			//close
			if(t_filestream_write != null){
				t_filestream_write.Close();
				t_filestream_write = null;
			}
		}else{
			Debug.Assert(false);
		}
	}
	#endif

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
					string t_asset_fullpath = "";
					string t_audio_name = "";
					if(t_pack_audioclip.audioclip_list[ii] != null){
						string t_asset_path = UnityEditor.AssetDatabase.GetAssetPath(t_pack_audioclip.audioclip_list[ii]);
						if(t_asset_path != null){
							string t_name = System.IO.Path.GetFileName(t_asset_path);
							if(t_name != null){
								t_audio_name = t_name;
							}
							t_asset_fullpath = Application.dataPath + "/" + System.IO.Path.GetDirectoryName(t_asset_path).Substring(7);
						}
					}

					//volume
					t_pack_soundpool.name_list.Add(t_audio_name);

					//name
					t_pack_soundpool.volume_list.Add(t_audio_volume);

					//Rawフォルダにコピー。
					if(t_asset_fullpath != null){
						CopyFile(t_audio_name,t_asset_fullpath,Application.dataPath + "/AssetBundle/Raw");
					}
				}
			}

			t_pack_soundpool.data_hash = t_pack_soundpool.GetHashCode();
		}

		//Rawフォルダに保存。
		{
			NJsonItem.JsonItem t_jsonitem = NJsonItem.ObjectToJson.Convert(t_pack_soundpool);
			string t_json_string = t_jsonitem.ConvertJsonString();

			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(Application.dataPath + "/AssetBundle/Raw/" + t_assetbundle_name + ".txt");
			System.IO.StreamWriter t_stream_writer = null;

			try{
				//open
				t_stream_writer = t_fileinfo.CreateText();

				//write
				if(t_stream_writer != null){
					t_stream_writer.Write(t_json_string);
					t_stream_writer.Flush();
				}
			}catch(System.Exception){
			}

			//close
			if(t_stream_writer != null){
				t_stream_writer.Close();
				t_stream_writer = null;
			}
		}

		UnityEditor.AssetDatabase.Refresh();
	}
	#endif

	/** 作成。
	*/
	#if(UNITY_EDITOR)
	[UnityEditor.MenuItem("Test/test11/MakeAssetBundle/SWA")]
	private static void MakeAssetBundle_SWA()
	{
		MakeAssetBundle_StandaloneWindows();
		MakeAssetBundle_WebGL();
		MakeAssetBundle_Android();
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

