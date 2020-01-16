

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。値。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
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
		public System.Collections.Generic.Dictionary<string,JsonItem> associative_array;

		/** インデックス配列。
		*/
		public System.Collections.Generic.List<JsonItem> index_array;

		/** 整数。
		*/
		public long signed_number;

		/** 整数。
		*/
		public ulong unsigned_number;

		/** 少数。
		*/
		public double floating_number;

		/** 真偽データ。
		*/
		public bool bool_data;

		/** バイナリ―。
		*/
		public System.Collections.Generic.List<byte> binary_data;

		/** リセット。
		*/
		public void Reset()
		{
			this.string_data = null;
			this.associative_array = null;
			this.index_array = null;
			this.signed_number = 0;
			this.unsigned_number = 0;
			this.floating_number = 0.0;
			this.bool_data = false;
			this.binary_data = null;
		}
	}

	/** 0:連想配列。
	*/
	public struct Value_AssociativeArray
	{
	}

	/** 1:インデックス配列。
	*/
	public struct Value_IndexArray
	{
	}

	/** 2:文字データ。
	*/
	public struct Value_StringData
	{
		public string value;
		public Value_StringData(string a_value)
		{
			this.value = a_value;
		}
	}

	/** 3:バイナリデータ。
	*/
	public struct Value_BinaryData
	{
		public System.Collections.Generic.List<byte> value;
		public Value_BinaryData(System.Collections.Generic.List<byte> a_value)
		{
			this.value = a_value;
		}
	}

	/** 数値。

		11:bool
		12:char
		13:float
		14:double
		15:decimal

		16:sbyte
		17:byte

		18:short
		19:ushort

		20:int
		21:uint

		22:long
		23:ulong

	*/
	public struct Value_Number<T>
	{
		public T value;
		public Value_Number(T a_value)
		{
			this.value = a_value;
		}
	}
}

