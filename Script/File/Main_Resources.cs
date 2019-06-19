

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。リソース。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Main_Resources
	*/
	public class Main_Resources : OnCoroutine_CallBack
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ロードリソース。アセットファイル。
			*/
			LoadResourcesAssetFile,

			/** ロードリソース。テキストファイル。
			*/
			LoadResourcesTextFile,

			/** ロードリソース。テクスチャーファイル。
			*/
			LoadResourcesTextureFile,
		};

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

			/** テキスト。
			*/
			Text,

			/** テクスチャー。
			*/
			Texture,
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

		/** request_type
		*/
		private RequestType request_type;

		/** request_relative_path
		*/
		private Fee.File.Path request_relative_path;

		/** result_progress_up
		*/
		private float result_progress_up;

		/** result_progress_down
		*/
		private float result_progress_down;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_asset
		*/
		private UnityEngine.Object result_asset;

		/** result_text
		*/
		private string result_text;

		/** result_texture
		*/
		private UnityEngine.Texture2D result_texture;

		/** constructor
		*/
		public Main_Resources()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_type = RequestType.None;
			this.request_relative_path = null;

			//result
			this.result_progress_up = 0.0f;
			this.result_progress_down = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_asset = null;
			this.result_text = null;
			this.result_texture = null;
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

		/** GetResultProgressUp
		*/
		public float GetResultProgressUp()
		{
			return this.result_progress_up;
		}

		/** GetResultProgressDown
		*/
		public float GetResultProgressDown()
		{
			return this.result_progress_down;
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
		public UnityEngine.Object GetResultAsset()
		{
			return this.result_asset;
		}

		/** GetResultText
		*/
		public string GetResultText()
		{
			return this.result_text;
		}

		/** GetResultTexture
		*/
		public UnityEngine.Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** [Fee.File.OnCoroutine_CallBack]コルーチンからのコールバック。

			return == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress_up,float a_progress_down)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress_up = a_progress_up;
			this.result_progress_down = a_progress_down;
			return true;
		}

		/** リクエスト。ロードリソース。アセットファイル。
		*/
		public bool RequestLoadResourcesAssetFile(Fee.File.Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;
				this.result_text = null;
				this.result_texture = null;

				//request
				this.request_type = RequestType.LoadResourcesAssetFile;
				this.request_relative_path = a_relative_path;

				Function.Function.StartCoroutine(this.DoLoadResourcesAssetFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードリソース。アセットファイル。
		*/
		private System.Collections.IEnumerator DoLoadResourcesAssetFile()
		{
			Tool.Assert(this.request_type == RequestType.LoadResourcesAssetFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = this.request_relative_path;

			Coroutine_LoadResourcesAssetFile t_coroutine = new Coroutine_LoadResourcesAssetFile();
			yield return t_coroutine.CoroutineMain(this,t_path);

			if(t_coroutine.result.asset_file != null){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_asset = t_coroutine.result.asset_file;
				this.result_type = ResultType.Asset;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}

		/** リクエスト。ロードリソース。テキストファイル。
		*/
		public bool RequestLoadResourcesTextFile(Fee.File.Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;
				this.result_text = null;
				this.result_texture = null;

				//request
				this.request_type = RequestType.LoadResourcesTextFile;
				this.request_relative_path = a_relative_path;

				Function.Function.StartCoroutine(this.DoLoadResourcesTextFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードリソース。アセットファイル。
		*/
		private System.Collections.IEnumerator DoLoadResourcesTextFile()
		{
			Tool.Assert(this.request_type == RequestType.LoadResourcesTextFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = this.request_relative_path;

			Coroutine_LoadResourcesTextFile t_coroutine = new Coroutine_LoadResourcesTextFile();
			yield return t_coroutine.CoroutineMain(this,t_path);

			if(t_coroutine.result.text_file != null){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_text = t_coroutine.result.text_file;
				this.result_type = ResultType.Text;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}




		/** リクエスト。ロードリソース。テクスチャーファイル。
		*/
		public bool RequestLoadResourcesTextureFile(Fee.File.Path a_relative_path)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress_up = 0.0f;
				this.result_progress_down = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_asset = null;
				this.result_text = null;
				this.result_texture = null;

				//request
				this.request_type = RequestType.LoadResourcesTextureFile;
				this.request_relative_path = a_relative_path;

				Function.Function.StartCoroutine(this.DoLoadResourcesTextureFile());
				return true;
			}

			return false;
		}

		/** 実行。ロードリソース。テクスチャーファイル。
		*/
		private System.Collections.IEnumerator DoLoadResourcesTextureFile()
		{
			Tool.Assert(this.request_type == RequestType.LoadResourcesTextureFile);

			//request_relative_pathは相対パス。
			Fee.File.Path t_path = this.request_relative_path;

			Coroutine_LoadResourcesTextureFile t_coroutine = new Coroutine_LoadResourcesTextureFile();
			yield return t_coroutine.CoroutineMain(this,t_path);

			if(t_coroutine.result.texture_file != null){
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_texture = t_coroutine.result.texture_file;
				this.result_type = ResultType.Texture;
				yield break;
			}else{
				this.result_progress_up = 1.0f;
				this.result_progress_down = 1.0f;
				this.result_errorstring = t_coroutine.result.errorstring;
				this.result_type = ResultType.Error;
				yield break;
			}
		}


	}
}

