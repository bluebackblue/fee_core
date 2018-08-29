using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。ＵＩ描画カメラ。
*/


/** NRender2D
*/
namespace NRender2D
{
	/** MonoBehaviour_Camera_UI
	*/
	public class MonoBehaviour_Camera_UI : MonoBehaviour
	{
		/** index
		*/
		public int index;

		/** log
		*/
		public int log_text_start_index;
		public int log_text_end_index;
		public int log_inputfield_start_index;
		public int log_inputfield_end_index;

		/** mycamera
		*/
		public Camera mycamera;

		/** cameradepth
		*/
		public float cameradepth;

		/** constructor
		*/
		public MonoBehaviour_Camera_UI()
		{
			//index
			this.index = -1;

			//log
			this.log_text_start_index = -1;
			this.log_text_end_index = -1;
			this.log_inputfield_start_index = -1;
			this.log_inputfield_end_index = -1;

			//mycamera
			this.mycamera = null;

			//cameradepth
			this.cameradepth = 0.0f;
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
	}
}

