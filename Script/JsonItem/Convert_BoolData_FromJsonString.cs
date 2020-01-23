

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。コンバート。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** Json文字列 ==> 真偽データ。
	*/
	public class Convert_BoolData_FromJsonString
	{
		/** Convert
		*/
		public static void Convert(string a_in_jsonstring,out bool a_out_value)
		{
			try{
				if(a_in_jsonstring.Length < 1){
					//不明。
					a_out_value = default;
					Tool.Assert(false);
					return;
				}

				switch(a_in_jsonstring[0]){
				case 't':
				case 'T':
					{
						a_out_value = true;
						return;
					}break;
				}

				a_out_value = false;
				return;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//不明。
			Tool.Assert(false);
			a_out_value = default;
			return;
		}
	}
}

