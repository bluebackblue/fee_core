

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
		public static void Sleep(int a_milliseconds)
		{
		}
		#else
		public static void Sleep(int a_milliseconds)
		{
			System.Threading.Thread.Sleep(a_milliseconds);
		}
		#endif

		/**　Delay
		*/
		#if((UNITY_5)||(UNITY_WEBGL))
		#else
		public static System.Threading.Tasks.Task Delay(int a_milliseconds)
		{
			return System.Threading.Tasks.Task.Delay(a_milliseconds);
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
	public class Task<TResult> : Task , System.IDisposable
	{
		/** mode
		*/
		private Mode mode;

		/** instancemode_function
		*/
		private System.Func<TResult> instancemode_function;

		/** task
		*/
		#if((UNITY_5)||(UNITY_WEBGL))
		private TResult result;
		#else
		private System.Threading.Tasks.Task<TResult> task;
		#endif

		#if((UNITY_5)||(UNITY_WEBGL))

		/** constructor

			実行する関数を取得する。

		*/
		public Task(System.Func<TResult> a_function)
		{
			//mode
			this.mode = Mode.OneShot;

			//result
			this.result = a_function();

			//instancemode_function
			this.instancemode_function = null;
		}

		#else

		/** constructor

			タスク生成する関数を取得する。

		*/
		public Task(System.Func<System.Threading.Tasks.Task<TResult>> a_function)
		{
			//OneShot
			this.mode = Mode.OneShot;

			//task
			this.task = System.Threading.Tasks.Task.Run(a_function);

			//instancemode_function
			this.instancemode_function = null;
		}

		/** constructor

			タスク実行する関数を取得する。

		*/
		public Task(System.Func<TResult> a_function)
		{
			//OneShot
			this.mode = Mode.OneShot;

			//task
			this.task = System.Threading.Tasks.Task.Run(a_function);

			//instancemode_function
			this.instancemode_function = null;
		}

		#endif

		/** constructor
		*/
		public Task(Mode a_mode)
		{
			//インスタンスモード。
			Tool.Assert(a_mode == Mode.InstanceMode_Function);

			//mode
			this.mode = Mode.InstanceMode_Function;

			//task
			#if((UNITY_5)||(UNITY_WEBGL))
			#else
			this.task = null;
			#endif

			//instancemode_function
			this.instancemode_function = null;
		}

		/** 関数。設定。
		*/
		public void SetFunction(System.Func<TResult> a_function)
		{
			//インスタンスモード。
			Tool.Assert(this.mode == Mode.InstanceMode_Function);

			this.instancemode_function = a_function;
		}

		/** 関数。開始。
		*/
		public void StartFunction()
		{
			//インスタンスモード。
			Tool.Assert(this.mode == Mode.InstanceMode_Function);

			#if((UNITY_5)||(UNITY_WEBGL))
			{
				//実行。
				this.result = this.instancemode_function();
			}
			#else
			{
				//前回のタスクが終了していない。
				Tool.Assert(this.task == null);

				//実行。
				this.task = System.Threading.Tasks.Task.Run(this.instancemode_function);
			}
			#endif
		}

		/** 関数。終了。
		*/
		public void EndFunction()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			#else
			this.Dispose();
			#endif
		}

		/** 関数。終了チェック。
		*/
		public bool IsEndFunction()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			{
				return true;
			}
			#else
			{
				if(this.task == null){
					return true;
				}
				return false;
			}
			#endif
		}

		/** タスク。取得。
		*/
		#if ((UNITY_5) || (UNITY_WEBGL))
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

		/** Wait
		*/
		public void Wait()
		{
			#if((UNITY_5)||(UNITY_WEBGL))
			#else
			{
				if(this.IsEnd() == false){
					try{
						this.task.Wait();
					}catch(System.OperationCanceledException t_exception_cancel){
						Tool.Log("Wait : Cancel",t_exception_cancel.Message);
					}catch(System.Exception t_exception){
						Tool.Log("Wait",t_exception.Message);
					}
				}
			}
			#endif
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
			if(this.task != null){
				if(this.IsEnd() == false){
					this.task.Wait();
				}
				this.task.Dispose();
				this.task = null;
			}
			#endif
		}
	}
}

