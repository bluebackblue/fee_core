

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。ストリーム。
*/


/** Fee.Network
*/
namespace Fee.Network
{
	/** Stream_Base
	*/
	public interface Stream_Base
	{
		/** SetSendData
		*/
		void SetSendData(System.Object a_object);

		/** GetRecvData
		*/
		System.Object GetRecvData();
	}
}

