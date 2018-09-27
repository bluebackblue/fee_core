using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。チェックボタン。
*/


/** NUi
*/
namespace NUi
{
	/** CheckButton
	*/
	public class CheckButton : CheckButton_Base
	{
		/** sprite_bg
		*/
		private NUi.ClipSprite sprite_bg;

		/** sprite_check
		*/
		private NUi.ClipSprite sprite_check;

		/** text
		*/
		private NRender2D.Text2D text;

		/** text_offset_x
		*/
		private int text_offset_x;

		/** constructor
		*/
		public CheckButton(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority,CheckButton_Base.CallBack_Click a_callback_click,int a_callback_click_value)
			:
			base(a_deleter,a_state,a_drawpriority,a_callback_click,a_callback_click_value)
		{
			//sprite_bg
			this.sprite_bg = new NUi.ClipSprite(this.deleter,a_state,a_drawpriority);
			this.sprite_bg.SetTextureRect(0.0f,0.0f,NRender2D.Render2D.TEXTURE_W / 2,NRender2D.Render2D.TEXTURE_H / 2);

			//sprite_check
			this.sprite_check = new NUi.ClipSprite(this.deleter,a_state,a_drawpriority + 1);
			this.sprite_check.SetTextureRect(0.0f,NRender2D.Render2D.TEXTURE_H / 2,NRender2D.Render2D.TEXTURE_W / 2,NRender2D.Render2D.TEXTURE_H / 2);
			this.sprite_check.SetVisible(false);

			//text
			this.text = new NRender2D.Text2D(this.deleter,a_state,a_drawpriority);
			this.text.SetCenter(false,true);

			this.text_offset_x = 5;
		}

		/** [Button_Base]コールバック。削除。
		*/
		protected override void OnDeleteCallBack()
		{
		}

		/** コールバック。矩形。設定。
		*/
		protected override void OnSetRectCallBack(int a_x,int a_y,int a_w,int a_h)
		{
			this.sprite_bg.SetRect(a_x,a_y,a_w,a_h);
			this.sprite_check.SetRect(a_x,a_y,a_w,a_h);

			this.text.SetRect(a_x + a_w + this.text_offset_x,a_y+a_h/2,0,0);
		}

		/** コールバック。矩形。設定。
		*/
		protected override void OnSetRectCallBack(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.sprite_bg.SetRect(ref a_rect);
			this.sprite_check.SetRect(ref a_rect);

			this.text.SetRect(a_rect.x + a_rect.w + this.text_offset_x,a_rect.y+a_rect.h/2,0,0);
		}

		/** コールバック。モード。設定。
		*/
		protected override void OnSetModeCallBack(CheckButton_Mode a_mode)
		{
			switch(a_mode){
			case CheckButton_Mode.Normal:
				{
					this.sprite_bg.SetTextureRect(0.0f,0.0f,NRender2D.Render2D.TEXTURE_W / 2,NRender2D.Render2D.TEXTURE_H / 2);
				}break;
			case CheckButton_Mode.On:
				{
					this.sprite_bg.SetTextureRect(NRender2D.Render2D.TEXTURE_W / 2,0.0f,NRender2D.Render2D.TEXTURE_W / 2,NRender2D.Render2D.TEXTURE_H / 2);
				}break;
			case CheckButton_Mode.Lock:
				{
					this.sprite_bg.SetTextureRect(NRender2D.Render2D.TEXTURE_W / 2,NRender2D.Render2D.TEXTURE_H / 2,NRender2D.Render2D.TEXTURE_W / 2,NRender2D.Render2D.TEXTURE_H / 2);
				}break;
			}
		}

		/** コールバック。チェック。設定。
		*/
		protected override void OnSetCheckCallBack(bool a_flag)
		{
			this.sprite_check.SetVisible(a_flag);
		}

		/** コールバック。クリップ。設定。
		*/
		protected override void OnSetClipCallBack(bool a_flag)
		{
			this.sprite_bg.SetClip(a_flag);
			this.sprite_check.SetClip(a_flag);
			this.text.SetClip(a_flag);
		}

		/** コールバック。クリップ矩形。設定。
		*/
		protected override void OnSetClipRectCallBack(int a_x,int a_y,int a_w,int a_h)
		{
			this.sprite_bg.SetClipRect(a_x,a_y,a_w,a_h);
			this.sprite_check.SetClipRect(a_x,a_y,a_w,a_h);
			this.text.SetClipRect(a_x,a_y,a_w,a_h);
		}

		/** コールバック。クリップ矩形。設定。
		*/
		protected override void OnSetClipRectCallBack(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.sprite_bg.SetClipRect(ref a_rect);
			this.sprite_check.SetClipRect(ref a_rect);
			this.text.SetClipRect(ref a_rect);
		}

		/** テクスチャ。設定。
		*/
		public void SetTexture(Texture2D a_texture)
		{
			this.sprite_bg.SetTexture(a_texture);
			this.sprite_check.SetTexture(a_texture);
		}

		/** テキスト。設定。
		*/
		public void SetText(string a_text)
		{
			this.text.SetText(a_text);
		}
	}
}

