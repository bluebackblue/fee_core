﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 暗号。コルーチン。
*/


/** NCrypt
*/
namespace NCrypt
{
	/** 証明作成。プライベートキー。
	*/
	public class Coroutine_CreateSignaturePrivateKey
	{
		/** ResultType
		*/
		public class ResultType
		{
			public byte[] binary;
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.binary = null;
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
		public IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,byte[] a_binary,string a_key)
		{
			//result
			this.result = new ResultType();

			//taskprogress
			this.taskprogress = 0.0f;

			//キャンセルトークン。
			NTaskW.CancelToken t_cancel_token = new NTaskW.CancelToken();

			//タスク起動。
			NTaskW.Task<Task_CreateSignaturePrivateKey.ResultType> t_task = Task_CreateSignaturePrivateKey.Run(a_binary,a_key,t_cancel_token);

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
			Task_CreateSignaturePrivateKey.ResultType t_result = t_task.GetResult();

			//成功。
			if(t_task.IsSuccess() == true){
				if(t_result.binary != null){
					this.result.binary = t_result.binary;
					yield break;
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_CreateSignaturePrivateKey : null";
				yield break;
			}
		}
	}
}
