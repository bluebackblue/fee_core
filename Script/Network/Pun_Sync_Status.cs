

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
	/** constructor
	*/
	#if(USE_DEF_FEE_PUN)
	public class Pun_Sync_Status : UnityEngine.MonoBehaviour , Photon.Pun.IPunObservable , Sync_Base
	#else
	public class Pun_Sync_Status : UnityEngine.MonoBehaviour , Sync_Base
	#endif
	{
		/** view
		*/
		#if(USE_DEF_FEE_PUN)
		public Photon.Pun.PhotonView view;
		#endif

		/** networkobject
		*/
		public NetworkObject_Player_Base networkobject;

		/** stream
		*/
		#if(USE_DEF_FEE_PUN)
		public Pun_Stream stream_send;
		public Pun_Stream stream_recv;
		#endif

		/** interval
		*/
		public int interval;
		public int interval_max;

		/** Awake
		*/
		private void Awake()
		{
			#if(USE_DEF_FEE_PUN)
			this.stream_send = new Pun_Stream();
			this.stream_recv = new Pun_Stream();
			#endif

			this.interval = 0;
			this.interval_max = Config.DEFAULT_PLAYER_STATUS_SEND_INTERVAL;
		}

		/** [Fee.Network.Sync_Base.IsSelf]自分自身。
		*/
		public bool IsSelf()
		{
			#if(USE_DEF_FEE_PUN)
			return this.view.IsMine;
			#else
			return true;
			#endif
		}

		/** [Fee.Network.Sync_Base.IsSelf]同期。設定。
		*/
		public void SetSync(bool a_flag)
		{
			#if(USE_DEF_FEE_PUN)
			if(a_flag == true){
				this.view.Synchronization = Photon.Pun.ViewSynchronization.ReliableDeltaCompressed;
			}else{
				this.view.Synchronization = Photon.Pun.ViewSynchronization.Off;
			}
			#endif
		}

		/** OnPhotonSerializeView
		*/
		#if(USE_DEF_FEE_PUN)
		public void OnPhotonSerializeView(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info)
		{
			if(this.networkobject != null){
				if(a_stream.IsWriting){
					this.interval--;
					if(this.interval <= 0){
						this.interval = this.interval_max;
						this.stream_send.SetStream(a_stream);
						this.networkobject.OnSendStatus(this.stream_send);
					}
				}else{
					Tool.Log("Recv","");

					this.stream_recv.SetStream(a_stream);
					this.networkobject.OnRecvStatus(this.stream_recv);
				}
			}
		}
		#endif
	}
}

