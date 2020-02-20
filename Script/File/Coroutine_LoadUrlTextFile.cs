

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
			public ResultType(string a_text_file,string a_errorstring,System.Collections.Generic.Dictionary<string,string> a_responseheader,long a_response_code)
			{
				//text_file
				this.text_file = a_text_file;

				//errorstring
				this.errorstring = a_errorstring;

				//responseheader
				this.responseheader = a_responseheader;

				//レスポンスコード。
				try{
					if(this.responseheader == null){
						this.responseheader = new System.Collections.Generic.Dictionary<string,string>();
					}
					if(this.responseheader.ContainsKey("ResponseCode") == true){
						Tool.Assert(false);
						this.responseheader["ResponseCode"] = a_response_code.ToString();
					}else{
						this.responseheader.Add("ResponseCode",a_response_code.ToString());
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}
		}

		/** result
		*/
		public ResultType result;

		/** CreateWebRequestInstance
		*/
		private static UnityEngine.Networking.UnityWebRequest CreateWebRequestInstance(Fee.File.Path a_path,System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler)
		{
			UnityEngine.Networking.UnityWebRequest t_webrequest = null;

			if(a_post_data != null){
				t_webrequest = UnityEngine.Networking.UnityWebRequest.Post(a_path.GetPath(),a_post_data);
			}else{
				t_webrequest = UnityEngine.Networking.UnityWebRequest.Get(a_path.GetPath());
			}

			if(a_certificate_handler != null){
				t_webrequest.certificateHandler = a_certificate_handler;
			}else{
				string t_certificate_string = Fee.File.File.GetInstance().GetCertificateString(a_path.GetPath());
				if(t_certificate_string != null){
					t_webrequest.certificateHandler = new Fee.File.CustomCertificateHandler(t_certificate_string);
				}
			}

			return t_webrequest;
		}

		/** CoroutineMain
		*/
		public System.Collections.IEnumerator CoroutineMain(Fee.File.OnFileCoroutine_CallBackInterface a_callback_interface,Fee.File.Path a_path,System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> a_post_data,Fee.File.CustomCertificateHandler a_certificate_handler)
		{
			//result
			this.result = null;

			//ロード。
			byte[] t_result_binary = null;
			System.Collections.Generic.Dictionary<string,string> t_result_responseheader = null;
			long t_result_responsecode = 0;
			{
				using(UnityEngine.Networking.UnityWebRequest t_webrequest = CreateWebRequestInstance(a_path,a_post_data,a_certificate_handler)){

					//通信。
					{
						UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;
						if(t_webrequest != null){
							t_webrequest_async = t_webrequest.SendWebRequest();
							if(t_webrequest_async == null){
								//エラー。
								this.result = new ResultType(null,"Unknown Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders(),t_webrequest.responseCode);
								yield break;
							}
						}else{
							//エラー。
							this.result = new ResultType(null,"Unknown Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders(),t_webrequest.responseCode);
							yield break;
						}

						do{
							//エラーチェック。

							if(t_webrequest.isNetworkError == true){
								//エラー。
								if(t_webrequest.error != null){
									this.result = new ResultType(null,"Connect Error : LoadUrlTextFile : " + a_path.GetPath() + " : " + t_webrequest.error,t_webrequest.GetResponseHeaders(),t_webrequest.responseCode);
								}else{
									this.result = new ResultType(null,"Connect Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders(),t_webrequest.responseCode);
								}
								yield break;
							}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)){
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
							this.result = new ResultType(null,"Convert Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders(),t_webrequest.responseCode);
							yield break;
						}

						byte[] t_result = t_webrequest.downloadHandler.data;
						if(t_result == null){
							//エラー。
							this.result = new ResultType(null,"Convert Error : LoadUrlTextFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders(),t_webrequest.responseCode);
							yield break;
						}

						t_result_binary = t_result;
					}catch(System.Exception t_exception){
						//エラー。
						this.result = new ResultType(null,"Convert Error : LoadUrlTextFile : " + a_path.GetPath() + " : " + t_exception.Message,t_webrequest.GetResponseHeaders(),t_webrequest.responseCode);
						yield break;
					}

					//レスポンスヘッダー。
					t_result_responseheader = t_webrequest.GetResponseHeaders();
					t_result_responsecode = t_webrequest.responseCode;
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
					this.result = new ResultType(null,"Convert Error : LoadUrlTextFile : " + a_path.GetPath(),t_result_responseheader,t_result_responsecode);
					yield break;
				}
			}

			//成功。
			this.result = new ResultType(t_result_text,null,t_result_responseheader,t_result_responsecode);
			yield break;
		}
	}
}

