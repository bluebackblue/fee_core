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
	/** ダウンロード。テキストファイル。
	*/
	public class Coroutine_DownLoadTextFile
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

		/** CoroutineMain
		*/
		public IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,string a_url)
		{
			//result
			this.result = new ResultType();

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = UnityEngine.Networking.UnityWebRequest.Get(a_url)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;
				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.result.errorstring = "Coroutine_DownLoadTextFile : webrequest_async == null";
						yield break;
					}
				}else{
					this.result.errorstring = "Coroutine_DownLoadTextFile : webrequest == null";
					yield break;
				}

				while(true){
					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.result.errorstring = "Coroutine_DownLoadTextFile : " + t_webrequest.error;
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if(a_instance != null){
						if(a_instance.OnCoroutine(t_webrequest.downloadProgress) == false){
							t_webrequest.Abort();
						}
					}

					yield return null;
				}

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				//コンバート。
				string t_result = null;
				try{
					if(t_webrequest.downloadHandler != null){
						t_result = t_webrequest.downloadHandler.text;
					}else{
						this.result.errorstring = "Coroutine_DownLoadTextFile : downloadHandler == null";
						yield break;
					}
				}catch(System.Exception t_exception){
					this.result.errorstring = "Coroutine_DownLoadTextFile : " + t_exception.Message;
					yield break;
				}

				//成功。
				if(t_result != null){
					this.result.text = t_result;
					yield break;
				}

				//失敗。
				this.result.errorstring = "Coroutine_DownLoadTextFile : null";
				yield break;
			}
		}
	}
}

