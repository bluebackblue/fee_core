

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。コンバート。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonItem ==> Json文字列。
	*/
	public enum ConvertToJsonStringOption : long
	{
		/** なし。
		*/
		None = 0,

		/** １０進数の浮動小数点数にサフィックスを付けない。
		*/
		NoDecimalNumberSuffix = 1,

		/** 浮動小数にサフィックスを付けない。
		*/
		NoFloatingNumberSuffix = 2,

		/** 符号あり整数にサフィックスを付けない。
		*/
		NoSignedNumberSuffix = 4,

		/** 符号なし整数にサフィックスを付けない。
		*/
		NoUnsignedNumberSuffix = 8,
	}
}

