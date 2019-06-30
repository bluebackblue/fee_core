

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** ロードＵＲＬ。テキストファイル。
	*/
	public class Coroutine_LoadUrlTextFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** テキストファイル。
			*/
			public string text_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** レスポンスヘッダー。
			*/
			public System.Collections.Generic.Dictionary<string,string> responseheader;

			/** constructor
			*/
			public ResultType()
			{
				//text_file
				this.text_file = null;
				
				//errorstring
				this.errorstring = null;

				//responseheader
				this.responseheader = null;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CreateWebRequestInstance
		*/
		private static UnityEngine.Networking.UnityWebRequest CreateWebRequestInstance(Fee.File.Path a_path,UnityEngine.WWWForm a_post_data)
		{
			if(a_post_data != null){
				return UnityEngine.Networking.UnityWebRequest.Post(a_path.GetPath(),a_post_data);
			}
			return UnityEngine.Networking.UnityWebRequest.Get(a_path.GetPath());
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path,UnityEngine.WWWForm a_post_data)
		{
			//result
			this.result = new ResultType();

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = CreateWebRequestInstance(a_path,a_post_data)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;
				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.result.errorstring = "Coroutine_LoadUrlTextFile : webrequest_async == null";
						yield break;
					}
				}else{
					this.result.errorstring = "Coroutine_LoadUrlTextFile : webrequest == null";
					yield break;
				}

				do{
					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.result.errorstring = "Coroutine_LoadUrlTextFile : " + t_webrequest.error + " : " + a_path.GetPath();
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if(a_callback_interface != null){
						float t_progress = (t_webrequest.uploadProgress + t_webrequest.downloadProgress) / 2;
						if(a_callback_interface.OnFileCoroutine(t_progress) == false){
							t_webrequest.Abort();
						}
					}

					yield return null;
				}while(true);

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				//コンバート。
				string t_result = null;
				try{
					//レスポンスヘッダー。
					this.result.responseheader = t_webrequest.GetResponseHeaders();

					if(t_webrequest.downloadHandler != null){
						t_result = t_webrequest.downloadHandler.text;
					}else{
						this.result.errorstring = "Coroutine_LoadUrlTextFile : downloadHandler == null";
						yield break;
					}
				}catch(System.Exception t_exception){
					this.result.errorstring = "Coroutine_LoadUrlTextFile : " + t_exception.Message;
					yield break;
				}

				//成功。
				if(t_result != null){
					this.result.text_file = t_result;
					yield break;
				}

				//失敗。
				this.result.errorstring = "Coroutine_LoadUrlTextFile : null";
				yield break;
			}
		}
	}
}

