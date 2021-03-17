

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。マテリアルリスト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** MaterialList
	*/
	public class MaterialList
	{
		/** material_list
		*/
		private MaterialItem[] material_list;

		/** constructor
		*/
		public MaterialList()
		{
			//material_list
			this.material_list = new MaterialItem[(int)MaterialType.Max];

			for(int ii=0;ii<this.material_list.Length;ii++){
				this.material_list[ii] = null;
				ShaderItem t_shaderitem;
				if(Config.SHADER_LIST.TryGetValue((MaterialType)ii,out t_shaderitem) == true){
					UnityEngine.Shader t_shader = UnityEngine.Shader.Find(t_shaderitem.shader_name);
					UnityEngine.Material t_material = null;
					if(t_shader != null){
						t_material = new UnityEngine.Material(t_shader);
					}else{
						Tool.LogError("Fee_Render2D_MaterialList::Shader.Find",t_shaderitem.shader_name);
					}
					this.material_list[ii] = new MaterialItem(t_material,t_shaderitem.property_list);
				}
			}
		}

		/** マテリアルアイテム。設定。
		*/
		public void SetMaterial(MaterialType a_material_type,MaterialItem a_material)
		{
			this.material_list[(int)a_material_type] = a_material;
		}

		/** マテリアルアイテム。取得。
		*/
		public MaterialItem GetMaterialItem(MaterialType a_material_type)
		{
			return this.material_list[(int)a_material_type];
		}

		/** 削除。
		*/
		public void Delete()
		{
			if(this.material_list != null){
				for(int ii=0;ii<this.material_list.Length;ii++){
					if(this.material_list[ii] != null){
						this.material_list[ii].DestroyImmediate();
						this.material_list[ii] = null;
					}
				}
				this.material_list = null;
			}
		}
	}
}

