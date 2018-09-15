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
		private Material material_blur_x;
		private Material material_blur_y;

		/** work_rendertexture
		*/
		private RenderTexture work_rendertexture;

		/** 初期化。
		*/
		public void Initialize()
		{
			//カメラ取得。
			this.mycamera = this.GetComponent<Camera>();

			//マテリアル読み込み。
			this.material_blur_x = Resources.Load<Material>(Config.MATERIAL_NAME_BLURX);
			this.material_blur_y = Resources.Load<Material>(Config.MATERIAL_NAME_BLURY);

			//work_rendertexture
			this.work_rendertexture = null;

			{
				float[] t_table = new float[8];
				float t_total = 0.0f;
				float t_dispersion = 4.0f;
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] = Mathf.Exp(-0.5f * ((float)(ii*ii)) / t_dispersion);
					t_total += t_table[ii] * 2;
				}
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] /= t_total;
				}
			}
		}

		/** 削除。
		*/
		public void Delete()
		{
			if(this.work_rendertexture != null){
				this.work_rendertexture.Release();
				this.work_rendertexture = null;
			}
		}

		/** カメラデプス。設定。
		*/
		public void SetCameraDepth(float a_depth)
		{
			this.mycamera.depth = a_depth;
		}

		/** OnRenderImage
		*/
		private void OnRenderImage(RenderTexture a_source,RenderTexture a_dest)
		{
			//レンダリングテクスチャ。
			if(this.work_rendertexture == null){
				this.work_rendertexture = new RenderTexture(a_source.width / 2,a_source.height / 2,0,a_source.format);
			}

			if(this.work_rendertexture != null){
				this.work_rendertexture.DiscardContents();
			}

			UnityEngine.Graphics.Blit(a_source,this.work_rendertexture,this.material_blur_x);

			/*
			いくつかのプラットフォームでは RenderTexture オブジェクトの現在のコンテンツが 必要でないタイミングを使用することはパフォーマンスに良い影響を与えます。 
			テクスチャを再利用したときに一種類のメモリから別のものに複製することを回避できます。 
			Xbox 360 および多くのモバイル GPU でこのメリットがあります。 
			カラーバッファおよびデプスバッファはデフォルトでは無視されますが、どちらも個別に任意の boolean 引数を使用して選択できます。
			discardColor	カラーバッファを無視するか
			discardDepth	デプスバッファを無視するか
			*/
			//this.work_rendertexture.DiscardContents(true,true);

			if(a_dest != null){
				UnityEngine.Graphics.Blit(this.work_rendertexture,a_dest,this.material_blur_y);
			}
		}
	}
}

