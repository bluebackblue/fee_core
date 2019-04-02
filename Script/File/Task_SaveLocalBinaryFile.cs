

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。タスク。
*/


//Async block lacks `await' operator and will run synchronously.
#pragma warning disable 1998


/** Fee.File
*/
namespace Fee.File
{
	/** セーブローカル。バイナリファイル。
	*/
	public class Task_SaveLocalBinaryFile
	{
		/** ResultType
		*/
		public struct ResultType
		{
			public bool saveend;
			public string errorstring;
		}

		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<ResultType> TaskMain(OnTask_CallBack a_callback,Path a_path,byte[] a_binary,System.Threading.CancellationToken a_cancel)
		{
			ResultType t_ret;
			{
				t_ret.saveend = false;
				t_ret.errorstring = null;
			}

			System.IO.FileStream t_filestream = null;

			try{
				//ファイルパス。
				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_path.GetPath());

				//開く。
				t_filestream = t_fileinfo.Create();

				//書き込み。
				if(t_filestream != null){
					if(a_binary != null){
						if(Config.USE_ASYNC == true){
							await t_filestream.WriteAsync(a_binary,0,a_binary.Length,a_cancel);
							await t_filestream.FlushAsync(a_cancel);
						}else{
							t_filestream.Write(a_binary,0,a_binary.Length);
							t_filestream.Flush();
						}
						t_ret.saveend = true;
					}else{
						t_ret.saveend = false;
						t_ret.errorstring = "Task_SaveLocalBinaryFile : binary == null";
					}
				}			
			}catch(System.Exception t_exception){
				t_ret.saveend = false;
				t_ret.errorstring = "Task_SaveLocalBinaryFile : " + t_exception.Message;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			Platform.Platform.SyncFs();

			if(a_cancel.IsCancellationRequested == true){
				t_ret.saveend = false;
				t_ret.errorstring = "Task_SaveLocalBinaryFile : Cancel";

				a_cancel.ThrowIfCancellationRequested();
			}

			if(t_ret.saveend == false){
				if(t_ret.errorstring == null){
					t_ret.errorstring = "Task_SaveLocalBinaryFile : null";
				}
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static Fee.TaskW.Task<ResultType> Run(OnTask_CallBack a_callback,Path a_path,byte[] a_binary,Fee.TaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new Fee.TaskW.Task<ResultType>(() => {
				return Task_SaveLocalBinaryFile.TaskMain(a_callback,a_path,a_binary,t_cancel_token);
			});
		}
	}
}

