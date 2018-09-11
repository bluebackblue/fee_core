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
	/** OnJoinedRoomCallBack
	*/
	public class OnJoinedRoomCallBack : MonoBehaviour
	{
		/** OnJoinedRoom
		*/
		public void OnJoinedRoom()
		{
			Tool.Log("OnJoinedRoomCallBack","OnJoinedRoom");

			//プレイヤ作成。
			PhotonNetwork.Instantiate(Config.PREFAB_NAME_PLAYERPREFAB,Vector3.zero,Quaternion.identity,0);
		}
	}
}

