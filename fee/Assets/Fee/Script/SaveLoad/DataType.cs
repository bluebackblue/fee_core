using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief セーブロード。データタイプ。
*/


/** NSaveLoad
*/
namespace NSaveLoad
{
	/** DataType
	*/
	public enum DataType
	{
		/** 未定義。
		*/
		None,

		/** セーブ完了。
		*/
		SaveEnd,

		/** エラー。
		*/
		Error,

		/** テキスト。
		*/
		Text,

		/** テクスチャ。
		*/
		Texture,
	};
}

