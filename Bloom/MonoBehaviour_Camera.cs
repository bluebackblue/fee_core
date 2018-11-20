using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ブルーム。カメラ。
*/


/** NBloom
*/
namespace NBloom
{
	/** MonoBehaviour_Camera
	*/
	public class MonoBehaviour_Camera : MonoBehaviour
	{
		/** mycamera
		*/
		public Camera mycamera;

		/** material_bloom_firstdownsampling
		*/
		private Material material_bloom_firstdownsampling;

		/** material_bloom_downsampling
		*/
		private Material material_bloom_downsampling;

		/** material_bloom_upsampling
		*/
		private Material material_bloom_upsampling;

		/** material_bloom_lastupsampling
		*/
		private Material material_bloom_lastupsampling;

		/** 輝度抽出閾値。
		*/
		[SerializeField,Range(0.0f,1.0f)]
		private float threshold;

		/** 加算強度。
		*/
		[SerializeField,Range(0.0f,30.0f)]
		private float intensity;

		/** work_rendertexture
		*/
		private RenderTexture[] work_rendertexture;

		/** 初期化。
		*/
		public void Initialize()
		{
			//カメラ取得。
			this.mycamera = this.GetComponent<Camera>();

			//マテリアル読み込み。
			this.material_bloom_firstdownsampling = Resources.Load<Material>(Config.MATERIAL_NAME_FIRSTDOWNSAMPLING);
			this.material_bloom_downsampling = Resources.Load<Material>(Config.MATERIAL_NAME_DOWNSAMPLING);
			this.material_bloom_upsampling = Resources.Load<Material>(Config.MATERIAL_NAME_UPSAMPLING);
			this.material_bloom_lastupsampling = Resources.Load<Material>(Config.MATERIAL_NAME_LASTUPSAMPLING);

			//閾値。
			this.threshold = Config.DEFAULT_THRESHOLD;

			//加算強度。
			this.intensity = Config.DEFAULT_INTENSITY;

			//レンダーテクスチャー。
			int t_downsampling_count = 3;
			this.work_rendertexture = new RenderTexture[t_downsampling_count];
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** カメラデプス。設定。
		*/
		public void SetCameraDepth(float a_depth)
		{
			this.mycamera.depth = a_depth;
		}

		/** 閾値。設定。
		*/
		public void SetThreshold(float a_threshold)
		{
			this.threshold = a_threshold;

			if(this.threshold < 0.0f){
				this.threshold = 0.0f;
			}else if(this.threshold > 1.0f){
				this.threshold = 1.0f;
			}
		}

		/** 加算強度。設定。
		*/
		public void SetIntensity(float a_intensity)
		{
			this.intensity = a_intensity;

			if(this.intensity < 0.0f){
				this.intensity = 0.0f;
			}
		}

		/** OnRenderImage
		*/
		private void OnRenderImage(RenderTexture a_source,RenderTexture a_dest)
		{
			//レンダリングテクスチャー作成。
			{
				int t_width = a_source.width;
				int t_height = a_source.height;
				for(int ii=0;ii<this.work_rendertexture.Length;ii++){
					t_width /= 2;
					t_height /= 2;
					this.work_rendertexture[ii] = RenderTexture.GetTemporary(t_width,t_height,0,a_source.format,RenderTextureReadWrite.Default);
				}
			}

			try{
				//初回ダウンサンプリング（輝度抽出）。
				this.material_bloom_firstdownsampling.SetFloat("threshold",this.threshold);

				//最終アップサンプリング（加算）。
				this.material_bloom_lastupsampling.SetFloat("intensity",this.intensity);
				this.material_bloom_lastupsampling.SetTexture("texture_original",a_source);

				//ダウンサンプリング。
				for(int ii=0;ii<this.work_rendertexture.Length;ii++) {
					if(ii==0){
						RenderTexture t_to = this.work_rendertexture[ii];
						RenderTexture t_from = a_source;

						//初回ダウンサンプリング（輝度抽出）。
						this.work_rendertexture[ii].DiscardContents(true,true);
						UnityEngine.Graphics.Blit(t_from,t_to,this.material_bloom_firstdownsampling);
					}else{
						RenderTexture t_to = this.work_rendertexture[ii];
						RenderTexture t_from = this.work_rendertexture[ii - 1];

						//ダウンサンプリング。
						this.work_rendertexture[ii].DiscardContents(true,true);
						UnityEngine.Graphics.Blit(t_from,t_to,this.material_bloom_downsampling);
					}
				}

				//アップサンプリング。
				for(int ii=0;ii<(this.work_rendertexture.Length - 1);ii++){
					RenderTexture t_to = this.work_rendertexture[this.work_rendertexture.Length - ii - 2];
					RenderTexture t_from = this.work_rendertexture[this.work_rendertexture.Length - ii - 1];

					//アップサンプリング。
					this.work_rendertexture[this.work_rendertexture.Length - ii - 2].MarkRestoreExpected();
					UnityEngine.Graphics.Blit(t_from,t_to,this.material_bloom_upsampling);
				}

				//最終アップサンプリング（加算）。
				Graphics.Blit(this.work_rendertexture[0],a_dest,this.material_bloom_lastupsampling);
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			//レンダーテクスチャー解放。
			for(int ii=0;ii<this.work_rendertexture.Length;ii++){
				if(this.work_rendertexture[ii] != null){
					RenderTexture.ReleaseTemporary(this.work_rendertexture[ii]);
					this.work_rendertexture[ii] = null;
				}
			}
		}
	}
}

