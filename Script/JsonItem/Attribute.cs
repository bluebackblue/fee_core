

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 属性。
*/


/** Fee.JsonItem
*/
namespace Fee.JsonItem
{
	/** コンバート時に除外する。
	*/
	public class Ignore : System.Attribute
	{
	}

	/** Enumを文字データとして出力する。
	*/
	public class EnumString : System.Attribute
	{
	}

	/** Enumを数値として出力する。
	*/
	public class EnumInt : System.Attribute
	{
	}
}

