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

	スプライト
	仮想スクリーンサイズと同じサイズのスプライトを設置。

	テキスト
	中央に文字を設置。

	入力フィールド
	テキストの下に入力フィールドを設置。

*/
public class test01 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** スプライト。
	*/
	private NRender2D.Sprite2D sprite;

	/** テキスト。
	*/
	private NRender2D.Text2D text;

	/** 入力フィールド。
	*/
	private NRender2D.InputField2D inputfield;

	/** ステータス。
	*/
	private NRender2D.Text2D status;

	/** ボタン。
	*/
	private NUi.Button button_log;
	private NUi.Button button_logerror;
	private NUi.Button button_assert;

	/** Start
	*/
	private void Start()
	{
		//タスク。インスタンス作成。
		NTaskW.TaskW.CreateInstance();

		//パフォーマンスカウンター。インスタンス作成。
		NPerformanceCounter.PerformanceCounter.CreateInstance();

		//２Ｄ描画。インスタンス作成。
		NRender2D.Config.LOG_ENABLE = true;
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Config.LOG_ENABLE = true;
		NInput.Mouse.CreateInstance();

		//イベントプレート。
		NEventPlate.Config.LOG_ENABLE = true;
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Config.LOG_ENABLE = true;
		NUi.Ui.CreateInstance();

		//フォント。
		Font t_font = Resources.Load<Font>("mplus-1p-medium");
		if(t_font != null){
			NRender2D.Render2D.GetInstance().SetDefaultFont(t_font);
		}

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//レイヤインデックス。
		int t_layerindex = 0;

		//描画プライオリティ。
		long t_drawpriority = NRender2D.Render2D.DRAWPRIORITY_STEP * t_layerindex;

		{
			//スプライト。
			this.sprite = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority);
			this.sprite.SetTexture(Texture2D.whiteTexture);
			this.sprite.SetTextureRect(0,0,NRender2D.Render2D.TEXTURE_W,NRender2D.Render2D.TEXTURE_H);
			this.sprite.SetRect(0,0,NRender2D.Render2D.VIRTUAL_W,NRender2D.Render2D.VIRTUAL_H);
			this.sprite.SetColor(0.0f,0.5f,0.0f,1.0f);
			this.sprite.SetMaterialType(NRender2D.Config.MaterialType.Simple);
		}

		{
			int t_xx = 700;
			int t_yy = 100;

			//テキスト。
			this.text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
			this.text.SetRect(t_xx,t_yy,0,0);
			this.text.SetText("abcあいうえおxyz");
			this.text.SetColor(0.0f,0.0f,0.0f,1.0f);
			this.text.SetOutLineColor(1.0f,1.0f,1.0f,0.3f);
			this.text.SetOutLine(true);
			this.text.SetShadow(true);

			t_yy += 50;

			//入力フィールド。
			{
				int t_w = 200;
				int t_h = 200;
				int t_x = t_xx;
				int t_y = t_yy;
		
				this.inputfield = new NRender2D.InputField2D(this.deleter,null,t_drawpriority);
				this.inputfield.SetRect(t_x,t_y,t_w,t_h);
				this.inputfield.SetText("defaultテキスト");
				this.inputfield.SetMultiLine(true);
				this.inputfield.SetImageColor(1.0f,0.6f,0.6f,1.0f);
				this.inputfield.SetTextColor(0.0f,0.0f,0.0f,0.5f);
			}
		}

		{
			string t_text = "";

			t_text += "Screen = "			+ Screen.width.ToString() + " x " + Screen.height.ToString() + "\n";
			t_text += "Data = "				+ Application.dataPath + "\n";
			t_text += "Persistent Data = "	+ Application.persistentDataPath + "\n";
			t_text += "Streaming Assets = "	+ Application.streamingAssetsPath + "\n";
			t_text += "Temporary Cache = "	+ Application.temporaryCachePath + "\n";

			this.status = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
			this.status.SetRect(10,100,0,0);
			this.status.SetFontSize(15);
			this.status.SetText(t_text);
		}

		{
			int t_xx = 150;
			int t_yy = 10;

			this.button_log = new NUi.Button(this.deleter,null,t_drawpriority + 1,Click,100);
			this.button_log.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_log.SetRect(t_xx,t_yy,80,50);
			this.button_log.SetText("Log");

			t_xx += 100;

			this.button_logerror = new NUi.Button(this.deleter,null,t_drawpriority + 1,Click,200);
			this.button_logerror.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_logerror.SetRect(t_xx,t_yy,80,50);
			this.button_logerror.SetText("LogError");

			t_xx += 100;

			this.button_assert = new NUi.Button(this.deleter,null,t_drawpriority + 1,Click,300);
			this.button_assert.SetTexture(Resources.Load<Texture2D>("button"));
			this.button_assert.SetRect(t_xx,t_yy,80,50);
			this.button_assert.SetText("Assert");
		}
	}

	/** クリック。
	*/
	private void Click(int a_value)
	{
		switch(a_value){
		case 100:
			{
				NRender2D.Tool.Log("Click","Log");
			}break;
		case 200:
			{
				NRender2D.Tool.LogError("Click","LogError");
			}break;
		case 300:
			{
				NRender2D.Tool.Assert(false);
			}break;
		}
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

