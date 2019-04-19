

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ブルーム。カメラ。
*/


/** Fee.Bloom
*/
namespace Fee.Bloom
{
	/** MonoBehaviour_Camera
	*/
	public class MonoBehaviour_Camera : UnityEngine.MonoBehaviour
	{
		/** mycamera
		*/
		public UnityEngine.Camera mycamera;

		/** material_bloom_firstdownsampling
		*/
		private UnityEngine.Material material_bloom_firstdownsampling;

		/** material_bloom_downsampling
		*/
		private UnityEngine.Material material_bloom_downsampling;

		/** material_bloom_upsampling
		*/
		private UnityEngine.Material material_bloom_upsampling;

		/** material_bloom_lastupsampling
		*/
		private UnityEngine.Material material_bloom_lastupsampling;

		/** 輝度抽出閾値。
		*/
		[UnityEngine.SerializeField,UnityEngine.Range(0.0f,1.0f)]
		private float threshold;

		/** 加算強度。
		*/
		[UnityEngine.SerializeField,UnityEngine.Range(0.0f,30.0f)]
		private float intensity;

		/** work_rendertexture
		*/
		private UnityEngine.RenderTexture[] work_rendertexture;

		/** 初期化。
		*/
		public void Initialize()
		{
			//カメラ取得。
			this.mycamera = this.GetComponent<UnityEngine.Camera>();

			//マテリアル読み込み。
			this.material_bloom_firstdownsampling = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_FIRSTDOWNSAMPLING);
			this.material_bloom_downsampling = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_DOWNSAMPLING);
			this.material_bloom_upsampling = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_UPSAMPLING);
			this.material_bloom_lastupsampling = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_LASTUPSAMPLING);

			//閾値。
			this.threshold = Config.DEFAULT_THRESHOLD;

			//加算強度。
			this.intensity = Config.DEFAULT_INTENSITY;

			//レンダーテクスチャー。
			int t_downsampling_count = 3;
			this.work_rendertexture = new UnityEngine.RenderTexture[t_downsampling_count];
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** カメラ深度。設定。
		*/
		public void SetCameraDepth(float a_depth)
		{
			this.mycamera.depth = a_depth;
		}

		/** カメラ深度。取得。
		*/
		public float GetCameraDepth()
		{
			return this.mycamera.depth;
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

		/** 閾値。取得。
		*/
		public float GetThreshold()
		{
			return this.threshold;
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

		/** 加算強度。取得。
		*/
		public float GetIntensity()
		{
			return this.intensity;
		}

		/** OnRenderImage
		*/
		private void OnRenderImage(UnityEngine.RenderTexture a_source,UnityEngine.RenderTexture a_dest)
		{
			//レンダリングテクスチャー作成。
			{
				int t_width = a_source.width;
				int t_height = a_source.height;
				for(int ii=0;ii<this.work_rendertexture.Length;ii++){
					t_width /= 2;
					t_height /= 2;
					this.work_rendertexture[ii] = UnityEngine.RenderTexture.GetTemporary(t_width,t_height,0,a_source.format,UnityEngine.RenderTextureReadWrite.Default);
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
						UnityEngine.RenderTexture t_to = this.work_rendertexture[ii];
						UnityEngine.RenderTexture t_from = a_source;

						//初回ダウンサンプリング（輝度抽出）。
						UnityEngine.Graphics.Blit(t_from,t_to,this.material_bloom_firstdownsampling);
					}else{
						UnityEngine.RenderTexture t_to = this.work_rendertexture[ii];
						UnityEngine.RenderTexture t_from = this.work_rendertexture[ii - 1];

						//ダウンサンプリング。
						UnityEngine.Graphics.Blit(t_from,t_to,this.material_bloom_downsampling);
					}
				}

				//アップサンプリング。
				for(int ii=0;ii<(this.work_rendertexture.Length - 1);ii++){
					UnityEngine.RenderTexture t_to = this.work_rendertexture[this.work_rendertexture.Length - ii - 2];
					UnityEngine.RenderTexture t_from = this.work_rendertexture[this.work_rendertexture.Length - ii - 1];

					//アップサンプリング。
					this.work_rendertexture[this.work_rendertexture.Length - ii - 2].MarkRestoreExpected();
					UnityEngine.Graphics.Blit(t_from,t_to,this.material_bloom_upsampling);
				}

				//最終アップサンプリング（加算）。
				UnityEngine.Graphics.Blit(this.work_rendertexture[0],a_dest,this.material_bloom_lastupsampling);
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			//レンダーテクスチャー解放。
			for(int ii=0;ii<this.work_rendertexture.Length;ii++){
				if(this.work_rendertexture[ii] != null){
					UnityEngine.RenderTexture.ReleaseTemporary(this.work_rendertexture[ii]);
					this.work_rendertexture[ii] = null;
				}
			}
		}
	}
}

