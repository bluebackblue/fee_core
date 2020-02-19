

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。
*/


/** Fee.Network
*/
#if(USE_DEF_FEE_PUN)
namespace Fee.Network
{
	/** Pun_MonoBehaviour
	*/
	public class Pun_MonoBehaviour : Photon.Pun.MonoBehaviourPunCallbacks
	{
		/** Mode
		*/
		public enum Mode
		{
			/** なし。
			*/
			None,

			/** リクエスト。
			*/
			Request,

			/** 処理中。
			*/
			Busy,

			/** 結果。
			*/
			Result,
		}

		/** Result_Master
		*/
		public class Result_Master
		{
			public Mode mode;
			public bool result;
			public void Reset()
			{
				this.mode = Mode.None;
				this.result = false;
			}
			public void SetBusy()
			{
				this.mode = Mode.Busy;
			}
			public void SetConnect()
			{
				this.mode = Mode.Result;
				this.result = true;
			}
			public void SetDisconnect()
			{
				this.mode = Mode.Result;
				this.result = false;
			}
		}

		/** Result_Lobby
		*/
		public class Result_Lobby
		{
			public Mode mode;
			public bool result;
			public void Reset()
			{
				this.mode = Mode.None;
				this.result = false;
			}
			public void SetBusy()
			{
				this.mode = Mode.Busy;
			}
			public void SetConnect()
			{
				this.mode = Mode.Result;
				this.result = true;
			}
			public void SetDisconnect()
			{
				this.mode = Mode.Result;
				this.result = false;
			}
		}

		/** Result_Room
		*/
		public class Result_Room
		{
			public Mode mode;
			public bool result;
			public string room_key;
			public string room_info;
			public void Reset()
			{
				this.mode = Mode.None;
				this.result = false;
			}
			public void SetBusy()
			{
				this.mode = Mode.Busy;
			}
			public void SetConnect()
			{
				this.mode = Mode.Result;
				this.result = true;
			}
			public void SetDisconnect()
			{
				this.mode = Mode.Result;
				this.result = false;
			}
		}

		/** game_version
		*/
		public string game_version;

		/** nick_name
		*/
		public string nick_name;

		/** result_master
		*/
		public Result_Master result_master;
		public Result_Master GetResultMaster()
		{
			return this.result_master;
		}

		/** result_lobby
		*/
		public Result_Lobby result_lobby;
		public Result_Lobby GetResultLobby()
		{
			return this.result_lobby;
		}

		/** result_room
		*/
		public Result_Room result_room;
		public Result_Room GetResultRoom()
		{
			return this.result_room;
		}

		/** Create
		*/
		public static Pun_MonoBehaviour Create()
		{
			UnityEngine.GameObject t_gameobject = new UnityEngine.GameObject();
			UnityEngine.GameObject.DontDestroyOnLoad(t_gameobject);
			Pun_MonoBehaviour t_this = t_gameobject.AddComponent<Pun_MonoBehaviour>();
			{
				t_this.game_version = "0.01";

				t_this.nick_name = "NickName";

				t_this.result_master = new Pun_MonoBehaviour.Result_Master();
				t_this.result_master.Reset();

				t_this.result_lobby = new Pun_MonoBehaviour.Result_Lobby();
				t_this.result_lobby.Reset();

				t_this.result_room = new Pun_MonoBehaviour.Result_Room();
				t_this.result_room.Reset();
			}
			return t_this;
		}

		/** Delete
		*/
		public void Delete()
		{
			UnityEngine.GameObject.Destroy(this.gameObject);
		}

		/** マスター。チェック。
		*/
		public bool IsConnectMaster()
		{
			if(Photon.Pun.PhotonNetwork.IsConnected == true){
				return true;
			}
			return false;
		}

		/** ロビー。チェック。
		*/
		public bool IsConnectLobby()
		{
			if(Photon.Pun.PhotonNetwork.InLobby == true){
				return true;
			}
			return false;
		}

		/** ルーム。チェック。
		*/
		public bool IsConnectRoom()
		{
			if(Photon.Pun.PhotonNetwork.InRoom == true){
				return true;
			}
			return false;
		}

		/** マスター。リクエスト。接続。
		*/
		public void RequestConnectMaster()
		{
			Result_Master t_result = this.result_master;

			if(t_result.mode != Mode.Request){
				//不明。
				Tool.Assert(false);
				t_result.mode = Mode.Result;
				return;
			}

			if(this.IsConnectMaster() == true){
				//すでに接続済み。
				t_result.SetConnect();;
				return;
			}

			//接続。
			{
				Photon.Pun.PhotonNetwork.AutomaticallySyncScene = false;
				Photon.Pun.PhotonNetwork.NickName = this.nick_name;
				Photon.Pun.PhotonNetwork.GameVersion = this.game_version;
				
				if(Photon.Pun.PhotonNetwork.ConnectUsingSettings() == true){
					t_result.SetBusy();
				}else{
					//リクエスト失敗。
					t_result.mode = Mode.Result;
				}
			}
		}

		/** マスター。リクエスト。切断。
		*/
		public void RequestDisconnectMaster()
		{
			Result_Master t_result = this.result_master;

			if(t_result.mode != Mode.Request){
				//不明。
				Tool.Assert(false);
				t_result.mode = Mode.Result;
				return;
			}

			if(this.IsConnectMaster() == false){
				//すでに切断済み。
				t_result.SetDisconnect();
				return;
			}

			//切断。
			{
				Photon.Pun.PhotonNetwork.Disconnect();
				t_result.SetBusy();
			}
		}

		/** マスター。接続。リージョン受信。
		*/
		public override void OnRegionListReceived(Photon.Realtime.RegionHandler a_region_handler)
		{
			string t_text = "";
			foreach(Photon.Realtime.Region t_item in a_region_handler.EnabledRegions){
				t_text += " Code = " + t_item.Code;
				t_text += " HostAndPort = " + t_item.HostAndPort;
				t_text += " Cluster = " + t_item.Cluster;
				t_text += " WasPinged = " + t_item.WasPinged.ToString();
				t_text += " Ping = " + t_item.Ping.ToString();
				t_text += "\n";
			}

			Tool.Log("Pun","OnRegionListReceived" + "\n" + t_text);
		}

		/** マスター。接続。低レベル接続。
		*/
		public override void OnConnected()
		{
			Tool.Log("Pun","OnConnected");
		}
 
		/** マスター。接続。成功。
		*/
		public override void OnConnectedToMaster()
		{
			this.result_master.SetConnect();

			Fee.Network.Network.GetInstance().room_list.Clear();

			Tool.Log("Pun","OnConnectedToMaster");
		}

		/** マスター。切断。失敗。
		*/
		public override void OnDisconnected(Photon.Realtime.DisconnectCause a_cause)
		{
			this.result_master.SetDisconnect();
			this.result_lobby.SetDisconnect();
			this.result_room.SetDisconnect();

			Fee.Network.Network.GetInstance().room_list.Clear();

			switch(a_cause){
			case Photon.Realtime.DisconnectCause.ExceptionOnConnect:
				{
					Tool.Log("Pun","OnDisconnected : ExceptionOnConnect");
				}break;
			case Photon.Realtime.DisconnectCause.Exception:
				{
					Tool.Log("Pun","OnDisconnected : Exception");
				}break;
			case Photon.Realtime.DisconnectCause.ServerTimeout:
				{
					Tool.Log("Pun","OnDisconnected : ServerTimeout");
				}break;
			case Photon.Realtime.DisconnectCause.ClientTimeout:
				{
					Tool.Log("Pun","OnDisconnected : ClientTimeout");
				}break;
			case Photon.Realtime.DisconnectCause.DisconnectByServerLogic:
				{
					Tool.Log("Pun","OnDisconnected : DisconnectByServerLogic");
				}break;
			case Photon.Realtime.DisconnectCause.DisconnectByServerReasonUnknown:
				{
					Tool.Log("Pun","OnDisconnected : DisconnectByServerReasonUnknown");
				}break;
			case Photon.Realtime.DisconnectCause.InvalidAuthentication:
				{
					Tool.Log("Pun","OnDisconnected : InvalidAuthentication");
				}break;
			case Photon.Realtime.DisconnectCause.CustomAuthenticationFailed:
				{
					Tool.Log("Pun","OnDisconnected : CustomAuthenticationFailed");
				}break;
			case Photon.Realtime.DisconnectCause.AuthenticationTicketExpired:
				{
					Tool.Log("Pun","OnDisconnected : AuthenticationTicketExpired");
				}break;
			case Photon.Realtime.DisconnectCause.MaxCcuReached:
				{
					Tool.Log("Pun","OnDisconnected : MaxCcuReached");
				}break;
			case Photon.Realtime.DisconnectCause.InvalidRegion:
				{
					Tool.Log("Pun","OnDisconnected : InvalidRegion");
				}break;
			case Photon.Realtime.DisconnectCause.OperationNotAllowedInCurrentState:
				{
					Tool.Log("Pun","OnDisconnected : OperationNotAllowedInCurrentState");
				}break;
			case Photon.Realtime.DisconnectCause.DisconnectByClientLogic:
				{
					Tool.Log("Pun","OnDisconnected : DisconnectByClientLogic");
				}break;
			default:
				{
					Tool.Log("Pun","OnDisconnected : Unknown");
					Tool.Assert(false);
				}break;
			}
		}

		/** リクエスト。ロビー。接続。
		*/
		public void RequestConnectLobby()
		{
			Result_Lobby t_result = this.result_lobby;

			if(t_result.mode != Mode.Request){
				//不明。
				Tool.Assert(false);
				t_result.mode = Mode.Result;
				return;
			}

			if(this.IsConnectLobby() == true){
				//すでに接続済み。
				t_result.SetConnect();
				return;
			}

			if(this.IsConnectMaster() == false){
				//マスターが切断されている。
				t_result.SetDisconnect();
				return;
			}

			//接続。
			{
				if(Photon.Pun.PhotonNetwork.JoinLobby() == true){
					t_result.SetBusy();
				}else{
					//リクエスト失敗。
					t_result.mode = Mode.Result;
				}
			}
		}

		/** リクエスト。ロビー。切断。
		*/
		public void RequestDisconnectLobby()
		{
			Result_Lobby t_result = this.result_lobby;

			if(t_result.mode != Mode.Request){
				//不明。
				Tool.Assert(false);
				t_result.mode = Mode.Result;
				return;
			}

			if(this.IsConnectLobby() == false){
				//すでに切断済み。
				t_result.SetDisconnect();
				return;
			}

			if(this.IsConnectMaster() == false){
				//マスターが切断されている。
				t_result.SetDisconnect();
				return;
			}

			//切断。
			{
				if(Photon.Pun.PhotonNetwork.LeaveLobby() == true){
					t_result.SetBusy();
				}else{
					//リクエスト失敗。
					t_result.mode = Mode.Result;
				}
			}
		}

		/** ロビー。接続。成功。
		*/
		public override void OnJoinedLobby()
		{
			this.result_lobby.SetConnect();

			Fee.Network.Network.GetInstance().room_list.Clear();

			Tool.Log("Pun","OnJoinedLobby");
		}
 
		/** ロビー。切断。失敗。
		*/
		public override void OnLeftLobby()
		{
			this.result_lobby.SetDisconnect();

			Fee.Network.Network.GetInstance().room_list.Clear();

			Tool.Log("Pun","OnLeftLobby");
		}

		/** ロビー。ルームリスト。取得。
		*/
		public override void OnRoomListUpdate(System.Collections.Generic.List<Photon.Realtime.RoomInfo> a_room_list)
		{
			foreach(Photon.Realtime.RoomInfo t_item in a_room_list){
				if(t_item.RemovedFromList == true){
					//削除。
					Fee.Network.Network.GetInstance().room_list.Remove(t_item.Name);
				}else{
					Fee.Network.RoomItem t_roomitem = new RoomItem();
					{
						//room_key
						t_roomitem.room_key = t_item.Name;

						//player_count
						t_roomitem.player_count = t_item.PlayerCount;

						//room_info
						foreach(System.Collections.DictionaryEntry t_entry in t_item.CustomProperties){
							if(t_entry.Key.GetType() == typeof(string)){
								switch((string)t_entry.Key){
								case "room_info":
									{
										if(t_entry.Value.GetType() == typeof(string)){
											t_roomitem.room_info = (string)t_entry.Value;
										}else{
											Tool.Assert(false);
										}
									}break;
								}
							}
						}

						if(Fee.Network.Network.GetInstance().room_list.ContainsKey(t_roomitem.room_key) == true){
							//更新。
							Fee.Network.Network.GetInstance().room_list[t_roomitem.room_key] = t_roomitem;
						}else{
							//新規。
							Fee.Network.Network.GetInstance().room_list.Add(t_roomitem.room_key,t_roomitem);
						}
					}
				}
			}

			Tool.Log("Pun","OnRoomListUpdate");
		}

		/** リクエスト。ルーム。接続。
		*/
		public void RequestConnectRoom()
		{
			Result_Room t_result = this.result_room;

			if(t_result.mode != Mode.Request){
				//不明。
				Tool.Assert(false);
				this.result_master.mode = Mode.Result;
				return;
			}

			if(this.IsConnectRoom() == true){
				//すでに接続済み。
				t_result.SetConnect();
				return;
			}

			if(this.IsConnectMaster() == false){
				//マスターが切断されている。
				t_result.SetDisconnect();
				return;
			}

			//接続。
			{
				Photon.Realtime.RoomOptions t_room_option = new Photon.Realtime.RoomOptions();
				{
					//MaxPlayers
					t_room_option.MaxPlayers = 16;

					//IsVisible
					t_room_option.IsVisible = true;

					//IsOpen
					t_room_option.IsOpen = true;

					//CustomRoomProperties
					ExitGames.Client.Photon.Hashtable t_custom_room_propertie = new ExitGames.Client.Photon.Hashtable();
					{
						t_custom_room_propertie.Add("room_info",this.result_room.room_info);
					}
					t_room_option.CustomRoomProperties = t_custom_room_propertie;
										
					//CustomRoomPropertiesForLobby
					t_room_option.CustomRoomPropertiesForLobby = new string[]{
						"room_info"
					};
				}

				Tool.Log("Pun","JoinOrCreateRoom : " + "room_key = " + this.result_room.room_key + " : room_info = " + this.result_room.room_info);
				if(Photon.Pun.PhotonNetwork.JoinOrCreateRoom(this.result_room.room_key,t_room_option,Photon.Realtime.TypedLobby.Default) == true){
					this.result_room.SetBusy();
				}else{
					//リクエスト失敗。
					this.result_room.mode = Mode.Result;
				}
			}
		}

		/** リクエスト。ルーム。切断。
		*/
		public void RequestDisconnectRoom()
		{
			Result_Room t_result = this.result_room;

			if(t_result.mode != Mode.Request){
				//不明。
				Tool.Assert(false);
				t_result.mode = Mode.Result;
				return;
			}

			if(this.IsConnectRoom() == false){
				//すでに切断済み。
				t_result.SetDisconnect();
				return;
			}

			if(this.IsConnectMaster() == false){
				//マスターが切断されている。
				t_result.SetDisconnect();
				return;
			}

			//切断。
			{
				if(Photon.Pun.PhotonNetwork.LeaveRoom() == true){
					t_result.SetBusy();
				}else{
					//リクエスト失敗。
					t_result.mode = Mode.Result;
				}
			}
		}

		/** ルーム。作成。成功。
		*/
		public override void OnCreatedRoom()
		{
			Tool.Log("Pun","OnCreatedRoom");
		}

		/** ルーム。作成。失敗。
		*/ 
		public override void OnCreateRoomFailed(short a_return_code,string a_message)
		{
			this.result_room.SetDisconnect();

			Tool.Log("Pun","OnCreateRoomFailed");
		}

		/** ルーム。接続。成功。
		*/
		public override void OnJoinedRoom()
		{
			this.result_room.SetConnect();

			Tool.Log("Pun","OnJoinedRoom");
		}
 
		/** ルーム。接続。失敗。
		*/
		public override void OnJoinRoomFailed(short a_return_code,string a_message)
		{
			this.result_room.SetDisconnect();

			Tool.Log("Pun","OnJoinRoomFailed");
		}

		/** ルーム。接続。失敗。
		*/ 
		public override void OnJoinRandomFailed(short a_return_code, string message)
		{
			this.result_room.SetDisconnect();

			Tool.Log("Pun","OnJoinRandomFailed");
		}

		/** ルーム。切断。失敗。
		*/ 
		public override void OnLeftRoom()
		{
			this.result_room.SetDisconnect();

			Tool.Log("Pun","OnLeftRoom");
		}

		/** CreatePlayer
		*/
		public UnityEngine.GameObject CreatePlayer()
		{
			if(this.IsConnectRoom() == false){
				//すでに切断済み。
				return null;
			}

			if(this.IsConnectMaster() == false){
				//マスターが切断されている。
				return null;
			}

			UnityEngine.GameObject t_gameobject = Photon.Pun.PhotonNetwork.Instantiate(Pun_DataLoader.ID_NETWORKOBJECT_PLAYER,UnityEngine.Vector3.zero,UnityEngine.Quaternion.identity,0);
			if(t_gameobject == null){
				Tool.Assert(false);
				return null;
			}

			return t_gameobject;
		}

		/** カスタム認証。レスポンス。
		*/
		public override void OnCustomAuthenticationResponse(System.Collections.Generic.Dictionary<string,object> a_data)
		{
			Tool.Log("Pun","OnCustomAuthenticationResponse");
		}
 
		/** カスタム認証。失敗。
		*/
		public override void OnCustomAuthenticationFailed(string a_message)
		{
			Tool.Log("Pun","OnCustomAuthenticationFailed");
		}

		/** ロビー。更新。
		*/
		public override void OnLobbyStatisticsUpdate(System.Collections.Generic.List<Photon.Realtime.TypedLobbyInfo> a_lobby_statistics)
		{
			Tool.Log("Pun","OnLobbyStatisticsUpdate");
		}
 
		/** 部屋。プレイヤー。入場。
		*/
		public override void OnPlayerEnteredRoom(Photon.Realtime.Player a_player)
		{
			Tool.Log("Pun","OnPlayerEnteredRoom");
		}
 
		/** 部屋。プレイヤー。退場。
		*/
		public override void OnPlayerLeftRoom(Photon.Realtime.Player a_player)
		{
			Tool.Log("Pun","OnPlayerLeftRoom");
		}
 
		/** 部屋。マスタークライアント。変更。
		*/
		public override void OnMasterClientSwitched(Photon.Realtime.Player a_player)
		{
			Tool.Log("Pun","OnMasterClientSwitched");
		}

		/** 部屋。ルームプロパティ。変更。
		*/
		public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable a_properties_that_changed)
		{
			Tool.Log("Pun","OnRoomPropertiesUpdate");
		}
 
		/** 部屋。プレイヤープロパティ。変更。
		*/
		public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player a_player,ExitGames.Client.Photon.Hashtable a_changed_props)
		{
			Tool.Log("Pun","OnPlayerPropertiesUpdate");
		}
 
		/** フレンドリスト。変更。
		*/
		public override void OnFriendListUpdate(System.Collections.Generic.List<Photon.Realtime.FriendInfo> a_friend_list)
		{
			Tool.Log("Pun","OnFriendListUpdate");
		}

		/** RPC
		*/ 
		public override void OnWebRpcResponse(ExitGames.Client.Photon.OperationResponse a_response)
		{
			Tool.Log("Pun","OnWebRpcResponse");
		}
	}
}
#endif

