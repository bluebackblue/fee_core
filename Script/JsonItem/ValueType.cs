

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
		None = 0,
		
		/** 文字データ。
		*/
		StringData,

		/** 連想配列。
		*/
		AssociativeArray,

		/** インデックス配列。
		*/
		IndexArray,

		/** 整数。
		*/
		SignedNumber,

		/** 整数。
		*/
		UnsignedNumber,

		/** 少数。
		*/
		FloatingNumber,

		/** 真偽データ。
		*/
		BoolData,

		/** バイナリデータ。
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

