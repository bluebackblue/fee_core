using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。値。
*/


/** NJsonItem
*/
namespace NJsonItem
{
	/** Value
	*/
	public struct Value
	{
		/** 文字データ。
		*/
		public string string_data;

		/** 連想配列。
		*/
		public Dictionary<string,JsonItem> associative_array;

		/** インデックス配列。
		*/
		public List<JsonItem> index_array;

		/** 整数。
		*/
		public long integer_number;

		/** 少数。
		*/
		public double float_number;

		/** 真偽データ。
		*/
		public bool bool_data;

		/** バイナリ―。
		*/
		public List<byte> binary_data;

		/** リセット。
		*/
		public void Reset()
		{
			this.string_data = null;
			this.associative_array = null;
			this.index_array = null;
			this.integer_number = 0;
			this.float_number = 0.0f;
			this.bool_data = false;
			this.binary_data = null;
		}
	}

	/** 文字データ。
	*/
	public struct Value_StringData
	{
		public string value;
		public Value_StringData(string a_value)
		{
			this.value = a_value;
		}
	}

	/** 連想配列。
	*/
	public struct Value_AssociativeArray
	{
	}

	/** インデックス配列。
	*/
	public struct Value_IndexArray
	{
	}

	/** 整数。
	*/
	public struct Value_Integer
	{
		public int value;
		public Value_Integer(int a_value)
		{
			this.value = a_value;
		}
	}

	/** TODO:整数。
	*/
	/*
	public struct Value_UnsignedInteger
	{
		public int value;
		public Value_UnsignedInteger(int a_value)
		{
			this.value = a_value;
		}
	}
	*/

	/** 少数。
	*/
	public struct Value_Float
	{
		public float value;
		public Value_Float(float a_value)
		{
			this.value = a_value;
		}
	}

	/** 真偽データ。
	*/
	public struct Value_BoolData
	{
		public bool value;
		public Value_BoolData(bool a_value)
		{
			this.value = a_value;
		}
	}

	/** バイナリデータ。
	*/
	public struct Value_BinaryData
	{
		public List<byte> value;
		Value_BinaryData(List<byte> a_value)
		{
			this.value = a_value;
		}
	}
}

