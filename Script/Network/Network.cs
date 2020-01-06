

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
		#if(USE_DEF_FEE_PUN)
		private Connect_Auto connect_auto;
		#endif

		/** ルート。
		*/
		private UnityEngine.GameObject root_gameobject;
		private UnityEngine.Transform root_transform;

		/** disconnect_request
		*/
		private bool disconnect_request;

		/** player_list
		*/
		private System.Collections.Generic.List<Player_MonoBehaviour> player_list;

		/** my_player
		*/
		private Player_MonoBehaviour my_player;

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
			#if(USE_DEF_FEE_PUN)
			this.connect_auto = null;
			#endif

			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Network";
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();

			//disconnect_request
			this.disconnect_request = false;

			//player_list
			this.player_list = new System.Collections.Generic.List<Player_MonoBehaviour>();

			//my_player
			this.my_player = null;

			//recv_callback
			this.recv_callback = null;
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** ルート。取得。
		*/
		public UnityEngine.Transform GetRoot()
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

				#if(USE_DEF_FEE_PUN)
				this.connect_auto = new Connect_Auto();
				#endif
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
		public System.Collections.Generic.List<Fee.Network.Player_MonoBehaviour> GetPlayerList()
		{
			return this.player_list;
		}

		/** プレイヤー。取得。
		*/
		public Fee.Network.Player_MonoBehaviour GetPlayer(int a_playerindex)
		{
			if((0<=a_playerindex)&&(a_playerindex<this.player_list.Count)){
				return this.player_list[a_playerindex];
			}
			return null;
		}

		/** 自分のプレイヤープレハブ。取得。
		*/
		public Fee.Network.Player_MonoBehaviour GetMyPlayer()
		{
			return this.my_player;
		}

		/** プレイヤ‐プレハブ。追加。
		*/
		public int AddPlayer(Fee.Network.Player_MonoBehaviour a_player)
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
		public void RemovePlayer(Fee.Network.Player_MonoBehaviour a_player)
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

					#if(USE_DEF_FEE_PUN)
					if(this.connect_auto.Main() == false){
						this.connect_auto = null;
						this.mode = Mode.Reset;
					}
					#endif
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

