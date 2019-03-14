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
	/** ロードローカル。サウンドプール。
	*/
	public class Coroutine_LoadLocalSoundPool
	{
		/** ResultType
		*/
		public class ResultType
		{
			public NAudio.Pack_SoundPool soundpool;
			public string errorstring;

			/** constructor
			*/
			public ResultType()
			{
				this.soundpool = null;
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
					NAudio.Pack_SoundPool t_soundpool = NJsonItem.JsonToObject<NAudio.Pack_SoundPool>.Convert(new NJsonItem.JsonItem(t_result.text));

					string t_errorstring;
					if(NAudio.Pack_SoundPool.CheckSoundPool(t_soundpool,out t_errorstring) == true){
						this.result.soundpool = t_soundpool;
						yield break;
					}else{
						this.result.errorstring = t_errorstring;
						yield break;
					}
				}
			}

			//失敗。
			if(t_result.errorstring != null){
				this.result.errorstring = t_result.errorstring;
				yield break;
			}else{
				this.result.errorstring = "Coroutine_LoadLocalSoundPool : null";
				yield break;
			}
		}
	}
}

