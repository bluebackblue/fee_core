

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。
*/


/** Fee.Network
*/
namespace Fee.Network
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

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Network GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

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

		/** pun
		*/
		#if(USE_DEF_FEE_PUN)
		public Pun_MonoBehaviour pun;
		#endif

		/** pun_dataloader
		*/
		#if(USE_DEF_FEE_PUN)
		public Pun_DataLoader pun_dataloader;
		#endif

		/** room_list
		*/
		public System.Collections.Generic.Dictionary<string,RoomItem> room_list;

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
		private Step step;

		/** [シングルトン]constructor
		*/
		private Network()
		{
			#if(USE_DEF_FEE_PUN)
			{
				//pun
				this.pun = Pun_MonoBehaviour.Create();

				//pun_dataloader
				this.pun_dataloader = new Pun_DataLoader();
				Photon.Pun.PhotonNetwork.PrefabPool = this.pun_dataloader;
			}
			#endif

			//room_list
			this.room_list = new System.Collections.Generic.Dictionary<string,RoomItem>();

			//step
			this.step = Step.None;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			#if(USE_DEF_FEE_PUN)
			if(this.pun != null){
				this.pun.Delete();
				this.pun = null;
			}
			#endif
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

		/** プレイヤータイプ。設定。
		*/
		public void SetPlayeType<T>()
			where T : Fee.Network.NetworkObject_Player_Base
		{
			#if(USE_DEF_FEE_PUN)
			{
				this.pun_dataloader.SetPlayerComponent<T>();
			}
			#endif
		}

		/** ルームリスト。取得。
		*/
		public System.Collections.Generic.Dictionary<string,RoomItem> GetRoomList()
		{
			return this.room_list;
		}

		/** マスター。接続リクエスト。
		*/
		public void RequestConnectMaster()
		{
			if(this.step == Step.None){
				//接続。
				this.step = Step.ConnectMaster;
				#if(USE_DEF_FEE_PUN)
				{
					this.pun.GetResultMaster().mode = Pun_MonoBehaviour.Mode.Request;
				}
				#endif
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
				#if(USE_DEF_FEE_PUN)
				{
					this.pun.GetResultLobby().mode = Pun_MonoBehaviour.Mode.Request;
				}
				#endif
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
				#if(USE_DEF_FEE_PUN)
				{
					this.pun.GetResultRoom().mode = Pun_MonoBehaviour.Mode.Request;
					this.pun.GetResultRoom().room_key = a_room_key;
					this.pun.GetResultRoom().room_info = a_room_info;
				}
				#endif
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
				#if(USE_DEF_FEE_PUN)
				{
					this.pun.GetResultMaster().mode = Pun_MonoBehaviour.Mode.Request;
				}
				#endif
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
				#if(USE_DEF_FEE_PUN)
				{
					this.pun.GetResultLobby().mode = Pun_MonoBehaviour.Mode.Request;
				}
				#endif
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
				#if(USE_DEF_FEE_PUN)
				{
					this.pun.GetResultRoom().mode = Pun_MonoBehaviour.Mode.Request;
				}
				#endif
			}else{
				//処理中。
				Tool.Assert(false);
			}
		}

		/** CreatePlayer
		*/
		public bool CreatePlayer()
		{
			#if(USE_DEF_FEE_PUN)
			{
				return this.pun.CreatePlayer();
			}
			#else
			{
				return false;
			}
			#endif
		}

		/** マスター。接続チェック。
		*/
		public bool IsConnectMaster()
		{
			#if(USE_DEF_FEE_PUN)
			{
				return this.pun.IsConnectMaster();
			}
			#else
			{
				return false;
			}
			#endif
		}

		/** ロビー。接続チェック。
		*/
		public bool IsConnectLobby()
		{
			#if(USE_DEF_FEE_PUN)
			{
				return this.pun.IsConnectLobby();
			}
			#else
			{
				return false;
			}
			#endif
		}

		/** ルーム。接続チェック。
		*/
		public bool IsConnectRoom()
		{
			#if(USE_DEF_FEE_PUN)
			{
				return this.pun.IsConnectRoom();
			}
			#else
			{
				return false;
			}
			#endif
		}

		/** 更新。
		*/
		public void Main()
		{
			#if(USE_DEF_FEE_PUN)
			{
				this.Main_Pun();
			}
			#endif
		}

		/** 更新。
		*/
		#if(USE_DEF_FEE_PUN)
		public void Main_Pun()
		{
			switch(this.step){
			case Step.ConnectMaster:
				{
					switch(this.pun.GetResultMaster().mode){
					case Pun_MonoBehaviour.Mode.Request:
						{
							//開始。
							this.pun.RequestConnectMaster();
						}break;
					case Pun_MonoBehaviour.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_MonoBehaviour.Mode.Result:
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
					switch(this.pun.GetResultLobby().mode){
					case Pun_MonoBehaviour.Mode.Request:
						{
							//開始。
							this.pun.RequestConnectLobby();
						}break;
					case Pun_MonoBehaviour.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_MonoBehaviour.Mode.Result:
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
					switch(this.pun.GetResultRoom().mode){
					case Pun_MonoBehaviour.Mode.Request:
						{
							//開始。
							this.pun.RequestConnectRoom();
						}break;
					case Pun_MonoBehaviour.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_MonoBehaviour.Mode.Result:
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
					switch(this.pun.GetResultMaster().mode){
					case Pun_MonoBehaviour.Mode.Request:
						{
							//開始。
							this.pun.RequestDisconnectMaster();
						}break;
					case Pun_MonoBehaviour.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_MonoBehaviour.Mode.Result:
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
					switch(this.pun.GetResultLobby().mode){
					case Pun_MonoBehaviour.Mode.Request:
						{
							//開始。
							this.pun.RequestDisconnectLobby();
						}break;
					case Pun_MonoBehaviour.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_MonoBehaviour.Mode.Result:
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
					switch(this.pun.GetResultRoom().mode){
					case Pun_MonoBehaviour.Mode.Request:
						{
							//開始。
							this.pun.RequestDisconnectRoom();
						}break;
					case Pun_MonoBehaviour.Mode.Busy:
						{
							//処理中。
						}break;
					case Pun_MonoBehaviour.Mode.Result:
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
		#endif
	}
}

