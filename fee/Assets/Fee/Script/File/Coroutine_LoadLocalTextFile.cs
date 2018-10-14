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
	/** ロードローカル。テキストファイル。
	*/
	public class Coroutine_LoadLocalTextFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			public string text;
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.text = null;
				this.errorstring = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** taskprogress
		*/
		public float taskprogress;

		/** CoroutineMain
		*/
		public IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,string a_full_path)
		{
			//result
			this.result = new ResultType();

			//taskprogress
			this.taskprogress = 0.0f;

			//キャンセルトークン。
			NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

			//タスク起動。
			NTaskW.Task<Task_LoadLocalTextFile.ResultType> t_task = Task_LoadLocalTextFile.Run(a_full_path,t_cancel_token);

			//終了待ち。
			do{
				//キャンセル。
				if(a_instance != null){
					if(a_instance.OnCoroutine(this.taskprogress) == false){
						t_cancel_token.Cancel();
					}
				}
				yield return null;
			}while(t_task.IsEnd() == false);

			//結果。
			Task_LoadLocalTextFile.ResultType t_result = t_task.GetResult();

			//成功。
			if(t_task.IsSuccess() == true){
				if(t_result.text != null){
					this.result.text = t_result.text;
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

