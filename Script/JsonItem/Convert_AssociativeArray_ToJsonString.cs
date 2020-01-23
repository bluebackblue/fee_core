

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
	/** 連想配列 ==> Json文字列。
	*/
	public class Convert_AssociativeArray_ToJsonString
	{
		/** Convert
		*/
		public static void Convert(System.Collections.Generic.Dictionary<string,JsonItem> a_in_list,System.Text.StringBuilder a_out_stringbuilder,ConvertToJsonStringOption a_option)
		{
			try{
				a_out_stringbuilder.Append("{");

				try{
					if(a_in_list != null){
						bool t_first = true;
						foreach(System.Collections.Generic.KeyValuePair<string,JsonItem> t_pair in a_in_list){
							if(t_first == true){
								//一つ目。

								if(t_pair.Value != null){
									t_first = false;
									a_out_stringbuilder.Append("\"");
									a_out_stringbuilder.Append(t_pair.Key);
									a_out_stringbuilder.Append("\":");
									t_pair.Value.ValueToJsonString(a_out_stringbuilder,a_option);
								}else{
									//NULL処理。
									t_first = false;
									a_out_stringbuilder.Append("\"");
									a_out_stringbuilder.Append(t_pair.Key);
									a_out_stringbuilder.Append("\":null");
								}
							}else{
								//二つ目以降。

								if(t_pair.Value != null){
									a_out_stringbuilder.Append(",\"");
									a_out_stringbuilder.Append(t_pair.Key);
									a_out_stringbuilder.Append("\":");
									t_pair.Value.ValueToJsonString(a_out_stringbuilder,a_option);
								}else{
									//NULL処理。
									a_out_stringbuilder.Append(",\"");
									a_out_stringbuilder.Append(t_pair.Key);
									a_out_stringbuilder.Append("\":null");
								}
							}
						}
					}else{
						Tool.Assert(false);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				a_out_stringbuilder.Append("}");
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

