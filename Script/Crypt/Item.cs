

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 暗号。アイテム。
*/


/** Fee.Crypt
*/
namespace Fee.Crypt
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

			/** バイナリ。
			*/
			Binary,

			/** 検証成功。
			*/
			VerifySuccess,
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

		/** result_binary
		*/
		private byte[] result_binary;

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

			//result_binary
			this.result_binary = null;
		}

		/** 削除。
		*/
		private void Delete()
		{
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
			return this.result_errorstring;
		}

		/** 結果。バイナリ。設定。
		*/
		public void SetResultBinary(byte[] a_binary)
		{
			this.result_type = ResultType.Binary;

			this.result_binary = a_binary;
		}

		/** 結果。バイナリ。取得。
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}

		/** 結果。検証成功。設定。
		*/
		public void SetResultVerifySuccess()
		{
			this.result_type = ResultType.VerifySuccess;
		}
	}
}

