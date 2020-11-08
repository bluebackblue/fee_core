

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

		/** is_render
		*/
		private bool is_render = false;

		/** OnWillRenderObject
		*/
		public void OnWillRenderObject()
		{
			if(this.is_render == false){
				this.is_render = true;
				this.mirror_camera.SetMirrorPlane(this.transform.rotation,this.transform.position);
				this.mirror_camera.RenderFromOnWillRenderObject();
				this.is_render = false;
			}else{
				//ミラーカメラにミラーが写っている。
			}
		}
	}
}

