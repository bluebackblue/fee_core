	

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
	/** Sprite2D_Mapping
	*/
	public class Sprite2D_Mapping
	{
		/** sprite
		*/
		public Fee.Render2D.Sprite2D sprite;

		/** constructor
		*/
		public Sprite2D_Mapping()
		{
			this.sprite = null;
		}

		/** constructor
		*/
		public Sprite2D_Mapping(Fee.Render2D.Sprite2D a_sprite)
		{
			this.sprite = a_sprite;
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureX(float a_texture_x)
		{
			this.sprite.SetTextureX(a_texture_x);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureY(float a_texture_y)
		{
			this.sprite.SetTextureY(a_texture_y);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureW(float a_texture_w)
		{
			this.sprite.SetTextureW(a_texture_w);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureH(float a_texture_h)
		{
			this.sprite.SetTextureH(a_texture_h);
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureX()
		{
			return this.sprite.GetTextureX();
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureY()
		{
			return this.sprite.GetTextureY();
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureW()
		{
			return this.sprite.GetTextureW();
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureH()
		{
			return this.sprite.GetTextureH();
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.sprite.SetX(a_x);
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.sprite.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.sprite.SetXY(a_x,a_y);
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.sprite.SetW(a_w);
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.sprite.SetH(a_h);
		}

		/** 矩形。取得。
		*/
		public int GetX()
		{
			return this.sprite.GetX();
		}

		/** 矩形。取得。
		*/
		public int GetY()
		{
			return this.sprite.GetY();
		}

		/** 矩形。取得。
		*/
		public int GetW()
		{
			return this.sprite.GetW();
		}

		/** 矩形。取得。
		*/
		public int GetH()
		{
			return this.sprite.GetH();
		}

		/** 矩形。矩形。設定。
		*/
		public void SetWH(int a_w,int a_h)
		{
			this.sprite.SetWH(a_w,a_h);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.sprite.SetTextureRect(in a_texture_rect);
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(float a_texture_x,float a_texture_y,float a_texture_w,float a_texture_h)
		{
			this.sprite.SetTextureRect(a_texture_x,a_texture_y,a_texture_w,a_texture_h);
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			this.sprite.SetRect(in a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.sprite.SetRect(a_x,a_y,a_w,a_h);
		}

		/** 回転。設定。
		*/
		public void SetRotate(bool a_flag)
		{
			this.sprite.SetRotate(a_flag);
		}

		/** 回転。取得。
		*/
		public bool IsRotate()
		{
			return this.sprite.IsRotate();
		}

		/** 中心。設定。
		*/
		public void SetCenter(int a_global_x,int a_global_y)
		{
			this.sprite.SetCenter(a_global_x,a_global_y);
		}

		/** 中心。取得。
		*/
		public int GetCenterX()
		{
			return this.sprite.GetCenterX();
		}

		/** 中心。取得。
		*/
		public int GetCenterY()
		{
			return this.sprite.GetCenterY();
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(float a_euler_x,float a_euler_y,float a_euler_z)
		{
			this.sprite.SetQuaternion(a_euler_x,a_euler_y,a_euler_z);
		}

		/** クォータニオン。設定。
		*/
		public void SetQuaternion(in UnityEngine.Quaternion a_quaternion)
		{
			this.sprite.SetQuaternion(in a_quaternion);
		}

		/** クォータニオン。取得。
		*/
		public UnityEngine.Quaternion GetQuaternion()
		{
			return this.sprite.GetQuaternion();
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			this.sprite.SetVisible(a_flag);
		}

		/** 表示。取得。
		*/
		public bool IsVisible()
		{
			return this.sprite.IsVisible();
		}

		/** 描画プライオリティ。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			this.sprite.SetDrawPriority(a_drawpriority);
		}

		/** 描画プライオリティ。取得。
		*/
		public long GetDrawPriority()
		{
			return this.sprite.GetDrawPriority();
		}

		/** テクスチャ。設定。
		*/
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			this.sprite.SetTexture(a_texture);
		}

		/** テクスチャ。取得。
		*/
		public UnityEngine.Texture GetTexture()
		{
			return this.sprite.GetTexture();
		}

		/** 色。設定。
		*/
		public void SetColor(in UnityEngine.Color a_color)
		{
			this.sprite.SetColor(in a_color);
		}

		/** 色。設定。
		*/
		public void SetColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** 色。取得。
		*/
		public UnityEngine.Color GetColor()
		{
			return this.sprite.GetColor();
		}

		/** マテリアルタイプ。設定。
		*/
		public void SetMaterialType(Render2D.MaterialType a_materialtype)
		{
			this.sprite.SetMaterialType(a_materialtype);
		}

		/** マテリアルタイプ。取得。
		*/
		public Render2D.MaterialType GetMaterialType()
		{
			return this.sprite.GetMaterialType();
		}
	}

}

