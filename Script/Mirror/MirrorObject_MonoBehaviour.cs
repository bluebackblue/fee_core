

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

		/** s_inside_rendering
		*/
		private static bool s_inside_rendering = false;

		/** OnWillRenderObject
		*/
		public void OnWillRenderObject()
		{
			if(s_inside_rendering == false){
				s_inside_rendering = true;
				this.mirror_camera.SetMirrorPlane(this.transform.rotation,this.transform.position);
				this.mirror_camera.RenderFromOnWillRenderObject();
				s_inside_rendering = false;
			}
		}
	}
}

