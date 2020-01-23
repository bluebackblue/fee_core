

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
	/** バイナリデータ ==> Json文字列。
	*/
	public class Convert_BinaryData_ToJsonString
	{
		/** Convert
		*/
		public static void Convert(System.Collections.Generic.List<byte> a_in_value,System.Text.StringBuilder a_out_stringbuilder,ConvertToJsonStringOption a_option)
		{
			try{
				a_out_stringbuilder.Append("<");

				try{
					if(a_in_value != null){
						for(int ii=0;ii<a_in_value.Count;ii++){
							a_out_stringbuilder.Append(string.Format("{0:X2}",a_in_value[ii]));
						}
					}else{
						Tool.Assert(false);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				a_out_stringbuilder.Append(">");
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

