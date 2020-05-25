

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
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
		public float startframe;
		public float startframe_old;
		public float fixedupdate;
		public float update;
		public float lateupdate;
		public float onprerender;
		public float onpostrender;

		/** constructor
		*/
		public FrameData()
		{
			this.startframe = UnityEngine.Time.realtimeSinceStartup;
			this.startframe_old = this.startframe;
			this.fixedupdate = this.startframe;
			this.update = this.startframe;
			this.lateupdate = this.startframe;
			this.onprerender = this.startframe;
			this.onpostrender = this.startframe;
		}

		/** 次のフレーム。
		*/
		public void NetFrame()
		{
			this.startframe_old = this.startframe;
			this.startframe = UnityEngine.Time.realtimeSinceStartup;

			this.fixedupdate = this.startframe;
			this.update = this.startframe;
			this.lateupdate = this.startframe;
			this.onprerender = this.startframe;
			this.onpostrender = this.startframe;
		}
	}
}

