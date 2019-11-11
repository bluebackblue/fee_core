

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief マテリアル。プロパティー。
*/


/** Fee.Material
*/
namespace Fee.Material
{
	/** Property_Int
	*/
	public struct Property_Int
	{
		/** material_raw
		*/
		public UnityEngine.Material material_raw;

		/** property_id
		*/
		public int property_id;

		/** cache_value
		*/
		public int cache_value;

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
			this.cache_value = a_material_raw.GetInt(this.property_id);
		}

		/** 値。設定。
		*/
		public bool SetValue(int a_value)
		{
			bool t_change = false;

			if(this.cache_value != a_value){
				this.cache_value = a_value;
				this.material_raw.SetInt(this.property_id,this.cache_value);
				t_change = true;
			}

			return t_change;
		}

		/** 値。取得。
		*/
		public int GetValue()
		{
			return this.cache_value;
		}
	}
}

