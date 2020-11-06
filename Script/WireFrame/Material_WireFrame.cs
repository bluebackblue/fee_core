

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ワイヤーフレーム。
*/


/** Fee.WireFrame
*/
namespace Fee.WireFrame
{
	/** Material_WireFrame
	*/
	public class Material_WireFrame
	{
		/** material
		*/
		public UnityEngine.Material material;

		/** limit
		*/
		public float limit;

		/** color
		*/
		public UnityEngine.Color color;

		/** constructor
		*/
		public Material_WireFrame(UnityEngine.Material a_material)
		{
			//material
			this.material = a_material;

			//limit
			this.limit = Config.DEFAULT_LIMIT;
			this.material.SetFloat("limit",this.limit);

			//color
			this.color = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
			this.material.SetColor("_Color",this.color);
		}

		/** SetLimit
		*/
		public void SetLimit(float a_value)
		{
			if(this.limit != a_value){
				this.limit = a_value;
				this.material.SetFloat("limit",this.limit);
			}
		}

		/** GetLimit
		*/
		public float GetLimit()
		{
			return this.limit;
		}

		/** SetColor
		*/
		public void SetColor(in UnityEngine.Color a_color)
		{
			if(this.color != a_color){
				this.color = a_color;
				this.material.SetColor("_Color",this.color);
			}
		}

		/** GetLimit
		*/
		public UnityEngine.Color GetColor()
		{
			return this.color;
		}
	}
}

