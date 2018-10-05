using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。アイテム。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** Item
	*/
	public class Item
	{
		/** result_datatype
		*/
		private DataType result_datatype;

		/** result_progress
		*/
		private float result_progress;

		/** result_errorstring
		*/
		private string result_errorstring;

		/** result_soundpool
		*/
		private NAudio.Pack_SoundPool result_soundpool;

		/** result_assetbundle
		*/
		private AssetBundle result_assetbundle;

		/** result_text
		*/
		private string result_text;

		/** result_texture
		*/
		private Texture2D result_texture;

		/** result_binary
		*/
		private byte[] result_binary;

		/** cancel_flag
		*/
		private bool cancel_flag;

		/** constructor
		*/
		public Item()
		{
			//result_datatype
			this.result_datatype = DataType.None;

			//result_progress
			this.result_progress = 0.0f;

			//result_errorstring
			this.result_errorstring = null;

			//result_soundpool
			this.result_soundpool = null;

			//result_assetbundle
			this.result_assetbundle = null;

			//result_text
			this.result_text = null;

			//result_texture
			this.result_texture = null;

			//result_binary
			this.result_binary = null;

			//cancel_flag
			this.cancel_flag = false;
		}

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if(this.result_datatype == DataType.None){
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

		/** データタイプ。取得。
		*/
		public DataType GetResultDataType()
		{
			return this.result_datatype;
		}

		/** 結果。設定。
		*/
		public void SetResultErrorString(string a_error_string)
		{
			this.result_datatype = DataType.Error;

			this.result_errorstring = a_error_string;
		}

		/** 結果。取得。
		*/
		public string GetResultErrorString()
		{
			return this.result_errorstring;
		}

		/** 結果。サウンドプール。設定。
		*/
		public void SetResultSoundPool(NAudio.Pack_SoundPool a_soundpool)
		{
			this.result_datatype = DataType.SoundPool;

			this.result_soundpool = a_soundpool;
		}

		/** 結果。サウンロプール。取得。
		*/
		public NAudio.Pack_SoundPool GetResultSoundPool()
		{
			return this.result_soundpool;
		}

		/** 結果。アセットバンドル。設定。
		*/
		public void SetResultAssetBundle(AssetBundle a_assetbundle)
		{
			this.result_datatype = DataType.AssetBundle;

			this.result_assetbundle = a_assetbundle;
		}

		/** 結果。アセットバンドル。取得。
		*/
		public AssetBundle GetResultAssetBundle()
		{
			return this.result_assetbundle;
		}

		/** 結果。テキスト。設定。
		*/
		public void SetResultText(string a_text)
		{
			this.result_datatype = DataType.Text;

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
		public void SetResultTexture(Texture2D a_texture)
		{
			this.result_datatype = DataType.Texture;

			this.result_texture = a_texture;
		}

		/** 結果。テクスチャ。取得。
		*/
		public Texture2D GetResultTexture()
		{
			return this.result_texture;
		}

		/** 結果。バイナリ。設定。
		*/
		public void SetResultBinary(byte[] a_binary)
		{
			this.result_datatype = DataType.Binary;

			this.result_binary = a_binary;
		}

		/** 結果。バイナリ。取得。
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}
	}
}

