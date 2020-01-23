

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
	/** Json文字列 ==> バイナリデータ。
	*/
	public class Convert_BinaryData_FromJsonString
	{
		/** Convert
		*/
		public static void Convert(string a_in_jsonstring,System.Collections.Generic.List<byte> a_out_list)
		{
			try{
				if(a_in_jsonstring.Length < 2){
					//不明。
					Tool.Assert(false);
					return;
				}

				if(a_in_jsonstring[0] != '<'){
					//不明。
					Tool.Assert(false);
					return;
				}

				int t_index = 1;
				while(t_index < a_in_jsonstring.Length){
					if(a_in_jsonstring[t_index] == '>'){
						//終端。
						Tool.Assert((t_index + 1) == a_in_jsonstring.Length);
						return;
					}else{
						if(t_index + 1 < a_in_jsonstring.Length){

							byte t_binary_1;
							byte t_binary_2;
							Fee.StringConvert.HexStringToByte.Convert_NoCheck(a_in_jsonstring[t_index + 0],out t_binary_1);
							Fee.StringConvert.HexStringToByte.Convert_NoCheck(a_in_jsonstring[t_index + 1],out t_binary_2);
							byte t_binary = (byte)(t_binary_1 << 8 | t_binary_2);

							//リストに追加。
							a_out_list.Add(t_binary);
							t_index += 2;
						}else{
							//不明。
							Tool.Assert(false);
							return;
						}
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

