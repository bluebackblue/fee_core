

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。ＧＬ描画カメラ。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** MonoBehaviour_Camera_GL
	*/
	public class MonoBehaviour_Camera_GL : UnityEngine.MonoBehaviour
	{
		/** index
		*/
		public int index;

		/** log
		*/
		public int log_start_index;
		public int log_end_index;

		/** mycamera
		*/
		public UnityEngine.Camera mycamera;

		/** cameradepth
		*/
		public float cameradepth;

		/** constructor
		*/
		public MonoBehaviour_Camera_GL()
		{
			//index
			this.index = -1;

			//log
			this.log_start_index = -1;
			this.log_end_index = -1;

			//camera
			this.mycamera = null;

			//cameradepth
			this.cameradepth = 0.0f;
		}

		/** SetActive
		*/
		public void SetActive(bool a_flag)
		{
			this.mycamera.enabled = a_flag;
		}

		/** カメラデプス。設定。
		*/
		public void SetDepth(float a_depth)
		{
			this.mycamera.depth = a_depth;
		}

		/**  デプスクリアーの設定。
		*/
		public void SetDepthClear(bool a_flag)
		{
			if(a_flag == true){
				this.mycamera.clearFlags = UnityEngine.CameraClearFlags.Depth;
			}else{
				this.mycamera.clearFlags = UnityEngine.CameraClearFlags.Nothing;
			}
		}

		/** ＧＬ描画。カメラがシーンのレンダリングを完了した後に呼び出されます。
		*/
		private void OnPostRender()
		{
			try{
				if(Render2D.GetInstance() != null){
					Render2D.GetInstance().Draw_GL(this.index);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

