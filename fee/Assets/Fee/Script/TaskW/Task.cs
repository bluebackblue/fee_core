using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief タスク。タスク。
*/


/** NTaskW
*/
namespace NTaskW
{
	/** Task
	*/
	public class Task
	{
		/**　Delay
		*/
		#if(UNITY_WEBGL)
		#else
		public static System.Threading.Tasks.Task Delay(int a_milliseconds_delay)
		{
			return System.Threading.Tasks.Task.Delay(a_milliseconds_delay);
		}
		#endif

		/** Yield
		*/
		public static System.Runtime.CompilerServices.YieldAwaitable Yield()
		{
			return System.Threading.Tasks.Task.Yield();
		}
	}

	/** Task
	*/
	public class Task<TResult> : Task
	{
		/** task
		*/
		private System.Threading.Tasks.Task<TResult> task;

		/** constructor
		*/
		public Task(System.Threading.Tasks.Task<TResult> a_task)
		{
			this.task = a_task;
		}

		/** constructor
		*/
		public Task(System.Func<System.Threading.Tasks.Task<TResult>> a_function)
		{
			#if(UNITY_WEBGL)
			this.task = a_function();
			#else
			this.task = System.Threading.Tasks.Task.Run(a_function);
			#endif
		}

		/** タスク。終了。
		*/
		public System.Threading.Tasks.Task<TResult> GetTask()
		{
			return this.task;
		}

		/** IsCompleted
		*/
		public bool IsCompleted()
		{
			return this.task.IsCompleted;
		}

		/** IsCanceled
		*/
		public bool IsCanceled()
		{
			return this.task.IsCanceled;
		}

		/** IsFaulted
		*/
		public bool IsFaulted()
		{
			return  this.task.IsFaulted;
		}

		/** IsEnd
		*/
		public bool IsEnd()
		{
			if(this.IsCompleted()||this.IsCanceled()||this.IsFaulted()){
				return true;
			}
			return false;
		}

		/** GetResult
		*/
		public TResult GetResult()
		{
			return this.task.Result;
		}

		/** Dispose
		*/
		public void Dispose()
		{
			this.task.Dispose();
			this.task = null;
		}
	}
}

