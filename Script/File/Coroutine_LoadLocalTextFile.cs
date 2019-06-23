

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
	/** ロードローカル。テキストファイル。
	*/
	public class Coroutine_LoadLocalTextFile : Fee.File.OnTask_CallBackInterface
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** テキストファイル。
			*/
			public string text_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				//text_file
				this.text_file = null;

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

			//taskprogress_
			this.taskprogress = 0.0f;

			//キャンセルトークン。
			Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

			//タスク起動。
			Fee.TaskW.Task<Task_LoadLocalTextFile.ResultType> t_task = Task_LoadLocalTextFile.Run(this,a_path,t_cancel_token);

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
			Task_LoadLocalTextFile.ResultType t_result = t_task.GetResult();

			//成功。
			if(t_task.IsSuccess() == true){
				if(t_result.text != null){
					this.result.text_file = t_result.text;
					yield break;
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_LoadLocalTextFile : null";
				yield break;
			}
		}
	}
}

