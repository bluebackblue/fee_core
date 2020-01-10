

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。アンドロイドコンテンツ。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Main_AndroidContent
	*/
	public class Main_AndroidContent : Fee.File.OnFileCoroutine_CallBackInterface
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

		/** constructor
		*/
		public Main_AndroidContent()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_path = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_asset = null;
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

		/** リクエスト。ロードアンドロイドコンテンツ。バイナリファイル。
		*/
		public bool RequestLoadAndroidContentBinaryFile(Path a_full_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_path = a_full_path;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;

				Function.Function.StartCoroutine(this.DoLoadAndroidContentBinaryFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードアンドロイドコンテンツ。バイナリファイル。
		*/
		private System.Collections.IEnumerator DoLoadAndroidContentBinaryFile()
		{
			Coroutine_LoadAndroidContentBinaryFile t_coroutine = new Coroutine_LoadAndroidContentBinaryFile();
			yield return t_coroutine.CoroutineMain(this,this.request_path);

			if(t_coroutine.result.binary_file != null){
				this.result_progress = 1.0f;
				this.result_asset = new Asset.Asset(Asset.AssetType.Binary,t_coroutine.result.binary_file);
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードアンドロイドコンテンツ。テキストファイル。
		*/
		public bool RequestLoadAndroidContentTextFile(Path a_full_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_path = a_full_path;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;

				Function.Function.StartCoroutine(this.DoLoadAndroidContentTextFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードアンドロイドコンテンツ。テキストファイル。
		*/
		private System.Collections.IEnumerator DoLoadAndroidContentTextFile()
		{
			Coroutine_LoadAndroidContentTextFile t_coroutine = new Coroutine_LoadAndroidContentTextFile();
			yield return t_coroutine.CoroutineMain(this,this.request_path);

			if(t_coroutine.result.text_file != null){
				this.result_progress = 1.0f;
				this.result_asset = new Asset.Asset(Asset.AssetType.Text,t_coroutine.result.text_file);
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードアンドロイドコンテンツ。テクスチャファイル。
		*/
		public bool RequestLoadAndroidContentTextureFile(Path a_full_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//request
				this.request_path = a_full_path;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;

				Function.Function.StartCoroutine(this.DoLoadAndroidContentTextureFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードアンドロイドコンテンツ。テクスチャファイル。
		*/
		private System.Collections.IEnumerator DoLoadAndroidContentTextureFile()
		{
			Coroutine_LoadAndroidContentTextureFile t_coroutine = new Coroutine_LoadAndroidContentTextureFile();
			yield return t_coroutine.CoroutineMain(this,this.request_path);

			if(t_coroutine.result.texture_file != null){
				this.result_progress = 1.0f;
				this.result_asset =new Asset.Asset(Asset.AssetType.Texture,t_coroutine.result.texture_file);
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

