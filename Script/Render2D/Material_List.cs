

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
	/** Material_List
	*/
	public class Material_List
	{
		/** material_list
		*/
		private Material_Item[] material_list;

		/** material_ui_text
		*/
		private Material_Item material_ui_text;

		/** material_ui_image
		*/
		private Material_Item material_ui_image;

		/** constructor
		*/
		public Material_List()
		{
			//material_list
			this.material_list = new Material_Item[Config.MATERIAL_DATA.Length];
			for(int ii=0;ii<this.material_list.Length;ii++){
				UnityEngine.Material t_material = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_DATA[ii].resource_path);
				if(t_material != null){
					//複製。
					this.material_list[ii] = new Material_Item(t_material,in Config.MATERIAL_DATA[ii],true);
				}
			}

			//参照。
			this.material_ui_text = new Material_Item(UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_DATA_UITEXT.resource_path),Config.MATERIAL_DATA_UITEXT,false);

			//参照。
			this.material_ui_image = new Material_Item(UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_DATA_UIIMAGE.resource_path),Config.MATERIAL_DATA_UITEXT,false);
		}

		/** マテリアルアイテム。取得。
		*/
		public Material_Item GetMaterialItem(Render2D.MaterialType a_material_type)
		{
			return this.material_list[(int)a_material_type];
		}

		/** ＵＩテキストマテリアルアイテム。取得。
		*/
		public Material_Item GetUiTextMaterialItem()
		{
			return this.material_ui_text;
		}

		/** ＵＩイメージマテリアルアイテム。取得。
		*/
		public Material_Item GetUiImageMaterialItem()
		{
			return this.material_ui_image;
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

