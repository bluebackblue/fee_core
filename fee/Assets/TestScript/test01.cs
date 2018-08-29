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


/** test01

	sprite
	仮想スクリーンサイズと同じサイズのスプライトを設置。

	text
	中央に文字を設置。

	inputfield
	テキストの下に入力フィールドを設置。

*/
public class test01 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** sprite
	*/
	private NRender2D.Sprite2D sprite;

	/** text
	*/
	private NRender2D.Text2D text;

	/** inputfield
	*/
	private NRender2D.InputField2D inputfield;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。
		NRender2D.Render2D.CreateInstance();

		//deleter
		this.deleter = new NDeleter.Deleter();

		//layerindex
		int t_layerindex = 0;

		//drawpriority
		long t_drawpriority = NRender2D.Render2D.DRAWPRIORITY_STEP * t_layerindex;

		//sprite
		this.sprite = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
		this.sprite.SetTexture(Texture2D.whiteTexture);
		this.sprite.SetTextureRect(0,0,NRender2D.Render2D.TEXTURE_W,NRender2D.Render2D.TEXTURE_H);
		this.sprite.SetRect(0,0,NRender2D.Render2D.VIRTUAL_W,NRender2D.Render2D.VIRTUAL_H);
		this.sprite.SetColor(0.0f,0.5f,0.0f,1.0f);
		this.sprite.SetMaterialType(NRender2D.Config.MaterialType.Normal);

		//text
		this.text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text.SetCenter(true);
		this.text.SetRect(NRender2D.Render2D.VIRTUAL_W / 2,NRender2D.Render2D.VIRTUAL_H / 2,0,0);
		this.text.SetText("abcdefghijklmnopqrstuvwxyz");
		this.text.SetColor(0.0f,0.0f,0.0f,1.0f);

		//inputfield
		{
			int t_w = 200;
			int t_h = 200;
			int t_x = (NRender2D.Render2D.VIRTUAL_W - t_w) / 2;
			int t_y = (NRender2D.Render2D.VIRTUAL_H / 2) + 50 ;
		
			this.inputfield = new NRender2D.InputField2D(this.deleter,null,t_drawpriority);
			this.inputfield.SetRect(t_x,t_y,t_w,t_h);
			this.inputfield.SetText("default_text");
			this.inputfield.SetMultiLine(true);
		}
	}

	/** Update
	*/
	private void Update()
	{
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

