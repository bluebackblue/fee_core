

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief デプス。
*/


/** Fee.Cloud
*/
namespace Fee.Cloud
{
	/** Material_VolumeCloud
	*/
	public class Material_VolumeCloud
	{
		/** material
		*/
		public UnityEngine.Material material;

		/** power
		*/
		public float power;

		/** noisescale
		*/
		public float noisescale;

		/** inv_scale
		*/
		public float inv_scale;

		/** noiseoffset
		*/
		public UnityEngine.Vector3 noiseoffset;

		/** color
		*/
		public UnityEngine.Color color;

		/** constructor
		*/
		public Material_VolumeCloud(UnityEngine.Material a_material)
		{
			//material
			this.material = a_material;

			/** power
			*/
			this.power = Config.DEFAULT_POWER;

			/** noisescale
			*/
			this.noisescale = Config.DEFAULT_NOISESCALE;

			/** inv_scale
			*/
			this.inv_scale = Config.DEFAULT_INV_SCALE;

			/** noiseoffset
			*/
			this.noiseoffset = UnityEngine.Vector3.zero;

			/** color
			*/
			this.color = Config.DEFAULT_COLOR;
		}

		/** SetPower
		*/
		public void SetPower(float a_value)
		{
			this.power = a_value;
		}

		/** GetPower
		*/
		public float GetPower()
		{
			return this.power;
		}

		/** SetNoiseScale
		*/
		public void SetNoiseScale(float a_value)
		{
			this.noisescale = a_value;
		}

		/** GetNoiseScale
		*/
		public float GetNoiseScale()
		{
			return this.noisescale;
		}

		/** SetInvScale
		*/
		public void SetInvScale(float a_value)
		{
			this.inv_scale = a_value;
		}

		/** GetInvScale
		*/
		public float GetInvScale()
		{
			return this.inv_scale;
		}

		/** SetNoiseOffset
		*/
		public void SetNoiseOffset(in UnityEngine.Vector3 a_value)
		{
			this.noiseoffset = a_value;
		}

		/** GetNoiseOffset
		*/
		public UnityEngine.Vector3 GetNoiseOffset()
		{
			return this.noiseoffset;
		}

		/** SetNoiseOffset
		*/
		public void SetColor(in UnityEngine.Color a_value)
		{
			this.color = a_value;
		}

		/** GetColor
		*/
		public UnityEngine.Color GetColor()
		{
			return this.color;
		}

		/** 適応。
		*/
		public void Apply()
		{
			//power
			this.material.SetFloat("power",this.power);

			//noisescale
			this.material.SetFloat("noisescale",this.noisescale);

			//inv_scale
			this.material.SetFloat("inv_scale",this.inv_scale);

			//noiseoffset
			this.material.SetVector("noiseoffset",this.noiseoffset);

			//color
			this.material.SetColor("_Color",this.color);
		}
	}
}

