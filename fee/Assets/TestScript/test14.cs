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


/** test14

	チェックボタン

*/
public class test14 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** checkbutton
	*/
	private NUi.CheckButton[] checkbutton;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		//NUi.Config.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//drawpriority
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;
		Texture2D t_texture = Resources.Load<Texture2D>("checkbutton");

		this.checkbutton = new NUi.CheckButton[10];
		for(int ii=0;ii<this.checkbutton.Length;ii++){
			int t_w = 20;
			int t_h = 20;
			int t_x = (NRender2D.Render2D.VIRTUAL_W - t_w) / 2;
			int t_y = 200 + ii * 30;

			this.checkbutton[ii] = new NUi.CheckButton(this.deleter,null,t_drawpriority);
			this.checkbutton[ii].SetRect(t_x,t_y,t_w,t_h);
			this.checkbutton[ii].SetTexture(t_texture);
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

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

