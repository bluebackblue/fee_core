using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。ＧＬ描画カメラ。
*/


/** NRender2D
*/
namespace NRender2D
{
	/** MonoBehaviour_Camera_GL
	*/
	public class MonoBehaviour_Camera_GL : MonoBehaviour
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
		public Camera mycamera;

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
				this.mycamera.clearFlags = CameraClearFlags.Depth;
			}else{
				this.mycamera.clearFlags = CameraClearFlags.Nothing;
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
				Tool.LogError(t_exception);
			}
		}
	}
}

