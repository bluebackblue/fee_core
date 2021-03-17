

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。トランスフォーム。
*/


/** Fee.EditorTool
*/
namespace Fee.EditorTool
{
	/** DebugTransform_MonoBehaviour
	*/
	#if(UNITY_EDITOR)
	public class DebugTransform_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** forward
		*/
		public UnityEngine.Vector3 forward;

		/** 更新。
		*/
		public void Update()
		{
			this.forward = this.gameObject.transform.rotation * UnityEngine.Vector3.forward;
		}
	}
	#endif
}

