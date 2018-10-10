using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。ウェブリクエスト。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** MonoBehaviour_WebRequest
	*/
	public class MonoBehaviour_WebRequest : MonoBehaviour_Base
	{
		/** リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ダウンロード。アセットバンドル。
			*/
			DownLoadAssetBundle,

			/** ダウンロード。バイナリ。
			*/
			DownLoadBinary,

			/** ダウンロード。テキスト。
			*/
			DownLoadText,

			/** ダウンロード。テクスチャ。
			*/
			DownLoadTexture,
		};

		/** request_type
		*/
		[SerializeField]
		private RequestType request_type;

		/** request_url
		*/
		[SerializeField]
		private string request_url;

		/** request_data_version
		*/
		[SerializeField]
		private uint request_data_version;

		/** request_assetbundle_id
		*/
		[SerializeField]
		private long request_assetbundle_id;

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//request_type
			this.request_type = RequestType.None;

			//request_url
			this.request_url = null;

			//request_data_version
			this.request_data_version = 0;

			//request_assetbundle_id
			this.request_assetbundle_id = Config.INVALID_ASSSETBUNDLE_ID;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.DownLoadAssetBundle:
			case RequestType.DownLoadBinary:
			case RequestType.DownLoadText:
			case RequestType.DownLoadTexture:
				{
					Tool.Log("MonoBehaviour_WebRequest",this.request_type.ToString());
					this.SetModeDo();
				}yield break;
			}

			//不明なリクエスト。
			this.SetResultErrorString("request_type == " + this.request_type.ToString());
			this.SetModeDoError();

			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。実行。
		*/
		protected override IEnumerator OnDo()
		{
			switch(this.request_type){
			case RequestType.DownLoadAssetBundle:
				{
					yield return this.Raw_Do_DownLoadAssetBundle();

					if(this.GetResultType() == ResultType.AssetBundle){
						if(this.GetResultAssetBundle() != null){
							this.SetModeDoSuccess();
							yield break;
						}
					}
				}break;
			case RequestType.DownLoadBinary:
				{
					yield return this.Raw_Do_DownLoadBinary();

					if(this.GetResultType() == ResultType.Binary){
						if(this.GetResultBinary() != null){
							this.SetModeDoSuccess();
							yield break;
						}
					}
				}break;
			case RequestType.DownLoadText:
				{
					yield return this.Raw_Do_DownLoadText();

					if(this.GetResultType() == ResultType.Text){
						if(this.GetResultText() != null){
							this.SetModeDoSuccess();
							yield break;
						}
					}
				}break;
			case RequestType.DownLoadTexture:
				{
					yield return this.Raw_Do_DownLoadTexture();

					if(this.GetResultType() == ResultType.Texture){
						if(this.GetResultTexture() != null){
							this.SetModeDoSuccess();
							yield break;
						}
					}
				}break;
			}

			this.SetModeDoError();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override IEnumerator OnDoError()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override IEnumerator OnDoSuccess()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** リクエスト。
		*/
		public bool Request(string a_url,DataType a_datatype,uint a_data_version,long a_assetbundle_id)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				switch(a_datatype){
				case DataType.AssetBundle:
					{
						this.request_type = RequestType.DownLoadAssetBundle;
					}break;
				case DataType.Binary:
					{
						this.request_type = RequestType.DownLoadBinary;
					}break;
				case DataType.Text:
					{
						this.request_type = RequestType.DownLoadText;
					}break;
				case DataType.Texture:
					{
						this.request_type = RequestType.DownLoadTexture;
					}break;
				default:
					{
						this.request_type = RequestType.None;
					}break;
				}

				this.request_url = a_url;
				this.request_data_version = a_data_version;
				this.request_assetbundle_id = a_assetbundle_id;

				return true;
			}

			return false;
		}

		/** [内部からの呼び出し]ダウンロード。アセットバンドル。
		*/
		private IEnumerator Raw_Do_DownLoadAssetBundle()
		{
			Tool.Log("MonoBehaviour_WebRequest",this.request_data_version.ToString() + " : " + this.request_url);

			AssetBundle t_result = null;
			string t_errorstring = null;

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(this.request_url,this.request_data_version,0)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;

				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.SetResultErrorString("webrequest_async == null");
						yield break;
					}
				}else{
					this.SetResultErrorString("webrequest == null");
					yield break;
				}

				while(true){
					//プログレス。
					{
						float t_progress = t_webrequest.downloadProgress;
						if(t_progress >= 0.999f){
							t_progress = 0.999f;
						}else if(t_progress < 0.0f){
							t_progress = 0.0f;
						}
						this.SetResultProgress(t_progress);
					}

					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.SetResultErrorString(t_webrequest.error);
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_webrequest.Abort();
					}

					yield return null;
				}

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				try{
					t_result = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(t_webrequest);
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}

			if(t_result != null){
				//アセットバンドルリストに登録。
				NDownLoad.DownLoad.GetInstance().GetAssetBundleList().Regist(this.request_assetbundle_id,t_result);

				this.SetResultAssetBundle(t_result);
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}

		/** [内部からの呼び出し]ダウンロード。バイナリ。
		*/
		private IEnumerator Raw_Do_DownLoadBinary()
		{
			Tool.Log("MonoBehaviour_WebRequest",this.request_url);

			byte[] t_result = null;
			string t_errorstring = null;

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = UnityEngine.Networking.UnityWebRequest.Get(this.request_url)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;

				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.SetResultErrorString("webrequest_async == null");
						yield break;
					}
				}else{
					this.SetResultErrorString("webrequest == null");
					yield break;
				}

				while(true){
					//プログレス。
					{
						float t_progress = t_webrequest.downloadProgress;
						if(t_progress >= 0.999f){
							t_progress = 0.999f;
						}else if(t_progress < 0.0f){
							t_progress = 0.0f;
						}
						this.SetResultProgress(t_progress);
					}

					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.SetResultErrorString(t_webrequest.error);
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_webrequest.Abort();
					}

					yield return null;
				}

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				try{
					t_result = t_webrequest.downloadHandler.data;
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}

			if(t_result != null){
				this.SetResultBinary(t_result);
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}

		/** [内部からの呼び出し]ダウンロード。テキスト。
		*/
		private IEnumerator Raw_Do_DownLoadText()
		{
			Tool.Log("MonoBehaviour_WebRequest",this.request_url);

			string t_result = null;
			string t_errorstring = null;

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = UnityEngine.Networking.UnityWebRequest.Get(this.request_url)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;

				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.SetResultErrorString("webrequest_async == null");
						yield break;
					}
				}else{
					this.SetResultErrorString("webrequest == null");
					yield break;
				}

				while(true){
					//プログレス。
					{
						float t_progress = t_webrequest.downloadProgress;
						if(t_progress >= 0.999f){
							t_progress = 0.999f;
						}else if(t_progress < 0.0f){
							t_progress = 0.0f;
						}
						this.SetResultProgress(t_progress);
					}

					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.SetResultErrorString(t_webrequest.error);
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_webrequest.Abort();
					}

					yield return null;
				}

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				try{
					t_result = t_webrequest.downloadHandler.text;
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}

			if(t_result != null){
				this.SetResultText(t_result);
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}

		/** [内部からの呼び出し]ダウンロード。テクスチャ。
		*/
		private IEnumerator Raw_Do_DownLoadTexture()
		{
			Tool.Log("MonoBehaviour_WebRequest",this.request_url);

			Texture2D t_result = null;
			string t_errorstring = null;

			using(UnityEngine.Networking.UnityWebRequest t_webrequest = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(this.request_url)){
				UnityEngine.Networking.UnityWebRequestAsyncOperation t_webrequest_async = null;

				if(t_webrequest != null){
					t_webrequest_async = t_webrequest.SendWebRequest();
					if(t_webrequest_async == null){
						this.SetResultErrorString("webrequest_async == null");
						yield break;
					}
				}else{
					this.SetResultErrorString("webrequest == null");
					yield break;
				}

				while(true){
					//プログレス。
					{
						float t_progress = t_webrequest.downloadProgress;
						if(t_progress >= 0.999f){
							t_progress = 0.999f;
						}else if(t_progress < 0.0f){
							t_progress = 0.0f;
						}
						this.SetResultProgress(t_progress);
					}

					//エラーチェック。
					if((t_webrequest.isNetworkError == true)||(t_webrequest.isHttpError == true)){
						//エラー終了。
						this.SetResultErrorString(t_webrequest.error);
						yield break;
					}else if((t_webrequest.isDone == true)&&(t_webrequest.isNetworkError == false)&&(t_webrequest.isHttpError == false)){
						//正常終了。
						break;
					}

					//キャンセル。
					if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
						t_webrequest.Abort();
					}

					yield return null;
				}

				if(t_webrequest_async != null){
					yield return t_webrequest_async;
				}

				try{
					t_result = UnityEngine.Networking.DownloadHandlerTexture.GetContent(t_webrequest);
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}

			if(t_result != null){
				this.SetResultTexture(t_result);
				yield break;
			}else{
				if(t_errorstring != null){
					this.SetResultErrorString(t_errorstring);
					yield break;
				}else{
					this.SetResultErrorString("null");
					yield break;
				}
			}
		}
	}
}

