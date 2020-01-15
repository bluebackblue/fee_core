

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
			public ResultType(string a_text_file,string a_errorstring,System.Collections.Generic.Dictionary<string,string> a_responseheader)
			{
				//text_file
				this.text_file = a_text_file;

				//errorstring
				this.errorstring = a_errorstring;
			}
		}

		/** result
		*/
		public ResultType result;

		/** CreateWebRequestInstance
		*/
		private static UnityEngine.Networking.UnityWebRequest CreateWebRequestInstance(Fee.File.Path a_path,UnityEngine.WWWForm a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler)
		{
			UnityEngine.Networking.UnityWebRequest t_webrequest = null;

			if(a_post_data != null){
				t_webrequest = UnityEngine.Networking.UnityWebRequest.Post(a_path.GetPath(),a_post_data);
			}else{
				t_webrequest = UnityEngine.Networking.UnityWebRequest.Get(a_path.GetPath());
			}

			if(a_certificate_handler != null){
				a_certificate_handler.InitializeCheck();
				t_webrequest.certificateHandler = a_certificate_handler;
			}else{
				t_webrequest.certificateHandler = new Fee.File.CustomCertificateHandler(Fee.File.File.GetInstance().GetPublicKey(a_path.GetPath()));
			}

			return t_webrequest;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path,UnityEngine.WWWForm a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler)
		{
			//result
			this.result = null;

			//ロード。
			byte[] t_result_binary = null;
			System.Collections.Generic.Dictionary<string,string> t_result_responseheader = null;
			{
				using(UnityEngine.Networking.UnityWebRequest t_webrequest = CreateWebRequestInstance(a_path,a_post_data,a_certificate_handler)){

					//通信。
					{
						UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;
						if(t_webrequest != null){
							t_webrequest_async = t_webrequest.SendWebRequest();
							if(t_webrequest_async == null){
								//エラー。
								this.result = new ResultType(null,"Unknown Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
								yield break;
							}
						}else{
							//エラー。
							this.result = new ResultType(null,"Unknown Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
							yield break;
						}

						do{
							//エラーチェック。

							if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
								//エラー。
								this.result = new ResultType(null,"Connect Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
								yield break;
							}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
								//正常終了。
								yield return t_webrequest_async;
								break;
							}

							//キャンセルチェック。
							{
								if(a_callback_interface != null){
									float t_progress = (t_webrequest.uploadProgress + t_webrequest.downloadProgress) / 2;
									if(a_callback_interface.OnFileCoroutine(t_progress) == false){
										t_webrequest.Abort();
									}
								}
							}

							yield return null;
						}while(true);
					}

					//コンバート。
					try{
						if(t_webrequest.downloadHandler == null){
							//エラー。
							this.result = new ResultType(null,"Convert Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
							yield break;
						}

						byte[] t_result = t_webrequest.downloadHandler.data;
						if(t_result == null){
							//エラー。
							this.result = new ResultType(null,"Convert Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
							yield break;
						}

						t_result_binary = t_result;
					}catch(System.Exception t_exception){
						//エラー。
						this.result = new ResultType(null,"Convert Error : LoadUrlTextFile : " + a_path.GetPath() + " : " + t_exception.Message,t_webrequest.GetResponseHeaders());
						yield break;
					}

					//レスポンスヘッダー。
					t_result_responseheader = t_webrequest.GetResponseHeaders();
				}
			}

			//コンバート。
			string t_result_text = null;
			{
				string t_result = System.Text.Encoding.UTF8.GetString(t_result_binary);
				if(t_result != null){
					//成功。
					t_result_text = t_result;
				}else{
					//エラー。
					this.result = new ResultType(null,"Convert Error : LoadUrlTextFile : " + a_path.GetPath(),t_result_responseheader);
					yield break;
				}
			}

			//成功。
			this.result = new ResultType(t_result_text,null,t_result_responseheader);
			yield break;
		}
	}
}

