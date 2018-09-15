using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief パフォーマンスカウンター。フレームデータ。
*/


/** NPerformanceCounter

	https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html

*/
namespace NPerformanceCounter
{
	/** FrameData
	*/
	public class FrameData
	{
		public float start_time;
		public float end_time;
	}
}

