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


/** test17
*/
public class test17 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** クライアント。
	*/
	private NSocket.TcpClient client;

	/** button_client
	*/
	private NUi.Button button_client;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//入力。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//イベントプレート。インスタンス作成。
		NEventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		NUi.Ui.CreateInstance();

		//deleter
		this.deleter = new NDeleter.Deleter();

		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		//button_client
		this.button_client = new NUi.Button(this.deleter,null,t_drawpriority,Click,0);
	}

	/** クリック。
	*/
	public void Click(int a_value)
	{
		this.client = new NSocket.TcpClient();
		this.client.Connect("127.0.0.1",5678);
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
	}
}


