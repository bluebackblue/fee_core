

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。同期。
*/


/** Fee.Network
*/
namespace Fee.Network
{
	/** Sync
	*/
	public class Sync
	{
		#if(USE_DEF_FEE_PUN)

		/** networkobject
		*/
		private NetworkObject_Player_MonoBehaviour_Base networkobject;

		/** sync
		*/
		private Pun_Sync_Player_MonoBehaviour player_sync;
		private Pun_Sync_Status_MonoBehaviour status_sync;

		/** view
		*/
		private Photon.Pun.PhotonView player_view;
		private Photon.Pun.PhotonView status_view;

		/** stream
		*/
		private Pun_Stream player_stream_send;
		private Pun_Stream player_stream_recv;
		private Pun_Stream status_stream_send;
		private Pun_Stream status_stream_recv;
	
		/** interval
		*/
		private int status_interval;
		private int status_interval_max;

		#endif

		/** constructor。
		*/
		public Sync(UnityEngine.GameObject a_networkobject)
		{
			#if(USE_DEF_FEE_PUN)

			this.player_sync = a_networkobject.AddComponent<Pun_Sync_Player_MonoBehaviour>();
			this.player_sync.SetSync(this);

			this.status_sync = a_networkobject.AddComponent<Pun_Sync_Status_MonoBehaviour>();
			this.status_sync.SetSync(this);

			this.player_view = a_networkobject.AddComponent<Photon.Pun.PhotonView>();
			this.status_view = a_networkobject.AddComponent<Photon.Pun.PhotonView>();

			this.player_stream_send = new Pun_Stream();
			this.player_stream_recv = new Pun_Stream();
			this.status_stream_send = new Pun_Stream();
			this.status_stream_recv = new Pun_Stream();

			this.status_interval = 0;
			this.status_interval_max = Config.DEFAULT_PLAYER_STATUS_SEND_INTERVAL;

			{
				this.player_view.ObservedComponents = new System.Collections.Generic.List<UnityEngine.Component>();
				this.player_view.ObservedComponents.Add(this.player_sync);
				this.status_view.ObservedComponents = new System.Collections.Generic.List<UnityEngine.Component>();
				this.status_view.ObservedComponents.Add(this.status_sync);
			}

			#endif
		}

		/** ネットワークオブジェクト。設定。
		*/
		public void SetNetworkObject(NetworkObject_Player_MonoBehaviour_Base a_networkobject)
		{
			#if(USE_DEF_FEE_PUN)
			this.networkobject = a_networkobject;
			#endif
		}

		/** 自分自身。
		*/
		public bool IsSelf()
		{
			#if(USE_DEF_FEE_PUN)
			return this.status_view.IsMine;
			#else
			return true;
			#endif
		}

		/** 同期。設定。
		*/
		public void SetStatusSync(bool a_flag)
		{
			#if(USE_DEF_FEE_PUN)
			if(a_flag == true){
				this.status_view.Synchronization = Photon.Pun.ViewSynchronization.ReliableDeltaCompressed;
			}else{
				this.status_view.Synchronization = Photon.Pun.ViewSynchronization.Off;
			}
			#endif
		}

		/** 同期。設定。
		*/
		public void SetPlayerSync(bool a_flag)
		{
			#if(USE_DEF_FEE_PUN)
			if(a_flag == true){
				this.status_view.Synchronization = Photon.Pun.ViewSynchronization.Unreliable;
			}else{
				this.status_view.Synchronization = Photon.Pun.ViewSynchronization.Off;
			}
			#endif
		}

		/** OnStartNetworkObject
		*/
		public void OnStartNetworkObject()
		{
			this.SetStatusSync(true);
			this.SetPlayerSync(true);
		}

		/** OnPhotonSerializeView_Status
		*/
		#if(USE_DEF_FEE_PUN)
		public void OnPhotonSerializeView_Status(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info)
		{
			if(this.networkobject != null){
				if(a_stream.IsWriting){
					this.status_interval--;
					if(this.status_interval <= 0){
						this.status_interval = this.status_interval_max;
						this.status_stream_send.SetStream(a_stream);
						this.networkobject.OnSendStatus(this.status_stream_send);
					}
				}else{
					Tool.Log("Recv","");

					this.status_stream_recv.SetStream(a_stream);
					this.networkobject.OnRecvStatus(this.status_stream_recv);
				}
			}
		}
		#endif

		/** OnPhotonSerializeView_Player
		*/
		#if(USE_DEF_FEE_PUN)
		public void OnPhotonSerializeView_Player(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info)
		{
			if(this.networkobject != null){
				if(a_stream.IsWriting){
					this.player_stream_send.SetStream(a_stream);
					this.networkobject.OnSendPlayer(this.player_stream_send);
				}else{
					this.player_stream_recv.SetStream(a_stream);
					this.networkobject.OnRecvPlayer(this.player_stream_recv);
				}
			}
		}
		#endif
	}
}

