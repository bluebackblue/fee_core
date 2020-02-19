

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

		/** interval
		*/
		public int interval;
		public int interval_max;

		/** Awake
		*/
		private void Awake()
		{
			this.stream_send = new Pun_Stream();
			this.stream_recv = new Pun_Stream();

			this.interval = 0;
			this.interval_max = Config.DEFAULT_PLAYER_STATUS_SEND_INTERVAL;
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
					this.interval--;
					if(this.interval <= 0){
						this.interval = this.interval_max;
						this.stream_send.SetStream(a_stream);
						this.networkobject.OnSendStatus(this.stream_send);

						Tool.Log("OnSendStatus","");
					}
				}else{
					this.stream_recv.SetStream(a_stream);
					this.networkobject.OnRecvStatus(this.stream_recv);
				}
			}
		}
	}
}

