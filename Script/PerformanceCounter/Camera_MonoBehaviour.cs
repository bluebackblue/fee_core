

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
	/** Camera_MonoBehaviour
	*/
	public class Camera_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** Start
		*/
		private System.Collections.IEnumerator Start()
		{
			yield return PerformanceCounter.GetInstance().Unity_Start();
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			PerformanceCounter.GetInstance().Unity_FixedUpdate();
		}

		/** Update
		*/
		private void Update()
		{
			PerformanceCounter.GetInstance().Unity_Update();
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
			PerformanceCounter.GetInstance().Unity_LateUpdate();
		}

		/** OnPreRender
		*/
		private void OnPreRender()
		{
			PerformanceCounter.GetInstance().Unity_OnPreRender();
		}

		/** OnPostRender
		*/
		private void OnPostRender()
		{
			PerformanceCounter.GetInstance().Unity_OnPostRender();
		}
	}
}

