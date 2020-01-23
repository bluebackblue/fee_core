

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
	/** 最初の一文字からタイプを推測。
	*/
	public class ValueType_FirstCharValueType
	{
		/** Get
		*/
		public static ValueType Get(char a_first_char)
		{
			switch(a_first_char){
			case '"':
			case '\'':
				{
					return ValueType.StringData;
				}break;
			case '{':
				{
					return ValueType.AssociativeArray;
				}break;
			case '[':
				{
					return ValueType.IndexArray;
				}break;
			case '<':
				{
					return ValueType.BinaryData;
				}break;
			case 't':
			case 'T':
				{
					return ValueType.Calc_BoolDataTrue;
				}break;
			case 'f':
			case 'F':
				{
					return ValueType.Calc_BoolDataFalse;
				}break;
			case '-':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				{
					return ValueType.Calc_UnknownNumber;
				}break;
			case 'n':
				{
					return ValueType.Null;
				}break;
			default:
				{
					//不明。
					Tool.Assert(false);
					return ValueType.Null;
				}break;
			}
		}
	}
}

