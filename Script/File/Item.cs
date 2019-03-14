

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。アイテム。
*/


/** Fee.File
*/
namespace Fee.File
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

			/** サウンドプール。
			*/
			SoundPool,
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

		/** result_text
		*/
		private string result_text;

		/** result_texture
		*/
		private UnityEngine.Texture2D result_texture;

		/** result_assetbundle
		*/
		private UnityEngine.AssetBundle result_assetbundle;

		/** result_soundpool
		*/
		private Fee.Audio.Pack_SoundPool result_soundpool;

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

			//result_text
			this.result_text = null;

			//result_texture
			this.result_texture = null;

			//result_assetbundle
			this.result_assetbundle = null;

			//result_soundpool
			this.result_soundpool = null;
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

		/** 結果。サウンドプール。設定。
		*/
		public void SetResultSoundPool(Fee.Audio.Pack_SoundPool a_soundpool)
		{
			this.result_type = ResultType.SoundPool;

			this.result_soundpool = a_soundpool;
		}

		/** 結果。サウンロプール。取得。
		*/
		public Fee.Audio.Pack_SoundPool GetResultSoundPool()
		{
			return this.result_soundpool;
		}
	}
}

