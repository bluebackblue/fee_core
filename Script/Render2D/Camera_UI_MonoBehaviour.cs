

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。ＵＩ描画カメラ。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Camera_UI_MonoBehaviour

		TODO:MonoBehaviour

	*/
	public class Camera_UI_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** index
		*/
		private int index;

		/** my_camera
		*/
		private UnityEngine.Camera my_camera;

		/** log
		*/
		#if(USE_DEF_FEE_DEBUGTOOL)
		public int log_text_start_index;
		public int log_text_end_index;
		public int log_inputfield_start_index;
		public int log_inputfield_end_index;
		#endif
		
		/** Awake

			AddComponent内から呼び出される。

		*/
		public void Awake()
		{
			//index
			this.index = -1;

			//my_camera
			this.my_camera = null;

			//log
			#if(USE_DEF_FEE_DEBUGTOOL)
			this.log_text_start_index = -1;
			this.log_text_end_index = -1;
			this.log_inputfield_start_index = -1;
			this.log_inputfield_end_index = -1;
			#endif
		}

		/** 初期化。
		*/
		public void Initialize(int a_index,UnityEngine.Camera a_my_camera,float a_camera_depth)
		{
			//index
			this.index = a_index;

			//my_camera
			this.my_camera = a_my_camera;
			this.my_camera.depth = a_camera_depth;
		}

		/** SetActive
		*/
		public void SetActive(bool a_flag)
		{
			this.my_camera.enabled = a_flag;
		}

		/** カメラデプス。設定。
		*/
		public void SetCameraDepth(float a_depth)
		{
			this.my_camera.depth = a_depth;
		}

		/** カメラデプス。取得。
		*/
		public float GetCameraDepth()
		{
			return this.my_camera.depth;
		}

		/** デプスクリアフラグ。設定。
		*/
		public void SetDepthFlagClear(bool a_flag)
		{
			if(a_flag == true){
				this.my_camera.clearFlags = UnityEngine.CameraClearFlags.Depth;
			}else{
				this.my_camera.clearFlags = UnityEngine.CameraClearFlags.Nothing;
			}
		}

		/** インデックス。取得。
		*/
		public int GetIndex()
		{
			return this.index;
		}
	}
}

