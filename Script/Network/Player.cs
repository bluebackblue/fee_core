

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。プレイヤー。
*/


/** Fee.Network
*/
namespace Fee.Network
{
	/** Player
	*/
	#if(USE_DEF_FEE_PUN)
	public class Player : Photon.Pun.MonoBehaviourPun
	#else
	public class Player : UnityEngine.MonoBehaviour
	#endif
	{
		/** トランスフォーム。
		*/
		private UnityEngine.Transform mytransform;

		/** is_mine
		*/
		private bool is_mine;

		/** playerlist_index
		*/
		private int playerlist_index;

		/** photon_view
		*/
		#if(USE_DEF_FEE_PUN)
		private Photon.Pun.PhotonView photon_view;
		#endif

		/** [内部からの呼び出し]リモートコールキー。
		*/
		public enum Raw_RemoteCallKey
		{
		};

		/** 開始。
		*/
		void Start()
		{
			Tool.Log("Player","Start");

			//トランスフォーム。
			this.mytransform = this.GetComponent<UnityEngine.Transform>();

			//親。設定。
			UnityEngine.Transform t_root = Fee.Network.Network.GetInstance().GetRoot();
			this.GetComponent<UnityEngine.Transform>().SetParent(t_root);

			//photon_view
			#if(USE_DEF_FEE_PUN)
			this.photon_view = this.GetComponent<Photon.Pun.PhotonView>();
			#endif

			#if(USE_DEF_FEE_PUN)
			{
				//is_mine
				this.is_mine = this.photon_view.IsMine;
			}
			#else
			{
				//is_mine
				this.is_mine = true;
			}
			#endif

			//playerlist_index
			this.playerlist_index = -1;

			//this.name
			this.name = "Player";

			{
				//プレイヤ－インデックス取得。
				this.playerlist_index = Fee.Network.Network.GetInstance().AddPlayer(this);

				//名前設定。
				if(this.playerlist_index >= 0){
					this.name = "Player_" + this.playerlist_index.ToString();
				}
			}
		}

		/** 削除。
		*/
		private void OnDestroy()
		{
			Tool.Log("Player","OnDestroy");

			Fee.Network.Network.GetInstance().RemovePlayer(this);
		}

		/** 位置。取得。
		*/
		public UnityEngine.Vector3 GetPosition()
		{
			return this.mytransform.position;
		}

		/** 回転。取得。
		*/
		public UnityEngine.Quaternion GetQuaternion()
		{
			return this.mytransform.rotation;
		}

		/** スケール。取得。
		*/
		public UnityEngine.Vector3 GetScale()
		{
			return this.mytransform.transform.localScale;
		}

		/** 位置。設定。
		*/
		public void SetPosition(float a_x,float a_y,float a_z)
		{
			this.mytransform.transform.position = new UnityEngine.Vector3(a_x,a_y,a_z);
		}

		/** 回転。設定。
		*/
		public void SetQuaternion(in UnityEngine.Quaternion a_quaternion)
		{
			this.mytransform.transform.rotation = a_quaternion;
		}

		/** スケール。設定。
		*/
		public void SetScale(float a_x,float a_y,float a_z)
		{
			this.mytransform.transform.localScale = new UnityEngine.Vector3(a_x,a_y,a_z);
		}

		/** 自分。チェック。
		*/
		public bool IsMine()
		{
			return this.is_mine;
		}

		/** ニックネーム。取得。
		*/
		public string GetNickName()
		{
			#if(USE_DEF_FEE_PUN)
			return this.photon_view.Owner.NickName;
			#else
			return "";
			#endif
		}

		/** ユニークID。取得。
		*/
		public string GetUniqueID()
		{
			#if(USE_DEF_FEE_PUN)
			return this.photon_view.Owner.UserId;
			#else
			return this.GetHashCode().ToString();
			#endif
		}

		/** マスタークライアント。
		*/
		public bool IsMasterClient()
		{
			#if(USE_DEF_FEE_PUN)
			return this.photon_view.Owner.IsMasterClient;
			#else
			return true;
			#endif
		}

		/** [内部からの呼び出し]リモートコール。受信。
		*/
		#if(USE_DEF_FEE_PUN)
		[Photon.Pun.PunRPC]
		public void Raw_Recv(Raw_RemoteCallKey a_key,int a_value)
		{
		}
		#endif

		/** [内部からの呼び出し]リモートコール。マスタークライアント。
		*/
		public void Raw_RemoteCall_MasterClient(Raw_RemoteCallKey a_key,int a_value)
		{
			#if(USE_DEF_FEE_PUN)
			if(this.photon_view != null){
				this.photon_view.RPC("Raw_Recv",Photon.Pun.RpcTarget.MasterClient,a_key,a_value);
			}
			#endif
		}

		/** [内部からの呼び出し]リモートコール。全員。
		*/
		public void Raw_RemoteCall_All(Raw_RemoteCallKey a_key,int a_value)
		{
			#if(USE_DEF_FEE_PUN)
			if(this.photon_view != null){
				this.photon_view.RPC("Raw_Recv",Photon.Pun.RpcTarget.All,a_key,a_value);
			}
			#endif
		}

		/** リモートコール。受信。
		*/
		#if(USE_DEF_FEE_PUN)
		[Photon.Pun.PunRPC]
		public void RecvInt(int a_key,int a_value)
		{
			OnRemoteCallBack_Base t_callback = Fee.Network.Network.GetInstance().GetRecvCallBack();
			if(t_callback != null){
				t_callback.OnRemoteCallInt(this.playerlist_index,a_key,a_value);
			}
		}
		#endif

		/** リモートコール。受信。
		*/
		#if(USE_DEF_FEE_PUN)
		[Photon.Pun.PunRPC]
		public void RecvString(int a_key,string a_value)
		{
			OnRemoteCallBack_Base t_callback = Fee.Network.Network.GetInstance().GetRecvCallBack();
			if(t_callback != null){
				t_callback.OnRemoteCallString(this.playerlist_index,a_key,a_value);
			}
		}
		#endif

		/** リモートコール。
		*/
		public void RemoteCallInt(int a_key,int a_value)
		{
			#if(USE_DEF_FEE_PUN)
			if(this.photon_view != null){
				this.photon_view.RPC("RecvInt",Photon.Pun.RpcTarget.All,a_key,a_value);
			}
			#endif
		}

		/** リモートコール。
		*/
		public void RemoteCallString(int a_key,string a_value)
		{
			#if(USE_DEF_FEE_PUN)
			if(this.photon_view != null){
				this.photon_view.RPC("RecvString",Photon.Pun.RpcTarget.All,a_key,a_value);
			}
			#endif
		}
	}
}

