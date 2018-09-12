using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ネットワーク。
*/


/** NNetwork
*/
namespace NNetwork
{
	/** Network
	*/
	public class Network
	{
		/** [シングルトン]s_instance
		*/
		private static Network s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Network();
			}
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Network GetInstance()
		{
			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** Mode
		*/
		private enum Mode
		{
			None,

			//自動接続。
			Connect_Auto,

			//リセット。
			Reset,
		};

		/** mode
		*/
		private Mode mode;

		/** connect_auto
		*/
		private Connect_Auto connect_auto;

		/** ルート。
		*/
		private GameObject root_gameobject;
		private Transform root_transform;

		/** disconnect_request
		*/
		private bool disconnect_request;

		/** player_list
		*/
		private List<Player> player_list;

		/** my_player
		*/
		private Player my_player;

		/** recv_callback
		*/
		private OnRemoteCallBack_Base recv_callback;

		/** [シングルトン]constructor
		*/
		private Network()
		{
			//mode
			this.mode = Mode.None;

			//connect_auto
			this.connect_auto = null;

			//ルート。
			this.root_gameobject = new GameObject();
			this.root_gameobject.name = "Network";
			GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_transform = this.root_gameobject.GetComponent<Transform>();

			//disconnect_request
			this.disconnect_request = false;

			//player_list
			this.player_list = new List<Player>();

			//my_player
			this.my_player = null;

			//recv_callback
			this.recv_callback = null;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			GameObject.Destroy(this.root_gameobject);
		}

		/** ルート。取得。
		*/
		public Transform GetRoot()
		{
			return this.root_transform;
		}

		/** 開始。
		*/
		public void Start_AutoJoinRandomRoom()
		{
			if(this.mode == Mode.None){
				this.mode = Mode.Connect_Auto;
				this.disconnect_request = false;
				this.connect_auto = new Connect_Auto();
			}
		}

		/** 切断リクエスト。設定。
		*/
		public void DisconnectRequest()
		{
			this.disconnect_request = true;
		}

		/** 切断リクエスト。チェック。
		*/
		public bool IsDisconnectRequest()
		{
			return this.disconnect_request;
		}

		/** 処理。チェック。
		*/
		public bool IsBusy()
		{
			if(this.mode == Mode.None){
				return false;
			}
			return true;
		}

		/** プレイヤープレハブリスト。取得。
		*/
		public List<NNetwork.Player> GetPlayerList()
		{
			return this.player_list;
		}

		/** プレイヤー。取得。
		*/
		public NNetwork.Player GetPlayer(int a_playerindex)
		{
			if((0<=a_playerindex)&&(a_playerindex<this.player_list.Count)){
				return this.player_list[a_playerindex];
			}
			return null;
		}

		/** 自分のプレイヤープレハブ。取得。
		*/
		public NNetwork.Player GetMyPlayer()
		{
			return this.my_player;
		}

		/** プレイヤ‐プレハブ。追加。
		*/
		public int AddPlayer(NNetwork.Player a_player)
		{
			if(a_player != null){
				if(a_player.IsMine() == true){
					this.my_player = a_player;
				}
			}

			this.player_list.Add(a_player);
			int t_playerlist_index = this.player_list.Count - 1;
			return t_playerlist_index;
		}

		/** プレイヤープレハブ。削除。
		*/
		public void RemovePlayer(NNetwork.Player a_player)
		{
			if(this.my_player == a_player){
				this.my_player = null;
			}

			this.player_list.Remove(a_player);
		}

		/** 受信コールバック。設定。
		*/
		public void SetRecvCallBack(OnRemoteCallBack_Base a_callback)
		{
			this.recv_callback = a_callback;
		}

		/** 受信コールバック。取得。
		*/
		public OnRemoteCallBack_Base GetRecvCallBack()
		{
			return this.recv_callback;
		}

		/** 更新。
		*/
		public void Main()
		{
			switch(this.mode){
			case Mode.Connect_Auto:
				{
					//自動接続。

					if(this.connect_auto.Main() == false){
						this.connect_auto = null;
						this.mode = Mode.Reset;
					}
				}break;
			case Mode.Reset:
				{
					//リセット。

					this.my_player = null;
					this.player_list.Clear();
					this.mode = Mode.None;
				}break;
			}
		}
	}
}

