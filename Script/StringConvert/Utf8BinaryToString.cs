

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
	/** byte[] => 文字列。
	*/
	public class Utf8BinaryToString
	{
		/** Convert
		*/
		public static string Convert(byte[] a_binary_utf8,int a_index,int a_length)
		{
			string t_string = null;

			try{
				t_string = System.Text.Encoding.UTF8.GetString(a_binary_utf8,a_index,a_length);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				t_string = null;
			}

			return t_string;
		}
	}
}

