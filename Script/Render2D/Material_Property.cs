

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。マテリアルプロパティー。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Material_Property_Int
	*/
	public struct Material_Property_Int
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

	/** Material_Property_Float
	*/
	public struct Material_Property_Float
	{
		/** material_raw
		*/
		public UnityEngine.Material material_raw;

		/** property_id
		*/
		public int property_id;

		/** cache_value
		*/
		public float cache_value;

		/** 初期化。
		*/
		public void Initialize(UnityEngine.Material a_material_raw,string a_property_name)
		{
			//material_raw
			this.material_raw = a_material_raw;

			//property_id
			this.property_id = UnityEngine.Shader.PropertyToID(a_property_name);

			//cache_value
			this.cache_value = a_material_raw.GetFloat(this.property_id);
		}

		/** 値。設定。
		*/
		public bool SetValue(float a_value)
		{
			bool t_change = false;

			if(this.cache_value != a_value){
				this.cache_value = a_value;
				this.material_raw.SetFloat(this.property_id,this.cache_value);
				t_change = true;
			}

			return t_change;
		}

		/** 値。取得。
		*/
		public float GetValue()
		{
			return this.cache_value;
		}
	}

	/** Material_Property_Texture
	*/
	public struct Material_Property_Texture
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
				this.material_raw.SetTexture(this.property_id,this.cache_value);
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

