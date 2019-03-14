

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。ＷＷＷ。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** MonoBehaviour_WebRequest
	*/
	public class MonoBehaviour_WebRequest : MonoBehaviour_Base , OnCoroutine_CallBack
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ダウンロード。バイナリファイル。
			*/
			DownLoadBinaryFile,

			/** ダウンロード。テキストファイル。
			*/
			DownLoadTextFile,

			/** ダウンロード。テクスチャファイル。
			*/
			DownLoadTextureFile,

			/** ダウンロード。アセットバンドル。
			*/
			DownLoadAssetBundle,

			/** ロードストリーミングアセット。バイナリファイル。
			*/
			LoadStreamingAssetsBinaryFile,
		};

		/** request_type
		*/
		[UnityEngine.SerializeField]
		private RequestType request_type;

		/** request_progress_mode
		*/
		[UnityEngine.SerializeField]
		private ProgressMode request_progress_mode;

		/** request_url
		*/
		[UnityEngine.SerializeField]
		private string request_url;

		/** request_post_data
		*/
		[UnityEngine.SerializeField]
		private UnityEngine.WWWForm request_post_data;

		/** request_filename
		*/
		[UnityEngine.SerializeField]
		private string request_filename;

		/** request_assetbundle_id
		*/
		[UnityEngine.SerializeField]
		private long request_assetbundle_id;

		/** request_data_version
		*/
		[UnityEngine.SerializeField]
		private uint request_data_version;

		/** request_data_crc
		*/
		[UnityEngine.SerializeField]
		private uint request_data_crc;

		/** [Fee.File.OnCoroutine_CallBack]コルーチン実行中。

		戻り値 == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress)
		{
			if((this.IsCancel() == true)||(this.IsDeleteRequest() == true)){
				return false;
			}

			this.SetResultProgress(a_progress);
			return true;
		}

		/** [MonoBehaviour_Base]コールバック。初期化。
		*/
		protected override void OnInitialize()
		{
			//request_type
			this.request_type = RequestType.None;

			//request_progress_mode
			this.request_progress_mode = ProgressMode.None;

			//request_url
			this.request_url = null;

			//request_post_data
			this.request_post_data = null;

			//request_filename
			this.request_filename = null;

			//request_assetbundle_id
			this.request_assetbundle_id = 0;

			//request_data_version
			this.request_data_version = 0;

			//request_data_crc
			this.request_data_crc = 0;
		}

		/** [MonoBehaviour_Base]コールバック。開始。
		*/
		protected override System.Collections.IEnumerator OnStart()
		{
			switch(this.request_type){
			case RequestType.DownLoadBinaryFile:
			case RequestType.DownLoadTextFile:
			case RequestType.DownLoadTextureFile:
			case RequestType.DownLoadAssetBundle:
			case RequestType.LoadStreamingAssetsBinaryFile:
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
		protected override System.Collections.IEnumerator OnDo()
		{
			switch(this.request_type){
			case RequestType.DownLoadBinaryFile:
				{
					Coroutine_DownLoadBinaryFile t_coroutine = new Coroutine_DownLoadBinaryFile();
					yield return t_coroutine.CoroutineMain(this,this.request_url,this.request_post_data,this.request_progress_mode);

					if(t_coroutine.result.binary != null){
						this.SetResultBinary(t_coroutine.result.binary);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.DownLoadTextFile:
				{
					Coroutine_DownLoadTextFile t_coroutine = new Coroutine_DownLoadTextFile();
					yield return t_coroutine.CoroutineMain(this,this.request_url,this.request_post_data,this.request_progress_mode);

					if(t_coroutine.result.text != null){
						this.SetResultText(t_coroutine.result.text);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.DownLoadTextureFile:
				{
					Coroutine_DownLoadTextureFile t_coroutine = new Coroutine_DownLoadTextureFile();
					yield return t_coroutine.CoroutineMain(this,this.request_url);

					if(t_coroutine.result.texture != null){
						this.SetResultTexture(t_coroutine.result.texture);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.DownLoadAssetBundle:
				{
					Coroutine_DownLoadAssetBundle t_coroutine = new Coroutine_DownLoadAssetBundle();
					yield return t_coroutine.CoroutineMain(this,this.request_url,request_assetbundle_id,this.request_data_version,this.request_data_crc);

					if(t_coroutine.result.assetbundle != null){
						this.SetResultAssetBundle(t_coroutine.result.assetbundle);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			case RequestType.LoadStreamingAssetsBinaryFile:
				{
					Coroutine_DownLoadBinaryFile t_coroutine = new Coroutine_DownLoadBinaryFile();
					yield return t_coroutine.CoroutineMain(this,UnityEngine.Application.streamingAssetsPath + "/" + this.request_filename,null,ProgressMode.DownLoad);

					if(t_coroutine.result.binary != null){
						this.SetResultBinary(t_coroutine.result.binary);
						this.SetModeDoSuccess();
					}else{
						this.SetResultErrorString(t_coroutine.result.errorstring);
					}
				}break;
			}

			this.SetModeDoError();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。エラー終了。
		*/
		protected override System.Collections.IEnumerator OnDoError()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** [MonoBehaviour_Base]コールバック。正常終了。
		*/
		protected override System.Collections.IEnumerator OnDoSuccess()
		{
			this.SetResultProgress(1.0f);

			this.SetModeFix();
			yield break;
		}

		/** リクエスト。ダウンロード。バイナリファイル。
		*/
		public bool RequestDownLoadBinaryFile(string a_url,UnityEngine.WWWForm a_post_data,ProgressMode a_progress_mode)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.DownLoadBinaryFile;
				this.request_url = a_url;
				this.request_post_data = a_post_data;
				this.request_progress_mode = a_progress_mode;

				return true;
			}

			return false;
		}

		/** リクエスト。ダウンロード。テキストファイル。
		*/
		public bool RequestDownLoadTextFile(string a_url,UnityEngine.WWWForm a_post_data,ProgressMode a_progress_mode)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.DownLoadTextFile;
				this.request_url = a_url;
				this.request_post_data = a_post_data;
				this.request_progress_mode = a_progress_mode;

				return true;
			}

			return false;
		}

		/** リクエスト。ダウンロード。テクスチャーファイル。
		*/
		public bool RequestDownLoadTextureFile(string a_url)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.DownLoadTextureFile;
				this.request_url = a_url;

				return true;
			}

			return false;
		}

		/** リクエスト。ダウンロード。アセットバンドル。
		*/
		public bool RequestDownLoadAssetBundle(string a_url,long a_assetbundle_id,uint a_data_version)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.DownLoadAssetBundle;
				this.request_url = a_url;
				this.request_assetbundle_id = a_assetbundle_id;
				this.request_data_version = a_data_version;
				this.request_data_crc = 0;

				return true;
			}

			return false;
		}

		/** リクエスト。ロードストリーミングアセット。バイナリファイル。
		*/
		public bool LoadStreamingAssetsBinaryFile(string a_filename)
		{
			if(this.IsWaitRequest() == true){
				this.SetModeStart();
				this.ResetResultFlag();

				this.request_type = RequestType.LoadStreamingAssetsBinaryFile;
				this.request_filename = a_filename;

				return true;
			}

			return false;
		}
	}
}

