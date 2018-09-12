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


/** test13

	ＢＧＭ

*/
public class test13 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** volume_master_bar
	*/
	private NRender2D.Sprite2D volume_master_bar_bg;
	private NRender2D.Sprite2D volume_master_bar;

	/** volume_bgm_bar
	*/
	private NRender2D.Sprite2D volume_bgm_bar_bg;
	private NRender2D.Sprite2D volume_bgm_bar;

	/** download_bgm
	*/
	private NDownLoad.Item download_bgm;

	/** text
	*/
	private NRender2D.Text2D text;

	/** ASSETBUNDLE_ID_BGM
	*/
	private const long ASSETBUNDLE_ID_BGM = 0x00000001;

	/** DATA_VERSION
	*/
	private const int DATA_VERSION = 4;

	/** bgm_index
	*/
	private int bgm_index;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//オーディオ。インスタンス作成。
		//NAudio.Config.ASSERT_ENABLE = true;
		//NAudio.Config.LOG_ENABLE = true;
		//NAudio.Config.BGM_PLAY_FADEIN = false;
		NAudio.Audio.CreateInstance();

		//ダウンロード。インスタンス作成。
		NDownLoad.Config.LOG_ENABLE = true;
		NDownLoad.DownLoad.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//ＢＧＭダウンロード。
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

			this.download_bgm = NDownLoad.DownLoad.GetInstance().RequestAssetBundle(t_url + "bgm",ASSETBUNDLE_ID_BGM,DATA_VERSION);
		}

		//ＢＧＭインデックス。
		this.bgm_index = -1;

		{
			//drawpriority
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

			int t_y = 280;

			//text
			this.text = new NRender2D.Text2D(this.deleter,null,t_drawpriority + 10);
			this.text.SetRect(100,t_y,0,0);
			this.text.SetColor(0.0f,0.0f,0.0f,1.0f);

			t_y = 300;

			//volume_master_bar_bg
			this.volume_master_bar_bg = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority + 1);
			this.volume_master_bar_bg.SetTexture(Texture2D.whiteTexture);
			this.volume_master_bar_bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.volume_master_bar_bg.SetRect(100,t_y,300,30);
			this.volume_master_bar_bg.SetColor(0.0f,0.0f,0.0f,1.0f);

			//volume_master_bar
			this.volume_master_bar = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority + 2);
			this.volume_master_bar.SetTexture(Texture2D.whiteTexture);
			this.volume_master_bar.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.volume_master_bar.SetRect(100,t_y,0,30);
			this.volume_master_bar.SetColor(0.5f,1.0f,0.5f,1.0f);

			t_y = 350;

			//volume_bgm_bar
			this.volume_bgm_bar_bg = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority + 1);
			this.volume_bgm_bar_bg.SetTexture(Texture2D.whiteTexture);
			this.volume_bgm_bar_bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.volume_bgm_bar_bg.SetRect(100,t_y,300,30);
			this.volume_bgm_bar_bg.SetColor(0.0f,0.0f,0.0f,1.0f);

			//volume_bgm_bar
			this.volume_bgm_bar = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority + 2);
			this.volume_bgm_bar.SetTexture(Texture2D.whiteTexture);
			this.volume_bgm_bar.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.volume_bgm_bar.SetRect(100,t_y,0,30);
			this.volume_bgm_bar.SetColor(0.5f,1.0f,0.5f,1.0f);
		}
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//ダウンロード。
		NDownLoad.DownLoad.GetInstance().Main();

		//ダウンロード。
		if(this.download_bgm != null){
			if(this.download_bgm.IsBusy() == false){
				AssetBundle t_assetbundle = this.download_bgm.GetResultAssetBundle();
				if(t_assetbundle != null){
					GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("bgm");
					if(t_prefab != null){
						NAudio.ClipPack t_clippack = t_prefab.GetComponent<NAudio.ClipPack>();
						if(t_clippack != null){
							NAudio.Audio.GetInstance().LoadBgm(t_clippack);
						}
					}
				}
				this.download_bgm = null;
			}
		}

		//クリックチェック。
		bool t_onover_volume = false;
		if(NInput.Mouse.GetInstance().left.on == true){
			if(NInput.Mouse.GetInstance().InRectCheck(this.volume_master_bar_bg.GetX(),this.volume_master_bar_bg.GetY(),this.volume_master_bar_bg.GetW(),this.volume_master_bar_bg.GetH())){
				//ボリューム変更。マスター。
				float t_volume = (NInput.Mouse.GetInstance().pos.x - this.volume_master_bar_bg.GetX()) / (float)this.volume_master_bar_bg.GetW();
				NAudio.Audio.GetInstance().SetMasterVolume(t_volume);

				t_onover_volume = true;
			}else if(NInput.Mouse.GetInstance().InRectCheck(this.volume_bgm_bar_bg.GetX(),this.volume_bgm_bar_bg.GetY(),this.volume_bgm_bar_bg.GetW(),this.volume_bgm_bar_bg.GetH())){
				//ボリューム変更。ＢＧＭ。
				float t_volume = (NInput.Mouse.GetInstance().pos.x - this.volume_bgm_bar_bg.GetX()) / (float)this.volume_bgm_bar_bg.GetW();
				NAudio.Audio.GetInstance().SetBgmVolume(t_volume);

				t_onover_volume = true;
			}
		}

		//再生。
		if(NInput.Mouse.GetInstance().left.down == true){
			if(t_onover_volume == false){
				this.bgm_index++;
				if(this.bgm_index >= NAudio.Audio.GetInstance().GetBgmMax()){
					this.bgm_index = -1;
				}

				if(this.bgm_index < 0){
					NAudio.Audio.GetInstance().StopBgm();
				}else{
					NAudio.Audio.GetInstance().PlayBgm(this.bgm_index);
				}
			}
		}

		//ボリューに合わせてバーの長さを変更。
		{
			this.volume_master_bar.SetW((int)(NAudio.Audio.GetInstance().GetMasterVolume() * this.volume_master_bar_bg.GetW()));
			this.volume_bgm_bar.SetW((int)(NAudio.Audio.GetInstance().GetBgmVolume() * this.volume_bgm_bar_bg.GetW()));
		}

		{
			int t_loopcount = NAudio.Audio.GetInstance().GetBgmLoopCount();
			float t_playposition = NAudio.Audio.GetInstance().GetBgmPlayPosition();

			string t_bgm_max = "";
			if(this.download_bgm != null){
				t_bgm_max = "DownLoad(" + this.download_bgm.GetProgress().ToString() + ")";
			}else{
				t_bgm_max = NAudio.Audio.GetInstance().GetBgmMax().ToString();
			}

			this.text.SetText(this.bgm_index.ToString() + " / " + t_bgm_max  + " : " + t_loopcount.ToString() + " : " + t_playposition.ToString());
		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

