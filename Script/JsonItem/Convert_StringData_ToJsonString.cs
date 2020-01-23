

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
	/** 文字データ ==> Json文字列。
	*/
	public class Convert_StringData_ToJsonString
	{
		/** Convert
		*/
		public static void Convert(System.String a_in_value,System.Text.StringBuilder a_out_stringbuilder,ConvertToJsonStringOption a_option)
		{
			try{
				a_out_stringbuilder.Append("\"");

				try{
					if(a_in_value != null){
						for(int ii=0;ii<a_in_value.Length;ii++){
							StringConvert.SpecialStringToJsonItemEscapeString.Convert(a_in_value,ii,a_out_stringbuilder);
						}
					}else{
						Tool.Assert(false);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				a_out_stringbuilder.Append( "\"");
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

