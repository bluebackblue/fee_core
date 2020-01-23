

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
	/** 浮動小数 ==> Json文字列。
	*/
	public class Convert_FloatingNumber_ToJsonString
	{
		/** Convert
		*/
		public static void Convert(System.Single a_in_value,System.Text.StringBuilder a_out_stringbuilder,ConvertToJsonStringOption a_option)
		{
			try{
				string t_string = string.Format(Config.CULTURE,Config.FLOATING_TO_STRING_FORMAT,a_in_value);
				a_out_stringbuilder.Append(t_string);

				if((a_option & ConvertToJsonStringOption.NoFloatingNumberSuffix) == 0){
					a_out_stringbuilder.Append("f");
				}
				return;
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//不明。
			Tool.Assert(false);
			return;
		}

		/** Create
		*/
		public static void Convert(System.Double a_value,System.Text.StringBuilder a_out_stringbuilder,ConvertToJsonStringOption a_option)
		{
			try{
				string t_string = string.Format(Config.CULTURE,Config.FLOATING_TO_STRING_FORMAT,a_value);
				a_out_stringbuilder.Append(t_string);

				if((a_option & ConvertToJsonStringOption.NoFloatingNumberSuffix) == 0){
					a_out_stringbuilder.Append("f");
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

