using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダウンロード。データタイプ。
*/


/** NDownLoad
*/
namespace NDownLoad
{
	/** DataType
	*/
	public enum DataType
	{
		/** 未定義。
		*/
		None,

		/** エラー。
		*/
		Error,

		/** テキスト。
		*/
		Text,

		/** テクスチャー。
		*/
		Texture,

		/** アセットバンドル。
		*/
		AssetBundle,
	};
}

