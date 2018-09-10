using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ネットワーク。
*/


/** NNetwork
*/
namespace NNetwork
{
	/** PlayerPrefab
	*/
	#if USE_PHOTON
	public class PlayerPrefab : PhotonView
	#else
	public class PlayerPrefab : MonoBehaviour
	#endif
	{
	}
}

