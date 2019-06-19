

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
	public struct Value_Int
	{
		public int value;
		public Value_Int(int a_value)
		{
			this.value = a_value;
		}
	}

	/** 整数。
	*/
	public struct Value_Long
	{
		public long value;
		public Value_Long(long a_value)
		{
			this.value = a_value;
		}
	}

	/** 整数。
	*/
	public struct Value_UnsignedInt
	{
		public uint value;
		public Value_UnsignedInt(uint a_value)
		{
			this.value = a_value;
		}
	}

	/** 整数。
	*/
	public struct Value_UnsignedLong
	{
		public ulong value;
		public Value_UnsignedLong(ulong a_value)
		{
			this.value = a_value;
		}
	}

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

	/** 少数。
	*/
	public struct Value_Double
	{
		public double value;
		public Value_Double(double a_value)
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
		public System.Collections.Generic.List<byte> value;
		public Value_BinaryData(System.Collections.Generic.List<byte> a_value)
		{
			this.value = a_value;
		}
	}
}

