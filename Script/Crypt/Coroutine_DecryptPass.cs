

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 暗号。コルーチン。
*/


/** Fee.Crypt
*/
namespace Fee.Crypt
{
	/** 複合化。パス。
	*/
	public class Coroutine_DecryptPass : Fee.Crypt.OnCryptTask_CallBackInterface
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** バイナリ。
			*/
			public byte[] binary;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//binary
				this.binary = null;

				//errorstring
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** taskprogress
		*/
		public float taskprogress;

		/** [Fee.Crypt.OnCryptCoroutine_CallBackInterface]タスク実行中。
		*/
		public void OnCryptTask(float a_progress)
		{
			this.taskprogress = a_progress;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.Crypt.OnCryptCoroutine_CallBackInterface a_callback_interface,byte[] a_binary,string a_pass,string a_salt)
		{
			//result
			this.result = new ResultType();

			//taskprogress
			this.taskprogress = 0.0f;

			//キャンセルトークン。
			Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

			//result
			Task_DecryptPass.ResultType t_result;

			//タスク起動。
			using(Fee.TaskW.Task<Task_DecryptPass.ResultType> t_task = Task_DecryptPass.Run(this,a_binary,a_pass,a_salt,t_cancel_token)){

				//終了待ち。
				do{
					//キャンセル。
					if(a_callback_interface != null){
						if(a_callback_interface.OnCryptCoroutine(this.taskprogress) == false){
							t_cancel_token.Cancel();
						}
					}
					yield return null;
				}while(t_task.IsEnd() == false);

				//結果。
				t_result = t_task.GetResult();

				//成功。
				if(t_task.IsSuccess() == true){
					if(t_result.binary != null){
						this.result.binary = t_result.binary;
						yield break;
					}
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_DecryptPass : null";
				yield break;
			}
		}
	}
}

