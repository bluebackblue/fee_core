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
		private static async System.Threading.Tasks.Task<ResultType> TaskMain(string a_full_path,System.Threading.CancellationToken a_cancel)
		{
			ResultType t_ret;
			{
				t_ret.text = null;
				t_ret.errorstring = null;
			}

			System.IO.StreamReader t_filestream = null;

			try{
				//ファイルパス。
				System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

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
		public static NTaskW.Task<ResultType> Run(string a_full_path,NTaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new NTaskW.Task<ResultType>(() => {
				return Task_LoadLocalTextFile.TaskMain(a_full_path,t_cancel_token);
			});
		}
	}
}

