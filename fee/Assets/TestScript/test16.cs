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


/** test16
*/
public class test16 : main_base
{
	/** 削除管理。
	*/
	private NDeleter.Deleter deleter;

	/** ステータス。
	*/
	private NRender2D.Text2D status_text;

	/** 開始ボタン。
	*/
	private NUi.Button start_button;

	/** 修了ボタン。
	*/
	private NUi.Button end_button;

	/** net_gameobject
	*/
	//private GameObject net_gameobject;

	/** net_control
	*/
	//private ConnectAndJoinRandom net_control;

	/** net_status
	*/
	//private ShowStatusWhenConnecting net_status;

	/** net_onjoined_instantiate
	*/
	//private OnJoinedInstantiate net_onjoined_instantiate;

	/** timeout
	*/
	//private int timeout;


	/** Mode
	*/
	private enum Mode
	{
		Init,
		Wait,
		Start,
		Do,
		DisConnect,
		DisConnectNow,
		ChangeScene,
	};

	private Mode mode;

	/** Start
	*/
	private void Start()
	{
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

		//ネットワーク。インスタンス作成。
		NNetwork.Network.CreateInstance();

		//削除管理。
		this.deleter = new NDeleter.Deleter();

		//layerindex
		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		//ステータス。
		{
			int t_x = 100;
			int t_y = 100;

			this.status_text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
			this.status_text.SetRect(t_x,t_y,0,0);
			this.status_text.SetText("");
		}

		//開始ボタン。
		{
			int t_w = 100;
			int t_h = 30;
			int t_x = 100;
			int t_y = 300;

			Texture2D t_texture = Resources.Load<Texture2D>("button");

			this.start_button = new NUi.Button(this.deleter,null,t_drawpriority,Click_Start,0);
			this.start_button.SetRect(t_x,t_y,t_w,t_h);
			this.start_button.SetTexture(t_texture);
			this.start_button.SetText("接続");
			this.start_button.SetVisible(false);
		}

		//修了ボタン。
		{
			int t_w = 100;
			int t_h = 30;
			int t_x = 100 + 110;
			int t_y = 300;

			Texture2D t_texture = Resources.Load<Texture2D>("button");

			this.end_button = new NUi.Button(this.deleter,null,t_drawpriority,Click_End,0);
			this.end_button.SetRect(t_x,t_y,t_w,t_h);
			this.end_button.SetTexture(t_texture);
			this.end_button.SetText("切断");
			this.end_button.SetVisible(false);
		}

		this.mode = Mode.Init;
	}

	/** クリック。開始。
	*/
	public void Click_Start(int a_value)
	{
		if(this.mode == Mode.Wait){
			this.mode = Mode.Start;
		}
	}

	/** クリック。終了。
	*/
	public void Click_End(int a_value)
	{
		if(this.mode == Mode.Do){
			this.mode = Mode.DisConnect;
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

		//ネットワーク。
		NNetwork.Config.LOG_ENABLE = true;
		NNetwork.Network.GetInstance().Main();

		switch(this.mode){
		case Mode.Init:
			{
				this.status_text.SetText("Mode.Init");
				this.mode = Mode.Wait;
			}break;
		case Mode.Wait:
			{
				this.status_text.SetText("Mode.Wait");

				if(this.IsChangeScene() == true){
					this.mode = Mode.ChangeScene;
				}else{
					this.start_button.SetVisible(true);
				}
			}break;
		case Mode.Start:
			{
				this.status_text.SetText("Mode.Start");

				//t_player_prefab
				GameObject t_player_prefab = Resources.Load<GameObject>("test16_player");

				//プレイヤプレハブ。設定。
				NNetwork.Network.GetInstance().SetPlayerPrefab(t_player_prefab);

				//開始。
				NNetwork.Network.GetInstance().Start_AutoJoinRandomRoom();

				//ボタン。
				this.start_button.SetVisible(false);
				this.end_button.SetVisible(true);

				this.mode = Mode.Do;
			}break;
		case Mode.Do:
			{
				this.status_text.SetText("Mode.Do");
			}break;
		case Mode.DisConnect:
			{
				this.status_text.SetText("Mode.DisConnect");

				//切断リクエスト。
				NNetwork.Network.GetInstance().DisconnectRequest();

				//ボタン。
				this.end_button.SetVisible(false);

				this.mode = Mode.DisConnectNow;
			}break;
		case Mode.DisConnectNow:
			{
				this.status_text.SetText("Mode.DisConnectNow");

				if(NNetwork.Network.GetInstance().IsBusy() == false){
					this.mode = Mode.Wait;
				}
			}break;
		case Mode.ChangeScene:
			{
				//シーン変更可能。
			}break;
		}
	}

	/** 削除前。
	*/
	public override bool PreDestroy(bool a_first)
	{
		if(this.mode == Mode.ChangeScene){
			return true;
		}
		return false;
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

