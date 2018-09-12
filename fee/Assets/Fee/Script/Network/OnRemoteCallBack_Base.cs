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
	/** OnRemoteCallBack_Base
	*/
	public interface OnRemoteCallBack_Base
	{
		/** リモートコール。
		*/
		void OnRemoteCallInt(int a_playerlist_index,int a_key,int a_value);

		/** リモートコール。
		*/
		void OnRemoteCallString(int a_playerlist_index,int a_key,string a_value);
	}
}

