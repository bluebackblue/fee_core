

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ミラー。
*/


/** Fee.Mirror
*/
namespace Fee.Mirror
{
	/** MirrorObject_MonoBehaviour
	*/
	public class MirrorObject_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** mirror_camera
		*/
		public MirrorCamera_MonoBehaviour mirror_camera;

		/** OnWillRenderObject
		*/
		public void OnWillRenderObject()
		{
			if(this.mirror_camera.target_camera == UnityEngine.Camera.current){
				this.mirror_camera.SetMirrorPlane(this.transform.rotation,this.transform.position);
				this.mirror_camera.Calc();
				this.mirror_camera.Render();
			}
		}
	}
}

