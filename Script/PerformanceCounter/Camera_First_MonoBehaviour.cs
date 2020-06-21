

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パフォーマンスカウンター。カメラ。
*/


/** Fee.PerformanceCounter
*/
namespace Fee.PerformanceCounter
{
	/** Camera_First_MonoBehaviour
	*/
	[UnityEngine.DefaultExecutionOrder(Config.EXECUTIONORDER_LAST)]
	public class Camera_First_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			if(PerformanceCounter.GetInstance() != null){
				PerformanceCounter.GetInstance().Unity_FixedUpdate_First();
			}
		}

		/** Update
		*/
		private void Update()
		{
			if(PerformanceCounter.GetInstance() != null){
				PerformanceCounter.GetInstance().Unity_Update_First();
			}
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
			if(PerformanceCounter.GetInstance() != null){
				PerformanceCounter.GetInstance().Unity_LateUpdate_First();
			}
		}

		/** OnPreRender
		*/
		private void OnPreRender()
		{
			if(PerformanceCounter.GetInstance() != null){
				PerformanceCounter.GetInstance().Unity_Render_First();
			}
		}
	}
}

