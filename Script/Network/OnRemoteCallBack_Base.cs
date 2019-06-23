

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。受信コールバック。
*/


/** Fee.Network
*/
namespace Fee.Network
{
	/** OnRemoteCallBack_Base

		TODO:OnRemote_CallBackInterface

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

