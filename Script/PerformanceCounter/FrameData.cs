

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief パフォーマンスカウンター。フレームデータ。
*/


/** NNFee.PerformanceCounter

	https://docs.unity3d.com/ja/current/Manual/ExecutionOrder.html

*/
namespace Fee.PerformanceCounter
{
	/** FrameData
	*/
	public class FrameData
	{
		public float start_time;
		public float end_time;

		/** constructor
		*/
		public FrameData()
		{
			this.start_time = UnityEngine.Time.realtimeSinceStartup;
			this.end_time = UnityEngine.Time.realtimeSinceStartup;
		}
	}
}

