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
	/** ダウンロード。バイナリファイル。
	*/
	public class Coroutine_DownLoadBinaryFile
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

		/** CreateWebRequestInstance
		*/
		private static UnityEngine.Networking.UnityWebRequest CreateWebRequestInstance(string a_url,WWWForm a_post_data)
		{
			if(a_post_data != null){
				return UnityEngine.Networking.UnityWebRequest.Post(a_url,a_post_data);
			}
			return UnityEngine.Networking.UnityWebRequest.Get(a_url);
		}

		/** CoroutineMain
		*/
		public IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,string a_url,WWWForm a_post_data,ProgressMode a_progress_mode)
		{
			//result
			this.result = new ResultType();

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = Coroutine_DownLoadBinaryFile.CreateWebRequestInstance(a_url,a_post_data)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;
				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.result.errorstring = "Coroutine_DownLoadBinaryFile : webrequest_async == null";
						yield break;
					}
				}else{
					this.result.errorstring = "Coroutine_DownLoadBinaryFile : webrequest == null";
					yield break;
				}

				while(true){
					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.result.errorstring = "Coroutine_DownLoadBinaryFile : webrequest : " + t_webrequest.error;
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if(a_instance != null){
						float t_progress = 0.0f;

						if(a_progress_mode == ProgressMode.DownLoad){
							t_progress = t_webrequest.downloadProgress;
						}else{
							t_progress = t_webrequest.uploadProgress;
						}

						if(a_instance.OnCoroutine(t_progress) == false){
							t_webrequest.Abort();
						}
					}

					yield return null;
				}

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				//コンバート。
				byte[] t_result = null;
				try{
					if(t_webrequest.downloadHandler != null){
						t_result = t_webrequest.downloadHandler.data;
					}else{
						this.result.errorstring = "Coroutine_DownLoadBinaryFile : downloadHandler == null";
						yield break;
					}
				}catch(System.Exception t_exception){
					this.result.errorstring = "Coroutine_DownLoadBinaryFile : " + t_exception.Message;
					yield break;
				}

				//成功。
				if(t_result != null){
					this.result.binary = t_result;
					yield break;
				}

				//失敗。
				this.result.errorstring = "Coroutine_DownLoadBinaryFile : null";
				yield break;
			}
		}
	}
}

