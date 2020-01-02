

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。チェックボタン。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** CheckButton
	*/
	public class CheckButton : CheckButton_Base
	{
		/** bg_normal_sprite
		*/
		private Fee.Ui.Sprite2D_Slice9 bg_normal_sprite;

		/** bg_on_sprite
		*/
		private Fee.Ui.Sprite2D_Slice9 bg_on_sprite;

		/** bg_lock_sprite
		*/
		private Fee.Ui.Sprite2D_Slice9 bg_lock_sprite;

		/** check_normal_sprite
		*/
		private Fee.Ui.Sprite2D_Clip check_normal_sprite;

		/** check_lock_sprite
		*/
		private Fee.Ui.Sprite2D_Clip check_lock_sprite;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** text_offset_x
		*/
		private int text_offset_x;

		/** constructor
		*/
		public CheckButton(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
			:
			base(a_deleter,a_drawpriority)
		{
			//bg_normal_sprite
			this.bg_normal_sprite = new Sprite2D_Slice9(this.deleter,a_drawpriority + 0);
			this.bg_normal_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg_normal_sprite.SetVisible(true);

			//bg_on_sprite
			this.bg_on_sprite = new Sprite2D_Slice9(this.deleter,a_drawpriority + 0);
			this.bg_on_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg_on_sprite.SetVisible(false);

			//bg_lock_sprite
			this.bg_lock_sprite = new Sprite2D_Slice9(this.deleter,a_drawpriority + 0);
			this.bg_lock_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg_lock_sprite.SetVisible(false);

			//check_normal_sprite
			this.check_normal_sprite = Fee.Ui.Sprite2D_Clip.Create(this.deleter,a_drawpriority + 1);
			this.check_normal_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.check_normal_sprite.SetVisible(false);

			//check_lock_sprite
			this.check_lock_sprite = Fee.Ui.Sprite2D_Clip.Create(this.deleter,a_drawpriority + 1);
			this.check_lock_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.check_lock_sprite.SetVisible(false);

			//text
			this.text = Fee.Render2D.Text2D.Create(this.deleter,a_drawpriority);
			this.text.SetAlignmentType(Render2D.Text2D_HorizontalAlignmentType.Left,Render2D.Text2D_VerticalAlignmentType.Middle);

			this.text_offset_x = 5;
		}

		/** [CheckButton_Base]コールバック。矩形変更。
		*/
		protected override void OnChangeRect()
		{
			//bg
			this.bg_normal_sprite.SetRect(in this.rect);
			this.bg_on_sprite.SetRect(in this.rect);
			this.bg_lock_sprite.SetRect(in this.rect);

			//check
			this.check_normal_sprite.SetRect(in this.rect);
			this.check_lock_sprite.SetRect(in this.rect);

			//text
			this.text.SetRect(this.rect.x + this.rect.w + this.text_offset_x,this.rect.y + this.rect.h / 2,0,0);
		}

		/** [CheckButton_Base]コールバック。クリップフラグ変更。
		*/
		protected override void OnChangeClipFlag()
		{
			//bg
			this.bg_normal_sprite.SetClip(this.clip_flag);
			this.bg_on_sprite.SetClip(this.clip_flag);
			this.bg_lock_sprite.SetClip(this.clip_flag);

			//check
			this.check_normal_sprite.SetClip(this.clip_flag);
			this.check_lock_sprite.SetClip(this.clip_flag);

			//text
			this.text.SetClip(this.clip_flag);
		}

		/** [CheckButton_Base]コールバック。クリップ矩形変更。
		*/
		protected override void OnChangeClipRect()
		{
			//bg
			this.bg_normal_sprite.SetClipRect(in this.clip_rect);
			this.bg_on_sprite.SetClipRect(in this.clip_rect);
			this.bg_lock_sprite.SetClipRect(in this.clip_rect);

			//check
			this.check_normal_sprite.SetClipRect(in this.clip_rect);
			this.check_lock_sprite.SetClipRect(in this.clip_rect);

			//text
			this.text.SetClipRect(in this.clip_rect);
		}

		/** [CheckButton_Base]コールバック。モード変更。
		*/
		protected override void OnChangeMode()
		{
			switch(this.mode){
			case CheckButton_Mode.Normal:
				{
					//bg
					this.bg_normal_sprite.SetVisible(this.visible_flag);
					this.bg_on_sprite.SetVisible(false);
					this.bg_lock_sprite.SetVisible(false);

					//check
					this.check_lock_sprite.SetVisible(false);
					this.check_normal_sprite.SetVisible(this.check_flag & this.visible_flag);
				}break;
			case CheckButton_Mode.On:
				{
					//bg
					this.bg_normal_sprite.SetVisible(false);
					this.bg_on_sprite.SetVisible(this.visible_flag);
					this.bg_lock_sprite.SetVisible(false);

					//check
					this.check_lock_sprite.SetVisible(false);
					this.check_normal_sprite.SetVisible(this.check_flag & this.visible_flag);
				}break;
			case CheckButton_Mode.Lock:
				{
					//bg
					this.bg_normal_sprite.SetVisible(false);
					this.bg_on_sprite.SetVisible(false);
					this.bg_lock_sprite.SetVisible(this.visible_flag);

					//check
					this.check_lock_sprite.SetVisible(this.check_flag & this.visible_flag);
					this.check_normal_sprite.SetVisible(false);
				}break;
			}
		}

		/** [CheckButton_Base]コールバック。チェックフラグ変更。
		*/
		protected override void OnChangeCheckFlag()
		{
			switch(this.mode){
			case CheckButton_Mode.Lock:
				{
					this.check_lock_sprite.SetVisible(this.check_flag & this.visible_flag);
					this.check_normal_sprite.SetVisible(false);
				}break;
			default:
				{
					this.check_lock_sprite.SetVisible(false);
					this.check_normal_sprite.SetVisible(this.check_flag & this.visible_flag);
				}break;
			}
		}

		/** [Slider_Base]コールバック。表示フラグ変更。
		*/
		protected override void OnChangeVisibleFlag()
		{
			switch(this.mode){
			case CheckButton_Mode.Normal:
				{
					//bg
					this.bg_normal_sprite.SetVisible(this.visible_flag);
					this.bg_on_sprite.SetVisible(false);
					this.bg_lock_sprite.SetVisible(false);

					//check
					this.check_lock_sprite.SetVisible(false);
					this.check_normal_sprite.SetVisible(this.check_flag & this.visible_flag);

				}break;
			case CheckButton_Mode.On:
				{
					//bg
					this.bg_normal_sprite.SetVisible(false);
					this.bg_on_sprite.SetVisible(this.visible_flag);
					this.bg_lock_sprite.SetVisible(false);

					//check
					this.check_lock_sprite.SetVisible(false);
					this.check_normal_sprite.SetVisible(this.check_flag & this.visible_flag);
				}break;
			case CheckButton_Mode.Lock:
				{
					//bg
					this.bg_normal_sprite.SetVisible(false);
					this.bg_on_sprite.SetVisible(false);
					this.bg_lock_sprite.SetVisible(this.visible_flag);

					//check
					this.check_lock_sprite.SetVisible(this.check_flag & this.visible_flag);
					this.check_normal_sprite.SetVisible(false);
				}break;
			}
		}

		/** [Slider_Base]コールバック。描画プライオリティ変更。
		*/
		protected override void OnChangeDrawPriority()
		{
			//bg
			this.bg_normal_sprite.SetDrawPriority(this.drawpriority + 0);
			this.bg_on_sprite.SetDrawPriority(this.drawpriority + 0);
			this.bg_lock_sprite.SetDrawPriority(this.drawpriority + 0);

			//check
			this.check_normal_sprite.SetDrawPriority(this.drawpriority + 1);
			this.check_lock_sprite.SetDrawPriority(this.drawpriority + 1);

			//text
			this.text.SetDrawPriority(this.drawpriority + 0);
		}

		/** テキスト。設定。
		*/
		public void SetText(string a_text)
		{
			this.text.SetText(a_text);
		}

		/** テキストフォントサイズ。設定。
		*/
		public void SetFontSize(int a_fontsize)
		{
			this.text.SetFontSize(a_fontsize);	
		}

		/**　ＢＧテクスチャコーナーサイズ。設定。
		*/
		public void SetBgTextureCornerSize(int a_corner_size)
		{
			//sprite
			this.bg_normal_sprite.SetCornerSize(a_corner_size);
			this.bg_on_sprite.SetCornerSize(a_corner_size);
			this.bg_lock_sprite.SetCornerSize(a_corner_size);
		}

		/** ＢＧノーマルテクスチャ。設定。
		*/
		public void SetBgNormalTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_normal_sprite.SetTexture(a_texture);
		}

		/** ＢＧオンテクスチャ。設定。
		*/
		public void SetBgOnTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_on_sprite.SetTexture(a_texture);
		}

		/** ＢＧロックテクスチャ。設定。
		*/
		public void SetBgLockTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_lock_sprite.SetTexture(a_texture);
		}

		/** ＢＧノーマルテクスチャ。設定。
		*/
		public void SetBgNormalTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.bg_normal_sprite.SetTextureRect(in a_texture_rect);
		}

		/** ＢＧオンテクスチャ。設定。
		*/
		public void SetBgOnTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.bg_on_sprite.SetTextureRect(in a_texture_rect);
		}

		/** ＢＧロックテクスチャ。設定。
		*/
		public void SetBgLockTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.bg_lock_sprite.SetTextureRect(in a_texture_rect);
		}

		/** ＢＧノーマル色。設定。
		*/
		public void SetBgNormalColor(in UnityEngine.Color a_color)
		{
			this.bg_normal_sprite.SetColor(in a_color);
		}

		/** ＢＧオン色。設定。
		*/
		public void SetBgOnColor(in UnityEngine.Color a_color)
		{
			this.bg_on_sprite.SetColor(in a_color);
		}

		/** ＢＧロック色。設定。
		*/
		public void SetBgLockColor(in UnityEngine.Color a_color)
		{
			this.bg_lock_sprite.SetColor(in a_color);
		}

		/** ＢＧノーマル色。設定。
		*/
		public void SetBgNormalColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.bg_normal_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** ＢＧオン色。設定。
		*/
		public void SetBgOnColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.bg_on_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** ＢＧロック色。設定。
		*/
		public void SetBgLockColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.bg_lock_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** チェックノーマルテクスチャ。設定。
		*/
		public void SetCheckNormalTexture(UnityEngine.Texture2D a_texture)
		{
			this.check_normal_sprite.SetTexture(a_texture);
		}

		/** チェックノーマルテクスチャ矩形。設定。
		*/
		public void SetCheckNormalTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.check_normal_sprite.SetTextureRect(in a_texture_rect);
		}

		/** チェックノーマルテクスチャ色。設定。
		*/
		public void SetCheckNormalTextureColor(in UnityEngine.Color a_color)
		{
			this.check_normal_sprite.SetColor(in a_color);
		}

		/** チェックノーマルテクスチャ色。設定。
		*/
		public void SetCheckNormalTextureColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.check_normal_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** チェックロックテクスチャ。設定。
		*/
		public void SetCheckLockTexture(UnityEngine.Texture2D a_texture)
		{
			this.check_lock_sprite.SetTexture(a_texture);
		}

		/** チェックロックテクスチャ矩形。設定。
		*/
		public void SetCheckLockTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.check_lock_sprite.SetTextureRect(in a_texture_rect);
		}

		/** チェックロックテクスチャ色。設定。
		*/
		public void SetCheckLockTextureColor(in UnityEngine.Color a_color)
		{
			this.check_lock_sprite.SetColor(in a_color);
		}

		/** チェックロックテクスチャ色。設定。
		*/
		public void SetCheckLockTextureColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.check_lock_sprite.SetColor(a_r,a_g,a_b,a_a);
		}
	}
}

