using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。ボタン。
*/


/** NUi
*/
namespace NUi
{
	/** Button
	*/
	public class Button : Button_Base
	{
		/** sprite
		*/
		private NUi.Button_Sprite2D sprite;

		/** text
		*/
		private NRender2D.Text2D text;

		/** constructor
		*/
		public Button(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority,Button_Base.CallBack_Click a_callback_click,int a_callback_click_value)
			:
			base(a_deleter,a_state,a_drawpriority,a_callback_click,a_callback_click_value)
		{
			//sprite
			this.sprite = new NUi.Button_Sprite2D(this.deleter,a_state,a_drawpriority);
			this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);

			//text
			this.text = new NRender2D.Text2D(this.deleter,a_state,a_drawpriority);
			this.text.SetCenter(true,true);
		}

		/** [Button_Base]コールバック。矩形。設定。
		*/
		protected override void OnChangeRect()
		{
			this.UpdateView();
		}

		/** [Button_Base]コールバック。モード変更。
		*/
		protected override void OnChangeMode()
		{
			this.sprite.SetMode(this.mode);
		}

		/** [Button_Base]コールバック。クリップフラグ変更。
		*/
		protected override void OnChangeClipFlag()
		{
			this.sprite.SetClip(this.clip_flag);
			this.text.SetClip(this.clip_flag);
		}

		/** [Button_Base]コールバック。クリップ矩形変更。
		*/
		protected override void OnChangeClipRect()
		{
			this.sprite.SetClipRect(ref this.clip_rect);
			this.text.SetClipRect(ref this.clip_rect);
		}

		/** [Button_Base]コールバック。表示フラグ変更。
		*/
		protected override void OnChangeVisibleFlag()
		{
			this.sprite.SetVisible(this.visible_flag);
			this.text.SetVisible(this.visible_flag);
		}

		/** [Button_Base]コールバック。描画プライオリティ変更。
		*/
		protected override void OnChangeDrawPriority()
		{
			this.sprite.SetDrawPriority(this.drawpriority);
			this.text.SetDrawPriority(this.drawpriority);
		}

		/** テクスチャ設定。
		*/
		public void SetTexture(Texture2D a_texture)
		{
			this.sprite.SetTexture(a_texture);
		}

		/** テキスト。
		*/
		public void SetText(string a_text)
		{
			this.text.SetText(a_text);
		}

		/**　テクスチャーコーナーサイズ。設定。
		*/
		public void SetTextureCornerSize(int a_corner_size)
		{
			this.sprite.SetCornerSize(a_corner_size);
		}

		/** 更新。表示。
		*/
		public void UpdateView()
		{
			this.sprite.SetRect(ref this.rect);
			this.text.SetRect(this.rect.x+this.rect.w/2,this.rect.y+this.rect.h/2,0,0);
		}
	}
}

