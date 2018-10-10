using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief セーブロード。タスク。
*/


//Async block lacks `await' operator and will run synchronously.
#pragma warning disable 1998


/** NSaveLoad
*/
namespace NSaveLoad
{
	/** [タスク]セーブローカル。ＰＮＧファイル。
	*/
	public class Task_SaveLocalPngFile
	{
		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<bool> TaskMain(MonoBehaviour_Io a_instance,string a_full_path,byte[] a_binary,System.Threading.CancellationToken a_cancel)
		{
			bool t_ret = true;
			System.IO.FileStream t_filestream = null;

			//ファイルパス。
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

			try{
				//開く。
				t_filestream = t_fileinfo.Create();

				//プログレス。
				NTaskW.TaskW.GetInstance().Post((a_state) => {a_instance.SetProgressFromTask(0.0f);},null);

				//書き込み。
				if(t_filestream != null){
					if(Config.USE_ASYNC == true){
						await t_filestream.WriteAsync(a_binary,0,a_binary.Length,a_cancel);
						await t_filestream.FlushAsync(a_cancel);
					}else{
						t_filestream.Write(a_binary,0,a_binary.Length);
						t_filestream.Flush();
					}
				}				
			}catch(System.Exception /*t_exception*/){
				t_ret = false;

				//エラー文字列。
				NTaskW.TaskW.GetInstance().Post((a_state) => {
					a_instance.SetErrorStringFromTask("System.Exception");
				},null);
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			if(a_cancel.IsCancellationRequested == true){
				t_ret = false;

				//エラー文字列。
				NTaskW.TaskW.GetInstance().Post((a_state) => {
					a_instance.SetErrorStringFromTask("Cancel");
				},null);

				a_cancel.ThrowIfCancellationRequested();
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static NTaskW.Task<bool> Run(MonoBehaviour_Io a_instance,string a_full_path,byte[] a_binary,NTaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new NTaskW.Task<bool>(() => {
				return Task_SaveLocalPngFile.TaskMain(a_instance,a_full_path,a_binary,t_cancel_token);
			});
		}
	}
}

