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


/** test03

	ＵＮＩＶＲＭ。

*/
public class test03 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** ステータス。
	*/
	private NRender2D.Text2D status;

	/** ダウンロードアイテム。
	*/
	private NDownLoad.Item download_item;

	/** バイナリ。
	*/
	private byte[] binary;

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

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//ダウンロード。インスタンス作成。
		NDownLoad.DownLoad.CreateInstance();

		//ＵＮＩＶＲＭ。インスタンス作成。
		NUniVrm.UniVrm.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//layerindex
		int t_layerindex = 0;

		//drawpriority
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		//ステータス。
		this.status = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.status.SetRect(100,100,0,0);

		//string t_full_path = Application.streamingAssetsPath + "/nana.vrmx";
		#if(true)
		this.download_item = NDownLoad.DownLoad.GetInstance().Request("http://bbbproject.sakura.ne.jp/www/project_webgl/fee/StreamingAssets/nana.vrmx",NDownLoad.DataType.Binary);
		this.binary = null;
		#else
		this.download_item = null;
		this.binary = System.IO.File.ReadAllBytes(t_full_path);
		#endif
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//ダウンロード。
		NDownLoad.DownLoad.GetInstance().Main();

		if(this.download_item != null){
			if(this.download_item.IsBusy() == true){
				//ダウンロード中。
				this.status.SetText("Download : " + this.download_item.GetResultProgress().ToString());
			}else{
				//ダウンロード完了。
				if(this.download_item.GetResultDataType() == NDownLoad.DataType.Binary){
					this.binary = this.download_item.GetResultBinary();
				}else{
					this.status.SetText("Download : Error");
				}
				this.download_item = null;
			}
		}
		
		if(this.binary != null){
			this.status.SetText("Create : size = " + this.binary.Length.ToString());
			NUniVrm.UniVrm.GetInstance().Create(this.binary);
			this.binary = null;
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

