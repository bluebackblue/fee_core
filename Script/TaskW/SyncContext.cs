

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief タスク。同期コンテキスト。
*/


/** Fee.TaskW
*/
namespace Fee.TaskW
{
	/** SyncContext
	*/
	public class SyncContext
	{
		/** context
		*/
		private System.Threading.SynchronizationContext context;

		/** constructor
		*/
		public SyncContext()
		{
			this.context = System.Threading.SynchronizationContext.Current;
		}

		/** 同期。
		*/
		public void SetSynchronizationContext()
		{
			System.Threading.SynchronizationContext.SetSynchronizationContext(this.context);
		}

		/** Post
		*/
		public void Post(System.Threading.SendOrPostCallback a_d,object a_state)
		{
			this.context.Post(a_d,a_state);
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.context = null;
		}
	}
}

