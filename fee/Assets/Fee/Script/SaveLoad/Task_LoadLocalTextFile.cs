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
	/** [タスク]ロードローカル。テキストファイル。
	*/
	public class Task_LoadLocalTextFile
	{
		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<string> TaskMain(MonoBehaviour_Io a_instance,string a_full_path,System.Threading.CancellationToken a_cancel)
		{
			string t_ret = null;
			System.IO.StreamReader t_filestream = null;

			//ファイルパス。
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

			try{
				//開く。
				t_filestream = t_fileinfo.OpenText();

				//プログレス。
				NTaskW.TaskW.GetInstance().Post((a_state) => {a_instance.SetProgressFromTask(0.0f);},null);

				//読み込み。
				if(t_filestream != null){
					if(Config.USE_ASYNC == true){
						//TODO:キャンセルトークン渡せない。
						t_ret = await t_filestream.ReadToEndAsync();
					}else{
						t_ret = t_filestream.ReadToEnd();
					}
				}		
			}catch(System.Exception /*t_exception*/){
				t_ret = null;

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
				t_ret = null;

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
		public static NTaskW.Task<string> Run(MonoBehaviour_Io a_instance,string a_full_path,NTaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new NTaskW.Task<string>(() => {
				return Task_LoadLocalTextFile.TaskMain(a_instance,a_full_path,t_cancel_token);
			});
		}
	}
}

