using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮ。タイプ。
*/


/** NJsonItem
*/
namespace NJsonItem
{
	/** ValueType
	*/
	public enum ValueType
	{
		None = 0,
		
		StringData,				//文字データ。
		AssociativeArray,		//連想配列。
		IndexArray,				//インデックス配列。
		IntegerNumber,			//整数。
		FloatingNumber,			//少数。
		BoolData,				//真偽データ。3
		BinaryData,				//バイナリデータ。
		
		//中間計算用。
		Calc_UnknownNumber,		//数値（少数/整数）。
		Calc_BoolDataTrue,		//真。
		Calc_BoolDataFalse,		//偽。
	}
}

