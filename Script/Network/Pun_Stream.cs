

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。ストリーム。
*/


/** Fee.Network
*/
#if(USE_DEF_FEE_PUN)
namespace Fee.Network
{
	/** Pun_Stream
	*/
	public class Pun_Stream : Stream_Base
	{
		/** raw_stream
		*/
		public Photon.Pun.PhotonStream raw_stream;

		/** constructor
		*/
		public Pun_Stream()
		{
			this.raw_stream = null;
		}

		/** SetStream
		*/
		public void SetStream(Photon.Pun.PhotonStream a_stream)
		{
			this.raw_stream = a_stream;
		}

		/** SetSendData
		*/
		public void SetSendData(System.Object a_object)
		{
			this.raw_stream.SendNext(a_object);
		}

		/** GetRecvData
		*/
		public System.Object GetRecvData()
		{
			return this.raw_stream.ReceiveNext();
		}
	}
}
#endif

