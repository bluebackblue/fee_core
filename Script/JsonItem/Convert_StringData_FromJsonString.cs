

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
	/** JSON文字列 ==> 文字データ。
	*/
	public class Convert_StringData_FromJsonString
	{
		/** Convert
		*/
		public static void Convert(string a_in_jsonstring,System.Text.StringBuilder a_out_stringbuilder)
		{
			try{
				if(a_in_jsonstring.Length < 2){
					//不明。
					Tool.Assert(false);
					return;
				}

				if(a_in_jsonstring[0] != '\"'){
					//不明。
					Tool.Assert(false);
					return;
				}

				int t_index = 1;
				while(t_index < a_in_jsonstring.Length){
					switch(a_in_jsonstring[t_index]){
					case '\"':
						{
							//終端。
							Tool.Assert((t_index + 1) == a_in_jsonstring.Length);
							return;
						}break;
					case '\\':
						{
							if((t_index + 1) < a_in_jsonstring.Length){
								int t_use_index = StringConvert.EscapeCodeStringToSpecialString.Convert(a_in_jsonstring,t_index + 1,a_out_stringbuilder);
								t_index += (t_use_index + 1);
							}else{
								//不明。
								Tool.Assert(false);
								return;
							}
						}break;
					default:
						{
							a_out_stringbuilder.Append(a_in_jsonstring[t_index]);
							t_index++;
						}break;
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//不明。
			Tool.Assert(false);
			return;
		}
	}
}

