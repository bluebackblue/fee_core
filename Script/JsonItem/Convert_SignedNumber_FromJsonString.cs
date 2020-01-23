

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
	/** Json文字列 ==> 符号あり整数。
	*/
	public class Convert_SignedNumber_FromJsonString
	{
		/** Convert
		*/
		public static void Convert(string a_in_jsonstring,out System.Int32 a_out_value)
		{
			try{
				if(a_in_jsonstring.Length < 1){
					//不明。
					Tool.Assert(false);
					a_out_value = default;
					return;
				}

				string t_value;
				if((a_in_jsonstring[a_in_jsonstring.Length - 1] == 'l')||(a_in_jsonstring[a_in_jsonstring.Length - 1] == 'L')){
					t_value = a_in_jsonstring.Substring(0,a_in_jsonstring.Length - 1);
				}else{
					t_value = a_in_jsonstring;
				}

				System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.AllowLeadingSign;
				if(System.Int32.TryParse(t_value,t_style,Config.CULTURE,out a_out_value) == true){
					return;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//不明。
			Tool.Assert(false);
			a_out_value = default;
			return;
		}

		/** Convert
		*/
		public static void Convert(string a_in_jsonstring,out System.Int64 a_out_value)
		{
			try{
				if(a_in_jsonstring.Length < 1){
					//不明。
					Tool.Assert(false);
					a_out_value = default;
					return;
				}

				string t_value;
				if((a_in_jsonstring[a_in_jsonstring.Length - 1] == 'l')||(a_in_jsonstring[a_in_jsonstring.Length - 1] == 'L')){
					t_value = a_in_jsonstring.Substring(0,a_in_jsonstring.Length - 1);
				}else{
					t_value = a_in_jsonstring;
				}

				System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.AllowLeadingSign;
				if(System.Int64.TryParse(t_value,t_style,Config.CULTURE,out a_out_value) == true){
					return;
				}
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

