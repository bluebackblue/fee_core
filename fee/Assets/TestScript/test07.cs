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


/** test07

	チェックボタン。

*/
public class test07 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** チェックボタン。
	*/
	private NUi.CheckButton checkbutton_free;
	private NUi.CheckButton checkbutton_lock;


	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マスウ。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//フォント。
		Font t_font = Resources.Load<Font>("mplus-1p-medium");
		if(t_font != null){
			NRender2D.Render2D.GetInstance().SetDefaultFont(t_font);
		}

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//描画プライオリティ。
		int t_layerindex = 1;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		int t_yy = 100;

		//チェックボタン。
		this.checkbutton_free = new NUi.CheckButton(this.deleter,null,t_drawpriority,this.Click,0);
		this.checkbutton_free.SetTexture(Resources.Load<Texture2D>("checkbutton"));
		this.checkbutton_free.SetRect(100,t_yy,30,30);
		this.checkbutton_free.SetText("テキストボックス");
		t_yy += 50;

		this.checkbutton_lock = new NUi.CheckButton(this.deleter,null,t_drawpriority,this.Click,1);
		this.checkbutton_lock.SetTexture(Resources.Load<Texture2D>("checkbutton"));
		this.checkbutton_lock.SetRect(100,t_yy,30,30);
		this.checkbutton_lock.SetCheck(true);
		this.checkbutton_lock.SetLock(true);
		this.checkbutton_lock.SetText("テキストボックス");
	}

	/** クリック。
	*/
	private void Click(int a_value,bool a_flag)
	{
		if(a_value == 0){
			this.checkbutton_free.SetText("テキストボックス" + a_flag.ToString());
		}else{
			this.checkbutton_lock.SetText("テキストボックス" + a_flag.ToString());
		}
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

