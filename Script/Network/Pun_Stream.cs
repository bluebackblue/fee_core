

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
		/** SendNext
		*/
		void SendNext(System.Object a_object);

		/** ReceiveNext
		*/
		System.Object ReceiveNext();
	}

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

		/** SendNext
		*/
		public void SendNext(System.Object a_object)
		{
			this.raw_stream.SendNext(a_object);
		}

		/** ReceiveNext
		*/
		public System.Object ReceiveNext()
		{
			return this.raw_stream.ReceiveNext();
		}
	}
}

