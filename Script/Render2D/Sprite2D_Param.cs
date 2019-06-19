

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。スプライト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Sprite2D_Param
	*/
	public struct Sprite2D_Param
	{
		/** テクスチャ。
		*/
		private UnityEngine.Texture2D texture;

		/** 色。
		*/
		private UnityEngine.Color color;

		/** マテリアルタイプ。
		*/
		private Render2D.MaterialType materialtype;

		/** 初期化。
		*/
		public void Initialize()
		{
			//テクスチャ。
			this.texture = null;

			//色。
			this.color = UnityEngine.Color.white;

			//マテリアルタイプ。
			this.materialtype = Config.DEFALUT_SPRITE_MATERIALTYPE;
		}

		/** テクスチャ。設定。
		*/
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			this.texture = a_texture;
		}

		/** テクスチャ。取得。
		*/
		public UnityEngine.Texture2D GetTexture()
		{
			return this.texture;
		}

		/** 色。設定。
		*/
		public void SetColor(ref UnityEngine.Color a_color)
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
		public UnityEngine.Color GetColor()
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

