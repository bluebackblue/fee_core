

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
		public Pun_MonoBehaviour pun;

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
			//pun
			this.pun = Pun_MonoBehaviour.Create();

			//room_list
			this.room_list = new System.Collections.Generic.Dictionary<string,RoomItem>();

			//step
			this.step = Step.None;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			this.pun.Delete();
			this.pun = null;
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
				this.pun.GetResultMaster().mode = Pun_MonoBehaviour.Mode.Request;
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
				this.pun.GetResultLobby().mode = Pun_MonoBehaviour.Mode.Request;
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
				this.pun.GetResultRoom().mode = Pun_MonoBehaviour.Mode.Request;
				this.pun.GetResultRoom().room_key = a_room_key;
				this.pun.GetResultRoom().room_info = a_room_info;
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
				this.pun.GetResultMaster().mode = Pun_MonoBehaviour.Mode.Request;
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
				this.pun.GetResultLobby().mode = Pun_MonoBehaviour.Mode.Request;
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
				this.pun.GetResultRoom().mode = Pun_MonoBehaviour.Mode.Request;
			}else{
				//処理中。
				Tool.Assert(false);
			}
		}

		/** CreatePlayer
		*/
		public bool CreatePlayer(Fee.File.Path a_resources_path)
		{
			return this.pun.CreatePlayer(a_resources_path);
		}

		/** マスター。接続チェック。
		*/
		public bool IsConnectMaster()
		{
			return this.pun.IsConnectMaster();
		}

		/** ロビー。接続チェック。
		*/
		public bool IsConnectLobby()
		{
			return this.pun.IsConnectLobby();
		}

		/** ルーム。接続チェック。
		*/
		public bool IsConnectRoom()
		{
			return this.pun.IsConnectRoom();
		}

		/** 更新。
		*/
		public void Main()
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
	}
}

