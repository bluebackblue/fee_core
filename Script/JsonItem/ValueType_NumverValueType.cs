

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
	public class ValueType_NumverValueType
	{
		/** 整数チェック。

			System.Text.RegularExpressions.Regex.IsMatch(a_string,"^[\\+\\-]?[0-9]+\\.[0-9]+$");

		*/
		public static ValueType GetNumberValueType(string a_string)
		{
			//サフィックス。
			{
				switch(a_string[a_string.Length - 1]){
				case 'm':
				case 'M':
					{
						return ValueType.DecimalNumber;
					}break;
				case 'f':
				case 'F':
					{
						return ValueType.FloatingNumber;
					}break;
				case 'l':
				case 'L':
					{
						if(a_string[0] == '-'){
							return ValueType.SignedNumber;
						}else{
							return ValueType.UnsignedNumber;
						}
					}break;
				}
			}

			for(int ii=0;ii<a_string.Length;ii++){
				if(a_string[ii] == Config.FLOATING_SEPARATOR){
					return ValueType.FloatingNumber;
				}
			}

			if(a_string[0] == '-'){
				return ValueType.SignedNumber;
			}

			return ValueType.UnsignedNumber;
		}
	}
}

