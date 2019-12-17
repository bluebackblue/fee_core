

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief マテリアル。テクスチャー。
*/


/** Fee.Material
*/
namespace Fee.Material
{
	/** Property_Texture
	*/
	public struct Property_Texture
	{
		/** material_raw
		*/
		public UnityEngine.Material material_raw;

		/** property_id
		*/
		public int property_id;

		/** cache_value
		*/
		public UnityEngine.Texture cache_value;

		/** 初期化。
		*/
		public void Initialize(UnityEngine.Material a_material_raw,string a_property_name)
		{
			Tool.Assert(a_material_raw != null);

			//material_raw
			this.material_raw = a_material_raw;

			//property_id
			this.property_id = UnityEngine.Shader.PropertyToID(a_property_name);

			//cache_value
			this.cache_value = a_material_raw.GetTexture(this.property_id);
		}

		/** 値。設定。
		*/
		public bool SetValue(UnityEngine.Texture a_value)
		{
			bool t_change = false;

			if(this.cache_value != a_value){
				this.cache_value = a_value;

				if(this.material_raw != null){
					this.material_raw.SetTexture(this.property_id,this.cache_value);
				}else{
					//未初期化。
					Tool.Assert(false);
				}


				t_change = true;
			}

			return t_change;
		}

		/** 値。取得。
		*/
		public UnityEngine.Texture GetValue()
		{
			return this.cache_value;
		}
	}
}

