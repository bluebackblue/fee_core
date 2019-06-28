

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。ＷＷＷ。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Main_WebRequest
	*/
	public class Main_WebRequest : Fee.File.OnFileCoroutine_CallBackInterface
	{
		/** ResultType
		*/
		public enum ResultType
		{
			/** 未定義。
			*/
			None,

			/** エラー。
			*/
			Error,

			/** アセット。
			*/
			Asset,
		};

		/** is_busy
		*/
		private bool is_busy;

		/** キャンセル。チェック。
		*/
		private bool is_cancel;

		/** シャットダウン。チェック。
		*/
		private bool is_shutdown;

		/** request_post_data
		*/
		private UnityEngine.WWWForm request_post_data;

		/** request_path
		*/
		private Path request_path;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_asset
		*/
		private Fee.Asset.Asset result_asset;

		/** result_responseheader
		*/
		private System.Collections.Generic.Dictionary<string,string> result_responseheader;

		/** constructor
		*/
		public Main_WebRequest()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_post_data = null;
			this.request_path = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_asset = null;
			this.result_responseheader = null;
		}
		
		/** 削除。
		*/
		public void Delete()
		{
			this.is_shutdown = true;
		}

		/** キャンセル。
		*/
		public void Cancel()
		{
			this.is_cancel = true;
		}

		/** 完了。
		*/
		public void Fix()
		{
			this.is_busy = false;
		}

		/** GetResultProgress
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** GetResultErrorString
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** GetResultType
		*/
		public ResultType GetResultType()
		{
			return this.result_type;
		}

		/** GetResultAsset
		*/
		public Fee.Asset.Asset GetResultAsset()
		{
			return this.result_asset;
		}

		/** GetResultResponseHeader
		*/
		public System.Collections.Generic.Dictionary<string,string> GetResultResponseHeader()
		{
			return this.result_responseheader;
		}

		/** [Fee.File.OnFileCoroutine_CallBackInterface]コルーチンからのコールバック。

			return == false : キャンセル。

		*/
		public bool OnFileCoroutine(float a_progress)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress = a_progress;
			return true;
		}

		/** リクエスト。ロードＵＲＬ。バイナリファイル。
		*/
		public bool RequestLoadUrlBinaryFile(Path a_url_path,UnityEngine.WWWForm a_post_data)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_post_data = a_post_data;
				this.request_path = a_url_path;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;
				this.result_responseheader = null;

				Function.Function.StartCoroutine(this.DoLoadUrlBinaryFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードＵＲＬ。バイナリファイル。
		*/
		private System.Collections.IEnumerator DoLoadUrlBinaryFile()
		{
			Coroutine_LoadUrlBinaryFile t_coroutine = new Coroutine_LoadUrlBinaryFile();
			yield return t_coroutine.CoroutineMain(this,this.request_path,this.request_post_data);

			if(t_coroutine.result.binary_file != null){
				this.result_progress = 1.0f;
				this.result_asset = new Asset.Asset(Asset.AssetType.Binary,t_coroutine.result.binary_file);
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードＵＲＬ。テキストファイル。
		*/
		public bool RequestLoadUrlTextFile(Path a_url_path,UnityEngine.WWWForm a_post_data)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_post_data = a_post_data;
				this.request_path = a_url_path;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;
				this.result_responseheader = null;

				Function.Function.StartCoroutine(this.DoLoadUrlTextFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードＵＲＬ。テキストファイル。
		*/
		private System.Collections.IEnumerator DoLoadUrlTextFile()
		{
			Coroutine_LoadUrlTextFile t_coroutine = new Coroutine_LoadUrlTextFile();
			yield return t_coroutine.CoroutineMain(this,this.request_path,this.request_post_data);

			if(t_coroutine.result.text_file != null){
				this.result_progress = 1.0f;
				this.result_asset = new Asset.Asset(Asset.AssetType.Text,t_coroutine.result.text_file);
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードＵＲＬ。テクスチャファイル。
		*/
		public bool RequestLoadUrlTextureFile(Path a_url_path,UnityEngine.WWWForm a_post_data)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_post_data = a_post_data;
				this.request_path = a_url_path;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;
				this.result_responseheader = null;

				Function.Function.StartCoroutine(this.DoLoadUrlTextureFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードＵＲＬ。テクスチャファイル。
		*/
		private System.Collections.IEnumerator DoLoadUrlTextureFile()
		{
			Coroutine_LoadUrlTextureFile t_coroutine = new Coroutine_LoadUrlTextureFile();
			yield return t_coroutine.CoroutineMain(this,this.request_path,this.request_post_data);

			if(t_coroutine.result.texture_file != null){
				this.result_progress = 1.0f;
				this.result_asset =new Asset.Asset(Asset.AssetType.Texture,t_coroutine.result.texture_file);
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードストリーミングアセット。バイナリファイル。
		*/
		public bool RequestLoadStreamingAssetsBinaryFile(Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_post_data = null;
				this.request_path = a_relative_path;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;
				this.result_responseheader = null;

				Function.Function.StartCoroutine(this.DoLoadStreamingAssetsBinaryFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードストリーミングアセット。バイナリファイル。
		*/
		private System.Collections.IEnumerator DoLoadStreamingAssetsBinaryFile()
		{
			//request_pathは相対パス。
			Fee.File.Path t_path = Fee.File.Path.CreateStreamingAssetsPath(this.request_path);

			Coroutine_LoadUrlBinaryFile t_coroutine = new Coroutine_LoadUrlBinaryFile();
			yield return t_coroutine.CoroutineMain(this,t_path,null);

			if(t_coroutine.result.binary_file != null){
				this.result_progress = 1.0f;
				this.result_asset = new Asset.Asset(Asset.AssetType.Binary,t_coroutine.result.binary_file);
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードストリーミングアセット。テキストファイル。
		*/
		public bool RequestLoadStreamingAssetsTextFile(Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_post_data = null;
				this.request_path = a_relative_path;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;
				this.result_responseheader = null;

				Function.Function.StartCoroutine(this.DoLoadStreamingAssetsTextFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードストリーミングアセット。テキストファイル。
		*/
		private System.Collections.IEnumerator DoLoadStreamingAssetsTextFile()
		{
			//request_pathは相対パス。
			Fee.File.Path t_path = Fee.File.Path.CreateStreamingAssetsPath(this.request_path);

			Coroutine_LoadUrlTextFile t_coroutine = new Coroutine_LoadUrlTextFile();
			yield return t_coroutine.CoroutineMain(this,t_path,null);

			if(t_coroutine.result.text_file != null){
				this.result_progress = 1.0f;
				this.result_asset = new Asset.Asset(Asset.AssetType.Text,t_coroutine.result.text_file);
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードストリーミングアセット。テクスチャファイル。
		*/
		public bool RequestLoadStreamingAssetsTextureFile(Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_post_data = null;
				this.request_path = a_relative_path;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;
				this.result_responseheader = null;

				Function.Function.StartCoroutine(this.DoLoadStreamingAssetsTextureFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードストリーミングアセット。テクスチャファイル。
		*/
		private System.Collections.IEnumerator DoLoadStreamingAssetsTextureFile()
		{
			//request_pathは相対パス。
			Fee.File.Path t_path = Fee.File.Path.CreateStreamingAssetsPath(this.request_path);

			Coroutine_LoadUrlTextureFile t_coroutine = new Coroutine_LoadUrlTextureFile();
			yield return t_coroutine.CoroutineMain(this,t_path,null);

			if(t_coroutine.result.texture_file != null){
				this.result_progress = 1.0f;
				this.result_asset = new Asset.Asset(Asset.AssetType.Texture,t_coroutine.result.texture_file);
				this.result_responseheader = t_coroutine.result.responseheader;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}
	}
}

