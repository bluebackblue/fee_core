using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ソケット。ＴＣＰサーバ。
*/


/** NSocket
*/
namespace NSocket
{
	/** TcpServer
	*/
	public class TcpServer
	{
		/** tcp_listener
		*/
		private System.Net.Sockets.TcpListener tcp_listener;

		/** constructor
		*/
		public TcpServer()
		{
			this.tcp_listener = null;
		}

		/** 待ち受け開始。
		*/
		public void Accept(int a_port)
		{
			System.Net.IPAddress t_ip_address = System.Net.IPAddress.Parse("127.0.0.1");

			this.tcp_listener = new System.Net.Sockets.TcpListener(t_ip_address,a_port);
			this.tcp_listener.Start();
			this.tcp_listener.BeginAcceptTcpClient(OnAcceptCallBack,this.tcp_listener);
		}

		/** [別スレッド]待ち受けコールバック。
		*/
		public void OnAcceptCallBack(System.IAsyncResult a_resuilt)
		{
			Tool.Log("TcpServer","OnAcceptCallBack");

			System.Net.Sockets.TcpListener t_listener = a_resuilt.AsyncState as System.Net.Sockets.TcpListener;
			System.Net.Sockets.TcpClient t_client = t_listener.EndAcceptTcpClient(a_resuilt);
		
			System.Net.Sockets.NetworkStream t_stream = t_client.GetStream();
			System.IO.StreamReader t_reader = new System.IO.StreamReader(t_stream);

			while(t_client.Connected == true){

				//受信。
				while(t_reader.EndOfStream == false){
					string t_data = t_reader.ReadLine();
					Tool.Log("TcpServer",t_data);
				}

				//切断。
				if(t_client.Client.Poll(1000,System.Net.Sockets.SelectMode.SelectRead)&&(t_client.Client.Available == 0)){
					Tool.Log("TcpServer","Disconnected");
					break;
				}
			}
		}
	}
}

