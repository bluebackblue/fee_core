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
*/
public class test07 : main_base , NEventPlate.OnOverCallBack_Base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** text
	*/
	private NRender2D.Text2D text;

	/** sprite
	*/
	private NRender2D.Sprite2D[] sprite;

	/** eventplate
	*/
	private NEventPlate.Item[] eventplate;

	/** onover
	*/
	private bool[] onover;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マスウ。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//描画プライオリティ。
		int t_layerindex = 1;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		//テキスト。
		this.text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
		this.text.SetRect(0,0,0,0);

		//スプライト。
		this.sprite = new NRender2D.Sprite2D[3];
		this.eventplate = new NEventPlate.Item[3];
		this.onover = new bool[3];
		for(int ii=0;ii<this.sprite.Length;ii++){
			this.sprite[ii] = new NRender2D.Sprite2D(this.deleter,null,t_drawpriority + ii);
			this.sprite[ii].SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite[ii].SetTexture(Texture2D.whiteTexture);
			this.sprite[ii].SetRect(100*(ii + 1),100*(ii + 1),200,200);

			this.eventplate[ii] = new NEventPlate.Item(this.deleter,NEventPlate.EventType.Button,t_drawpriority);
			this.eventplate[ii].SetRect(this.sprite[ii].GetX(),this.sprite[ii].GetY(),this.sprite[ii].GetW(),this.sprite[ii].GetH());
			this.eventplate[ii].SetOnOverCallBackValue(ii);
			this.eventplate[ii].SetOnOverCallBack(this);

			if(ii % 3 == 0){
				this.sprite[ii].SetColor(1.0f,0.0f,0.0f,1.0f);
			}else if(ii % 3 == 1){
				this.sprite[ii].SetColor(0.0f,1.0f,0.0f,1.0f);
			}else{
				this.sprite[ii].SetColor(0.0f,0.0f,1.0f,1.0f);
			}

			this.onover[ii] = false;
		}
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.GetInstance().Main(NInput.Mouse.GetInstance().pos.x,NInput.Mouse.GetInstance().pos.y);
		
		//テキスト。
		this.text.SetText(this.onover[0].ToString() + " " + this.onover[1].ToString() + " " + this.onover[2].ToString());
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}

	/** [NEventPlate.OnOverCallBack_Base]イベントプレートに入場。
	*/
	public void OnOverEnter(int a_value)
	{
		this.onover[a_value] = true;
	}

	/** [NEventPlate.OnOverCallBack_Base]イベントプレートから退場。
	*/
	public void OnOverLeave(int a_value)
	{
		this.onover[a_value] = false;
	}
}

