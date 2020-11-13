

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

		/** look
		*/
		public UnityEngine.Camera look_camera;
		public UnityEngine.Transform look_transform;

		/** OnWillRenderObject
		*/
		public void OnWillRenderObject()
		{
			if(this.look_camera == UnityEngine.Camera.current){
				this.Render();
			}
		}

		/** Render
		*/
		public void Render()
		{
			this.mirror_camera.SetMirrorPlane(this.transform.rotation,this.transform.position);
			this.mirror_camera.Calc(this.look_camera,this.look_transform);
			this.mirror_camera.Render();
		}
	}
}

