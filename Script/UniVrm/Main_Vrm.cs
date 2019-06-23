

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＮＩＶＲＭ。
*/


/** Fee.UniVrm
*/
namespace Fee.UniVrm
{
	/** Main_Vrm
	*/
	public class Main_Vrm : Fee.UniVrm.OnCoroutine_CallBackInterface
	{
		/**  リクエストタイプ。
		*/
		private enum RequestType
		{
			/** None
			*/
			None = -1,

			/** ロードＶＲＭ。
			*/
			LoadVrm,
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

			/** コンテキスト。
			*/
			Context,
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

		/** request_binary
		*/
		private byte[] request_binary;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_type
		*/
		private ResultType result_type;

		/** result_context
		*/
		private VRM.VRMImporterContext result_context;

		/** constructor
		*/
		public Main_Vrm()
		{
			this.is_busy = false;
			this.is_cancel = false;
			this.is_shutdown = false;

			//request
			this.request_type = RequestType.None;
			this.request_binary = null;

			//result
			this.result_progress = 0.0f;
			this.result_errorstring = null;
			this.result_type = ResultType.None;
			this.result_context = null;
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

		/** GetResultContext
		*/
		public VRM.VRMImporterContext GetResultContext()
		{
			return this.result_context;
		}

		/** [Fee.UniVrm.OnCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		public bool OnCoroutine(float a_progress)
		{
			if((this.is_cancel == true)||(this.is_shutdown == true)){
				return false;
			}

			this.result_progress = a_progress;
			return true;
		}

		/** リクエスト。ロードＶＲＭ。
		*/
		public bool RequestLoadVrm(byte[] a_binary)
		{
			if(this.is_busy == false){
				this.is_busy = true;

				//is_cancel
				this.is_cancel = false;

				//result
				this.result_progress = 0.0f;
				this.result_errorstring = null;
				this.result_type = ResultType.None;
				this.result_context = null;

				//request
				this.request_type = RequestType.LoadVrm;
				this.request_binary = a_binary;

				Function.Function.StartCoroutine(this.DoLoadVrm());
				return true;
			}

			return false;
		}

		/** 実行。ロード。
		*/
		private System.Collections.IEnumerator DoLoadVrm()
		{
			Tool.Assert(this.request_type == RequestType.LoadVrm);

			Coroutine_LoadVrm t_coroutine = new Coroutine_LoadVrm();
			yield return t_coroutine.CoroutineMain(this,this.request_binary);

			if(t_coroutine.result.context != null){
				this.result_progress = 1.0f;
				this.result_context = t_coroutine.result.context;
				this.result_type = ResultType.Context;
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

