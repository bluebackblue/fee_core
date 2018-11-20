using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。タッチ。フェイズ。
*/


/** NInput
*/
namespace NInput
{
	/** Touch_Phase_Key_Base
	*/
	public interface Touch_Phase_Key_Base
	{
		/** [Touch_Phase_Key_Base]更新。
		*/
		void OnUpdate();

		/** [Touch_Phase_Key_Base]リストから削除。
		*/
		void OnRemove();
	}
}

