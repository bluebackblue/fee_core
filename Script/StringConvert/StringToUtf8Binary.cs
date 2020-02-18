

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
	/** 文字列 ==> byte[]。
	*/
	public class StringToUtf8Binary
	{
		/** Convert
		*/
		public static byte[] Convert(string a_string)
		{
			byte[] t_binary = null;

			try{
				t_binary = System.Text.Encoding.UTF8.GetBytes(a_string);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				t_binary = null;
			}

			return t_binary;
		}
	}
}

