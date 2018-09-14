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

		/** material_text
		*/
		private Material material_text;

		/** constructor
		*/
		public MaterialList()
		{
			//list
			this.list = new Material[Config.MATERIAL_STRING.Length];
			for(int ii=0;ii<this.list.Length;ii++){
				Material t_material = Resources.Load<Material>(Config.MATERIAL_STRING[ii]);
				if(t_material != null){
					this.list[ii] = new Material(t_material);
				}
			}

			//material_text
			this.material_text = Resources.Load<Material>(Config.MATERIAL_STRING_TEXT);
		}

		/** マテリアル。取得。
		*/
		public Material GetMaterial(Sprite2D a_sprite)
		{
			return this.list[(int)a_sprite.GetMaterialType()];
		}

		/** マテリアル。取得。
		*/
		public Material GetTextMaterial()
		{
			return this.material_text;
		}

		/** マテリアル。取得。
		*/
		public Material GetMaterial(Config.MaterialType a_material_type)
		{
			return this.list[(int)a_material_type];
		}
	}
}

