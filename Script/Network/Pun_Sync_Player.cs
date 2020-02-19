

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
	public class Pun_Sync_Player : UnityEngine.MonoBehaviour , Photon.Pun.IPunObservable , Sync_Base
	#else
	public class Pun_Sync_Player : UnityEngine.MonoBehaviour , Sync_Base
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

		/** Awake
		*/
		private void Awake()
		{
			#if(USE_DEF_FEE_PUN)
			this.stream_send = new Pun_Stream();
			this.stream_recv = new Pun_Stream();
			#endif
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
				this.view.Synchronization = Photon.Pun.ViewSynchronization.Unreliable;
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
					this.stream_send.SetStream(a_stream);
					this.networkobject.OnSendPlayer(this.stream_send);
				}else{
					this.stream_recv.SetStream(a_stream);
					this.networkobject.OnRecvPlayer(this.stream_recv);
				}
			}
		}
		#endif
	}
}

