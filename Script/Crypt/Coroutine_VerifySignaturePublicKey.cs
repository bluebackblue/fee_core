

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
	/** 証明書検証。パブリックキー。
	*/
	public class Coroutine_VerifySignaturePublicKey : Fee.Crypt.OnTask_CallBackInterface
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** 検証。
			*/
			public bool verify;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//verify
				this.verify = false;

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

		/** [Fee.Crypt.OnTask_CallBackInterface]タスク実行中。
		*/
		public void OnTask(float a_progress)
		{
			this.taskprogress = a_progress;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.Crypt.OnCoroutine_CallBackInterface a_callback,byte[] a_binary,byte[] a_signature_binary,string a_key)
		{
			//result
			this.result = new ResultType();

			//taskprogress
			this.taskprogress = 0.0f;

			//キャンセルトークン。
			Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

			//タスク起動。
			Fee.TaskW.Task<Task_VerifySignaturePublicKey.ResultType> t_task = Task_VerifySignaturePublicKey.Run(this,a_binary,a_signature_binary,a_key,t_cancel_token);

			//終了待ち。
			do{
				//キャンセル。
				if(a_callback != null){
					if(a_callback.OnCoroutine(this.taskprogress) == false){
						t_cancel_token.Cancel();
					}
				}
				yield return null;
			}while(t_task.IsEnd() == false);

			//結果。
			Task_VerifySignaturePublicKey.ResultType t_result = t_task.GetResult();

			//成功。
			if(t_task.IsSuccess() == true){
				if(t_result.verify == true){
					this.result.verify = true;
					yield break;
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_VerifySignaturePublicKey : null";
				yield break;
			}
		}
	}
}

