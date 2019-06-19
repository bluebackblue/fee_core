

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。アイテム。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Item_Base
	*/
	public interface Item_Base
	{
		/** 処理中。チェック。
		*/
		bool IsBusy();

		/** キャンセル。設定。
		*/
		void Cancel();

		/** キャンセル。取得。
		*/
		bool IsCancel();

		/** 結果。タイプ。取得。
		*/
		Item.ResultType GetResultType();

		/** プログレス。設定。
		*/
		void SetResultProgressUp(float a_result_progress_up);

		/** プログレス。取得。
		*/
		float GetResultProgressUp();

		/** プログレス。設定。
		*/
		void SetResultProgressDown(float a_result_progress_down);

		/** プログレス。取得。
		*/
		float GetResultProgressDown();

		/** 結果。エラー文字。設定。
		*/
		void SetResultErrorString(string a_error_string);

		/** 結果。エラー文字。取得。
		*/
		string GetResultErrorString();

		/** 結果。セーブ完了。設定。
		*/
		void SetResultSaveEnd();

		/** 結果。バイナリ。設定。
		*/
		void SetResultBinary(byte[] a_binary);

		/** 結果。バイナリ。取得。
		*/
		byte[] GetResultBinary();

		/** 結果。テキスト。設定。
		*/
		void SetResultText(string a_text);

		/** 結果。テキスト。取得。
		*/
		string GetResultText();

		/** 結果。テクスチャ。設定。
		*/
		void SetResultTexture(UnityEngine.Texture2D a_texture);

		/** 結果。テクスチャ。取得。
		*/
		UnityEngine.Texture2D GetResultTexture();

		/** 結果。アセットバンドル。設定。
		*/
		void SetResultAssetBundle(UnityEngine.AssetBundle a_assetbundle);

		/** 結果。アセットバンドル。取得。
		*/
		UnityEngine.AssetBundle GetResultAssetBundle();

		/** 結果。レスポンスヘッダー。設定。
		*/
		void SetResultResponseHeader(System.Collections.Generic.Dictionary<string,string> a_responseheader);

		/** 結果。レスポンスヘッダー。取得。
		*/
		System.Collections.Generic.Dictionary<string,string> GetResultResponseHeader();
	}

	/** Item
	*/
	public class Item : Item_Base
	{
		/** ResultType
		*/
		public enum ResultType
		{
			/** 未定義。
			*/
			None,

			/** セーブ完了。
			*/
			SaveEnd,

			/** エラー。
			*/
			Error,

			/** バイナリ。
			*/
			Binary,

			/** テキスト。
			*/
			Text,

			/** テクスチャ。
			*/
			Texture,

			/** アセットバンドル。
			*/
			AssetBundle,

			/** アセットファイル。
			*/
			Asset,
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

		/** result_binary
		*/
		private byte[] result_binary;

		/** result_text
		*/
		private string result_text;

		/** result_texture
		*/
		private UnityEngine.Texture2D result_texture;

		/** result_asset
		*/
		private UnityEngine.Object result_asset;

		/** result_assetbundle
		*/
		private UnityEngine.AssetBundle result_assetbundle;

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

			//result_binary
			this.result_binary = null;

			//result_text
			this.result_text = null;

			//result_texture
			this.result_texture = null;

			//result_asset
			this.result_asset = null;

			//result_assetbundle
			this.result_assetbundle = null;

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

		/** 結果。セーブ完了。設定。
		*/
		public void SetResultSaveEnd()
		{
			this.result_type = ResultType.SaveEnd;
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

		/** 結果。テキスト。設定。
		*/
		public void SetResultText(string a_text)
		{
			this.result_type = ResultType.Text;

			this.result_text = a_text;
		}

		/** 結果。テキスト。取得。
		*/
		public string GetResultText()
		{
			return this.result_text;
		}

		/** 結果。テクスチャ。設定。
		*/
		public void SetResultTexture(UnityEngine.Texture2D a_texture)
		{
			this.result_type = ResultType.Texture;

			this.result_texture = a_texture;
		}

		/** 結果。テクスチャ。取得。
		*/
		public UnityEngine.Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** 結果。アセット。設定。
		*/
		public void SetResultAsset(UnityEngine.Object a_asset)
		{
			this.result_type = ResultType.Asset;

			this.result_asset = a_asset;
		}

		/** 結果。アセット。取得。
		*/
		public UnityEngine.Object GetResultAsset()
		{
			return this.result_asset;
		}

		/** 結果。アセットバンドル。設定。
		*/
		public void SetResultAssetBundle(UnityEngine.AssetBundle a_assetbundle)
		{
			this.result_type = ResultType.AssetBundle;

			this.result_assetbundle = a_assetbundle;
		}

		/** 結果。アセットバンドル。取得。
		*/
		public UnityEngine.AssetBundle GetResultAssetBundle()
		{
			return this.result_assetbundle;
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

