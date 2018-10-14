using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** NFile
*/
namespace NFile
{
	/** セーブローカル。テキストファイル。
	*/
	public class Coroutine_SaveLocalTextFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			public bool saveend;
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.saveend = false;
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CoroutineMain
		*/
		public IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,string a_full_path,string a_text)
		{
			//result
			this.result = new ResultType();

			//キャンセルトークン。
			NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

			//タスク起動。
			NTaskW.Task<Task_SaveLocalTextFile.ResultType> t_task = Task_SaveLocalTextFile.Run(a_full_path,a_text,t_cancel_token);

			//終了待ち。
			do{
				//キャンセル。
				if(a_instance.OnCoroutine() == false){
					t_cancel_token.Cancel();
				}
				yield return null;
			}while(t_task.IsEnd() == false);

			//プログレス。
			/*
			a_instance.SetResultProgress(0.999f);
			*/

			//結果。
			Task_SaveLocalTextFile.ResultType t_result = t_task.GetResult();

			//成功。
			if(t_task.IsSuccess() == true){
				if(t_result.saveend == true){
					this.result.saveend = true;
					yield break;
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "null";
				yield break;
			}
		}
	}
}

