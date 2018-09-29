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
		/** bg_sprite
		*/
		private NUi.Slider_Bg_Sprite2D bg_sprite;

		/** sprite
		*/
		private NUi.ClipSprite sprite_value;

		/** button_sprite
		*/
		private NUi.Slider_Button_Sprite2D button_sprite;

		/** button_size
		*/
		private NRender2D.Size2D<int> button_size;		

		/** constructor
		*/
		public Slider(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority,Slider_Base.CallBack_Change a_callback_change,int a_callback_change_index)
			:
			base(a_deleter,a_state,a_drawpriority,a_callback_change,a_callback_change_index)
		{
			//bg_sprite
			this.bg_sprite = new NUi.Slider_Bg_Sprite2D(this.deleter,null,a_drawpriority + 0);
			this.bg_sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bg_sprite.SetTexture(Texture2D.whiteTexture);
			this.bg_sprite.SetColor(0.0f,0.0f,0.0f,1.0f);

			//sprite_value
			this.sprite_value = new ClipSprite(this.deleter,null,a_drawpriority + 1);
			this.sprite_value.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite_value.SetTexture(Texture2D.whiteTexture);
			this.sprite_value.SetColor(0.5f,1.0f,0.5f,1.0f);

			//button_sprite
			this.button_sprite = new Slider_Button_Sprite2D(this.deleter,null,a_drawpriority + 2);
			this.button_sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.button_sprite.SetTexture(Texture2D.whiteTexture);
			this.button_sprite.SetColor(1.0f,1.0f,1.0f,1.0f);

			//button_size
			this.button_size.Set(0,0);
		}

		/** [Slider_Base]コールバック。値変更。
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
			this.bg_sprite.SetClip(this.clip_flag);
			this.sprite_value.SetClip(this.clip_flag);
		}

		/** [Slider_Base]コールバック。クリップ矩形変更。
		*/
		protected override void OnChangeClipRect()
		{
			this.bg_sprite.SetClipRect(ref this.clip_rect);
			this.sprite_value.SetClipRect(ref this.clip_rect);
		}

		/** [Slider_Base]コールバック。表示フラグ変更。
		*/
		protected override void OnChangeVisibleFlag()
		{
			this.bg_sprite.SetVisible(this.visible_flag);
			this.sprite_value.SetVisible(this.visible_flag);
		}

		/** [Slider_Base]コールバック。描画プライオリティ変更。
		*/
		protected override void OnChangeDrawPriority()
		{
			this.bg_sprite.SetDrawPriority(this.drawpriority + 0);
			this.sprite_value.SetDrawPriority(this.drawpriority + 1);
			this.button_sprite.SetDrawPriority(this.drawpriority + 2);
		}

		/** 更新。表示。
		*/
		public void UpdateView()
		{
			int t_value_w = (int)(this.rect.w * this.value);

			this.bg_sprite.SetRect(ref this.rect);
			this.sprite_value.SetRect(this.rect.x,this.rect.y,t_value_w,this.rect.h);

			{
				int t_w = this.button_size.w;
				int t_h = this.button_size.h;

				if(t_w <= 0){
					t_w = this.rect.h;
				}

				if(t_h <= 0){
					t_h = this.rect.h;
				}

				this.button_sprite.SetRect(this.rect.x + t_value_w - t_w / 2,this.rect.y + (this.rect.h - t_h) / 2,t_w,t_h);
			}
		}

		/** ボタンテクスチャ。設定。
		*/
		public void SetButtonTexture(Texture2D a_texture)
		{
			this.button_sprite.SetTexture(a_texture);
		}

		/** ボタンサイズ。設定。
		*/
		public void SetButtonSize(int a_w,int a_h)
		{
			this.button_size.Set(a_w,a_h);

			this.UpdateView();
		}

		/**　ボタンテクスチャーコーナーサイズ。設定。
		*/
		public void SetButtonTextureCornerSize(int a_corner_size)
		{
			this.button_sprite.SetCornerSize(a_corner_size);
		}

		/** 背景テクスチャ。設定。
		*/
		public void SetBgTexture(Texture2D a_texture)
		{
			this.bg_sprite.SetTexture(a_texture);
		}

		/**　ボタンテクスチャーコーナーサイズ。設定。
		*/
		public void SetBgTextureCornerSize(int a_corner_size)
		{
			this.bg_sprite.SetCornerSize(a_corner_size);
		}


		/** 背景テクスチャ。設定。
		*/
		/*
		public void SetBgTexture(Texture2D a_texture)
		{
			this.sprite_bg.SetTexture(a_texture);
		}
		*/

		/** 値テクスチャ。設定。
		*/
		/*
		public void SetValueTexture(Texture2D a_texture)
		{
			this.sprite_value.SetTexture(a_texture);
		}
		*/
	}
}

