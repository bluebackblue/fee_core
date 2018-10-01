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


/** NSaveLoad
*/
namespace NSaveLoad
{
	/** [タスク]ロードローカル。ＰＮＧファイル。
	*/
	public class Task_LoadLocalPngFile
	{
		/** TaskMain
		*/
		private static async System.Threading.Tasks.Task<byte[]> TaskMain(MonoBehaviour_Io a_instance,string a_full_path,System.Threading.CancellationToken a_cancel)
		{
			byte[] t_ret = null;
			System.IO.FileStream t_filestream = null;

			//ファイルパス。
			System.IO.FileInfo t_fileinfo = new System.IO.FileInfo(a_full_path);

			try{
				//開く。
				t_filestream = t_fileinfo.OpenRead();

				//プログレス。
				NTaskW.TaskW.GetInstance().Post((a_state) => {a_instance.SetProgressFromTask(0.0f);},null);

				//読み込み。
				if(t_filestream != null){
					t_ret = new byte[t_filestream.Length];
					if(Config.USE_ASYNC == true){
						await t_filestream.ReadAsync(t_ret,0,t_ret.Length,a_cancel);
						await t_filestream.FlushAsync(a_cancel);
					}else{
						t_filestream.Read(t_ret,0,t_ret.Length);
					}
				}			
			}catch(System.Exception /*t_exception*/){
				t_ret = null;
			}

			//閉じる。
			if(t_filestream != null){
				t_filestream.Close();
			}

			if(a_cancel.IsCancellationRequested == true){
				t_ret = null;
				a_cancel.ThrowIfCancellationRequested();
			}

			return t_ret;
		}

		/** 実行。
		*/
		public static NTaskW.Task<byte[]> Run(MonoBehaviour_Io a_instance,string a_full_path,NTaskW.CancelToken a_cancel)
		{
			System.Threading.CancellationToken t_cancel_token = a_cancel.GetToken();

			return new NTaskW.Task<byte[]>(() => {
				return Task_LoadLocalPngFile.TaskMain(a_instance,a_full_path,t_cancel_token);
			});
		}
	}
}

