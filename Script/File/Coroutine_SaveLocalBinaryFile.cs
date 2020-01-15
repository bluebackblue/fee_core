

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
	/** セーブローカル。バイナリファイル。
	*/
	public class Coroutine_SaveLocalBinaryFile : Fee.File.OnFileTask_CallBackInterface
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** セーブ完了。
			*/
			public bool saveend;

			/** エラー文字列。
			*/
			public string errorstring;

			/** constructor
			*/
			public ResultType(bool a_saveend,string a_errorstring)
			{
				//saveend
				this.saveend = a_saveend;

				//errorstring
				this.errorstring = a_errorstring;
			}
		}

		/** result
		*/
		public ResultType result;

		/** taskprogress
		*/
		public float taskprogress;

		/** [Fee.File.OnFileTask_CallBackInterface]タスク実行中。
		*/
		public void OnFileTask(float a_progress)
		{
			this.taskprogress = a_progress;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path,byte[] a_binary)
		{
			//result
			this.result = null;

			//taskprogress_
			this.taskprogress = 0.0f;

			//チェック。
			if(a_binary == null){
				//エラー。
				this.result = new ResultType(false,"Param Error : SaveLocalBinaryFile : " + a_path.GetPath());
				yield break;
			}

			//セーブ。
			bool t_result_saveend = false;
			{
				//キャンセルトークン。
				Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

				//タスク起動。
				using(Fee.TaskW.Task<Task_SaveLocalBinaryFile.ResultType> t_task = Task_SaveLocalBinaryFile.Run(this,a_path,a_binary,t_cancel_token)){

					//終了待ち。
					do{
						//キャンセルチェック。
						{
							if(a_callback_interface != null){
								if(a_callback_interface.OnFileCoroutine(this.taskprogress) == false){
									t_cancel_token.Cancel();
								}
							}
						}

						yield return null;
					}while(t_task.IsEnd() == false);

					//結果。
					Task_SaveLocalBinaryFile.ResultType t_result = t_task.GetResult();

					if(t_result.errorstring != null){
						//エラー。
						this.result = new ResultType(false,t_result.errorstring);
						yield break;
					}

					if(t_task.IsSuccess() == false){
						//エラー。
						this.result = new ResultType(false,"Task Error : SaveLocalBinaryFile : " + a_path.GetPath());
						yield break;
					}

					if(t_result.saveend == false){
						//エラー。
						this.result = new ResultType(false,"Unknown Error : SaveLocalBinaryFile : " + a_path.GetPath());
						yield break;
					}

					//成功。
					t_result_saveend = t_result.saveend;
				}
			}

			this.result = new ResultType(t_result_saveend,null);
			yield break;
		}
	}
}

