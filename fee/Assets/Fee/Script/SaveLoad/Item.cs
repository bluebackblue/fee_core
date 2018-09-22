using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief セーブロード。アイテム。
*/


/** NSaveLoad
*/
namespace NSaveLoad
{
	/** Item
	*/
	public class Item
	{
		/** datatype
		*/
		private DataType datatype;

		/** result_binary
		*/
		private byte[] result_binary;

		/** result_text
		*/
		private string result_text;

		/** result_texture
		*/
		private Texture2D result_texture;

		/** constructor
		*/
		public Item()
		{
			//datatype
			this.datatype = DataType.None;

			//result_binary
			this.result_binary = null;

			//result_text
			this.result_text = null;

			//result_texture
			this.result_texture = null;
		}

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if(this.datatype == DataType.None){
				return true;
			}
			return false;
		}

		/** データタイプ。取得。
		*/
		public DataType GetDataType()
		{
			return this.datatype;
		}

		/** 結果。セーブ完了。
		*/
		public void SetResultSaveEnd()
		{
			this.datatype = DataType.SaveEnd;
		}

		/** 結果。エラー。
		*/
		public void SetResultError()
		{
			this.datatype = DataType.Error;
		}

		/** 結果。設定。
		*/
		public void SetResultBinary(byte[] a_binary)
		{
			this.datatype = DataType.Binary;

			this.result_binary = a_binary;
		}

		/** 結果。設定。
		*/
		public void SetResultText(string a_text)
		{
			this.datatype = DataType.Text;

			this.result_text = a_text;
		}

		/** 結果。取得。
		*/
		public byte[] GetResultBinary()
		{
			return this.result_binary;
		}

		/** 結果。取得。
		*/
		public string GetResultText()
		{
			return this.result_text;
		}

		/** 結果。設定。
		*/
		public void SetResultTexture(Texture2D a_texture)
		{
			this.datatype = DataType.Texture;

			this.result_texture = a_texture;
		}

		/** 結果。取得。
		*/
		public Texture2D GetResultTexture()
		{
			return this.result_texture;
		}
	}
}

