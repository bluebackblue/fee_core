

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。自動接続。
*/


/** Fee.Network
*/
namespace Fee.Network
{
	/** Connect_Auto
	*/
	#if(USE_DEF_FEE_PUN)
	public class Connect_Auto
	{
		/** Mode
		*/
		private enum Mode
		{
			/** 初期化。
			*/
			Init,

			/** マスターへの接続。
			*/
			ConnectMaster,

			/** マスターへの接続処理中。
			*/
			ConnectMasterNow,

			/** マスターへの接続完了。
			*/
			ConnectMasterFix,

			/** 参加。
			*/
			Join,

			/** 参加処理中。
			*/
			JoinNow,

			/** 参加完了。
			*/
			JoinFix,

			/** 参加失敗。
			*/
			JoinFailed,

			/** 部屋作成。
			*/
			CreateRoom,

			/** 部屋作成処理中。
			*/
			CreateRoomNow,

			/** 部屋作成完了。
			*/
			CreateRoomFix,

			/** 部屋作成失敗。
			*/
			CreateRoomFailed,

			/** 部屋。
			*/
			Room,

			/** 部屋。プレイヤー作成処理中。
			*/
			Room_CreatePlayerNow,

			/** 部屋処理中。
			*/
			RoomNow,

			/** 切断。
			*/
			Disconnect,

			/** 切断処理中。
			*/
			DisconnectNow,

			/** 切断完了。
			*/
			DisconnectFix,

			/** 終了。
			*/
			End,
		};

		/** mode
		*/
		private Mode mode;

		/** auto
		*/
		private UnityEngine.GameObject connect_gameobject;
		private MonoBehaviour_Connect_Auto connect_script;

		/** constructor
		*/
		public Connect_Auto()
		{
			//ルート。
			Transform t_root = Fee.Network.Network.GetInstance().GetRoot();

			//自動。
			{
				this.connect_gameobject = new GameObject();
				this.connect_gameobject.name = "Connect_Auto";
				this.connect_gameobject.GetComponent<Transform>().SetParent(t_root);
				this.connect_script = this.connect_gameobject.AddComponent<MonoBehaviour_Connect_Auto>();
			}

			//mode
			this.mode = Mode.Init;
		}

		/** モード。設定。
		*/
		private void SetMode(Mode a_mode)
		{
			if(this.mode != a_mode){
				Tool.Log("AutoJoinRandomRoom","SetMode : " + a_mode.ToString());
				this.mode = a_mode;
			}
		}

		/** 更新。
		*/
		public bool Main()
		{
			switch(this.mode){
			case Mode.Init:
				{
					//初期化。

					//フラグリセット。
					this.connect_script.ResetAll();

					//ニックネーム。
					Photon.Pun.PhotoFee.Network.NickName = Time.time.ToString();

					this.SetMode(Mode.ConnectMaster);
				}break;
			case Mode.ConnectMaster:
				{
					//マスターへの接続。

					//バージョン違いとはマッチングしない。
					Photon.Pun.PhotoFee.Network.GameVersion = Config.GAME_VERSION;

					//接続開始。
					bool t_ret = Photon.Pun.PhotoFee.Network.ConnectUsingSettings();

					if(t_ret == true){
						this.SetMode(Mode.ConnectMasterNow);
					}
				}break;
			case Mode.ConnectMasterNow:
				{
					//マスターへの接続処理中。

					if(this.connect_script.result_connected_to_master == true){
						this.SetMode(Mode.ConnectMasterFix);
					}else{
						//TODO:失敗。
					}

				}break;
			case Mode.ConnectMasterFix:
				{
					//マスターへの接続完了。

					this.SetMode(Mode.Join);
				}break;
			case Mode.Join:
				{
					//参加。

					this.connect_script.ResetCreateJoinRoom();

					Photon.Pun.PhotoFee.Network.JoinRandomRoom();

					this.SetMode(Mode.JoinNow);
				}break;
			case Mode.JoinNow:
				{
					//参加処理中。

					if(this.connect_script.result_joinroom_fix == true){
						this.SetMode(Mode.JoinFix);
					}else if(this.connect_script.result_joinroom_failed == true){
						this.SetMode(Mode.JoinFailed);
					}

				}break;
			case Mode.JoinFix:
				{
					//参加完了。

					this.SetMode(Mode.Room);
				}break;
			case Mode.JoinFailed:
				{
					//参加失敗。

					this.SetMode(Mode.CreateRoom);
				}break;
			case Mode.CreateRoom:
				{
					//部屋作成。

					this.connect_script.ResetCreateJoinRoom();

					Photon.Realtime.RoomOptions t_room_optopm = new Photon.Realtime.RoomOptions();
					{
						t_room_optopm.MaxPlayers = 3;	
						t_room_optopm.PublishUserId = true;
					}

					Photon.Pun.PhotoFee.Network.CreateRoom(null,t_room_optopm);

					this.SetMode(Mode.CreateRoomNow);
				}break;
			case Mode.CreateRoomNow:
				{
					//部屋作成処理中。

					if(this.connect_script.result_createroom_failed == true){
						this.SetMode(Mode.CreateRoomFailed);
						break;
					}else if(this.connect_script.result_joinroom_failed == true){
						this.SetMode(Mode.CreateRoomFailed);
						break;
					}else{
						if(this.connect_script.result_createroom_fix == true){
							if(this.connect_script.result_joinroom_fix == true){
								this.SetMode(Mode.CreateRoomFix);
								break;
							}
						}
					}
				}break;
			case Mode.CreateRoomFix:
				{
					//部屋作成完了。

					this.SetMode(Mode.Room);
				}break;
			case Mode.CreateRoomFailed:
				{
					//部屋作成失敗。

					this.SetMode(Mode.Disconnect);
				}break;
			case Mode.Room:
				{
					//部屋。

					//プレイヤー作成。
					Photon.Pun.PhotoFee.Network.Instantiate(Config.PREFAB_NAME_PLAYER,Vector3.zero,Quaternion.identity,0);					

					this.SetMode(Mode.Room_CreatePlayerNow);
				}break;
			case Mode.Room_CreatePlayerNow:
				{
					//部屋。プレイヤー作成処理中。

					if(Fee.Network.Network.GetInstance().GetMyPlayer() != null){
						this.SetMode(Mode.RoomNow);
						break;
					}

					if(Fee.Network.Network.GetInstance().IsDisconnectRequest() == true){
						//切断リクエストあり。
						this.SetMode(Mode.Disconnect);
						break;
					}else if(this.connect_script.result_disconnected == true){
						//切断。
						this.SetMode(Mode.Disconnect);
						break;
					}
				}break;
			case Mode.RoomNow:
				{
					//部屋処理中。

					if(Fee.Network.Network.GetInstance().IsDisconnectRequest() == true){
						//切断リクエストあり。
						this.SetMode(Mode.Disconnect);
						break;
					}else if(this.connect_script.result_disconnected == true){
						//切断。
						this.SetMode(Mode.Disconnect);
						break;
					}
				}break;
			case Mode.Disconnect:
				{
					//切断。

					Photon.Pun.PhotoFee.Network.Disconnect();
					this.SetMode(Mode.DisconnectNow);
				}break;
			case Mode.DisconnectNow:
				{
					//切断処理中。

					if(this.connect_script.result_disconnected == true){
						this.SetMode(Mode.DisconnectFix);
					}
				}break;
			case Mode.DisconnectFix:
				{
					//切断完了。

					if(Fee.Network.Network.GetInstance().IsDisconnectRequest() == true){
						if(Fee.Network.Network.GetInstance().GetPlayerList().Count == 0){
							this.SetMode(Mode.End);
						}
					}
				}break;
			case Mode.End:
				{
					//終了。

					GameObject.Destroy(this.connect_gameobject);
					this.connect_gameobject = null;

				}return false;
			}

			return true;
		}
	}
	#endif
}

