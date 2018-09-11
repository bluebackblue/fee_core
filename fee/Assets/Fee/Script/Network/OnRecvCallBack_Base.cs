using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ネットワーク。受信コールバック。
*/


/** NNetwork
*/
namespace NNetwork
{
	/** OnRecvCallBack_Base
	*/
	public interface OnRecvCallBack_Base
	{
		/** 受信。
		*/
		void OnRecvInt(int a_playerlist_index,int a_key,int a_value);

		/** 受信。
		*/
		void OnRecvString(int a_playerlist_index,int a_key,string a_value);
	}
}

