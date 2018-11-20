using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。タスク。
*/


//Async block lacks `await' operator and will run synchronously.
#pragma warning disable 1998


/** NFile
*/
namespace NFile
{
	/** セーブローカル。テキストファイル。
	*/
	public class Task_SaveLocalTextFile
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
		private static async System.Threading.Tasks.Task<ResultType> TaskMain(string a_full_path,string a_text,System.Threading.CancellationToken a_cancel)
		{
			ResultType t_ret;
			{
				t_ret.saveend = false;
				t_ret.errorstring = null;
			}

			System.IO.StreamWriter t_filestream = null;

			try{
				//ファイルパス。
				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

				//開く。
				t_filestream = t_fileinfo.CreateText();

				//書き込み。
				if(t_filestream != null){
					if(a_text != null){
						if(Config.USE_ASYNC == true){
							await t_filestream.WriteAsync(a_text);
							await t_filestream.FlushAsync();
						}else{
							t_filestream.Write(a_text);
							t_filestream.Flush();
						}
						t_ret.saveend = true;
					}else{
						t_ret.saveend = false;
						t_ret.errorstring = "Task_SaveLocalTextFile : text == null";
					}
				}			
			}catch(System.Exception t_exception){
				t_ret.saveend = false;
				t_ret.errorstring = "Task_SaveLocalTextFile : " + t_exception.Message;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			if(a_cancel.IsCancellationRequested == true){
				t_ret.saveend = false;
				t_ret.errorstring = "Task_SaveLocalTextFile : Cancel";

				a_cancel.ThrowIfCancellationRequested();
			}

			if(t_ret.saveend == false){
				if(t_ret.errorstring == null){
					t_ret.errorstring = "Task_SaveLocalTextFile : null";
				}
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static NTaskW.Task<ResultType> Run(string a_full_path,string a_text,NTaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new NTaskW.Task<ResultType>(() => {
				return Task_SaveLocalTextFile.TaskMain(a_full_path,a_text,t_cancel_token);
			});
		}
	}
}

