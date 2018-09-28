using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スライダー。
*/


/** NUi
*/
namespace NUi
{
	/** Slider
	*/
	public class Slider : Slider_Base
	{
		/** sprite
		*/
		private NUi.ClipSprite sprite_bg;

		/** sprite
		*/
		private NUi.ClipSprite sprite_value;

		/** constructor
		*/
		public Slider(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority,Slider_Base.CallBack_Change a_callback_change,int a_callback_change_index)
			:
			base(a_deleter,a_state,a_drawpriority,a_callback_change,a_callback_change_index)
		{
			//sprite_bg
			this.sprite_bg = new ClipSprite(this.deleter,null,a_drawpriority);
			this.sprite_bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite_bg.SetTexture(Texture2D.whiteTexture);
			this.sprite_bg.SetColor(0.0f,0.0f,0.0f,1.0f);

			//sprite_value
			this.sprite_value = new ClipSprite(this.deleter,null,a_drawpriority);
			this.sprite_value.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite_value.SetTexture(Texture2D.whiteTexture);
			this.sprite_value.SetColor(0.5f,1.0f,0.5f,1.0f);
		}

		/** コールバック。削除。
		*/
		protected override void OnDeleteCallBack()
		{
		}

		/** [Slider_Base]コールバック。矩形変更。
		*/
		protected override void OnChangeRect()
		{
			this.UpdateView();
		}

		/** コールバック。クリップ。設定。
		*/
		/*
		protected override void OnSetClipCallBack(bool a_flag)
		{
			this.sprite_bg.SetClip(a_flag);
		}
		*/

		/** コールバック。クリップ矩形。設定。
		*/
		/*
		protected override void OnSetClipRectCallBack(int a_x,int a_y,int a_w,int a_h)
		{
			this.sprite_bg.SetClipRect(a_x,a_y,a_w,a_h);
		}
		*/

		/** コールバック。クリップ矩形。設定。
		*/
		/*
		protected override void OnSetClipRectCallBack(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.sprite_bg.SetClipRect(ref a_rect);
		}
		*/

		/** コールバック。表示。設定。
		*/
		/*
		protected override void OnSetVisibleCallBack(bool a_flag)
		{
			this.sprite_bg.SetVisible(a_flag);
		}
		*/

		/** コールバック。描画プライオリティ。設定。
		*/
		protected override void OnSetDrawPriority(long a_drawpriority)
		{
			this.UpdateView();
		}

		/** コールバック。
		*/
		protected override void OnChangeValue()
		{
			this.UpdateView();
		}

		/** 更新。表示。
		*/
		public void UpdateView()
		{
			this.sprite_bg.SetRect(ref this.rect);
			this.sprite_value.SetRect(this.rect.x,this.rect.y,(int)(this.rect.w * this.value),this.rect.h);
		}
	}
}

