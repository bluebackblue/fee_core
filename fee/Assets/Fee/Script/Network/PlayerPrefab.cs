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
	public class PlayerPrefab : MonoBehaviour
	{
		/** 開始。
		*/
		void Start()
		{
			Tool.Log("PlayerPrefab","Start");

			//親。設定。
			Transform t_root = NNetwork.Network.GetInstance().GetRoot();
			this.GetComponent<Transform>().SetParent(t_root);

			int t_playerlist_index = NNetwork.Network.GetInstance().AddPlayerPrefab(this);

			this.name = "PlayerPrefab_" + t_playerlist_index.ToString();
		}

		/** 削除。
		*/
		private void OnDestroy()
		{
			Tool.Log("PlayerPrefab","OnDestroy");

			NNetwork.Network.GetInstance().RemovePlayerPrefab(this);
		}
	}
}

