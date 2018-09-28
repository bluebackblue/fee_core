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

		/** sprite_button
		*/
		private NUi.ClipSprite sprite_button;

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
			this.sprite_value = new ClipSprite(this.deleter,null,a_drawpriority + 1);
			this.sprite_value.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite_value.SetTexture(Texture2D.whiteTexture);
			this.sprite_value.SetColor(0.5f,1.0f,0.5f,1.0f);

			//sprite_button
			this.sprite_button = new ClipSprite(this.deleter,null,a_drawpriority + 2);
			this.sprite_button.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite_button.SetTexture(Texture2D.whiteTexture);
			this.sprite_button.SetColor(1.0f,1.0f,1.0f,1.0f);
		}

		/** コールバック。
		*/
		protected override void OnChangeValue()
		{
			this.UpdateView();
		}

		/** [Slider_Base]コールバック。矩形変更。
		*/
		protected override void OnChangeRect()
		{
			this.UpdateView();
		}

		/** [Slider_Base]コールバック。クリップフラグ変更。
		*/
		protected override void OnChangeClipFlag()
		{
			this.sprite_bg.SetClip(this.clip_flag);
			this.sprite_value.SetClip(this.clip_flag);
		}

		/** [Slider_Base]コールバック。クリップ矩形変更。
		*/
		protected override void OnChangeClipRect()
		{
			this.sprite_bg.SetClipRect(ref this.clip_rect);
			this.sprite_value.SetClipRect(ref this.clip_rect);
		}

		/** [Slider_Base]コールバック。表示フラグ変更。
		*/
		protected override void OnChangeVisibleFlag()
		{
			this.sprite_bg.SetVisible(this.visible_flag);
			this.sprite_value.SetVisible(this.visible_flag);
		}

		/** [Slider_Base]コールバック。描画プライオリティ変更。
		*/
		protected override void OnChangeDrawPriority()
		{
			this.sprite_bg.SetDrawPriority(this.drawpriority);
			this.sprite_value.SetDrawPriority(this.drawpriority);
		}

		/** 更新。表示。
		*/
		public void UpdateView()
		{
			int t_value_w = (int)(this.rect.w * this.value);

			this.sprite_bg.SetRect(ref this.rect);
			this.sprite_value.SetRect(this.rect.x,this.rect.y,t_value_w,this.rect.h);
			this.sprite_button.SetRect(this.rect.x + t_value_w - this.rect.h/2,this.rect.y,this.rect.h,this.rect.h);
		}

		/** ボタンテクスチャ。設定。
		*/
		public void SetButtonTexture(Texture2D a_texture)
		{
			this.sprite_button.SetTexture(a_texture);
		}

		/** 背景テクスチャ。設定。
		*/
		public void SetBgTexture(Texture2D a_texture)
		{
			this.sprite_bg.SetTexture(a_texture);
		}

		/** 値テクスチャ。設定。
		*/
		public void SetValueTexture(Texture2D a_texture)
		{
			this.sprite_value.SetTexture(a_texture);
		}
	}
}

