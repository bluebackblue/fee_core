using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 暗号。コルーチン。
*/


/** NCrypt
*/
namespace NCrypt
{
	/** OnCoroutine_CallBack
	*/
	public interface OnCoroutine_CallBack
	{
		/** [NFile.OnCoroutine_CallBack]コルーチン実行中。

		戻り値 == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress);
	}
}

