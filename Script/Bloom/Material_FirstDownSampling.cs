

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
	/** Material_FirstDownSampling
	*/
	public class Material_FirstDownSampling
	{
		/** material
		*/
		public UnityEngine.Material material;

		/** 輝度抽出閾値。
		*/
		public float threshold;

		/** constructor
		*/
		public Material_FirstDownSampling(UnityEngine.Material a_material)
		{
			//material
			this.material = a_material;

			//threshold
			this.threshold = Config.DEFAULT_THRESHOLD;
		}

		/** 輝度抽出閾値。設定。
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

		/** 輝度抽出閾値。取得。
		*/
		public float GetThreshold()
		{
			return this.threshold;
		}

		/** 適応。
		*/
		public void Apply()
		{
			//threshold
			this.material.SetFloat("threshold",this.threshold);
		}
	}
}

