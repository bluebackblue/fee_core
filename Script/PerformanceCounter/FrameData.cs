

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
		/** startframe
		*/
		public float startframe;
		public float startframe_old;

		/** fixedupdate
		*/
		public float fixedupdate_first;
		public float fixedupdate_last;

		/** update
		*/
		public float update_first;
		public float update_last;

		/** lateupdate
		*/
		public float lateupdate_first;
		public float lateupdate_last;

		/** render
		*/
		public float render_first;
		public float render_last;

		/** constructor
		*/
		public FrameData()
		{
			this.startframe = UnityEngine.Time.realtimeSinceStartup;
			this.startframe_old = this.startframe;

			this.fixedupdate_first = this.startframe;
			this.fixedupdate_last = this.startframe;

			this.update_first = this.startframe;
			this.update_last = this.startframe;

			this.lateupdate_first = this.startframe;
			this.lateupdate_last = this.startframe;

			this.render_first = this.startframe;
			this.render_last = this.startframe;
		}

		/** 次のフレーム。
		*/
		public void NetFrame()
		{
			this.startframe_old = this.startframe;
			this.startframe = UnityEngine.Time.realtimeSinceStartup;

			this.fixedupdate_first = this.startframe;
			this.fixedupdate_last = this.startframe;

			this.update_first = this.startframe;
			this.update_last = this.startframe;

			this.lateupdate_first = this.startframe;
			this.lateupdate_last = this.startframe;

			this.render_first = this.startframe;
			this.render_last = this.startframe;
		}
	}
}

