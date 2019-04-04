

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 深度。カメラ。
*/


/** Fee.Depth
*/
namespace Fee.Depth
{
	/** MonoBehaviour_Camera
	*/
	public class MonoBehaviour_Camera : UnityEngine.MonoBehaviour
	{
		/** mycamera
		*/
		public UnityEngine.Camera mycamera;

		/** material_depth_view
		*/
		private UnityEngine.Material material_depth_view;

		/** rendertexture_depth
		*/
		private UnityEngine.RenderTexture rendertexture_depth;

		/** ブレンド比率。
		*/
		[UnityEngine.SerializeField,UnityEngine.Range(0.0f,1.0f)]
		private float rate_blend;

		/** 初期化。
		*/
		public void Initialize()
		{
			//カメラ取得。
			this.mycamera = this.GetComponent<UnityEngine.Camera>();

			//マテリアル読み込み。
			this.material_depth_view = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_DEPTHVIEW);

			//比率。
			this.rate_blend = 1.0f;

			//rendertexture_depth
			this.rendertexture_depth = null;
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

		/** 深度テクスチャー。設定。
		*/
		public void SetDepthTexture(UnityEngine.RenderTexture a_rendertexture_depth)
		{
			this.rendertexture_depth = a_rendertexture_depth;
		}

		/** ブレンド比率。設定。
		*/
		public void SetBlendRate(float a_blend)
		{
			this.rate_blend = a_blend;

			if(this.rate_blend < 0.0f){
				this.rate_blend = 0.0f;
			}else if(this.rate_blend > 1.0f){
				this.rate_blend = 1.0f;
			}
		}

		/** ブレンド比率。取得。
		*/
		public float GetBlendRate()
		{
			return this.rate_blend;
		}

		/** OnRenderImage
		*/
		private void OnRenderImage(UnityEngine.RenderTexture a_source,UnityEngine.RenderTexture a_dest)
		{
			try{
				this.material_depth_view.SetFloat("rate_blend",this.rate_blend);

				if(this.rendertexture_depth != null){
					this.material_depth_view.SetInt("camera_depth_flag",0);
					this.material_depth_view.SetTexture("texture_depth",this.rendertexture_depth);
				}else{
					this.material_depth_view.SetInt("camera_depth_flag",1);
				}
				UnityEngine.Graphics.Blit(a_source,a_dest,this.material_depth_view);
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

