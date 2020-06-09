

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。
*/


/** Fee.Network
*/
#if(USE_DEF_FEE_PUN)
namespace Fee.Network
{
	/** Pun_Status_Lobby
	*/
	public class Pun_Status_Lobby
	{
		/** Mode
		*/
		public enum Mode
		{
			/** なし。
			*/
			None,

			/** リクエスト。
			*/
			Request,

			/** 処理中。
			*/
			Busy,

			/** 結果。
			*/
			Result,
		}

		public Mode mode;
		public bool result;
		public void Reset()
		{
			this.mode = Mode.None;
			this.result = false;
		}
		public void SetBusy()
		{
			this.mode = Mode.Busy;
		}
		public void SetConnect()
		{
			this.mode = Mode.Result;
			this.result = true;
		}
		public void SetDisconnect()
		{
			this.mode = Mode.Result;
			this.result = false;
		}
	}
}
#endif

