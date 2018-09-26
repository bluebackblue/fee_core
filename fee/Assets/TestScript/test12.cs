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


/** test12

	ＳＥ

*/
public class test12 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** volume_master_bar
	*/
	private NRender2D.Sprite2D volume_master_bar_bg;
	private NRender2D.Sprite2D volume_master_bar;

	/** volume_se_bar_bg
	*/
	private NRender2D.Sprite2D volume_se_bar_bg;
	private NRender2D.Sprite2D volume_se_bar;

	/** download_se
	*/
	private NDownLoad.Item download_se;

	/** SE_ID_TESTDATA
	*/
	private const long SE_ID_TESTDATA = 0x12345678;

	/** ASSETBUNDLE_ID_SE
	*/
	private const long ASSETBUNDLE_ID_SE = 0x00000002;

	/** DATA_VERSION
	*/
	private const int DATA_VERSION = 3;

	/** Start
	*/
	private void Start()
	{
		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//オーディオ。インスタンス作成。
		NAudio.Audio.CreateInstance();

		//ダウンロード。インスタンス作成。
		NDownLoad.DownLoad.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//ＳＥダウンロード。
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

			this.download_se = NDownLoad.DownLoad.GetInstance().RequestAssetBundle(t_url + "se",ASSETBUNDLE_ID_SE,DATA_VERSION);
		}

		{
			//drawpriority
			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

			int t_y = 300;

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

			//volume_se_bar_bg
			this.volume_se_bar_bg = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority + 1);
			this.volume_se_bar_bg.SetTexture(Texture2D.whiteTexture);
			this.volume_se_bar_bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.volume_se_bar_bg.SetRect(100,t_y,300,30);
			this.volume_se_bar_bg.SetColor(0.0f,0.0f,0.0f,1.0f);

			//volume_se_bar
			this.volume_se_bar = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority + 2);
			this.volume_se_bar.SetTexture(Texture2D.whiteTexture);
			this.volume_se_bar.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.volume_se_bar.SetRect(100,t_y,0,30);
			this.volume_se_bar.SetColor(0.5f,1.0f,0.5f,1.0f);
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

		//クリックチェック。
		bool t_onover_volume = false;
		if(NInput.Mouse.GetInstance().left.on == true){
			if(NInput.Mouse.GetInstance().InRectCheck(this.volume_master_bar_bg.GetX(),this.volume_master_bar_bg.GetY(),this.volume_master_bar_bg.GetW(),this.volume_master_bar_bg.GetH())){
				//ボリューム変更。マスター。
				float t_volume = (NInput.Mouse.GetInstance().pos.x - this.volume_master_bar_bg.GetX()) / (float)this.volume_master_bar_bg.GetW();
				NAudio.Audio.GetInstance().SetMasterVolume(t_volume);

				t_onover_volume = true;
			}else if(NInput.Mouse.GetInstance().InRectCheck(this.volume_se_bar_bg.GetX(),this.volume_se_bar_bg.GetY(),this.volume_se_bar_bg.GetW(),this.volume_se_bar_bg.GetH())){
				//ボリューム変更。ＳＥ。
				float t_volume = (NInput.Mouse.GetInstance().pos.x - this.volume_se_bar_bg.GetX()) / (float)this.volume_se_bar_bg.GetW();
				NAudio.Audio.GetInstance().SetSeVolume(t_volume);

				t_onover_volume = true;
			}
		}

		//ダウンロード。
		if(this.download_se != null){
			if(this.download_se.IsBusy() == false){
				AssetBundle t_assetbundle = this.download_se.GetResultAssetBundle();
				if(t_assetbundle != null){
					GameObject t_prefab = t_assetbundle.LoadAsset<GameObject>("se");
					if(t_prefab != null){
						NAudio.Pack_AudioClip t_pack = t_prefab.GetComponent<NAudio.Pack_AudioClip>();
						if(t_pack != null){
							NAudio.Audio.GetInstance().LoadSe(t_pack,SE_ID_TESTDATA);
						}
					}
				}
				this.download_se = null;
			}
		}

		//再生。
		if(NInput.Mouse.GetInstance().left.down == true){
			if(t_onover_volume == false){
				int t_index = 3;
				NAudio.Audio.GetInstance().PlaySe(SE_ID_TESTDATA,t_index);
			}
		}else if(NInput.Mouse.GetInstance().right.down == true){
			if(t_onover_volume == false){
				int t_index = 0;
				NAudio.Audio.GetInstance().PlaySe(SE_ID_TESTDATA,t_index);
			}
		}

		//ボリューに合わせてバーの長さを変更。
		{
			this.volume_master_bar.SetW((int)(NAudio.Audio.GetInstance().GetMasterVolume() * this.volume_master_bar_bg.GetW()));
			this.volume_se_bar.SetW((int)(NAudio.Audio.GetInstance().GetSeVolume() * this.volume_se_bar_bg.GetW()));
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
}

