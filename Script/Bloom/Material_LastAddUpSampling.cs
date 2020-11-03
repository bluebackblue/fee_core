

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ブルーム。
*/


/** Fee.Bloom
*/
namespace Fee.Bloom
{
	/** Material_LastAddUpSampling
	*/
	public class Material_LastAddUpSampling
	{
		/** material
		*/
		public UnityEngine.Material material;

		/** 加算強度。
		*/
		public float intensity;

		/** texture_original
		*/
		private UnityEngine.Texture texture_original;

		/** constructor
		*/
		public Material_LastAddUpSampling(UnityEngine.Material a_material)
		{
			//material
			this.material = a_material;

			//intensity
			this.intensity = Config.DEFAULT_INTENSITY;

			//texture_original
			this.texture_original = null;
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

		/** SetOriginalTexture
		*/
		public void SetOriginalTexture(UnityEngine.Texture a_texture)
		{
			this.texture_original = a_texture;
		}

		/** GetOriginalTexture
		*/
		public UnityEngine.Texture GetOriginalTexture()
		{
			return this.texture_original;
		}

		/** 適応。
		*/
		public void Apply()
		{
			//threshold
			this.material.SetFloat("intensity",this.intensity);

			//texture_original
			this.material.SetTexture("texture_original",this.texture_original);
		}
	}
}

