

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
	/** ダウンロード。テクスチャーファイル。
	*/
	public class Coroutine_DownLoadTextureFile
	{
		/** ResultType
		*/
		public class ResultType
		{
			/** テクスチャー。
			*/
			public UnityEngine.Texture2D texture;

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
				this.texture = null;
				this.errorstring = null;
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
			#if(false)
			{
				return UnityEngine.Networking.UnityWebRequestTexture.GetTexture(a_path.GetPath());
			}
			#else
			{
				if(a_post_data != null){
					return UnityEngine.Networking.UnityWebRequest.Post(a_path.GetPath(),a_post_data);
				}
				return UnityEngine.Networking.UnityWebRequest.Get(a_path.GetPath());
			}
			#endif
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
						this.result.errorstring = "Coroutine_DownLoadTextureFile : webrequest_async == null";
						yield break;
					}
				}else{
					this.result.errorstring = "Coroutine_DownLoadTextureFile : webrequest == null";
					yield break;
				}

				while(true){
					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.result.errorstring = "Coroutine_DownLoadTextureFile : " + t_webrequest.error;
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
				UnityEngine.Texture2D t_result_texture = null;

				try{
					//レスポンスヘッダー。
					this.result.responseheader = t_webrequest.GetResponseHeaders();

					#if(false)
					{
						t_result_texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(t_webrequest);
					}
					#else
					{
						t_result_texture = BinaryToTexture2D.Convert(t_webrequest.downloadHandler.data);
					}
					#endif

				}catch(System.Exception t_exception){
					this.result.errorstring = "Coroutine_DownLoadTextureFile : " + t_exception.Message;
					yield break;
				}

				//成功。
				if(t_result_texture != null){
					this.result.texture = t_result_texture;
					yield break;
				}

				//失敗。
				this.result.errorstring = "Coroutine_DownLoadTextureFile : null";
				yield break;
			}
		}
	}
}

