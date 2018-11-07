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


/** test21
*/
public class test21 : main_base
{
	/** Step
	*/
	private enum Step
	{
		Init,

		Dialog_Start,
		Dialog_Do,

		LoadVrm_Start,
		LoadVrm_Do,

		UploadVrm_Start,
		UploadVrm_Do,

		End,
	};

	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** step
	*/
	private Step step;

	/** start_button
	*/
	private NUi.Button start_button;

	/** status_text;
	*/
	private NRender2D.Text2D status_text;

	/** url
	*/
	private string url;

	/** file_vrm
	*/
	private NFile.Item file_vrm;

	/** binary_vrm
	*/
	private byte[] binary_vrm;

	/** upload_vrm
	*/
	private NFile.Item upload_vrm;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.Config.LOG_ENABLE = true;
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//キー。インスタンス作成。
		NInput.Key.CreateInstance();

		//パッド。インスタンス作成。
		NInput.Pad.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//ファイル。インスタンス作成。
		NFile.File.CreateInstance();

		//ＵＮＩＶＲＭ。インスタンス作成。
		NUniVrm.UniVrm.CreateInstance();

		//フォント。
		Font t_font = Resources.Load<Font>("mplus-1p-medium");
		if(t_font != null){
			NRender2D.Render2D.GetInstance().SetDefaultFont(t_font);
		}

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

		//step
		this.step = Step.Init;

		//start_button
		{
			this.start_button = new NUi.Button(this.deleter,null,0,this.CallBack_Click_StartButton,-1);
			this.start_button.SetTexture(Resources.Load<Texture2D>("button"));
			this.start_button.SetRect(100,100,100,50);
			this.start_button.SetText("開始");
		}

		//status_text
		{
			this.status_text = new NRender2D.Text2D(this.deleter,null,0);
			this.status_text.SetRect(100,150,0,0);
			this.status_text.SetText("---");
		}

		//url
		this.url = null;

		//file_vrm
		this.file_vrm = null;

		//bianry_vrm
		this.binary_vrm = null;

		//upload_vrm
		this.upload_vrm = null;
	}

	/** [Button_Base]コールバック。クリック。
	*/
	public void CallBack_Click_StartButton(int a_id)
	{
		if(this.step == Step.Init){
			this.step = Step.Dialog_Start;
		}
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//キー。
		NInput.Key.GetInstance().Main();

		//パッド。
		NInput.Pad.GetInstance().Main();

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//ファイル。
		NFile.File.GetInstance().Main();

		//ＵＮＩＶＲＭ。
		NUniVrm.UniVrm.GetInstance().Main();

		switch(this.step){
		case Step.Init:
			{
			}break;
		case Step.Dialog_Start:
			{
				//ダイアログ。開始。
				this.status_text.SetText(this.step.ToString());

				this.step = Step.Dialog_Do;
			}break;
		case Step.Dialog_Do:
			{
				//ダイアログ。処理中。
				this.status_text.SetText(this.step.ToString());

				//TODO:ＵＲＬ固定。
				this.url = "https://bbbproject.sakura.ne.jp/www/project_webgl/fee/StreamingAssets/nana.vrmx";

				this.step = Step.LoadVrm_Start;
			}break;
		case Step.LoadVrm_Start:
			{
				//ＶＲＭ読み込み。開始。
				this.status_text.SetText(this.step.ToString());

				//ＵＲＬから。
				this.file_vrm = NFile.File.GetInstance().RequestDownLoadBinaryFile(this.url,null,NFile.ProgressMode.DownLoad);
				this.step = Step.LoadVrm_Do;
			}break;
		case Step.LoadVrm_Do:
			{
				//ＶＲＭ読み込み。処理中。

				if(this.file_vrm.IsBusy() == true){
					this.status_text.SetText(this.step.ToString() + ":" + this.file_vrm.GetResultProgress().ToString());
				}else{
					if(this.file_vrm.GetResultType() == NFile.Item.ResultType.Binary){
						this.binary_vrm = this.file_vrm.GetResultBinary();
						this.step = Step.UploadVrm_Start;
					}else{
						this.step = Step.End;
					}
				}
			}break;
		case Step.UploadVrm_Start:
			{
				//ＶＲＭアップロード。開始。
				this.status_text.SetText(this.step.ToString());

				WWWForm t_post_data = new WWWForm();

				//発行したapikey。
				t_post_data.AddField("apikey_token","0CBaQJfJ4WTcAkO919b8tIO+IiIV2JcCqZzPem0Wv91TeBP/IT4DzhqAQic+tUwb9");

				//発行時に設定したパスワード。
				t_post_data.AddField("apikey_pass","password");

				//ＶＲＭファイル。
				t_post_data.AddBinaryData("file_vrm",this.binary_vrm);

				//リクエスト。
				this.upload_vrm = NFile.File.GetInstance().RequestDownLoadTextFile("https://bbbproject.sakura.ne.jp/www/project_gameparam/api/vrm/set/",t_post_data,NFile.ProgressMode.UpLoad);

				this.step = Step.UploadVrm_Do;
			}break;
		case Step.UploadVrm_Do:
			{
				//ＶＲＭアップロード。処理中。

				if(this.upload_vrm.IsBusy() == true){
					this.status_text.SetText(this.step.ToString() + ":" + this.upload_vrm.GetResultProgress().ToString());
				}else{
					if(this.upload_vrm.GetResultType() == NFile.Item.ResultType.Text){
						this.status_text.SetText(this.step.ToString() + ":" + this.upload_vrm.GetResultText());
						this.step = Step.End;
					}else{
						this.status_text.SetText(this.step.ToString() + ":" + "error");
						this.step = Step.End;
					}
				}
			}break;
		case Step.End:
			{
			}break;
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

