

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
	/** Pun_Player_MonoBehaviour
	*/
	public abstract class Pun_Player_MonoBehaviour :  UnityEngine.MonoBehaviour ,  Photon.Pun.IPunObservable
	{
		/** OnPhotonSerializeView
		*/
		public abstract void OnPhotonSerializeView(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info);
	}
}

