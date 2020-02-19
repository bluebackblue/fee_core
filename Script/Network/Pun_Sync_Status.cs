

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
	public class Pun_Sync_Status : UnityEngine.MonoBehaviour , Photon.Pun.IPunObservable
	{
		/** view
		*/
		public Photon.Pun.PhotonView view;

		/** networkobject
		*/
		public NetworkObject_Player_Base networkobject;

		/** stream
		*/
		public Pun_Stream stream_send;
		public Pun_Stream stream_recv;

		/** Awake
		*/
		private void Awake()
		{
			this.stream_send = new Pun_Stream();
			this.stream_recv = new Pun_Stream();
		}

		/** Start
		*/
		private void Start()
		{
			this.view.Synchronization = Photon.Pun.ViewSynchronization.UnreliableOnChange;
		}

		/** OnPhotonSerializeView
		*/
		public void OnPhotonSerializeView(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info)
		{
			if(this.networkobject != null){
				if(a_stream.IsWriting){
					this.stream_send.SetStream(a_stream);
					this.networkobject.OnSendStatus(this.stream_send);
				}else{
					this.stream_recv.SetStream(a_stream);
					this.networkobject.OnRecvStatus(this.stream_recv);
				}
			}
		}
	}
}

