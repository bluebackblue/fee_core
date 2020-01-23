

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
	/** JsonItemToObject_FromStringData
	*/
	public class JsonItemToObject_FromStringData
	{
		/** Convert
		*/
		public static void Convert(ref System.Object a_to_object,System.Type a_to_type,JsonItem a_from_jsonitem)
		{
			try{
				switch(a_to_type.FullName){
				case "System." + nameof(System.String):
					{
						a_to_object = a_from_jsonitem.GetStringData();
						return;
					}break;
				case "System." + nameof(System.Char):
					{
						char t_value;
						if(System.Char.TryParse(a_from_jsonitem.GetStringData(),out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.SByte):
					{
						System.SByte t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.AllowLeadingSign;
						if(System.SByte.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.Byte):
					{
						System.Byte t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.None;
						if(System.Byte.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.Int16):
					{
						System.Int16 t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.AllowLeadingSign;
						if(System.Int16.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.UInt16):
					{
						System.UInt16 t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.None;
						if(System.UInt16.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.Int32):
					{
						System.Int32 t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.AllowLeadingSign;
						if(System.Int32.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.UInt32):
					{
						System.UInt32 t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.None;
						if(System.UInt32.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.Int64):
					{
						System.Int64 t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.AllowLeadingSign;
						if(System.Int64.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.UInt64):
					{
						System.UInt64 t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.None;
						if(System.UInt64.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.Single):
					{
						System.Single t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint;
						if(System.Single.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.Double):
					{
						System.Double t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint;
						if(System.Double.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.Boolean):
					{
						System.Boolean t_value;
						if(System.Boolean.TryParse(a_from_jsonitem.GetStringData(),out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				case "System." + nameof(System.Decimal):
					{
						System.Decimal t_value;
						System.Globalization.NumberStyles t_style = System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint;
						if(System.Decimal.TryParse(a_from_jsonitem.GetStringData(),t_style,Config.CULTURE,out t_value) == true){
							a_to_object = t_value;
							return;
						}
					}break;
				default:
					{
						if(a_to_type.IsEnum == true){
							a_to_object = System.Enum.Parse(a_to_type,a_from_jsonitem.GetStringData());
							return;
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

