

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
	#if(USE_DEF_FEE_PUN)
	public abstract class Pun_Player_MonoBehaviour :  UnityEngine.MonoBehaviour ,  Photon.Pun.IPunObservable
	#else
	public abstract class Pun_Player_MonoBehaviour :  UnityEngine.MonoBehaviour
	#endif
	{
		/** OnPhotonSerializeView
		*/
		#if(USE_DEF_FEE_PUN)
		public abstract void OnPhotonSerializeView(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info);
		#endif
	}
}

