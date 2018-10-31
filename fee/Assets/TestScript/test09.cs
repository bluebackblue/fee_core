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


/** test09
*/
public class test09 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** text
	*/
	private NRender2D.Text2D text;

	/** item
	*/
	private NFile.Item item;

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
		//NUi.Config.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();

		//ファイル。インスタンス作成。
		NFile.File.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

		//text
		this.text = new NRender2D.Text2D(this.deleter,null,0);
		this.text.SetRect(100,100,0,0);
		this.text.SetText("---");

		//item
		this.item = NFile.File.GetInstance().RequestDownLoadTextFile("https://bbbproject.sakura.ne.jp/www/project_discord/main/",null,NFile.ProgressMode.DownLoad);
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		//ファイル。
		NFile.File.GetInstance().Main();

		if(this.item != null){
			if(this.item.IsBusy() == true){
			}else{
				if(this.item.GetResultType() == NFile.Item.ResultType.Text){
					this.text.SetText(this.item.GetResultText());
				}else{
					this.text.SetText("error");
				}
				this.item = null;
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
		this.deleter.DeleteAll();
	}
}

