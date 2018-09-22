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


/** test10

	ブラー

*/
public class test10 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** sprite
	*/
	private NRender2D.Sprite2D sprite;

	/** flag
	*/
	private bool flag;

	/** Start
	*/
	private void Start()
	{
		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//ブラー。インスタンス作成。
		NBlur.Blur.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//スプライト。
		{
			int t_w = 200;
			int t_h = 200;
			int t_x = (NRender2D.Render2D.VIRTUAL_W - t_w) / 2;
			int t_y = (NRender2D.Render2D.VIRTUAL_H - t_h) / 2;

			int t_layerindex = 0;
			long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;
			this.sprite = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
			this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite.SetRect(t_x,t_y,t_w,t_h);
			this.sprite.SetTexture(Resources.Load<Texture2D>("IMGP8657"));
		}

		this.flag =  false;
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		if(NInput.Mouse.GetInstance().left.down == true){
			this.flag = !this.flag;
			NBlur.Blur.GetInstance().SetEnable(this.flag);
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

