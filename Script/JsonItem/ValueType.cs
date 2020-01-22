

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。タイプ。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** ValueType
	*/
	public enum ValueType : ulong
	{
		/** ＮＵＬＬ。
		*/
		Null = 1,

		/** 連想配列。
		*/
		AssociativeArray = 2,

		/** インデックス配列。
		*/
		IndexArray = 4,

		/** 文字データ。

			System.String(string)

		*/
		StringData = 8,

		/** 符号あり整数。

			System.Char(char)
			System.SByte(sbyte)
			System.Byte(byte)
			System.Int16(short)
			System.UInt16(ushort)
			System.Int32(int)
			System.UInt32(uint)
			System.Int64(long)
		
		*/
		SignedNumber = 16,

		/** 符号なし整数。

			System.UInt64(ulong)

		*/
		UnsignedNumber = 32,

		/** 浮動小数。

			System.Single(float)
			System.Double(double)

		*/
		FloatingNumber = 64,

		/** 真偽データ。

			System.Boolean(bool)

		*/
		BoolData = 128,


		/** １０進数の浮動小数点数。

			decimal

		*/
		DecimalNumber = 256,

		/** バイナリデータ。
		*/
		BinaryData = 512,




		/** 中間計算用。
		*/
		Calc = 32768,

		/** 中間計算用。数値（少数/整数）。
		*/
		Calc_UnknownNumber = 32768 + 1,

		/** 中間計算用。真。
		*/
		Calc_BoolDataTrue = 32768 + 2,

		/** 中間計算用。偽。
		*/
		Calc_BoolDataFalse = 32768 + 3,




		/** Mask
		*/
		Mask_All = Null | AssociativeArray | IndexArray | StringData | BoolData | SignedNumber | DecimalNumber | UnsignedNumber | FloatingNumber | BinaryData,
	}
}

