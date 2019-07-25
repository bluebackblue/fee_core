

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
	public class CancelToken
	{
		/** souce
		*/
		#if((UNITY_5)||(UNITY_WEBGL))
		private bool cancel_flag;
		#else
		private System.Threading.CancellationTokenSource source;
		#endif

		/** constructor
		*/
		public CancelToken()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			this.cancel_flag = false;
			#else
			this.source = new System.Threading.CancellationTokenSource();
			#endif
		}

		/** トークン取得。
		*/
		#if((UNITY_5)||(UNITY_WEBGL))
		#else
		public System.Threading.CancellationToken GetToken()
		{
			return this.source.Token;
		}
		#endif

		/** キャンセル。設定。
		*/
		public void Cancel()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			this.cancel_flag = true;
			#else
			this.source.Cancel();
			#endif
		}

		/** IsCancellationRequested
		*/
		public bool IsCancellationRequested()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			return this.cancel_flag;
			#else
			return this.source.IsCancellationRequested;
			#endif
		}

		/** ThrowIfCancellationRequested
		*/
		public void ThrowIfCancellationRequested()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			{
				Tool.Assert(false);
			}
			#else
			{
				this.source.Token.ThrowIfCancellationRequested();
			}
			#endif
		}
	}
}

