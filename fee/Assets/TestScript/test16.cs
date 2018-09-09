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
		[SerializeField]
		private bool start_host;

		[SerializeField]
		private bool start_client;

		/** 開始。
		*/
		void Start()
		{
			this.start_host = false;
			this.start_client = false;
		}

		/** 削除。
		*/
		public void OnDestroy()
		{
			Debug.Log("OnDestroy");

			this.Close();
		}

		/** Close
		*/
		public void Close()
		{
			Debug.Log("Close");

			if(this.start_host == true){
				this.StopHost();
			}

			if(this.start_client == true){
				this.StopClient();
			}
		}

		/** OnStartHost
		*/
		public override void OnStartHost()
		{
			this.start_host = true;

			Debug.Log("OnStartHost : ホスト開始");
			base.OnStartHost();
		}

		/** OnStopHost
		*/
		public override void OnStopHost()
		{
			this.start_host = false;

			Debug.Log("OnStopHost : ホスト終了");
			base.OnStopHost();
		}

		/** OnStartServer
		*/
		public override void OnStartServer()
		{
			Debug.Log("OnStartServer : サーバ開始");
			base.OnStartServer();
		}

		/** OnStopServer
		*/
		public override void OnStopServer()
		{
			Debug.Log("OnStopServer : サーバ終了");
			base.OnStopServer();
		}

		/** OnStartClient
		*/
		public override void OnStartClient(NetworkClient a_client)
		{
			this.start_client = true;

			Debug.Log("OnStartClient : クライアント開始");
			base.OnStartClient(a_client);
		}

		/** OnStopClient
		*/
		public override void OnStopClient()
		{
			this.start_client = false;

			Debug.Log("OnStopClient : クライアント終了");
			base.OnStopClient();
		}

		#if false

		/** OnClientError
		*/
		public override void OnClientError(NetworkConnection a_network_connection,int a_errorcode)
		{
			Debug.Log("OnClientError : " + a_errorcode.ToString());
			base.OnClientError(a_network_connection,a_errorcode);
		}

		/** OnServerConnect
		*/
		public override void OnServerConnect(NetworkConnection a_network_connection)
		{
			Debug.Log("OnServerConnect : " + a_network_connection.connectionId.ToString());
			base.OnServerConnect(a_network_connection);			
		}

		/** OnServerAddPlayer
		*/
		public override void OnServerAddPlayer(NetworkConnection a_network_connection,short player_controller_id,NetworkReader a_network_reader)
		{
			Debug.Log("OnServerAddPlayer : " + player_controller_id.ToString());
			base.OnServerAddPlayer(a_network_connection,player_controller_id,a_network_reader);
		}

		/** OnServerAddPlayer
		*/
		public override void OnServerAddPlayer(NetworkConnection a_network_connection,short player_controller_id)
		{
			Debug.Log("OnServerAddPlayer : " + player_controller_id.ToString());
			base.OnServerAddPlayer(a_network_connection,player_controller_id);
		}

		public override void OnServerRemovePlayer(NetworkConnection a_network_connection,PlayerController a_player_controller)
		{
			Debug.Log("OnServerRemovePlayer : " + a_network_connection.connectionId.ToString());
			base.OnServerRemovePlayer(a_network_connection,a_player_controller);
		}

		/** OnClientSceneChanged
		*/
		public override void OnClientSceneChanged(NetworkConnection a_network_connection)
		{
			Debug.Log("OnClientSceneChanged");
			base.OnClientSceneChanged(a_network_connection);
		}

		/** OnClientConnect
		*/
		public override void OnClientConnect(NetworkConnection a_network_connection)
		{
			Debug.Log("OnClientConnect : " + a_network_connection.connectionId.ToString());
			base.OnClientConnect(a_network_connection);
		}

		#endif
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

	/** disconnected_button
	*/
	private NUi.Button disconnected_button;

	/** status_text
	*/
	private NRender2D.Text2D status_text;

	/** playerlist_text
	*/
	private NRender2D.Text2D[] playerlist_text;

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
		GameObject t_player_prefab = Resources.Load<GameObject>("test16_player");

		//networkmanager
		this.networkmanager_gameobject = new GameObject();
		this.networkmanager_gameobject.SetActive(false);
		this.networkmanager_gameobject.name = "NetworkManager";

		//networkmanager
		this.networkmanager = this.networkmanager_gameobject.AddComponent<Ntest16.CustomNetworkManager>();
		this.networkmanager.playerPrefab = t_player_prefab;
		this.networkmanager.useWebSockets = false;
		this.networkmanager.dontDestroyOnLoad = false;
		this.networkmanager.runInBackground = true;
		this.networkmanager.autoCreatePlayer = true;

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
			this.host_button.SetText("Host");

			t_x += 150;

			this.client_button = new NUi.Button(this.deleter,null,t_drawpriority,Click,1);
			this.client_button.SetRect(t_x,t_y,t_w,t_h);
			this.client_button.SetTexture(Resources.Load<Texture2D>("button"));
			this.client_button.SetVisible(false);
			this.client_button.SetText("Client");
		}

		{
			int t_x = 300;
			int t_y = 100;
			int t_w = 100;
			int t_h = 50;

			this.disconnected_button = new NUi.Button(this.deleter,null,t_drawpriority,Click,999);
			this.disconnected_button.SetRect(t_x,t_y,t_w,t_h);
			this.disconnected_button.SetTexture(Resources.Load<Texture2D>("button"));
			this.disconnected_button.SetVisible(false);
			this.disconnected_button.SetText("DisConnected");
		}

		{
			int t_x = 300;
			int t_y = 150;

			this.status_text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
			this.status_text.SetRect(t_x,t_y,0,0);
			this.status_text.SetText("---");
		}

		{
			int t_x = 300;
			int t_y = 200;

			this.playerlist_text = new NRender2D.Text2D[10];
			for(int ii=0;ii<this.playerlist_text.Length;ii++){
				this.playerlist_text[ii] = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
				this.playerlist_text[ii].SetRect(t_x,t_y + 20 * ii,0,0);
				this.playerlist_text[ii].SetText("---");
			}
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

		//mode
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
				this.button_result = -1;
				this.disconnected_button.SetVisible(true);

				this.status_text.SetText("Mode.Start_Host");

				this.networkmanager.StartHost();
				this.mode = Mode.Do;
			}break;
		case Mode.Start_Client:
			{
				this.button_result = -1;
				this.disconnected_button.SetVisible(true);

				this.status_text.SetText("Mode.Start_Client");

				this.networkmanager.StartClient();
				this.mode = Mode.Do;
			}break;
		case Mode.Do:
			{
				if(this.button_result == 999){
					this.disconnected_button.SetVisible(false);

					this.networkmanager.Close();
					this.mode = Mode.Init;
				}
			}break;
		}

		#if true

		this.status_text.SetText("ready = " + ClientScene.ready.ToString());
		for(int ii=0;ii<this.playerlist_text.Length;ii++){
			PlayerController t_pc = null;
			if(UnityEngine.Networking.ClientScene.localPlayers != null){
				if(ii < UnityEngine.Networking.ClientScene.localPlayers.Count){
					t_pc = UnityEngine.Networking.ClientScene.localPlayers[ii];
				}
			}
			if(t_pc != null){
				this.playerlist_text[ii].SetText(t_pc.playerControllerId.ToString());
			}else{
				this.playerlist_text[ii].SetText("---");
			}
		}

		#endif

		//mode
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

