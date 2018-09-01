using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief イベントプレート。コールバック。
*/


/** NEventPlate
*/
namespace NEventPlate
{
	/** OnOverCallBack_Base
	*/
	public interface OnOverCallBack_Base
	{
		/** イベントプレートに入場。
		*/
		void OnOverEnter(int a_value);

		/** イベントプレートから退場。
		*/
		void OnOverLeave(int a_value);
	}
}

