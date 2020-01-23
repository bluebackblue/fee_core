

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。JsonItem化。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** ObjectToJsonItem_FromEnum
	*/
	public class ObjectToJsonItem_FromEnum
	{
		/** Convert
		*/
		public static JsonItem Convert(System.Object a_from_object,ConvertToJsonItemOption a_from_option)
		{
			if((a_from_option & ConvertToJsonItemOption.EnumString) > 0){
				//enumの文字列化。

				string t_value = a_from_object.ToString();

				if(t_value != null){
					return new JsonItem(new Value_StringData(t_value));
				}else{
					//NULL処理。
					return null;
				}
			}else{
				//enumの数値化。

				System.Enum t_enum = (System.Enum)a_from_object;
				if(t_enum != null){
					switch(t_enum.GetTypeCode()){
					case System.TypeCode.Byte:
						{
							return new JsonItem(new Value_Number<System.Byte>((System.Byte)a_from_object));
						}break;
					case System.TypeCode.SByte:
						{
							return new JsonItem(new Value_Number<System.SByte>((System.SByte)a_from_object));
						}break;
					case System.TypeCode.Int16:
						{
							return new JsonItem(new Value_Number<System.Int16>((System.Int16)a_from_object));
						}break;
					case System.TypeCode.UInt16:
						{
							return new JsonItem(new Value_Number<System.UInt16>((System.UInt16)a_from_object));
						}break;
					case System.TypeCode.Int32:
						{
							return new JsonItem(new Value_Number<System.Int32>((System.Int32)a_from_object));
						}break;
					case System.TypeCode.UInt32:
						{
							return new JsonItem(new Value_Number<System.UInt32>((System.UInt32)a_from_object));
						}break;
					case System.TypeCode.Int64:
						{
							return new JsonItem(new Value_Number<System.Int64>((System.Int64)a_from_object));
						}break;
					case System.TypeCode.UInt64:
						{
							return new JsonItem(new Value_Number<System.UInt64>((System.UInt64)a_from_object));
						}break;
					default:
						{
							Tool.Assert(false);
						}break;
					}
				}
			}

			//失敗。
			Tool.Assert(false);
			return null;
		}
	}
}

