using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。マテリアルリスト。
*/


/** Render2D
*/
namespace NRender2D
{
	/** MaterialList
	*/
	public class MaterialList
	{
		/** list
		*/
		private Material[] list;

		/** material_ui_text
		*/
		private Material material_ui_text;

		/** material_ui_image
		*/
		private Material material_ui_image;

		/** constructor
		*/
		public MaterialList()
		{
			//list
			this.list = new Material[Config.MATERIAL_NAME.Length];
			for(int ii=0;ii<this.list.Length;ii++){
				Material t_material = Resources.Load<Material>(Config.MATERIAL_NAME[ii]);
				if(t_material != null){
					this.list[ii] = new Material(t_material);
				}
			}

			//material_ui_text
			this.material_ui_text = Resources.Load<Material>(Config.MATERIAL_NAME_UITEXT);

			//material_ui_image
			this.material_ui_image = Resources.Load<Material>(Config.MATERIAL_NAME_UIIMAGE);
		}

		/** マテリアル。取得。
		*/
		public Material GetMaterial(Sprite2D a_sprite)
		{
			return this.list[(int)a_sprite.GetMaterialType()];
		}

		/** ＵＩテキストマテリアル。取得。
		*/
		public Material GetUiTextMaterial()
		{
			return this.material_ui_text;
		}

		/** ＵＩイメージマテリアル。取得。
		*/
		public Material GetUiImageMaterial()
		{
			return this.material_ui_image;
		}

		/** マテリアル。取得。
		*/
		/*
		public Material GetMaterial(Config.MaterialType a_material_type)
		{
			return this.list[(int)a_material_type];
		}
		*/
	}
}

