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
	/** Sprite2D_Rect
	*/
	public struct Sprite2D_Rect
	{
		/** テクスチャ矩形。
		*/
		private Rect2D_R<float> texture_rect;

		/** 矩形。
		*/
		private Rect2D_R<int> rect;

		/** 矩形。設定。
		*/
		public void SetRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.rect = a_rect;
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureRect(ref NRender2D.Rect2D_R<float> a_texture_rect)
		{
			this.texture_rect = a_texture_rect;
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureX(float a_texture_x)
		{
			this.texture_rect.x = a_texture_x;
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureY(float a_texture_y)
		{
			this.texture_rect.y = a_texture_y;
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureW(float a_texture_w)
		{
			this.texture_rect.w = a_texture_w;
		}

		/** テクスチャ矩形。設定。
		*/
		public void SetTextureH(float a_texture_h)
		{
			this.texture_rect.h = a_texture_h;
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
		*/
		public void SetX(int a_x)
		{
			this.rect.x = a_x;
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.rect.y = a_y;
		}

		/** 矩形。設定。
		*/
		public void SetW(int a_w)
		{
			this.rect.w = a_w;
		}

		/** 矩形。設定。
		*/
		public void SetH(int a_h)
		{
			this.rect.h = a_h;
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

