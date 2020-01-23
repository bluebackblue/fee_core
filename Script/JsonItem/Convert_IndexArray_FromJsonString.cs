

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
	/** Json文字列 ==> インデックス配列。
	*/
	public class Convert_IndexArray_FromJsonString
	{
		/** Create
		*/
		public static void Convert(string a_in_jsonstring,System.Collections.Generic.List<JsonItem> a_out_list)
		{
			try{
				if(a_in_jsonstring.Length < 2){
					//不明。
					Tool.Assert(false);
					return;
				}

				if(a_in_jsonstring[0] != '['){
					//不明。
					Tool.Assert(false);
					return;
				}

				int t_index = 1;
				while(t_index < a_in_jsonstring.Length){
					switch(a_in_jsonstring[t_index]){
					case ']':
						{
							//終端。
							Tool.Assert(t_index + 1 == a_in_jsonstring.Length);
							return;
						}break;
					case ',':
						{
							//次の項目あり。
							t_index++;
						}break;
					}

					//値。
					int t_value_size = 0;
					{
						switch(ValueType_FirstCharValueType.Get(a_in_jsonstring[t_index])){
						case ValueType.StringData:
							{
								t_value_size = JsonStringLength.GetStringDataLength(a_in_jsonstring,t_index);
							}break;
						case ValueType.Calc_UnknownNumber:
						case ValueType.SignedNumber:
						case ValueType.UnsignedNumber:
						case ValueType.FloatingNumber:
							{
								t_value_size = JsonStringLength.GetNumberLength(a_in_jsonstring,t_index);
							}break;
						case ValueType.AssociativeArray:
							{
								t_value_size = JsonStringLength.GetAssociateArrayLength(a_in_jsonstring,t_index);
							}break;
						case ValueType.IndexArray:
							{
								t_value_size = JsonStringLength.GetIndexArrayLength(a_in_jsonstring,t_index);
							}break;
						case ValueType.Calc_BoolDataTrue:
							{
								t_value_size = JsonStringLength.GetBoolDataTrueLength(a_in_jsonstring,t_index);
							}break;
						case ValueType.Calc_BoolDataFalse:
							{
								t_value_size = JsonStringLength.GetBoolDataFalseLength(a_in_jsonstring,t_index);
							}break;
						case ValueType.BinaryData:
							{
								t_value_size = JsonStringLength.GetBinaryDataLength(a_in_jsonstring,t_index);
							}break;
						case ValueType.Null:
							{
								t_value_size = JsonStringLength.GetNullLength(a_in_jsonstring,t_index);
							}break;
						default:
							{
								//不明。
								Tool.Assert(false);
								return;
							}break;
						}
					}
			
					//リストに追加。
					if(t_value_size > 0){
						JsonItem t_additem = new JsonItem();
						{
							t_additem.SetJsonString(a_in_jsonstring.Substring(t_index,t_value_size));
						}

						a_out_list.Add(t_additem);
						t_index += t_value_size;
					}else{
						//不明。
						Tool.Assert(false);
						return;
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

