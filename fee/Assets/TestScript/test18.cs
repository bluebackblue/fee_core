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


/** test18
*/
public class test18 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** サーバ。
	*/
	private NSocket.TcpServer server;

	/** text
	*/
	NRender2D.Text2D[] text;

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
		NUi.Ui.CreateInstance();

		//deleter
		this.deleter = new NDeleter.Deleter();

		//server
		this.server = new NSocket.TcpServer();
		string t_ret = this.server.Accept(11111);

		//drawpriority
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		//text
		this.text = new NRender2D.Text2D[10];
		for(int ii=0;ii<this.text.Length;ii++){
			this.text[ii] = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
			this.text[ii].SetRect(100,100 + ii * 20,0,0);
			this.text[ii].SetText("---");
			this.text[ii].SetOutLine(true);
			this.text[ii].SetFontSize(17);
		}

		//add text
		this.text[this.text.Length - 1].SetText(t_ret);
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

		//受信チェック。
		{
			string t_recvdata = this.server.GetRecvData();
			if(t_recvdata != null){
				for(int ii=0;ii<this.text.Length - 1;ii++){
					this.text[ii].SetText(this.text[ii + 1].GetText());
				}
				this.text[this.text.Length - 1].SetText(t_recvdata);
			}
		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

