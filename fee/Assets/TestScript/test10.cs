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

	/** Mode
	*/
	private enum Mode
	{
		None,
		Blur,
		Bloom,
		BlurBloom,
	}

	/** mode
	*/
	private Mode mode;

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

		//ブラー。インスタンス作成。
		NBlur.Blur.CreateInstance();

		//ブルーム。インスタンス作成。
		NBloom.Bloom.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		//NUi.Config.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();


		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(NRender2D.Render2D.MAX_LAYER - 1) * NRender2D.Render2D.DRAWPRIORITY_STEP);

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

		this.mode = Mode.None;
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

		if(NInput.Mouse.GetInstance().left.down == true){
			if(NInput.Mouse.GetInstance().InRectCheck(ref NRender2D.Render2D.VIRTUAL_RECT_MAX)){
				switch(this.mode){
				case Mode.None:			this.mode = Mode.Blur;		break;
				case Mode.Blur:			this.mode = Mode.Bloom;		break;
				case Mode.Bloom:		this.mode = Mode.BlurBloom;	break;
				case Mode.BlurBloom:	this.mode = Mode.None;		break;
				}

				switch(this.mode){
				case Mode.None:
					{
						NBlur.Blur.GetInstance().SetEnable(false);
						NBloom.Bloom.GetInstance().SetEnable(false);
					}break;
				case Mode.Blur:
					{
						NBlur.Blur.GetInstance().SetEnable(true);
						NBloom.Bloom.GetInstance().SetEnable(false);
					}break;
				case Mode.Bloom:
					{
						NBlur.Blur.GetInstance().SetEnable(false);
						NBloom.Bloom.GetInstance().SetEnable(true);
					}break;
				case Mode.BlurBloom:
					{
						NBlur.Blur.GetInstance().SetEnable(true);
						NBloom.Bloom.GetInstance().SetEnable(true);
					}break;
				}
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

