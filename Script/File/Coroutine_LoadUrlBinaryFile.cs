

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
	/** ロードＵＲＬ。バイナリファイル。
	*/
	public class Coroutine_LoadUrlBinaryFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** バイナリファイル。
			*/
			public byte[] binary_file;

			/** エラー文字列。
			*/
			public string errorstring;

			/** レスポンスヘッダー。
			*/
			public System.Collections.Generic.Dictionary<string,string> responseheader;

			/** constructor
			*/
			public ResultType(byte[] a_binary_file,string a_errorstring,System.Collections.Generic.Dictionary<string,string> a_responseheader)
			{
				//binary_file
				this.binary_file = a_binary_file;

				//errorstring
				this.errorstring = a_errorstring;

				//responseheader
				this.responseheader = a_responseheader;
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
								this.result = new ResultType(null,"Unknown Error : LoadUrlBinaryFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
								yield break;
							}
						}else{
							//エラー。
							this.result = new ResultType(null,"Unknown Error : LoadUrlBinaryFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
							yield break;
						}

						do{
							//エラーチェック。

							if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
								//エラー。
								this.result = new ResultType(null,"Connect Error : LoadUrlBinaryFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
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
							this.result = new ResultType(null,"Convert Error : LoadUrlBinaryFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
							yield break;
						}

						byte[] t_result = t_webrequest.downloadHandler.data;
						if(t_result == null){
							//エラー。
							this.result = new ResultType(null,"Convert Error : LoadUrlBinaryFile : " + a_path.GetPath(),t_webrequest.GetResponseHeaders());
							yield break;
						}

						t_result_binary = t_result;
					}catch(System.Exception t_exception){
						//エラー。
						this.result = new ResultType(null,"Convert Error : LoadUrlBinaryFile : " + a_path.GetPath() + " : " + t_exception.Message,t_webrequest.GetResponseHeaders());
						yield break;
					}

					//レスポンスヘッダー。
					t_result_responseheader = t_webrequest.GetResponseHeaders();
				}
			}

			//成功。
			this.result = new ResultType(t_result_binary,null,t_result_responseheader);
			yield break;
		}
	}
}

