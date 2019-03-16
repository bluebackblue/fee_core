

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ブラー。カメラ。
*/


/** Fee.Blur
*/
namespace Fee.Blur
{
	/** MonoBehaviour_Camera
	*/
	public class MonoBehaviour_Camera : UnityEngine.MonoBehaviour
	{
		/** mycamera
		*/
		public UnityEngine.Camera mycamera;

		/** material_blur_x
		*/
		private UnityEngine.Material material_blur_x;

		/** material_blur_y
		*/
		private UnityEngine.Material material_blur_y;

		/** work_rendertexture
		*/
		private UnityEngine.RenderTexture work_rendertexture;

		/** 初期化。
		*/
		public void Initialize()
		{
			//カメラ取得。
			this.mycamera = this.GetComponent<UnityEngine.Camera>();

			//マテリアル読み込み。
			this.material_blur_x = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_BLURX);
			this.material_blur_y = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_BLURY);

			//レンダーテクスチャー。
			this.work_rendertexture = null;

			#if(UNITY_EDITOR)
			{
				float[] t_table = new float[8];
				float t_total = 0.0f;
				float t_dispersion = 4.0f;
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] = UnityEngine.Mathf.Exp(-0.5f * ((float)(ii*ii)) / t_dispersion);
					t_total += t_table[ii] * 2;
				}
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] /= t_total;
					Tool.Log("MonoBehaviour_Camera","param = " + t_table[ii].ToString());
				}
			}
			#endif
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

		/** OnRenderImage
		*/
		private void OnRenderImage(UnityEngine.RenderTexture a_source,UnityEngine.RenderTexture a_dest)
		{
			//レンダリングテクスチャー作成。
			this.work_rendertexture = UnityEngine.RenderTexture.GetTemporary(a_source.width/2,a_source.height/2,0,a_source.format,UnityEngine.RenderTextureReadWrite.Default);

			try{
				UnityEngine.Graphics.Blit(a_source,this.work_rendertexture,this.material_blur_x);
				UnityEngine.Graphics.Blit(this.work_rendertexture,a_dest,this.material_blur_y);
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			//レンダーテクスチャー解放。
			if(this.work_rendertexture != null){
				UnityEngine.RenderTexture.ReleaseTemporary(this.work_rendertexture);
				this.work_rendertexture = null;
			}
		}
	}
}

