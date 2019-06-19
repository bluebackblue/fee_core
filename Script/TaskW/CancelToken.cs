

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief タスク。キャンセル。
*/


/** Fee.TaskW
*/
namespace Fee.TaskW
{
	/** CancelToken
	*/
	public class CancelToken
	{
		/** souce
		*/
		private System.Threading.CancellationTokenSource source;

		/** constructor
		*/
		public CancelToken()
		{
			this.source = new System.Threading.CancellationTokenSource();
		}

		/** トークン取得。
		*/
		public System.Threading.CancellationToken GetToken()
		{
			return this.source.Token;
		}

		/** キャンセル。設定。
		*/
		public void Cancel()
		{
			this.source.Cancel();
		}
	}
}

