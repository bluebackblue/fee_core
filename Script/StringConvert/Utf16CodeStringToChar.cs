

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
	/** ＵＴＦ１６コード文字列 ==> Char。

		"3040" ==> "あ"

	*/
	public class Utf16CodeStringToChar
	{
		/** BUFFER_FROM
		*/
		private static byte[] BUFFER_FROM = new byte[2];

		/** BUFFER_TO
		*/
		private static char[] BUFFER_TO = new char[2];

		/** LOACKOBJECT
		*/
		private static System.Object LOACKOBJECT = new System.Object();

		/** Convert_NoCheck
		*/
		public static char Convert_NoCheck(string a_string,int a_offset)
		{
			byte t_buffer_0;
			byte t_buffer_1;
			byte t_buffer_2;
			byte t_buffer_3;
			HexStringToByte.Convert_NoCheck(a_string,a_offset + 0,out t_buffer_0);
			HexStringToByte.Convert_NoCheck(a_string,a_offset + 1,out t_buffer_1);
			HexStringToByte.Convert_NoCheck(a_string,a_offset + 2,out t_buffer_2);
			HexStringToByte.Convert_NoCheck(a_string,a_offset + 3,out t_buffer_3);

			lock(LOACKOBJECT){
				BUFFER_FROM[0] = (byte)((t_buffer_2 << 4) | t_buffer_3);
				BUFFER_FROM[1] = (byte)((t_buffer_0 << 4) | t_buffer_1);
				if(System.Text.Encoding.Unicode.GetChars(BUFFER_FROM,0,2,BUFFER_TO,0) == 1){
					return BUFFER_TO[0];
				}
			}

			//不明なコード。
			return ' ';
		}
	}
}

