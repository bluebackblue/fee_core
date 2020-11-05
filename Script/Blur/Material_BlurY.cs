

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ブラー。
*/


/** Fee.Blur
*/
namespace Fee.Blur
{
	/** Material_BlurY
	*/
	public class Material_BlurY
	{
		/** material
		*/
		public UnityEngine.Material material;

		/** ブレンド率。
		*/
		public float blendrate;

		/** texture_original
		*/
		public UnityEngine.Texture texture_original;

		/** constructor
		*/
		public Material_BlurY(UnityEngine.Material a_material)
		{
			//material
			this.material = a_material;

			//blendrate
			this.blendrate = Config.DEFAULT_BLENDRATE;

			//texture
			this.texture_original = null;
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
			//blendrate
			this.material.SetFloat("blendrate",this.blendrate);

			//texture_original
			this.material.SetTexture("texture_original",this.texture_original);
		}
	}
}

