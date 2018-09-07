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


/** Client
*/
#if false
public class Client
{
	/** クライアント。
	*/
	System.Net.Sockets.TcpClient tcpclient;

	/** ストリーム。
	*/
	System.IO.StreamWriter streamwriter;

	/** constructor
	*/
	public Client()
	{
		this.tcpclient = new System.Net.Sockets.TcpClient("127.0.0.1",12345);

		System.Net.Sockets.NetworkStream t_stream = this.tcpclient.GetStream();

		this.streamwriter = new System.IO.StreamWriter(t_stream);
	}

	public void Send()
	{
		if(this.tcpclient.Connected == true){
			this.streamwriter.WriteLine("abc");
			this.streamwriter.Flush();
		}
	}
}
#endif

/** Server
*/
#if false
public class Server
{
	/** サーバ。
	*/
	System.Net.Sockets.TcpListener tcplistener;

	/** constructor
	*/
	public Server()
	{
		int t_port = 12345;
		System.Net.IPAddress t_ip = System.Net.IPAddress.Parse("127.0.0.1");
	
		//待ち受け開始。
		this.tcplistener = new System.Net.Sockets.TcpListener(t_ip,t_port);
		this.tcplistener.Start();
		this.tcplistener.BeginAcceptSocket(OnAcceptCallBack,this.tcplistener);
	}

	/** 待ち受けコールバック。
	*/
	public void OnAcceptCallBack(System.IAsyncResult a_resuilt)
	{
		Debug.Log("OnAcceptCallBack");

		//サーバ。
        System.Net.Sockets.TcpListener t_listener = a_resuilt.AsyncState as System.Net.Sockets.TcpListener;

		//クライアント。
        System.Net.Sockets.TcpClient t_client = t_listener.EndAcceptTcpClient(a_resuilt);
		
		//ストリーム。
        System.Net.Sockets.NetworkStream t_stream = t_client.GetStream();

		System.IO.StreamReader t_reader = new System.IO.StreamReader(t_stream);

        while(t_client.Connected == true){

			//受信。
            while(t_reader.EndOfStream == false){
				string t_data = t_reader.ReadLine();
				Debug.Log(t_data);
            }

			//切断。
            if(t_client.Client.Poll(1000,System.Net.Sockets.SelectMode.SelectRead)&&(t_client.Client.Available == 0)){

				Debug.Log("disconnected");

                break;
            }

        }
	}
}
#endif


/** test16
*/
public class test16 : main_base
{
	/** サーバ。
	*/
	private NSocket.TcpServer server;

	/** クライアント。
	*/
	private NSocket.TcpClient client;

	/** モード。
	*/
	private int mode;

	/** Start
	*/
	private void Start()
	{
		//２Ｄ描画。インスタンス作成。
		NRender2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		NInput.Mouse.CreateInstance();

		//ソケット。インスタンス作成。
		NSocket.Config.LOG_ENABLE = true;

		this.mode = 0;
	}

	/** Update
	*/
	private void Update()
	{
		//マウス。
		NInput.Mouse.GetInstance().Main(NRender2D.Render2D.GetInstance());

		switch(this.mode){
		case 0:
			{
				//サーバ作成。
				if(this.server == null){
					Debug.Log("new TcpServer()");
					this.server = new NSocket.TcpServer();
					this.server.Accept(12345);
				}

				if(NInput.Mouse.GetInstance().left.down == true){
					this.mode++;
				}
			}break;
		case 1:
			{
				//クライアント作成。
				if(this.client == null){
					Debug.Log("new TcpClient()");
					this.client = new NSocket.TcpClient();
					this.client.Connect("127.0.0.1",12345);
				}

				if(NInput.Mouse.GetInstance().left.down == true){
					this.mode++;
				}
			}break;
		case 2:
			{
				//送信。
				if(NInput.Mouse.GetInstance().left.down == true){
					Debug.Log("this.client.Send()");
					this.client.Send("ABCDEFG");
				}
			}break;

		}
	}

	/** OnDestroy
	*/
	private void OnDestroy()
	{
	}
}


