

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
	public enum ValueType
	{
		/** ＮＵＬＬ。

		24:null

		*/
		None = 0,

		/** 0:連想配列。
		*/
		AssociativeArray,

		/** 1:インデックス配列。
		*/
		IndexArray,

		/** 2:文字データ。
		*/
		StringData,

		/** 11:bool。
		*/
		BoolData,

		/** 整数。

		12:char
		15:decimal
		16:sbyte
		17:byte
		18:short
		19:ushort
		20:int
		21:uint
		22:long
		
		*/
		SignedNumber,

		/** 整数。

		23:ulong

		*/
		UnsignedNumber,

		/** 少数。

		13:float
		14:double

		*/
		FloatingNumber,

		/** 3:バイナリデータ。
		*/
		BinaryData,


		

		/** 中間計算用。数値（少数/整数）。
		*/
		Calc_UnknownNumber,

		/** 中間計算用。真。
		*/
		Calc_BoolDataTrue,

		/** 中間計算用。偽。
		*/
		Calc_BoolDataFalse,




		/** Mask : SignedNumber & UnsignedNumber として扱う。
		*/
		Mask_Integer_Number,

		/** Mask : SignedNumber & UnsignedNumber & FloatingNumber として扱う。
		*/
		Mask_All_Number,
	}
}

