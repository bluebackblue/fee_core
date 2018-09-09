using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** Ntest16
*/
namespace Ntest16
{
	/** CustomNetworkManager
	*/
	public class CustomNetworkManager :  UnityEngine.Networking.NetworkManager
	{
		private bool start_server;
		private bool start_client;

		/** 開始。
		*/
		void Start()
		{
			this.start_server = false;
			this.start_client = false;
		}

		/** OnStartServer
		*/
		public override void OnStartServer()
		{
			this.start_server = true;

			Debug.Log("OnStartServer");
			base.OnStartServer();
		}

		/** OnStopServer
		*/
		public override void OnStopServer()
		{
			this.start_server = false;

			Debug.Log("OnStopServer");
			base.OnStopServer();
		}

		public override void OnStartClient(NetworkClient a_client)
		{
			this.start_client = true;

			Debug.Log("OnStartClient");
			base.OnStartClient(a_client);
		}

		public override void OnStopClient()
		{
			this.start_client = false;

			Debug.Log("OnStopClient");
			base.OnStopClient();
		}

		/** 削除。
		*/
		private void OnDestroy()
		{
			Debug.Log("OnDestroy");

			if(this.start_server == true){
				this.StopServer();
			}

			if(this.start_client == true){
				this.StopClient();
			}
		}
	}

	/** test16_player
	*/
	public class test16_player :  UnityEngine.Networking.NetworkBehaviour
	{
	}
}


/** test16
*/
public class test16 : main_base
{
	/** deleter
	*/
	private NDeleter.Deleter deleter;

	/** networkmanager_gameobject
	*/
	private GameObject networkmanager_gameobject;

	/** networkmanager
	*/
	private Ntest16.CustomNetworkManager networkmanager;

	/** networkmanager_hud
	*/
	private UnityEngine.Networking.NetworkManagerHUD networkmanager_hud;

	/** button_result
	*/
	private int button_result;

	/** host_button
	*/
	private NUi.Button host_button;

	/** client_button
	*/
	private NUi.Button client_button;



	/** Mode
	*/
	private enum Mode
	{
		Init,

		Select_HostClient,

		Start_Host,
		Start_Client,

		Do,
	};

	/**
	*/
	private Mode mode;
	private bool mode_first;

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

		//player_prefab
		GameObject t_player_prefab = new GameObject();
		{
			t_player_prefab.AddComponent<UnityEngine.Networking.NetworkIdentity>();
			t_player_prefab.AddComponent<Ntest16.test16_player>();
			t_player_prefab.name = "test16_player";
		}

		//networkmanager
		this.networkmanager_gameobject = new GameObject();
		this.networkmanager_gameobject.SetActive(false);
		this.networkmanager_gameobject.name = "NetworkManager";
		this.networkmanager = this.networkmanager_gameobject.AddComponent<Ntest16.CustomNetworkManager>();
		this.networkmanager.playerPrefab = t_player_prefab;
		this.networkmanager.useWebSockets = true;
		this.networkmanager.dontDestroyOnLoad = false;

		//networkmanager_hud
		this.networkmanager_hud = this.networkmanager_gameobject.AddComponent<UnityEngine.Networking.NetworkManagerHUD>();
		this.networkmanager_hud.offsetX = 200;
		this.networkmanager_hud.offsetY = 200;

		//networkmanager
		this.networkmanager_gameobject.SetActive(true);

		//button_result
		this.button_result = -1;

		int t_layerindex = 0;
		long t_drawpriority = t_layerindex * NRender2D.Render2D.DRAWPRIORITY_STEP;

		{
			int t_x = 100;
			int t_y = 100;
			int t_w = 100;
			int t_h = 50;

			this.host_button = new NUi.Button(this.deleter,null,t_drawpriority,Click,0);
			this.host_button.SetRect(t_x,t_y,t_w,t_h);
			this.host_button.SetTexture(Resources.Load<Texture2D>("button"));
			this.host_button.SetVisible(false);

			t_x += 150;

			this.client_button = new NUi.Button(this.deleter,null,t_drawpriority,Click,1);
			this.client_button.SetRect(t_x,t_y,t_w,t_h);
			this.client_button.SetTexture(Resources.Load<Texture2D>("button"));
			this.client_button.SetVisible(false);
		}
	
		//mode
		this.mode = Mode.Init;
		this.mode_first = true;
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

		Mode t_old = this.mode;

		switch(this.mode){
		case Mode.Init:
			{
				this.mode = Mode.Select_HostClient;
			}break;
		case Mode.Select_HostClient:
			{
				if(this.mode_first == true){
					this.button_result = -1;
					this.host_button.SetVisible(true);
					this.client_button.SetVisible(true);
				}else{
					if(this.button_result >= 0){
						this.host_button.SetVisible(false);
						this.client_button.SetVisible(false);

						if(this.button_result == 0){
							this.mode = Mode.Start_Host;	
						}else{
							this.mode = Mode.Start_Client;
						}
					}
				}
			}break;
		case Mode.Start_Host:
			{
				this.networkmanager.StartServer();
				this.mode = Mode.Do;
			}break;
		case Mode.Start_Client:
			{
				this.networkmanager.StartClient();
				this.mode = Mode.Do;
			}break;
		case Mode.Do:
			{
			}break;
		}

		if(t_old != this.mode){
			this.mode_first = true;
		}else{
			this.mode_first = false;
		}
	}

	/** クリック。
	*/
	public void Click(int a_value)
	{
		this.button_result = a_value;
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
		this.deleter.DeleteAll();
	}
}

