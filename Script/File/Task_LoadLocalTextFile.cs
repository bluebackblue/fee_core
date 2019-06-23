

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。タスク。
*/


//Async block lacks await operator and will run synchronously.
#pragma warning disable 1998


/** Fee.File
*/
namespace Fee.File
{
	/** ロードローカル。テキストファイル。
	*/
	public class Task_LoadLocalTextFile
	{
		/** ResultType
		*/
		public struct ResultType
		{
			public string text;
			public string errorstring;
		}

		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<ResultType> TaskMain(Fee.File.OnTask_CallBackInterface a_callback,Path a_path,System.Threading.CancellationToken a_cancel)
		{
			ResultType t_ret;
			{
				t_ret.text = null;
				t_ret.errorstring = null;
			}

			System.IO.StreamReader t_filestream = null;

			try{
				//ファイルパス。
				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_path.GetPath());

				//開く。
				t_filestream = t_fileinfo.OpenText();

				//読み込み。
				if(t_filestream != null){
					if(Config.USE_ASYNC == true){
						t_ret.text = await t_filestream.ReadToEndAsync();
					}else{
						t_ret.text = t_filestream.ReadToEnd();
					}
				}	
			}catch(System.Exception t_exception){
				t_ret.text = null;
				t_ret.errorstring = "Task_LoadLocalTextFile : " + t_exception.Message;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			if(a_cancel.IsCancellationRequested == true){
				t_ret.text = null;
				t_ret.errorstring = "Task_LoadLocalTextFile : Cancel";

				a_cancel.ThrowIfCancellationRequested();
			}

			if(t_ret.text == null){
				if(t_ret.errorstring == null){
					t_ret.errorstring = "Task_LoadLocalTextFile : null";
				}
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static Fee.TaskW.Task<ResultType> Run(Fee.File.OnTask_CallBackInterface a_callback,Path a_path,Fee.TaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new Fee.TaskW.Task<ResultType>(() => {
				return Task_LoadLocalTextFile.TaskMain(a_callback,a_path,t_cancel_token);
			});
		}
	}
}

