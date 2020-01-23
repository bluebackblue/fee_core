

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
	/** １０進数の浮動小数点数 ==> JSON文字列。
	*/
	public class Convert_DecimalNumber_ToJsonString
	{
		/** Convert
		*/
		public static void Convert(System.Decimal a_in_value,System.Text.StringBuilder a_out_stringbuilder,ConvertToJsonStringOption a_option)
		{
			try{
				try{
					string t_string = a_in_value.ToString(Config.CULTURE);
					a_out_stringbuilder.Append(t_string);

					if((a_option & ConvertToJsonStringOption.NoDecimalNumberSuffix) == 0){
						a_out_stringbuilder.Append("m");
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				return;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//不明。
			Tool.Assert(false);
			return;
		}
	}
}

