

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
	/** Sprite2D_Rect
	*/
	public struct Sprite2D_Rect
	{
		/** テクスチャ矩形。
		*/
		private	Fee.Geometry.Rect2D_R<float> texture_rect;

		/** 矩形。
		*/
		private Fee.Geometry.Rect2D_R<int> rect;

		/** 矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			bool t_change = false;

			if((this.rect.x != a_rect.x)||(this.rect.y != a_rect.y)||(this.rect.w != a_rect.w)||(this.rect.h != a_rect.h)){
				this.rect = a_rect;
				t_change = true;
			}

			return t_change;
		}

		/** テクスチャ矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			bool t_change = false;

			if((this.texture_rect.x != a_texture_rect.x)||(this.texture_rect.y != a_texture_rect.y)||(this.texture_rect.w != a_texture_rect.w)||(this.texture_rect.h != a_texture_rect.h)){
				this.texture_rect = a_texture_rect;
				t_change = true;
			}

			return t_change;
		}

		/** テクスチャ矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetTextureX(float a_texture_x)
		{
			bool t_change = false;

			if(this.texture_rect.x != a_texture_x){
				this.texture_rect.x = a_texture_x;
				t_change = true;
			}

			return t_change;
		}

		/** テクスチャ矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetTextureY(float a_texture_y)
		{
			bool t_change = false;

			if(this.texture_rect.y != a_texture_y){
				this.texture_rect.y = a_texture_y;
				t_change = true;
			}

			return t_change;
		}

		/** テクスチャ矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetTextureW(float a_texture_w)
		{
			bool t_change = false;

			if(this.texture_rect.w != a_texture_w){
				this.texture_rect.w = a_texture_w;
				t_change = true;
			}

			return t_change;
		}

		/** テクスチャ矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetTextureH(float a_texture_h)
		{
			bool t_change = false;

			if(this.texture_rect.h != a_texture_h){
				this.texture_rect.h = a_texture_h;
				t_change = true;
			}

			return t_change;
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureX()
		{
			return this.texture_rect.x;
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureY()
		{
			return this.texture_rect.y;
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureW()
		{
			return this.texture_rect.w;
		}

		/** テクスチャ矩形。設定。
		*/
		public float GetTextureH()
		{
			return this.texture_rect.h;
		}

		/** 矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetX(int a_x)
		{
			bool t_change = false;

			if(this.rect.x != a_x){
				this.rect.x = a_x;
				t_change = true;
			}

			return t_change;
		}

		/** 矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetY(int a_y)
		{
			bool t_change = false;

			if(this.rect.y != a_y){
				this.rect.y = a_y;
				t_change = true;
			}

			return t_change;
		}

		/** 矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetW(int a_w)
		{
			bool t_change = false;

			if(this.rect.w != a_w){
				this.rect.w = a_w;
				t_change = true;
			}

			return t_change;
		}

		/** 矩形。設定。

			return == true : 変更あり。

		*/
		public bool SetH(int a_h)
		{
			bool t_change = false;

			if(this.rect.h != a_h){
				this.rect.h = a_h;
				t_change = true;
			}

			return t_change;
		}

		/** 矩形。取得。
		*/
		public int GetX()
		{
			return this.rect.x;
		}

		/** 矩形。取得。
		*/
		public int GetY()
		{
			return this.rect.y;
		}

		/** 矩形。取得。
		*/
		public int GetW()
		{
			return this.rect.w;
		}

		/** 矩形。取得。
		*/
		public int GetH()
		{
			return this.rect.h;
		}
	}
}

