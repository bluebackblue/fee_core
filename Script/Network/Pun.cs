

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
	/** Pun
	*/
	public class Pun
	{
		/** Step
		*/
		public enum Step
		{
			/** None
			*/
			None,

			/** ConnectMaster
			*/
			ConnectMaster,

			/** ConnectLobby
			*/
			ConnectLobby,

			/** ConnectRoom
			*/
			ConnectRoom,

			/** DisconnectMaster
			*/
			DisconnectMaster,

			/** DisconnectLobby
			*/
			DisconnectLobby,

			/** ConnectRoom
			*/
			DisconnectRoom,
		}

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

		/** pun
		*/
		private UnityEngine.GameObject pun_gameobject;
		private Pun_MonoBehaviour pun_monobehaviour;

		/** pun_dataloader
		*/
		private Pun_DataLoader pun_dataloader;

		/** step
		*/
		private Step step;

		/** status_master
		*/
		private Status_Master status_master;

		/** status_lobby
		*/
		private Pun_Status_Lobby status_lobby;

		/** status_room
		*/
		private Pun_Status_Room status_room;

		/** constructor
		*/
		public Pun()
		{
			//pun
			{
				this.pun_gameobject = new UnityEngine.GameObject("Pun");
				UnityEngine.GameObject.DontDestroyOnLoad(this.pun_gameobject);
				this.pun_monobehaviour = this.pun_gameobject.AddComponent<Pun_MonoBehaviour>();
				this.pun_monobehaviour.pun = this;
			}

			//pun_dataloader
			this.pun_dataloader = new Pun_DataLoader();
			Photon.Pun.PhotonNetwork.PrefabPool = this.pun_dataloader;

			//step
			this.step = Step.None;

			//status_master
			this.status_master = new Status_Master();
			this.status_master.Reset();

			//status_lobby
			this.status_lobby = new Pun_Status_Lobby();
			this.status_lobby.Reset();

			//status_room
			this.status_room = new Pun_Status_Room();
			this.status_room.Reset();

			//SetRowUpdate
			Fee.Function.Function.GetInstance().SetRowUpdate(this.Main);
		}

		/** 削除。
		*/
		public void Delete()
		{
			if(this.pun_gameobject != null){
				this.pun_monobehaviour.pun = null;
				UnityEngine.GameObject.Destroy(this.pun_gameobject);
				this.pun_gameobject = null;
			}

			//UnSetRowUpdate
			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.Main);
		}

		/** プレイヤータイプ。設定。
		*/
		public void SetPlayerType<T>()
			where T : Fee.Network.NetworkObject_Player_MonoBehaviour_Base
		{
			this.pun_dataloader.SetPlayerComponent<T>();
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

		/** IsBusy
		*/
		public bool IsBusy()
		{
			if(this.step == Step.None){
				return false;
			}
			return true;
		}

		/** マスター。接続リクエスト。
		*/
		public void RequestConnectMaster()
		{
			if(this.step == Step.None){
				//接続。
				this.step = Step.ConnectMaster;
				
				this.status_master.mode = Status_Master.Mode.Request;
			}else{
				//処理中。
				Tool.Assert(false);
			}
		}

		/** ロビー。接続リクエスト。
		*/
		public void RequestConnectLobby()
		{
			if(this.step == Step.None){
				//接続。
				this.step = Step.ConnectLobby;
				
				this.status_lobby.mode = Pun_Status_Lobby.Mode.Request;
			}else{
				//処理中。
				Tool.Assert(false);
			}
		}

		/** ロビー。接続リクエスト。
		*/
		public void RequestConnectRoom(string a_room_key,string a_room_info)
		{
			if(this.step == Step.None){
				//接続。
				this.step = Step.ConnectRoom;
				{
					this.status_room.mode = Pun_Status_Room.Mode.Request;
					this.status_room.room_key = a_room_key;
					this.status_room.room_info = a_room_info;
				}
			}else{
				//処理中。
				Tool.Assert(false);
			}
		}

		/** マスター。切断リクエスト。
		*/
		public void RequestDisconnectMaster()
		{
			if(this.step == Step.None){
				//切断。
				this.step = Step.DisconnectMaster;
				{
					this.status_master.mode = Status_Master.Mode.Request;
				}
			}else{
				//処理中。
				Tool.Assert(false);
			}
		}

		/** ロビー。切断リクエスト。
		*/
		public void RequestDisconnectLobby()
		{
			if(this.step == Step.None){
				//切断。
				this.step = Step.DisconnectLobby;
				{
					this.status_lobby.mode = Pun_Status_Lobby.Mode.Request;
				}
			}else{
				//処理中。
				Tool.Assert(false);
			}
		}

		/** ロビー。切断リクエスト。
		*/
		public void RequestDisconnectRoom()
		{
			if(this.step == Step.None){
				//切断。
				this.step = Step.DisconnectRoom;
				{
					this.status_room.mode = Pun_Status_Room.Mode.Request;
				}
			}else{
				//処理中。
				Tool.Assert(false);
			}
		}

		/** マスター。接続チェック。
		*/
		public bool IsConnectMaster()
		{
			if(Photon.Pun.PhotonNetwork.IsConnected == true){
				return true;
			}
			return false;
		}

		/** ロビー。接続チェック。
		*/
		public bool IsConnectLobby()
		{
			if(Photon.Pun.PhotonNetwork.InLobby == true){
				return true;
			}
			return false;
		}

		/** ルーム。接続チェック。
		*/
		public bool IsConnectRoom()
		{
			if(Photon.Pun.PhotonNetwork.InRoom == true){
				return true;
			}
			return false;
		}

		/** [Fee.Network.Pun_MonoBehaviour]マスター。接続。リージョン受信。
		*/
		public void OnPunRegionListReceived(Photon.Realtime.RegionHandler a_region_handler)
		{
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
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
			#endif
		}

		/** [Fee.Network.Pun_MonoBehaviour]マスター。切断。失敗。
		*/
		public void OnPunDisconnected(Photon.Realtime.DisconnectCause a_cause)
		{
			this.status_master.SetDisconnect();
			this.status_lobby.SetDisconnect();
			this.status_room.SetDisconnect();

			Fee.Network.Network.GetInstance().room_list.Clear();

			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			{
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
			#endif
		}

		/** [Fee.Network.Pun_MonoBehaviour]ロビー。ルームリスト。取得。
		*/
		public void OnPunRoomListUpdate(System.Collections.Generic.List<Photon.Realtime.RoomInfo> a_room_list)
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
		}

		/** [Fee.Network.Pun_MonoBehaviour]マスター。接続。成功。
		*/
		public void OnPunConnectedToMaster()
		{
			this.status_master.SetConnect();
			Fee.Network.Network.GetInstance().room_list.Clear();
		}

		/** [Fee.Network.Pun_MonoBehaviour]ロビー。接続。成功。
		*/
		public void OnPunJoinedLobby()
		{
			this.status_lobby.SetConnect();
			Fee.Network.Network.GetInstance().room_list.Clear();
		}
 
		/** [Fee.Network.Pun_MonoBehaviour]ロビー。切断。失敗。
		*/
		public void OnPunLeftLobby()
		{
			this.status_lobby.SetDisconnect();
			Fee.Network.Network.GetInstance().room_list.Clear();
		}

		/** [Fee.Network.Pun_MonoBehaviour]ルーム。作成。失敗。
		*/ 
		public void OnPunCreateRoomFailed(short a_return_code,string a_message)
		{
			this.status_room.SetDisconnect();
		}

		/** [Fee.Network.Pun_MonoBehaviour]ルーム。接続。成功。
		*/
		public void OnPunJoinedRoom()
		{
			this.status_room.SetConnect();
		}

		/** [Fee.Network.Pun_MonoBehaviour]ルーム。接続。失敗。
		*/
		public void OnPunJoinRoomFailed(short a_return_code,string a_message)
		{
			this.status_room.SetDisconnect();
		}

		/** [Fee.Network.Pun_MonoBehaviour]ルーム。接続。失敗。
		*/ 
		public void OnPunJoinRandomFailed(short a_return_code, string message)
		{
			this.status_room.SetDisconnect();
		}

		/** [Fee.Network.Pun_MonoBehaviour]ルーム。切断。失敗。
		*/ 
		public void OnPunLeftRoom()
		{
			this.status_room.SetDisconnect();
		}

		/** Main
		*/
		private void Main()
		{
			switch(this.step){
			case Step.ConnectMaster:
				{
					switch(this.status_master.mode){
					case Status_Master.Mode.Request:
						{
							//開始。
							this.Main_RequestConnectMaster();
						}break;
					case Status_Master.Mode.Busy:
						{
							//処理中。
						}break;
					case Status_Master.Mode.Result:
						{
							//完了。
							this.step = Step.None;
						}break;
					default:
						{
							Tool.Assert(false);
						}break;
					}
				}break;
			case Step.ConnectLobby:
				{
					switch(this.status_lobby.mode){
					case Pun_Status_Lobby.Mode.Request:
						{
							//開始。
							this.Main_RequestConnectLobby();
						}break;
					case Pun_Status_Lobby.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_Status_Lobby.Mode.Result:
						{
							//完了。
							this.step = Step.None;
						}break;
					default:
						{
							Tool.Assert(false);
						}break;
					}
				}break;
			case Step.ConnectRoom:
				{
					switch(this.status_room.mode){
					case Pun_Status_Room.Mode.Request:
						{
							//開始。
							this.Main_RequestConnectRoom();
						}break;
					case Pun_Status_Room.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_Status_Room.Mode.Result:
						{
							//完了。
							this.step = Step.None;
						}break;
					default:
						{
							Tool.Assert(false);
						}break;
					}
				}break;
			case Step.DisconnectMaster:
				{
					switch(this.status_master.mode){
					case Status_Master.Mode.Request:
						{
							//開始。
							this.Main_RequestDisconnectMaster();
						}break;
					case Status_Master.Mode.Busy:
						{
							//処理中。
						}break;
					case Status_Master.Mode.Result:
						{
							//完了。
							this.step = Step.None;
						}break;
					default:
						{
							Tool.Assert(false);
						}break;
					}
				}break;
			case Step.DisconnectLobby:
				{
					switch(this.status_lobby.mode){
					case Pun_Status_Lobby.Mode.Request:
						{
							//開始。
							this.Main_RequestDisconnectLobby();
						}break;
					case Pun_Status_Lobby.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_Status_Lobby.Mode.Result:
						{
							//完了。
							this.step = Step.None;
						}break;
					default:
						{
							Tool.Assert(false);
						}break;
					}
				}break;
			case Step.DisconnectRoom:
				{
					switch(this.status_room.mode){
					case Pun_Status_Room.Mode.Request:
						{
							//開始。
							this.Main_RequestDisconnectRoom();
						}break;
					case Pun_Status_Room.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_Status_Room.Mode.Result:
						{
							//完了。
							this.step = Step.None;
						}break;
					default:
						{
							Tool.Assert(false);
						}break;
					}
				}break;
			}
		}

		/** マスター。リクエスト。接続。
		*/
		private void Main_RequestConnectMaster()
		{
			if(this.status_master.mode != Status_Master.Mode.Request){
				//不明。
				Tool.Assert(false);
				this.status_master.mode = Status_Master.Mode.Result;
				return;
			}

			if(this.IsConnectMaster() == true){
				//すでに接続済み。
				this.status_master.SetConnect();;
				return;
			}

			//接続。
			{
				Photon.Pun.PhotonNetwork.AutomaticallySyncScene = false;
				Photon.Pun.PhotonNetwork.NickName = Fee.Network.Network.GetInstance().nick_name;
				Photon.Pun.PhotonNetwork.GameVersion = Fee.Network.Network.GetInstance().game_version;
				
				if(Photon.Pun.PhotonNetwork.ConnectUsingSettings() == true){
					this.status_master.SetBusy();
				}else{
					//リクエスト失敗。
					this.status_master.mode = Status_Master.Mode.Result;
				}
			}
		}

		/** マスター。リクエスト。切断。
		*/
		private void Main_RequestDisconnectMaster()
		{
			if(this.status_master.mode != Status_Master.Mode.Request){
				//不明。
				Tool.Assert(false);
				this.status_master.mode = Status_Master.Mode.Result;
				return;
			}

			if(this.IsConnectMaster() == false){
				//すでに切断済み。
				this.status_master.SetDisconnect();
				return;
			}

			//切断。
			{
				Photon.Pun.PhotonNetwork.Disconnect();
				this.status_master.SetBusy();
			}
		}

		/** リクエスト。ロビー。接続。
		*/
		private void Main_RequestConnectLobby()
		{
			if(this.status_lobby.mode != Pun_Status_Lobby.Mode.Request){
				//不明。
				Tool.Assert(false);
				this.status_lobby.mode = Pun_Status_Lobby.Mode.Result;
				return;
			}

			if(this.IsConnectLobby() == true){
				//すでに接続済み。
				this.status_lobby.SetConnect();
				return;
			}

			if(this.IsConnectMaster() == false){
				//マスターが切断されている。
				this.status_lobby.SetDisconnect();
				return;
			}

			//接続。
			{
				if(Photon.Pun.PhotonNetwork.JoinLobby() == true){
					this.status_lobby.SetBusy();
				}else{
					//リクエスト失敗。
					this.status_lobby.mode = Pun_Status_Lobby.Mode.Result;
				}
			}
		}

		/** リクエスト。ロビー。切断。
		*/
		private void Main_RequestDisconnectLobby()
		{
			if(this.status_lobby.mode != Pun_Status_Lobby.Mode.Request){
				//不明。
				Tool.Assert(false);
				this.status_lobby.mode = Pun_Status_Lobby.Mode.Result;
				return;
			}

			if(this.IsConnectLobby() == false){
				//すでに切断済み。
				this.status_lobby.SetDisconnect();
				return;
			}

			if(this.IsConnectMaster() == false){
				//マスターが切断されている。
				this.status_lobby.SetDisconnect();
				return;
			}

			//切断。
			{
				if(Photon.Pun.PhotonNetwork.LeaveLobby() == true){
					this.status_lobby.SetBusy();
				}else{
					//リクエスト失敗。
					this.status_lobby.mode = Pun_Status_Lobby.Mode.Result;
				}
			}
		}

		/** リクエスト。ルーム。接続。
		*/
		private void Main_RequestConnectRoom()
		{
			if(this.status_room.mode != Pun_Status_Room.Mode.Request){
				//不明。
				Tool.Assert(false);
				this.status_master.mode = Status_Master.Mode.Result;
				return;
			}

			if(this.IsConnectRoom() == true){
				//すでに接続済み。
				this.status_room.SetConnect();
				return;
			}

			if(this.IsConnectMaster() == false){
				//マスターが切断されている。
				this.status_room.SetDisconnect();
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
						t_custom_room_propertie.Add("room_info",this.status_room.room_info);
					}
					t_room_option.CustomRoomProperties = t_custom_room_propertie;
										
					//CustomRoomPropertiesForLobby
					t_room_option.CustomRoomPropertiesForLobby = new string[]{
						"room_info"
					};
				}

				Tool.Log("Pun","JoinOrCreateRoom : " + "room_key = " + this.status_room.room_key + " : room_info = " + this.status_room.room_info);
				if(Photon.Pun.PhotonNetwork.JoinOrCreateRoom(this.status_room.room_key,t_room_option,Photon.Realtime.TypedLobby.Default) == true){
					this.status_room.SetBusy();
				}else{
					//リクエスト失敗。
					this.status_room.mode = Pun_Status_Room.Mode.Result;
				}
			}
		}

		/** リクエスト。ルーム。切断。
		*/
		private void Main_RequestDisconnectRoom()
		{
			if(this.status_room.mode != Pun_Status_Room.Mode.Request){
				//不明。
				Tool.Assert(false);
				this.status_room.mode = Pun_Status_Room.Mode.Result;
				return;
			}

			if(this.IsConnectRoom() == false){
				//すでに切断済み。
				this.status_room.SetDisconnect();
				return;
			}

			if(this.IsConnectMaster() == false){
				//マスターが切断されている。
				this.status_room.SetDisconnect();
				return;
			}

			//切断。
			{
				if(Photon.Pun.PhotonNetwork.LeaveRoom() == true){
					this.status_room.SetBusy();
				}else{
					//リクエスト失敗。
					this.status_room.mode = Pun_Status_Room.Mode.Result;
				}
			}
		}
	}
}
#endif

