using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ソケット。ＴＣＰクライアント。
*/


/** NSocket
*/
namespace NSocket
{
	/** TcpClient
	*/
	public class TcpClient
	{
		/** tcp_client
		*/
		private System.Net.Sockets.TcpClient tcp_client;

		/** streamwriter
		*/
		System.IO.StreamWriter streamwriter;

		/** constructor
		*/
		public TcpClient()
		{
			//tcp_client
			this.tcp_client = null;

			//streamwriter
			this.streamwriter = null;
		}

		/** 接続。
		*/
		public void Connect(string a_ip,int a_port)
		{
			//tcp_client
			this.tcp_client = new System.Net.Sockets.TcpClient(a_ip,a_port);

			//streamwriter
			System.Net.Sockets.NetworkStream t_stream = this.tcp_client.GetStream();
			this.streamwriter = new System.IO.StreamWriter(t_stream);
		}

		/** 送信。
		*/
		public void Send(string a_text)
		{
			this.streamwriter.WriteLine(a_text);
			this.streamwriter.Flush();
		}
	}
}

