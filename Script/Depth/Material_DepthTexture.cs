

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief デプス。
*/


/** Fee.Depth
*/
namespace Fee.Depth
{
	/** Material_DepthTexture
	*/
	public class Material_DepthTexture
	{
		/** material
		*/
		public UnityEngine.Material material;

		/** ブレンド率。
		*/
		public float blendrate;

		/** texture_depth
		*/
		public UnityEngine.Texture texture_depth;

		/** near
		*/
		public float near;

		/** far
		*/
		public float far;

		/** constructor
		*/
		public Material_DepthTexture(UnityEngine.Material a_material)
		{
			//material
			this.material = a_material;

			//blendrate
			this.blendrate = Config.DEFAULT_BLENDRATE;

			//texture_depth
			this.texture_depth = null;

			//near
			this.near = 1.0f;

			//far
			this.far = 1000.0f;
		}

		/** SetBlendRate
		*/
		public void SetBlendRate(float a_value)
		{
			this.blendrate = a_value;
		}

		/** GetBlendRate
		*/
		public float GetBlendRate()
		{
			return this.blendrate;
		}

		/** SetDepthTexture
		*/
		public void SetDepthTexture(UnityEngine.Texture a_texture)
		{
			this.texture_depth = a_texture;
		}

		/** GetDepthTexture
		*/
		public UnityEngine.Texture GetDepthTexture()
		{
			return this.texture_depth;
		}

		/** SetNear
		*/
		public void SetNear(float a_near)
		{
			this.near = a_near;
		}

		/** GetNear
		*/
		public float GetNear()
		{
			return this.near;
		}

		/** SetFar
		*/
		public void SetFar(float a_far)
		{
			this.far = a_far;
		}

		/** GetFar
		*/
		public float GetFar()
		{
			return this.far;
		}

		/** 適応。
		*/
		public void Apply()
		{
			//blendrate
			this.material.SetFloat("blendrate",this.blendrate);

			//texture_depth
			this.material.SetTexture("texture_depth",this.texture_depth);

			//zbufferparam
			if(UnityEngine.SystemInfo.usesReversedZBuffer == true){
				this.material.SetFloat("zbufferparam_x",(this.far / this.near - 1.0f));
				this.material.SetFloat("zbufferparam_y",1.0f);
			}else{
				this.material.SetFloat("zbufferparam_x",(1.0f - this.far / this.near));
				this.material.SetFloat("zbufferparam_y",this.far / this.near);
			}
		}
	}
}

