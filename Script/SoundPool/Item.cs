

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief サウンドプール。アイテム。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
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

			/** サウンドプール。
			*/
			SoundPool,
		}

		/** result_type
		*/
		private ResultType result_type;

		/** result_progress_up
		*/
		private float result_progress_up;

		/** result_progress_down
		*/
		private float result_progress_down;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** cancel_flag
		*/
		private bool cancel_flag;

		/** result_soundpool
		*/
		private Fee.Audio.Pack_SoundPool result_soundpool;

		/** result_responseheader
		*/
		private System.Collections.Generic.Dictionary<string,string> result_responseheader;

		/** constructor
		*/
		public Item()
		{
			//result_type
			this.result_type = ResultType.None;

			//result_progress_up
			this.result_progress_up = 0.0f;

			//result_progress_down
			this.result_progress_down = 0.0f;

			//result_errorstring
			this.result_errorstring = null;

			//cancel_flag
			this.cancel_flag = false;

			//result_responseheader
			this.result_responseheader = null;
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
		public void SetResultProgressUp(float a_result_progress_up)
		{
			this.result_progress_up = a_result_progress_up;
		}

		/** プログレス。取得。
		*/
		public float GetResultProgressUp()
		{
			return this.result_progress_up;
		}

		/** プログレス。設定。
		*/
		public void SetResultProgressDown(float a_result_progress_down)
		{
			this.result_progress_down = a_result_progress_down;
		}

		/** プログレス。取得。
		*/
		public float GetResultProgressDown()
		{
			return this.result_progress_down;
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

		/** 結果。サウンドプール。設定。
		*/
		public void SetResultSoundPool(Fee.Audio.Pack_SoundPool a_soundpool)
		{
			this.result_type = ResultType.SoundPool;

			this.result_soundpool = a_soundpool;
		}

		/** 結果。サウンドプール。取得。
		*/
		public Fee.Audio.Pack_SoundPool GetResultSoundPool()
		{
			return this.result_soundpool;
		}

		/** 結果。レスポンスヘッダー。設定。
		*/
		public void SetResultResponseHeader(System.Collections.Generic.Dictionary<string,string> a_responseheader)
		{
			this.result_responseheader = a_responseheader;
		}

		/** 結果。レスポンスヘッダー。取得。
		*/
		public System.Collections.Generic.Dictionary<string,string> GetResultResponseHeader()
		{
			return this.result_responseheader;
		}
	}
}

