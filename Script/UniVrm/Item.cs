

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
	/** Item
	*/
	public class Item
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

			/** コンテキスト。
			*/
			Context,
		}

		/** result_type
		*/
		private ResultType result_type;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** cancel_flag
		*/
		private bool cancel_flag;

		/** result_context
		*/
		private VRM.VRMImporterContext result_context;

		/** constructor
		*/
		public Item()
		{
			//result_type
			this.result_type = ResultType.None;

			//result_progress
			this.result_progress = 0.0f;

			//result_errorstring
			this.result_errorstring = null;

			//cancel_flag
			this.cancel_flag = false;

			//result_context
			this.result_context = null;
		}

		/** 削除。
		*/
		public void Delete()
		{
			if(this.result_context != null){
				this.result_context.Dispose();
			}
		}

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if(this.result_type == ResultType.None){
				return true;
			}
			return false;
		}

		/** キャンセル。設定。
		*/
		public void Cancel()
		{
			this.cancel_flag = true;
		}

		/** キャンセル。取得。
		*/
		public bool IsCancel()
		{
			return this.cancel_flag;
		}

		/** 結果。タイプ。取得。
		*/
		public ResultType GetResultType()
		{
			return this.result_type;
		}

		/** プログレス。設定。
		*/
		public void SetResultProgress(float a_result_progress)
		{
			this.result_progress = a_result_progress;
		}

		/** プログレス。取得。
		*/
		public float GetResultProgress()
		{
			return this.result_progress;
		}

		/** 結果。エラー文字。設定。
		*/
		public void SetResultErrorString(string a_error_string)
		{
			this.result_type = ResultType.Error;

			this.result_errorstring = a_error_string;
		}

		/** 結果。エラー文字。取得。
		*/
		public string GetResultErrorString()
		{
			if(this.result_errorstring != null){
				return this.result_errorstring;
			}else{
				return "";
			}
		}

		/** 結果。コンテキスト。設定。
		*/
		public void SetResultContext(VRM.VRMImporterContext a_context)
		{
			this.result_type = ResultType.Context;
			this.result_context = a_context;
		}

		/** 結果。コンテキスト。取得。
		*/
		public VRM.VRMImporterContext GetResultContext()
		{
			return this.result_context;
		}

		/** CreateSimpleController
		*/
		public Controller CreateSimpleController()
		{
			return new Controller(this,Controller.ControllerType.SimpleAnimation);
		}

		/** CreateRuntimeAnimatorController
		*/
		public Controller CreateRuntimeAnimatorController()
		{
			return new Controller(this,Controller.ControllerType.RuntimeAnimatorController);
		}
	}
}

