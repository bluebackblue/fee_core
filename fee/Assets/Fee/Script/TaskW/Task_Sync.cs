using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief タスク。同期。
*/


/** NTaskW
*/
namespace NTaskW
{
	/** Task_Sync
	*/
	#if(UNITY_WEBGL)
	#else
	public class Task_Sync
	{
		/** context
		*/
		private System.Threading.SynchronizationContext context;

		/** constructor
		*/
		public Task_Sync()
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
	#endif
}

