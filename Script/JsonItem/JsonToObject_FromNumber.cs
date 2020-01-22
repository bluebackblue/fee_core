

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。オブジェクト化。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonToObject_FromNumber
	*/
	public class JsonToObject_FromNumber
	{
		/** Convert
		*/
		public static void Convert(ref System.Object a_to_object,System.Type a_to_type,JsonItem a_from_jsonitem)
		{
			try{
				switch(a_to_type.FullName){
				case "System." + nameof(System.Char):
					{
						a_to_object = a_from_jsonitem.CastToChar();
						return;
					}break;
				case "System." + nameof(System.SByte):
					{
						a_to_object = a_from_jsonitem.CastToSbyte();
						return;
					}break;
				case "System." + nameof(System.Byte):
					{
						a_to_object = a_from_jsonitem.CastToByte();
						return;
					}break;
				case "System." + nameof(System.Int16):
					{
						a_to_object = a_from_jsonitem.CastToInt16();
						return;
					}break;
				case "System." + nameof(System.UInt16):
					{
						a_to_object = a_from_jsonitem.CastToUint16();
						return;
					}break;
				case "System." + nameof(System.Int32):
					{
						a_to_object = a_from_jsonitem.CastToInt32();
						return;
					}break;
				case "System." + nameof(System.UInt32):
					{
						a_to_object = a_from_jsonitem.CastToUint32();
						return;
					}break;
				case "System." + nameof(System.Int64):
					{
						a_to_object = a_from_jsonitem.CastToInt64();
						return;
					}break;
				case "System." + nameof(System.UInt64):
					{
						a_to_object = a_from_jsonitem.CastToUint64();
						return;
					}break;
				case "System." + nameof(System.Single):
					{
						a_to_object = a_from_jsonitem.CastToSingle();
						return;
					}break;
				case "System." + nameof(System.Double):
					{
						a_to_object = a_from_jsonitem.CastToDouble();
						return;
					}break;
				case "System." + nameof(System.Boolean):
					{
						a_to_object = a_from_jsonitem.CastToBoolData();
						return;
					}break;
				case "System." + nameof(System.Decimal):
					{
						a_to_object = a_from_jsonitem.CastToDecimal();
						return;
					}break;
				default:
					{
						if(a_to_type.IsEnum == true){
							System.Enum t_enum = (System.Enum)a_to_object;
							if(t_enum != null){
								switch(t_enum.GetTypeCode()){
								case System.TypeCode.Byte:
									{
										a_to_object = System.Enum.ToObject(a_to_type,a_from_jsonitem.CastToByte());
										return;
									}break;
								case System.TypeCode.SByte:
									{
										a_to_object = System.Enum.ToObject(a_to_type,a_from_jsonitem.CastToSbyte());
										return;
									}break;
								case System.TypeCode.Int16:
									{
										a_to_object = System.Enum.ToObject(a_to_type,a_from_jsonitem.CastToInt16());
										return;
									}break;
								case System.TypeCode.UInt16:
									{
										a_to_object = System.Enum.ToObject(a_to_type,a_from_jsonitem.CastToUint16());
										return;
									}break;
								case System.TypeCode.Int32:
									{
										a_to_object = System.Enum.ToObject(a_to_type,a_from_jsonitem.CastToInt32());
										return;
									}break;
								case System.TypeCode.UInt32:
									{
										a_to_object = System.Enum.ToObject(a_to_type,a_from_jsonitem.CastToUint32());
										return;
									}break;
								case System.TypeCode.Int64:
									{
										a_to_object = System.Enum.ToObject(a_to_type,a_from_jsonitem.CastToInt64());
										return;
									}break;
								case System.TypeCode.UInt64:
									{
										a_to_object = System.Enum.ToObject(a_to_type,a_from_jsonitem.CastToUint64());
										return;
									}break;
								}
							}
						}
					}break;
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//失敗。
			Tool.Assert(false);
		}
	}
}

