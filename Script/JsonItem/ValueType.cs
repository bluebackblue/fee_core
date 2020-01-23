

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

			System.Char
			System.SByte
			System.Byte
			System.Int16
			System.UInt16
			System.Int32
			System.UInt32
			System.Int64
		
		*/
		SignedNumber = 16,

		/** 符号なし整数。

			System.UInt64

		*/
		UnsignedNumber = 32,

		/** 浮動小数。

			System.Single
			System.Double

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

		/** 中間計算用。数値。
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

