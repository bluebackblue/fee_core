

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
	/** インデックス配列 ==> JSON文字列。
	*/
	public class Convert_IndexArray_ToJsonString
	{
		/** Convert
		*/
		public static void Convert(System.Collections.Generic.List<JsonItem> a_in_list,System.Text.StringBuilder a_out_stringbuilder,ConvertToJsonStringOption a_option)
		{
			try{
				a_out_stringbuilder.Append("[");

				try{
					if(a_in_list != null){
						int t_count = a_in_list.Count;
						int t_index = 0;

						if(t_count > 0){

							//一つ目。
							if(a_in_list[0] != null){
								a_in_list[0].ValueToJsonString(a_out_stringbuilder,a_option);
							}else{
								//NULL処理。
								a_out_stringbuilder.Append("null");
							}

							//二つ目以降。
							t_index++;
							for(;t_index<t_count;t_index++){
								a_out_stringbuilder.Append(",");
								if(a_in_list[t_index] != null){
									a_in_list[t_index].ValueToJsonString(a_out_stringbuilder,a_option);
								}else{
									//NULL処理。
									a_out_stringbuilder.Append("null");
								}
							}
						}
					}else{
						Tool.Assert(false);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				a_out_stringbuilder.Append("]");
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

