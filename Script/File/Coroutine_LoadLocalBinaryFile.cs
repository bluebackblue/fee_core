

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** ロードローカル。バイナリファイル。
	*/
	public class Coroutine_LoadLocalBinaryFile : Fee.File.OnTask_CallBackInterface
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** バイナリーファイル。
			*/
			public byte[] binary_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//binary_file
				this.binary_file = null;

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

		/** [Fee.File.OnTask_CallBackInterface]タスク実行中。
		*/
		public void OnTask(float a_progress)
		{
			this.taskprogress = a_progress;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnCoroutine_CallBackInterface a_callback,Fee.File.Path a_path)
		{
			//result
			this.result = new ResultType();

			//taskprogress
			this.taskprogress = 0.0f;

			//キャンセルトークン。
			Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

			//タスク起動。
			Fee.TaskW.Task<Task_LoadLocalBinaryFile.ResultType> t_task = Task_LoadLocalBinaryFile.Run(this,a_path,t_cancel_token);

			//終了待ち。
			do{
				//キャンセル。
				if(a_callback != null){
					if(a_callback.OnCoroutine(1.0f,this.taskprogress) == false){
						t_cancel_token.Cancel();
					}
				}
				yield return null;
			}while(t_task.IsEnd() == false);

			//結果。
			Task_LoadLocalBinaryFile.ResultType t_result = t_task.GetResult();

			//成功。
			if(t_task.IsSuccess() == true){
				if(t_result.binary != null){
					this.result.binary_file = t_result.binary;
					yield break;
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_LoadLocalBinaryFile : null";
				yield break;
			}
		}
	}
}

