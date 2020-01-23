

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 文字コンバート。
*/


/** Fee.StringConvert
*/
namespace Fee.StringConvert
{
	/** １６進数文字列 ==> Byte。

		"F" ==> 15

	*/
	public class HexStringToByte
	{
		/** TABLE
		*/
		private readonly static byte[] TABLE = new byte[0x40]{
			0,1,2,3,4,5,6,7,8,9,	//'0' -- '9'

			255,255,255,255,255,255,255,

			10,11,12,13,14,15,		//'A' -- 'F'

			255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,255,

			10,11,12,13,14,15,		//'a' -- 'f'

			255,255,255,255,255,255,255,255,255
		};

		/** Convert_NoCheck

			(0 - 9 / A - F / a - f)以外のcharが渡された場合、正しく機能しない。

		*/
		public static void Convert_NoCheck(char a_char,out byte a_out_byte)
		{
			a_out_byte = TABLE[(a_char - 48) & 0x3F];
		}

		/** Convert_NoCheck

			(0 - 9 / A - F / a - f)以外のcharが渡された場合、正しく機能しない。

		*/
		public static void Convert_NoCheck(string a_string,int a_offset,out byte a_out_byte)
		{
			Convert_NoCheck(a_string[a_offset],out a_out_byte);
		}
	}
}

