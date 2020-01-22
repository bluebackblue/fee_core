

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。値。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** TYPE
	*/
	using SIGNED_NUMBER_TYPE = System.Int64;
	using UNSIGNED_NUMBER_TYPE = System.UInt64;
	using FLOATING_NUMBER_TYPE = System.Double;


	/** Value
	*/
	public struct Value
	{
		/** タイプ。
		*/
		public ValueType valuetype;

		/** 値。
		*/
		public System.Object raw;

		/** タイプチェック。
		*/
		public bool IsNull()
		{
			if(this.valuetype == ValueType.Null){
				return true;
			}
			return false;
		}

		/** タイプチェック。
		*/
		public bool IsAssociativeArray()
		{
			if(this.valuetype == ValueType.AssociativeArray){
				return true;
			}
			return false;
		}

		/** タイプチェック。
		*/
		public bool IsIndexArray()
		{
			if(this.valuetype == ValueType.IndexArray){
				return true;
			}
			return false;
		}

		/** タイプチェック。
		*/
		public bool IsStringData()
		{
			if(this.valuetype == ValueType.StringData){
				return true;
			}
			return false;
		}

		/** タイプチェック。
		*/
		public bool IsSignedNumber()
		{
			if(this.valuetype == ValueType.SignedNumber){
				return true;
			}
			return false;
		}

		/** タイプチェック。
		*/
		public bool IsUnSignedNumber()
		{
			if(this.valuetype == ValueType.UnsignedNumber){
				return true;
			}
			return false;
		}


		/** タイプチェック。
		*/
		public bool IsFloatingNumber()
		{
			if(this.valuetype == ValueType.FloatingNumber){
				return true;
			}
			return false;
		}


		/** タイプチェック。
		*/
		public bool IsBoolData()
		{
			if(this.valuetype == ValueType.BoolData){
				return true;
			}
			return false;
		}

		/** タイプチェック。
		*/
		public bool IsDecimalNumber()
		{
			if(this.valuetype == ValueType.DecimalNumber){
				return true;
			}
			return false;
		}

		/** タイプチェック。
		*/
		public bool IsBinaryData()
		{
			if(this.valuetype == ValueType.BinaryData){
				return true;
			}
			return false;
		}

		/** リセット。
		*/
		public void ResetFromType(ValueType a_valuetype)
		{
			this.valuetype = a_valuetype;
			this.raw = null;
		}
	
		/** 設定。
		*/
		public void Reset(System.Collections.Generic.Dictionary<string,JsonItem> a_raw)
		{
			this.valuetype = ValueType.AssociativeArray;
			this.raw = a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Collections.Generic.List<JsonItem> a_raw)
		{
			this.valuetype = ValueType.IndexArray;
			this.raw = a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.String a_raw)
		{
			this.valuetype = ValueType.StringData;
			this.raw = a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Char a_raw)
		{
			this.valuetype = ValueType.SignedNumber;
			this.raw = (SIGNED_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.SByte a_raw)
		{
			this.valuetype = ValueType.SignedNumber;
			this.raw = (SIGNED_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Byte a_raw)
		{
			this.valuetype = ValueType.SignedNumber;
			this.raw = (SIGNED_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Int16 a_raw)
		{
			this.valuetype = ValueType.SignedNumber;
			this.raw = (SIGNED_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.UInt16 a_raw)
		{
			this.valuetype = ValueType.SignedNumber;
			this.raw = (SIGNED_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Int32 a_raw)
		{
			this.valuetype = ValueType.SignedNumber;
			this.raw = (SIGNED_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.UInt32 a_raw)
		{
			this.valuetype = ValueType.SignedNumber;
			this.raw = (SIGNED_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Int64 a_raw)
		{
			this.valuetype = ValueType.SignedNumber;
			this.raw = (SIGNED_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.UInt64 a_raw)
		{
			this.valuetype = ValueType.UnsignedNumber;
			this.raw = (UNSIGNED_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Single a_raw)
		{
			this.valuetype = ValueType.FloatingNumber;
			this.raw = (FLOATING_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Double a_raw)
		{
			this.valuetype = ValueType.FloatingNumber;
			this.raw = (FLOATING_NUMBER_TYPE)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Boolean a_raw)
		{
			this.valuetype = ValueType.BoolData;
			this.raw = a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Decimal a_raw)
		{
			this.valuetype = ValueType.DecimalNumber;
			this.raw = (System.Decimal)a_raw;
		}

		/** 設定。
		*/
		public void Reset(System.Collections.Generic.List<byte> a_raw)
		{
			this.valuetype = ValueType.BinaryData;
			this.raw = a_raw;
		}

		/** GetAssociativeArray
		*/
		public System.Collections.Generic.Dictionary<string,JsonItem> GetAssociativeArray()
		{
			Tool.Assert(this.valuetype == ValueType.AssociativeArray);
			return (System.Collections.Generic.Dictionary<string,JsonItem>)this.raw;
		}

		/** GetIndexArray
		*/
		public System.Collections.Generic.List<JsonItem> GetIndexArray()
		{
			Tool.Assert(this.valuetype == ValueType.IndexArray);
			return (System.Collections.Generic.List<JsonItem>)this.raw;
		}

		/** GetStringData
		*/
		public string GetStringData()
		{
			Tool.Assert(this.valuetype == ValueType.StringData);
			return (string)this.raw;
		}

		/** GetSignedNumber
		*/
		public SIGNED_NUMBER_TYPE GetSignedNumber()
		{
			Tool.Assert(this.valuetype == ValueType.SignedNumber);
			return (SIGNED_NUMBER_TYPE)this.raw;
		}

		/** GetUnsignedNumber
		*/
		public UNSIGNED_NUMBER_TYPE GetUnsignedNumber()
		{
			Tool.Assert(this.valuetype == ValueType.UnsignedNumber);
			return (UNSIGNED_NUMBER_TYPE)this.raw;
		}

		/** GetFloatingNumber
		*/
		public FLOATING_NUMBER_TYPE GetFloatingNumber()
		{
			Tool.Assert(this.valuetype == ValueType.FloatingNumber);
			return (FLOATING_NUMBER_TYPE)this.raw;
		}

		/** GetBoolData
		*/
		public System.Boolean GetBoolData()
		{
			Tool.Assert(this.valuetype == ValueType.BoolData);
			return (System.Boolean)this.raw;
		}

		/** GetDecimalNumber
		*/
		public decimal GetDecimalNumber()
		{
			Tool.Assert(this.valuetype == ValueType.DecimalNumber);
			return (System.Decimal)this.raw;
		}

		/** GetBinaryData
		*/
		public System.Collections.Generic.List<byte> GetBinaryData()
		{
			Tool.Assert(this.valuetype == ValueType.BinaryData);
			return (System.Collections.Generic.List<byte>)this.raw;
		}

		/** CastToChar
		*/
		public System.Char CastToChar()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.Char)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.Char)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.Char)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.Char)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.Char)1 : (System.Char)0);
				}break;
			}

			Tool.Assert(false);
			return (System.Char)0;
		}

		/** CastToSbyte
		*/
		public System.SByte CastToSbyte()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.SByte)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.SByte)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.SByte)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.SByte)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.SByte)1 : (System.SByte)0);
				}break;
			}

			Tool.Assert(false);
			return (System.SByte)0;
		}

		/** CastToByte
		*/
		public System.Byte CastToByte()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.Byte)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.Byte)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.Byte)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.Byte)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.Byte)1 : (System.Byte)0);
				}break;
			}

			Tool.Assert(false);
			return (System.Byte)0;
		}

		/** CastToInt16
		*/
		public System.Int16 CastToInt16()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.Int16)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.Int16)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.Int16)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.Int16)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.Int16)1 : (System.Int16)0);
				}break;
			}

			Tool.Assert(false);
			return (System.Int16)0;
		}

		/** CastToUint16
		*/
		public System.UInt16 CastToUint16()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.UInt16)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.UInt16)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.UInt16)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.UInt16)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.UInt16)1 : (System.UInt16)0);
				}break;
			}

			Tool.Assert(false);
			return (System.UInt16)0;
		}

		/** CastToInt32
		*/
		public System.Int32 CastToInt32()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.Int32)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.Int32)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.Int32)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.Int32)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.Int32)1 : (System.Int32)0);
				}break;
			}

			Tool.Assert(false);
			return (System.Int32)0;
		}

		/** CastToUint32
		*/
		public System.UInt32 CastToUint32()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.UInt32)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.UInt32)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.UInt32)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.UInt32)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.UInt32)1 : (System.UInt32)0);
				}break;
			}

			Tool.Assert(false);
			return (System.UInt32)0;
		}

		/** CastToInt64
		*/
		public System.Int64 CastToInt64()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.Int64)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.Int64)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.Int64)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.Int64)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.Int64)1 : (System.Int64)0);
				}break;
			}

			Tool.Assert(false);
			return (System.Int64)0;
		}

		/** CastToUint64
		*/
		public System.UInt64 CastToUint64()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.UInt64)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.UInt64)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.UInt64)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.UInt64)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.UInt64)1 : (System.UInt64)0);
				}break;
			}

			Tool.Assert(false);
			return (System.UInt64)0;
		}

		/** CastToSingle
		*/
		public System.Single CastToSingle()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.Single)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.Single)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.Single)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.Single)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.Single)1 : (System.Single)0);
				}break;
			}

			Tool.Assert(false);
			return (System.Single)0;
		}

		/** CastToDouble
		*/
		public System.Double CastToDouble()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.Double)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.Double)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.Double)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.Double)(System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.Double)1 : (System.Double)0);
				}break;
			}

			Tool.Assert(false);
			return (System.Double)0;
		}

		/** CastToBoolData
		*/
		public System.Boolean CastToBoolData()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return ((SIGNED_NUMBER_TYPE)this.raw > 0) ? (System.Boolean)true : (System.Boolean)false;
				}break;
			case ValueType.FloatingNumber:
				{
					return ((FLOATING_NUMBER_TYPE)this.raw > 0) ? (System.Boolean)true : (System.Boolean)false;
				}break;
			case ValueType.UnsignedNumber:
				{
					return ((UNSIGNED_NUMBER_TYPE)this.raw > 0) ? (System.Boolean)true : (System.Boolean)false;
				}break;
			case ValueType.DecimalNumber:
				{
					return ((System.Decimal)this.raw > 0) ? (System.Boolean)true : (System.Boolean)false;
				}break;
			case ValueType.BoolData:
				{
					return (System.Boolean)this.raw;
				}break;
			}

			Tool.Assert(false);
			return (System.Boolean)false;
		}

		/** CastToDecimal
		*/
		public System.Decimal CastToDecimal()
		{
			switch(this.valuetype){
			case ValueType.SignedNumber:
				{
					return (System.Decimal)(SIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.UnsignedNumber:
				{
					return (System.Decimal)(UNSIGNED_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.FloatingNumber:
				{
					return (System.Decimal)(FLOATING_NUMBER_TYPE)this.raw;
				}break;
			case ValueType.DecimalNumber:
				{
					return (System.Decimal)this.raw;
				}break;
			case ValueType.BoolData:
				{
					return ((System.Boolean)this.raw ? (System.Decimal)1 : (System.Decimal)0);
				}break;
			}

			Tool.Assert(false);
			return (System.Decimal)0;
		}
	}

	/** 連想配列。
	*/
	public readonly struct Value_AssociativeArray
	{
	}

	/** インデックス配列。
	*/
	public readonly struct Value_IndexArray
	{
	}

	/** Value_StringData
	*/
	public readonly struct Value_StringData
	{
		readonly public string value;
		public Value_StringData(string a_value)
		{
			this.value = a_value;
		}
	}

	/** 数値。

		System.Char(char)
		System.SByte(sbyte)
		System.Byte(byte)
		System.Int16(short)
		System.UInt16(ushort)
		System.Int32(int)
		System.UInt32(uint)
		System.Int64(long)

		System.UInt64(ulong)

		System.Single(float)
		System.Double(double)

		System.Boolean(bool)

		System.Decimal(decimal)

	*/
	public readonly struct Value_Number<T>
	{
		readonly public T value;
		public Value_Number(T a_value)
		{
			this.value = a_value;
		}
	}

	/** Value_BinaryData
	*/
	public readonly struct Value_BinaryData
	{
		readonly public System.Collections.Generic.List<byte> value;
		public Value_BinaryData(System.Collections.Generic.List<byte> a_value)
		{
			this.value = a_value;
		}
	}
}

