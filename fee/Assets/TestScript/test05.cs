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


/** test05
*/
public class test05 : main_base
{
	/** 削除管理。
	*/
	NDeleter.Deleter deleter;

	/** 背景。
	*/
	NRender2D.Sprite2D sprite_bg;

	/** テキスト。
	*/
	NRender2D.Text2D text_mouse;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//ジョイスティック。インスタンス作成。
		NInput.Joy.CreateInstance();

		//キー。インスタンス作成。
		NInput.Key.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//背景。
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;
		this.sprite_bg = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
		this.sprite_bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
		this.sprite_bg.SetTexture(Texture2D.whiteTexture);
		this.sprite_bg.SetRect(ref NRender2D.Render2D.VIRTUAL_RECT_MAX);
		this.sprite_bg.SetMaterialType(NRender2D.Config.MaterialType.Alpha);
		this.sprite_bg.SetColor(0.0f,0.0f,0.0f,0.5f);

		//テキスト。
		this.text_mouse = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text_mouse.SetRect(10,10,0,0);
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//ジョイスティック。
		NInput.Joy.GetInstance().Main();

		//キー。
		NInput.Key.GetInstance().Main();

		//ホイール。
		if(NInput.Mouse.GetInstance().mouse_wheel_action == true){
			if(NInput.Mouse.GetInstance().mouse_wheel > 0){
				Debug.Log("mouse_wheel : -");
			}else{
				Debug.Log("mouse_wheel : +");
			}
		}

		//キー。
		if(NInput.Key.GetInstance().enter.down == true){
			NInput.Mouse.GetInstance().SetVisible(false);
			NInput.Mouse.GetInstance().SetLock(true);
			Debug.Log("key.enter.down");
		}

		if(NInput.Key.GetInstance().escape.down == true){
			NInput.Mouse.GetInstance().SetVisible(true);
			NInput.Mouse.GetInstance().SetLock(false);
			Debug.Log("key.escape.down");
		}

		//マウス位置。
		this.text_mouse.SetText("x = " + NInput.Mouse.GetInstance().pos.x.ToString() + " y = " + NInput.Mouse.GetInstance().pos.y.ToString());
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

