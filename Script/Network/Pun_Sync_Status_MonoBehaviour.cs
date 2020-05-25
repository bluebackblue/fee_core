

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。
*/


/** Fee.Network
*/
#if(USE_DEF_FEE_PUN)
namespace Fee.Network
{
	/** constructor
	*/
	public class Pun_Sync_Status_MonoBehaviour : UnityEngine.MonoBehaviour , Photon.Pun.IPunObservable
	{
		/** sync
		*/
		private Sync sync;

		/** 設定。
		*/
		public void SetSync(Sync a_sync)
		{
			this.sync = a_sync;
		}

		/** OnPhotonSerializeView
		*/
		public void OnPhotonSerializeView(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info)
		{
			this.sync.OnPhotonSerializeView_Status(a_stream,a_info);
		}
	}
}
#endif

