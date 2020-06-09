

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

		/** room_list
		*/
		public System.Collections.Generic.Dictionary<string,RoomItem> room_list;

		/** game_version
		*/
		public string game_version;

		/** nick_name
		*/
		public string nick_name;

		/** pun
		*/
		#if(USE_DEF_FEE_PUN)
		private Pun pun;
		#endif

		/** [シングルトン]constructor
		*/
		private Network()
		{
			//room_list
			this.room_list = new System.Collections.Generic.Dictionary<string,RoomItem>();

			//game_version
			this.game_version = "0.01";

			//nick_name
			this.nick_name = "NickName";

			//pun
			#if(USE_DEF_FEE_PUN)
			this.pun = new Pun();
			#endif
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			#if(USE_DEF_FEE_PUN)
			this.pun.Delete();
			#endif
		}

		/** IsBusy
		*/
		public bool IsBusy()
		{
			#if(USE_DEF_FEE_PUN)
			return this.pun.IsBusy();
			#else
			return false;
			#endif
		}

		/** プレイヤータイプ。設定。
		*/
		public void SetPlayerType<T>()
			where T : Fee.Network.NetworkObject_Player_MonoBehaviour_Base
		{
			#if(USE_DEF_FEE_PUN)
			this.pun.SetPlayerType<T>();
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
			#if(USE_DEF_FEE_PUN)
			this.pun.RequestConnectMaster();
			#endif
		}

		/** ロビー。接続リクエスト。
		*/
		public void RequestConnectLobby()
		{
			#if(USE_DEF_FEE_PUN)
			this.pun.RequestConnectLobby();
			#endif
		}

		/** ロビー。接続リクエスト。
		*/
		public void RequestConnectRoom(string a_room_key,string a_room_info)
		{
			#if(USE_DEF_FEE_PUN)
			this.pun.RequestConnectRoom(a_room_key,a_room_info);
			#endif
		}

		/** マスター。切断リクエスト。
		*/
		public void RequestDisconnectMaster()
		{
			#if(USE_DEF_FEE_PUN)
			this.pun.RequestDisconnectMaster();
			#endif
		}

		/** ロビー。切断リクエスト。
		*/
		public void RequestDisconnectLobby()
		{
			#if(USE_DEF_FEE_PUN)
			this.pun.RequestDisconnectLobby();
			#endif
		}

		/** ロビー。切断リクエスト。
		*/
		public void RequestDisconnectRoom()
		{
			#if(USE_DEF_FEE_PUN)
			this.pun.RequestDisconnectRoom();
			#endif
		}

		/** CreatePlayer
		*/
		public UnityEngine.GameObject CreatePlayer()
		{
			#if(USE_DEF_FEE_PUN)
			return this.pun.CreatePlayer();
			#else
			return null;
			#endif
		}

		/** マスター。接続チェック。
		*/
		public bool IsConnectMaster()
		{
			#if(USE_DEF_FEE_PUN)
			return this.pun.IsConnectMaster();
			#else
			return false;
			#endif
		}

		/** ロビー。接続チェック。
		*/
		public bool IsConnectLobby()
		{
			#if(USE_DEF_FEE_PUN)
			return this.pun.IsConnectLobby();
			#else
			return false;
			#endif
		}

		/** ルーム。接続チェック。
		*/
		public bool IsConnectRoom()
		{
			#if(USE_DEF_FEE_PUN)
			return this.pun.IsConnectRoom();
			#else
			return false;
			#endif
		}
	}
}

