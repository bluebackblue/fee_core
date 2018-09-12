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

	ボタン

*/
public class test09 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** ボタン。
	*/
	private NUi.Button button;

	/** タイム。
	*/
	float time;

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
		NUi.Ui.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//テクスチャ。
		Texture2D t_texture = Resources.Load<Texture2D>("button");
		t_texture.filterMode = FilterMode.Point;
		t_texture.wrapMode = TextureWrapMode.Clamp;

		//ボタン。
		{
			int t_w = 100;
			int t_h = 100;
			int t_x = (NRender2D.Render2D.VIRTUAL_W - t_w) / 2;
			int t_y = (NRender2D.Render2D.VIRTUAL_H - t_h) / 2;

			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP + 0;
			this.button = new NUi.Button(this.deleter,null,t_drawpriority,Click,0);
			this.button.SetRect(t_x,t_y,t_w,t_h);
			this.button.SetTexture(t_texture);
		}

		//タイム。
		this.time = 0.0f;
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ
		NUi.Ui.GetInstance().Main();

		this.time += 0.01f;

		{
			int t_w = 40 + (int)(100 + Mathf.Sin(this.time) * 100);
			int t_h = 40 + (int)(100 + Mathf.Cos(this.time) * 100);
			int t_x = (NRender2D.Render2D.VIRTUAL_W - t_w) / 2;
			int t_y = (NRender2D.Render2D.VIRTUAL_H - t_h) / 2;

			this.button.SetRect(t_x,t_y,t_w,t_h);
		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}

	/** クリック。
	*/
	public void Click(int a_value)
	{
		Debug.Log("Click :" + a_value);
	}
}

