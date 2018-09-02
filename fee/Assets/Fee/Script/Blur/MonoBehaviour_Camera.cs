using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ブラー。カメラ。
*/


/** NBlur
*/
namespace NBlur
{
	/** MonoBehaviour_Camera
	*/
	public class MonoBehaviour_Camera : MonoBehaviour
	{
		/** mycamera
		*/
		public Camera mycamera;

		/** material_blur
		*/
		private Material material_blur;

		/** 初期化。
		*/
		public void Initialize()
		{
			//カメラ取得。
			this.mycamera = this.GetComponent<Camera>();

			//マテリアル読み込み。
			this.material_blur = Resources.Load<Material>(Config.MATERIAL_NAME_BLUR);
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** OnRenderImage
		*/
		private void OnRenderImage(RenderTexture a_source,RenderTexture a_dest)
		{
			UnityEngine.Graphics.Blit(a_source,a_dest,this.material_blur);
		}
	}
}

