

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

		１：空のプレハブを作成。
		２：Photon.Pun.PhotonViewをアタッチする。
		３：Fee.Network.Pun_Player_MonoBehaviourを継承したクラスを作る。
		４：Fee.Network.Pun_Player_MonoBehaviourを継承したクラスをアタッチする。
		５：２でアタッチしたコンポーネントのObservedComponentsに４でアタッチしたコンポーネントを追加する。

	*/
	#if(USE_DEF_FEE_PUN)
	public abstract class Pun_Player_MonoBehaviour :  UnityEngine.MonoBehaviour ,  Photon.Pun.IPunObservable
	#else
	public abstract class Pun_Player_MonoBehaviour :  UnityEngine.MonoBehaviour
	#endif
	{
		/** [Fee.Network.Pun_Player_MonoBehaviour]OnPhotonSerializeView
		*/
		#if(USE_DEF_FEE_PUN)
		public abstract void OnPhotonSerializeView(Photon.Pun.PhotonStream a_stream,Photon.Pun.PhotonMessageInfo a_info);
		#endif
	}
}

