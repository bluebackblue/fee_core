

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スライダー。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Slider
	*/
	public class Slider : Slider_Base
	{
		/** bg_sprite
		*/
		private Fee.Ui.Slider_Bg_Sprite2D bg_sprite;

		/** sprite
		*/
		private Fee.Ui.Slider_Value_Sprite2D sprite_value;

		/** button_sprite
		*/
		private Fee.Ui.Slider_Button_Sprite2D button_sprite;

		/** constructor
		*/
		public Slider(Fee.Deleter.Deleter a_deleter,long a_drawpriority,Slider_Base.CallBack_Change a_callback_change,int a_callback_id)
			:
			base(a_deleter,a_drawpriority,a_callback_change,a_callback_id)
		{
			//bg_sprite
			this.bg_sprite = new Fee.Ui.Slider_Bg_Sprite2D(this.deleter,a_drawpriority + 0);
			this.bg_sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.bg_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);

			//sprite_value
			this.sprite_value = new Fee.Ui.Slider_Value_Sprite2D(this.deleter,a_drawpriority + 1);
			this.sprite_value.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite_value.SetTexture(UnityEngine.Texture2D.whiteTexture);

			//button_sprite
			this.button_sprite = new Slider_Button_Sprite2D(this.deleter,a_drawpriority + 2);
			this.button_sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.button_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
		}

		/** [Slider_Base]コールバック。ロックフラグ変更。
		*/
		protected override void OnChangeLockFlag()
		{
			this.bg_sprite.SetLock(this.lock_flag);
			this.sprite_value.SetLock(this.lock_flag);
			this.button_sprite.SetLock(this.lock_flag);
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
			this.button_sprite.SetClip(this.clip_flag);
		}

		/** [Slider_Base]コールバック。クリップ矩形変更。
		*/
		protected override void OnChangeClipRect()
		{
			this.bg_sprite.SetClipRect(ref this.clip_rect);
			this.sprite_value.SetClipRect(ref this.clip_rect);
			this.button_sprite.SetClipRect(ref this.clip_rect);
		}

		/** [Slider_Base]コールバック。表示フラグ変更。
		*/
		protected override void OnChangeVisibleFlag()
		{
			this.bg_sprite.SetVisible(this.visible_flag);
			this.sprite_value.SetVisible(this.visible_flag);
			this.button_sprite.SetVisible(this.visible_flag);
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
			int t_value_w = (int)(this.rect.w * this.value / this.value_scale);

			this.bg_sprite.SetRect(ref this.rect);
			this.sprite_value.SetRect(this.rect.x,this.rect.y,t_value_w,this.rect.h);
			this.button_sprite.SetRect(this.eventplate_button.GetX(),this.eventplate_button.GetY(),this.eventplate_button.GetW(),this.eventplate_button.GetH());
		}

		/** テクスチャ。設定。
		*/
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_sprite.SetTexture(a_texture);
			this.sprite_value.SetTexture(a_texture);
		}

		/**　ボタンテクスチャーコーナーサイズ。設定。
		*/
		public void SetTextureCornerSize(int a_corner_size)
		{
			this.bg_sprite.SetCornerSize(a_corner_size);
			this.sprite_value.SetCornerSize(a_corner_size);
		}

		/** ボタンテクスチャ。設定。
		*/
		public void SetButtonTexture(UnityEngine.Texture2D a_texture)
		{
			this.button_sprite.SetTexture(a_texture);
		}

		/**　ボタンテクスチャーコーナーサイズ。設定。
		*/
		public void SetButtonTextureCornerSize(int a_corner_size)
		{
			this.button_sprite.SetCornerSize(a_corner_size);
		}
	}
}

