

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドル。ファイル。
*/


/** Fee.AssetBundle
*/
namespace Fee.AssetBundle
{
	/** Main_File
	*/
	public class Main_File : Fee.AssetBundle.OnAssetBundleCoroutine_CallBackInterface
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			None = -1,

			/** ファイル
			*/
			File,
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

			/** アセットバンドル。
			*/
			AssetBundle,
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

		/** request_id
		*/
		private string request_id;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_assetbundle
		*/
		private UnityEngine.AssetBundle result_assetbundle;

		/** constructor
		*/
		public Main_File()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_type = RequestType.None;
			this.request_id = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_assetbundle = null;
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

		/** GetResultAssetBundle
		*/
		public UnityEngine.AssetBundle GetResultAssetBundle()
		{
			return this.result_assetbundle;
		}

		/** [Fee.AssetBundle.OnAssetBundleCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		public bool OnAssetBundleCoroutine(float a_progress)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress = a_progress;
			return true;
		}

		/** リクエスト。ファイル。
		*/
		public bool RequestFile(string a_id)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_assetbundle = null;

				//request
				this.request_type = RequestType.File;
				this.request_id = a_id;

				Function.Function.StartCoroutine(this.DoFile());
				return true;
			}

			return false;
		}

		/** 実行。ファイル。
		*/
		private System.Collections.IEnumerator DoFile()
		{
			Tool.Assert(this.request_type == RequestType.File);

			Coroutine_File t_coroutine = new Coroutine_File();
			yield return t_coroutine.CoroutineMain(this,this.request_id);

			if(t_coroutine.result.assetbundle_file != null){
				this.result_progress = 1.0f;
				this.result_assetbundle = t_coroutine.result.assetbundle_file;
				this.result_type = ResultType.AssetBundle;
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

