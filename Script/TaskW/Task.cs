

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief タスク。タスク。
*/


/** Fee.TaskW
*/
namespace Fee.TaskW
{
	/** Task
	*/
	public class Task
	{
		/**　Delay
		*/
		#if((UNITY_5)||(UNITY_WEBGL))
		#else
		public static System.Threading.Tasks.Task Delay(int a_milliseconds_delay)
		{
			return System.Threading.Tasks.Task.Delay(a_milliseconds_delay);
		}
		#endif

		/** Yield
		*/
		#if((UNITY_5)||(UNITY_WEBGL))
		#else
		public static System.Runtime.CompilerServices.YieldAwaitable Yield()
		{
			return System.Threading.Tasks.Task.Yield();
		}
		#endif
	}

	/** Task
	*/
	public class Task<TResult> : Task
	{
		/** task
		*/
		#if((UNITY_5)||(UNITY_WEBGL))
		private TResult result;
		#else
		private System.Threading.Tasks.Task<TResult> task;
		#endif

		/** constructor
		*/
		/*
		#if((UNITY_5)||(UNITY_WEBGL))
		#else
		public Task(System.Threading.Tasks.Task<TResult> a_task)
		{
			this.task = a_task;
		}
		#endif
		*/

		/** constructor
		*/
		#if((UNITY_5)||(UNITY_WEBGL))
		public Task(System.Func<TResult> a_function)
		{
			this.result = a_function();
		}
		#else
		public Task(System.Func<System.Threading.Tasks.Task<TResult>> a_function)
		{
			#if(UNITY_WEBGL)
			this.task = a_function();
			#else
			this.task = System.Threading.Tasks.Task.Run(a_function);
			#endif
		}
		#endif

		/** タスク。取得。
		*/
		#if((UNITY_5)||(UNITY_WEBGL))
		#else
		public System.Threading.Tasks.Task<TResult> GetTask()
		{
			return this.task;
		}
		#endif

		/** IsCompleted
		*/
		public bool IsCompleted()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			return true;
			#else
			return this.task.IsCompleted;
			#endif
		}

		/** IsCanceled
		*/
		public bool IsCanceled()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			return false;
			#else
			return this.task.IsCanceled;
			#endif
		}

		/** IsFaulted
		*/
		public bool IsFaulted()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			return false;
			#else
			return this.task.IsFaulted;
			#endif
		}

		/** IsEnd
		*/
		public bool IsEnd()
		{
			if((this.IsCompleted() == true)||(this.IsCanceled() == true)||(this.IsFaulted() == true)){
				return true;
			}
			return false;
		}

		/** IsSuccess
		*/
		public bool IsSuccess()
		{
			if((this.IsCompleted() == true)&&(this.IsCanceled() == false)&&(this.IsFaulted() == false)){
				return true;
			}
			return false;
		}

		/** GetResult
		*/
		public TResult GetResult()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			return this.result;
			#else
			return this.task.Result;
			#endif
		}

		/** Dispose
		*/
		public void Dispose()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			#else
			this.task.Dispose();
			this.task = null;
			#endif
		}
	}
}

