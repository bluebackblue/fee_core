

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** ダウンロード。バイナリファイル。
	*/
	public class Coroutine_DownLoadBinaryFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** バイナリーファイル。
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
			public ResultType()
			{
				//binary_file
				this.binary_file = null;

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
		public System.Collections.IEnumerator CoroutineMain(OnCoroutine_CallBack a_instance,Fee.File.Path a_path,UnityEngine.WWWForm a_post_data)
		{
			//result
			this.result = new ResultType();

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = CreateWebRequestInstance(a_path,a_post_data)){
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
						if(a_instance.OnCoroutine(t_webrequest.uploadProgress,t_webrequest.downloadProgress) == false){
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
					//レスポンスヘッダー。
					this.result.responseheader = t_webrequest.GetResponseHeaders();

					System.Collections.Generic.Dictionary<string,string> t_header = t_webrequest.GetResponseHeaders();
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
					this.result.binary_file = t_result;
					yield break;
				}

				//失敗。
				this.result.errorstring = "Coroutine_DownLoadBinaryFile : null";
				yield break;
			}
		}
	}
}

