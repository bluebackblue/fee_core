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


/** test15

	クリップ

*/
public class test15 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** is_clip
	*/
	private bool is_clip;

	/** update_clip_rect
	*/
	private bool update_clip_rect;

	/** clipsprite
	*/
	private NUi.ClipSprite clipsprite;

	/** text
	*/
	private NRender2D.Text2D text;

	/** button
	*/
	private NUi.Button button;

	/** checkbutton
	*/
	private NUi.CheckButton checkbutton;

	/** inputfield
	*/
	private NRender2D.InputField2D inputfield;

	/** slider
	*/
	private NUi.Slider slider;

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

		//キ。インスタンス作成。
		NInput.Key.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//フォント。
		Font t_font = Resources.Load<Font>("mplus-1p-medium");
		if(t_font != null){
			NRender2D.Render2D.GetInstance().SetDefaultFont(t_font);
		}

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//is_clip
		this.is_clip = true;

		//update_clip_rect
		this.update_clip_rect = true;

		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		//clipsprite
		{
			Texture2D t_texture = Resources.Load<Texture2D>("IMGP8657");

			this.clipsprite = new NUi.ClipSprite(this.deleter,null,t_drawpriority + 1);
			this.clipsprite.SetRect(ref NRender2D.Render2D.VIRTUAL_RECT_MAX);
			this.clipsprite.SetTexture(Texture2D.whiteTexture);
			this.clipsprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.clipsprite.SetColor(0.7f,0.7f,0.7f,1.0f);
			this.clipsprite.SetTexture(t_texture);
		}

		//text
		{
			int t_x = NRender2D.Render2D.VIRTUAL_W / 2;
			int t_y = 100;

			this.text = new NRender2D.Text2D(this.deleter,null,t_drawpriority + 1);
			this.text.SetRect(t_x,t_y,0,0);
			this.text.SetText("ESC ENTER");
			this.text.SetCenter(true,true);
			this.text.SetColor(1.0f,0.0f,0.0f,1.0f);
			this.text.SetFontSize(27);
			this.text.SetShadow(true);
			this.text.SetOutLine(true);
		}

		//button
		{
			int t_w = 100;
			int t_h = 30;
			int t_x = (NRender2D.Render2D.VIRTUAL_W - t_w) /2;
			int t_y = 300;

			Texture2D t_texture = Resources.Load<Texture2D>("button");

			this.button = new NUi.Button(this.deleter,null,t_drawpriority + 2,this.CallBack_Click,0);
			this.button.SetRect(t_x,t_y,t_w,t_h);
			this.button.SetTexture(t_texture);
		}

		//checkbutton
		{
			int t_w = 50;
			int t_h = 50;
			int t_x = (NRender2D.Render2D.VIRTUAL_W - t_w) /2;
			int t_y = 200;

			Texture2D t_texture = Resources.Load<Texture2D>("checkbutton");

			this.checkbutton = new NUi.CheckButton(this.deleter,null,t_drawpriority + 2,this.CallBack_Change,0);
			this.checkbutton.SetRect(t_x,t_y,t_w,t_h);
			this.checkbutton.SetTexture(t_texture);
		}

		//inputfield
		{
			int t_w = 100;
			int t_h = 100;
			int t_x = 100;
			int t_y = 100;

			this.inputfield = new NRender2D.InputField2D(this.deleter,null,t_drawpriority + 2);
			this.inputfield.SetRect(t_x,t_y,t_w,t_h);
		}

		//slider
		{
			int t_w = 300;
			int t_h = 20;
			int t_x = (NRender2D.Render2D.VIRTUAL_W - t_w) /2;
			int t_y = 350;

			this.slider = new NUi.Slider(this.deleter,null,t_drawpriority + 2,this.CallBack_Change,0);
			this.slider.SetRect(t_x,t_y,t_w,t_h);
			this.slider.SetTexture(Resources.Load<Texture2D>("slider"));
			this.slider.SetButtonTexture(Resources.Load<Texture2D>("button"));
			this.slider.SetButtonSize(0,30);
		}

		//is_clip
		{
			this.clipsprite.SetClip(this.is_clip);
			this.text.SetClip(this.is_clip);
			this.button.SetClip(this.is_clip);
			this.checkbutton.SetClip(this.is_clip);
			this.inputfield.SetClip(this.is_clip);
			this.slider.SetClip(this.is_clip);
		}
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//キー。
		NInput.Key.GetInstance().Main();

		//イベントプレート。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);

		//ＵＩ。
		NUi.Ui.GetInstance().Main();

		if(NInput.Key.GetInstance().enter.down == true){
			this.is_clip = !this.is_clip;

			this.clipsprite.SetClip(this.is_clip);
			this.text.SetClip(this.is_clip);
			this.button.SetClip(this.is_clip);
			this.checkbutton.SetClip(this.is_clip);
			this.inputfield.SetClip(this.is_clip);
			this.slider.SetClip(this.is_clip);
		}

		if(NInput.Key.GetInstance().escape.down == true){
			this.update_clip_rect = !this.update_clip_rect;
		}

		if(this.update_clip_rect == true){
			NRender2D.Rect2D_R<int> t_cliprect;
			{
				t_cliprect.w = 200;
				t_cliprect.h = 200;
				t_cliprect.x = NInput.Mouse.GetInstance().pos.x - t_cliprect.w / 2;
				t_cliprect.y = NInput.Mouse.GetInstance().pos.y - t_cliprect.h / 2;
			}

			this.clipsprite.SetClipRect(ref t_cliprect);
			this.text.SetClipRect(ref t_cliprect);
			this.button.SetClipRect(ref t_cliprect);
			this.checkbutton.SetClipRect(ref t_cliprect);
			this.inputfield.SetClipRect(ref t_cliprect);
			this.slider.SetClipRect(ref t_cliprect);
		}
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click(int a_id)
	{
		Debug.Log("CallBack_Click : " + a_id.ToString());
	}

	/** [CheckButton_Base]コールバック。変更。
	*/
	private void CallBack_Change(int a_id,bool a_flag)
	{
		Debug.Log("CallBack_Click : " + a_id.ToString() + " : " + a_flag.ToString());
	}

	/** [Slider_Base]コールバック。変更。
	*/
	private void CallBack_Change(int a_id,float a_value)
	{
		Debug.Log("CallBack_Change : " + a_id.ToString() + " : " + a_value.ToString());
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

