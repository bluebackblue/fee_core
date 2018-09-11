using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ネットワーク。ランダムルーム自動接続。
*/


/** NNetwork
*/
#if USE_PHOTON
namespace NNetwork
{
	/** AutoJoinRandomRoom
	*/
	public class AutoJoinRandomRoom
	{
		/** コントロール。
		*/
		private GameObject control;

		/** Mode
		*/
		private enum Mode
		{
			/** 初期化。
			*/
			Init,

			/** 処理中。
			*/
			Connect,

			/** 接続中。
			*/
			Do,

			/** 切断中。
			*/
			DisConnected,

			/** 終了。
			*/
			End,
		};

		/** mode
		*/
		private Mode mode;

		/** time
		*/
		private int time;

		/** constructor
		*/
		public AutoJoinRandomRoom()
		{
			//ルート。
			Transform t_root = NNetwork.Network.GetInstance().GetRoot();

			//コントロール作成。
			{
				this.control = new GameObject();
				this.control.GetComponent<Transform>().SetParent(t_root);
				this.control.name = "control";
				this.control.SetActive(false);
	
				this.control.AddComponent<ConnectAndJoinRandom>();
				this.control.AddComponent<NNetwork.OnJoinedRoomCallBack>();
			}

			//mode
			this.mode = Mode.Init;

			//time
			this.time = 0;
		}

		/** 更新。
		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Init:
				{
					//初期化。

					this.control.SetActive(true);
					this.time = 0;
					this.mode = Mode.Connect;
				}break;
			case Mode.Connect:
				{
					//処理中。

					//Tool.Log("AutoJoinRandomRoom",PhotonNetwork.connectionState.ToString() + " : " + PhotonNetwork.connectionStateDetailed.ToString());

					this.time++;
					if(this.time >= 300){
						//タイムアウト。
						this.mode = Mode.Do;
					}else if((PhotonNetwork.connectionState == ConnectionState.Connected)&&(PhotonNetwork.connectionStateDetailed == ClientState.Joined)){
						//接続完了。
						this.mode = Mode.Do;
					}
				}break;
			case Mode.Do:
				{
					//接続中。

					if(NNetwork.Network.GetInstance().IsDisconnectRequest() == true){
						//切断開始。
						PhotonNetwork.Disconnect();
						this.mode = Mode.DisConnected;
					}
				}break;
			case Mode.DisConnected:
				{
					//切断中。

					if(PhotonNetwork.connectionState == ConnectionState.Disconnected){
						//切断完了。
						GameObject.Destroy(this.control);
						this.control = null;
						this.mode = Mode.End;			
					}
				}break;
			case Mode.End:
				{
					//終了。
				}return false;
			}

			return true;
		}
	}
}
#else
namespace NNetwork
{
	/** AutoJoinRandomRoom
	*/
	public class AutoJoinRandomRoom
	{
		public bool Main(){return false;}
	}
}
#endif

