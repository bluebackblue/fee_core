using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。スプライト。
*/


/** Render2D
*/
namespace NRender2D
{
	/** Sprite2D_Param
	*/
	public struct Sprite2D_Param
	{
		/** テクスチャ。
		*/
		private Texture2D texture;

		/** 色。
		*/
		private Color color;

		/** マテリアルタイプ。
		*/
		private Render2D.MaterialType materialtype;

		/** 初期化。
		*/
		public void Initialize()
		{
			//テクスチャー。
			this.texture = null;

			//色。
			this.color = Color.white;

			//マテリアルタイプ。
			this.materialtype = Config.DEFALUT_SPRITE_MATERIALTYPE;
		}

		/** テクスチャー。設定。
		*/
		public void SetTexture(Texture2D a_texture)
		{
			this.texture = a_texture;
		}

		/** テクスチャー。取得。
		*/
		public Texture2D GetTexture()
		{
			return this.texture;
		}

		/** 色。設定。
		*/
		public void SetColor(ref Color a_color)
		{
			this.color = a_color;
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.color.r = a_r;
			this.color.g = a_g;
			this.color.b = a_b;
			this.color.a = a_a;
		}

		/** 色。取得。
		*/
		public Color GetColor()
		{
			return this.color;
		}

		/** マテリアルタイプ。設定。
		*/
		public void SetMaterialType(Render2D.MaterialType a_materialtype)
		{
			this.materialtype = a_materialtype;
		}

		/** マテリアルタイプ。取得。
		*/
		public Render2D.MaterialType GetMaterialType()
		{
			return this.materialtype;
		}

		/** 削除。
		*/
		public void Delete()
		{
			//テキスチャ。
			this.texture = null;
		}
	}
}

