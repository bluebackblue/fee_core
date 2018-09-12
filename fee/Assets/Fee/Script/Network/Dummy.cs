using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ネットワーク。ダミー。
*/


#if USE_PUN
#else

/** NNetwork
*/
namespace NNetwork
{
	/** Connect_Auto
	*/
	public class Connect_Auto
	{
		/** Main
		*/
		public bool Main(){return false;}
	}

	/** Photon.Pun
	*/
	namespace Photon.Pun
	{
		/** MonoBehaviourPun
		*/
		public class MonoBehaviourPun : UnityEngine.MonoBehaviour
		{
		}

		/** RpcTarget
		*/
		public enum RpcTarget
		{
			All,
		};

		/** PhotonView
		*/
		public class PhotonView
		{
			/** IsMine
			*/
			public bool IsMine;

			/** RPC
			*/
			public void RPC(params object[] a_list){}
		}

		/** PunRPC
		*/
		class PunRPC : System.Attribute
		{
		}
	}
}

#endif

