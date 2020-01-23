

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮ。タイプ。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** JsonItemからコンバート先の型を決める
	*/
	public class ValueType_ConvertToType
	{
		/** Get
		*/
		public static System.Type Get(Fee.JsonItem.JsonItem a_jsonitem)
		{
			System.Type t_type = null;

			switch(a_jsonitem.GetValueType()){
			case ValueType.Null:
				{
					//値型が不明なので値型はSystem.Objectにする。
					t_type = typeof(System.Object);
				}break;
			case ValueType.AssociativeArray:
				{
					//値型が不明なので値型はSystem.Objectにする。
					t_type = typeof(System.Collections.Generic.Dictionary<string,System.Object>);
				}break;
			case ValueType.IndexArray:
				{
					//値型が不明なので値型はSystem.Objectにする。
					t_type = typeof(System.Collections.Generic.List<System.Object>);
				}break;
			case ValueType.StringData:
				{
					t_type = typeof(System.String);
				}break;
			case ValueType.SignedNumber:
				{
					t_type = JsonItem.GetSignedNumberType();
				}break;
			case ValueType.UnsignedNumber:
				{
					t_type = JsonItem.GetUnsignedNumberType();
				}break;
			case ValueType.FloatingNumber:
				{
					t_type = JsonItem.GetFloatingNumberType();
				}break;
			case ValueType.BoolData:
				{
					t_type = typeof(System.Boolean);
				}break;
			case ValueType.DecimalNumber:
				{
					t_type = typeof(System.Decimal);
				}break;
			case ValueType.BinaryData:
				{
					t_type = typeof(System.Collections.Generic.List<byte>);
				}break;
			default:
				{
					Tool.Assert(false);
				}break;
			}

			return t_type;
		}
	}
}

