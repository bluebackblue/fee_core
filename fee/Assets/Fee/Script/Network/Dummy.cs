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


#if true
#else

/** PunRPC
*/
public class PunRPC : System.Attribute
{
}

/** PhotonView
*/
public class PhotonView
{
	/** isMine
	*/
	public bool isMine;

	/** RPC
	*/
	public void RPC(params object[] a_list)
	{
	}
}

/** PhotonTargets
*/
public enum PhotonTargets
{
	All,
}

/** PhotonNetwork
*/
public class PhotonNetwork
{
	/** Instantiate
	*/
	public static void Instantiate(params object[] a_list)
	{
	}
}

/** Photon
*/
namespace Photon
{
	/** MonoBehaviour
	*/
	public class MonoBehaviour : UnityEngine.MonoBehaviour
	{
	}
}

#endif

