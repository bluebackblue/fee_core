

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
	public class Coroutine_LoadLocalTextFile : Fee.File.OnFileTask_CallBackInterface
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
			public ResultType(string a_text_file,string a_errorstring)
			{
				//text_file
				this.text_file = a_text_file;

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
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path)
		{
			//result
			this.result = null;

			//taskprogress_
			this.taskprogress = 0.0f;

			//ロード。
			byte[] t_result_binary = null;
			{
				//キャンセルトークン。
				Fee.TaskW.CancelToken t_cancel_token = new Fee.TaskW.CancelToken();

				//タスク起動。
				using(Fee.TaskW.Task<Task_LoadLocalBinaryFile.ResultType> t_task = Task_LoadLocalBinaryFile.Run(this,a_path,t_cancel_token)){

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
					Task_LoadLocalBinaryFile.ResultType t_result = t_task.GetResult();

					if(t_result.errorstring != null){
						//エラー。
						this.result = new ResultType(null,t_result.errorstring);
						yield break;
					}

					if(t_task.IsSuccess() == false){
						//エラー。
						this.result = new ResultType(null,"Task Error : LoadLocalTextFile : " + a_path.GetPath());
						yield break;
					}

					if(t_result.binary == null){
						//エラー。
						this.result = new ResultType(null,"Unknown Error : LoadLocalTextFile : " + a_path.GetPath());
						yield break;
					}

					//成功。
					t_result_binary = t_result.binary;
				}
			}

			//コンバート。
			string t_result_text = null;
			{
				string t_result = Fee.StringConvert.Utf8BinaryToString.Convert(t_result_binary,0,t_result_binary.Length);
				if(t_result != null){
					//成功。
					t_result_text = t_result;
				}else{
					//エラー。
					this.result = new ResultType(null,"Convert Error : LoadLocalTextFile : " + a_path.GetPath());
					yield break;
				}
			}

			//成功。
			this.result = new ResultType(t_result_text,null);
			yield break;
		}
	}
}

