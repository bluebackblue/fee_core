

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
		private Fee.Ui.Slice9Sprite bg_normal_sprite;

		/** bg_on_sprite
		*/
		private Fee.Ui.Slice9Sprite bg_on_sprite;

		/** bg_lock_sprite
		*/
		private Fee.Ui.Slice9Sprite bg_lock_sprite;

		/** check_sprite
		*/
		private Fee.Ui.ClipSprite check_sprite;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** text_offset_x
		*/
		private int text_offset_x;

		/** constructor
		*/
		public CheckButton(Fee.Deleter.Deleter a_deleter,long a_drawpriority,CheckButton_Base.CallBack_Change a_callback_chnage,int a_callback_id)
			:
			base(a_deleter,a_drawpriority,a_callback_chnage,a_callback_id)
		{
			//bg_normal_sprite
			this.bg_normal_sprite = new Slice9Sprite(this.deleter,a_drawpriority + 0);
			this.bg_normal_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg_normal_sprite.SetVisible(true);

			//bg_on_sprite
			this.bg_on_sprite = new Slice9Sprite(this.deleter,a_drawpriority + 0);
			this.bg_on_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg_on_sprite.SetVisible(false);

			//bg_lock_sprite
			this.bg_lock_sprite = new Slice9Sprite(this.deleter,a_drawpriority + 0);
			this.bg_lock_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg_lock_sprite.SetVisible(false);

			//check_sprite
			this.check_sprite = new Fee.Ui.ClipSprite(this.deleter,a_drawpriority + 1);
			this.check_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.check_sprite.SetVisible(false);

			//text
			this.text = new Fee.Render2D.Text2D(this.deleter,a_drawpriority);
			this.text.SetCenter(false,true);

			this.text_offset_x = 5;
		}

		/** [Button_Base]コールバック。削除。

			TODO:いらない。

		*/
		protected override void OnDeleteCallBack()
		{
		}

		/** コールバック。矩形。設定。

			TODO:OnChange

		*/
		protected override void OnSetRectCallBack(int a_x,int a_y,int a_w,int a_h)
		{
			//bg
			this.bg_normal_sprite.SetRect(a_x,a_y,a_w,a_h);
			this.bg_on_sprite.SetRect(a_x,a_y,a_w,a_h);
			this.bg_lock_sprite.SetRect(a_x,a_y,a_w,a_h);

			//check
			this.check_sprite.SetRect(a_x,a_y,a_w,a_h);

			this.text.SetRect(a_x + a_w + this.text_offset_x,a_y+a_h/2,0,0);
		}

		/** コールバック。矩形。設定。

			TODO:OnChange

		*/
		protected override void OnSetRectCallBack(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			//bg
			this.bg_normal_sprite.SetRect(ref a_rect);
			this.bg_on_sprite.SetRect(ref a_rect);
			this.bg_lock_sprite.SetRect(ref a_rect);

			//check
			this.check_sprite.SetRect(ref a_rect);

			this.text.SetRect(a_rect.x + a_rect.w + this.text_offset_x,a_rect.y+a_rect.h/2,0,0);
		}

		/** コールバック。モード。設定。

			TODO:OnChange

		*/
		protected override void OnSetModeCallBack(CheckButton_Mode a_mode)
		{
			switch(a_mode){
			case CheckButton_Mode.Normal:
				{
					this.bg_normal_sprite.SetVisible(true);
					this.bg_on_sprite.SetVisible(false);
					this.bg_lock_sprite.SetVisible(false);
				}break;
			case CheckButton_Mode.On:
				{
					this.bg_normal_sprite.SetVisible(false);
					this.bg_on_sprite.SetVisible(true);
					this.bg_lock_sprite.SetVisible(false);
				}break;
			case CheckButton_Mode.Lock:
				{
					this.bg_normal_sprite.SetVisible(false);
					this.bg_on_sprite.SetVisible(false);
					this.bg_lock_sprite.SetVisible(true);
				}break;
			}
		}

		/** コールバック。チェック。設定。

			TODO:OnChangeCheckFlag

		*/
		protected override void OnSetCheckCallBack(bool a_flag)
		{
			this.check_sprite.SetVisible(a_flag);
		}

		/** コールバック。クリップ。設定。

			TODO:OnChangeClipFlag

		*/
		protected override void OnSetClipCallBack(bool a_flag)
		{
			//bg
			this.bg_normal_sprite.SetClip(a_flag);
			this.bg_on_sprite.SetClip(a_flag);
			this.bg_lock_sprite.SetClip(a_flag);

			//check
			this.check_sprite.SetClip(a_flag);

			//text
			this.text.SetClip(a_flag);
		}

		/** コールバック。クリップ矩形。設定。

			TODO:OnChangeClipRect

		*/
		protected override void OnSetClipRectCallBack(int a_x,int a_y,int a_w,int a_h)
		{
			//bg
			this.bg_normal_sprite.SetClipRect(a_x,a_y,a_w,a_h);
			this.bg_on_sprite.SetClipRect(a_x,a_y,a_w,a_h);
			this.bg_lock_sprite.SetClipRect(a_x,a_y,a_w,a_h);

			//check
			this.check_sprite.SetClipRect(a_x,a_y,a_w,a_h);

			//text
			this.text.SetClipRect(a_x,a_y,a_w,a_h);
		}

		/** コールバック。クリップ矩形。設定。

			TODO:OnChangeClipRect

		*/
		protected override void OnSetClipRectCallBack(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			//bg
			this.bg_normal_sprite.SetClipRect(ref a_rect);
			this.bg_on_sprite.SetClipRect(ref a_rect);
			this.bg_lock_sprite.SetClipRect(ref a_rect);

			//check
			this.check_sprite.SetClipRect(ref a_rect);

			//text
			this.text.SetClipRect(ref a_rect);
		}

		/** テキスト。設定。
		*/
		public void SetText(string a_text)
		{
			this.text.SetText(a_text);
		}

		/** テクスチャ。設定。
		*/
		#if(false)
		public void SetPackTexture(UnityEngine.Texture2D a_texture)
		{
			//bg
			this.bg_normal_sprite.SetTexture(a_texture);
			this.bg_on_sprite.SetTexture(a_texture);
			this.bg_lock_sprite.SetTexture(a_texture);
			this.bg_normal_sprite.SetTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.bg_on_sprite.SetTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.bg_lock_sprite.SetTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);

			//check
			this.sprite_check.SetTexture(a_texture);
			this.sprite_check.SetTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
		}
		#endif

		/** ＢＧノーマルテクスチャー。設定。
		*/
		public void SetBgNormalTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_normal_sprite.SetTexture(a_texture);
		}

		/** ＢＧオンテクスチャー。設定。
		*/
		public void SetBgOnTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_on_sprite.SetTexture(a_texture);
		}

		/** ＢＧロックテクスチャー。設定。
		*/
		public void SetBgLockTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_lock_sprite.SetTexture(a_texture);
		}

		/** ＢＧノーマルテクスチャー。設定。
		*/
		public void SetBgNormalTextureRect(ref Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.bg_normal_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** ＢＧオンテクスチャー。設定。
		*/
		public void SetBgOnTextureRect(ref Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.bg_on_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** ＢＧロックテクスチャー。設定。
		*/
		public void SetBgLockTextureRect(ref Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.bg_lock_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** ＢＧノーマル色。設定。
		*/
		public void SetBgNormalColor(ref UnityEngine.Color a_color)
		{
			this.bg_normal_sprite.SetColor(ref a_color);
		}

		/** ＢＧオン色。設定。
		*/
		public void SetBgOnColor(ref UnityEngine.Color a_color)
		{
			this.bg_on_sprite.SetColor(ref a_color);
		}

		/** ＢＧロック色。設定。
		*/
		public void SetBgLockColor(ref UnityEngine.Color a_color)
		{
			this.bg_lock_sprite.SetColor(ref a_color);
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

		/** チェックテクスチャー。設定。
		*/
		public void SetCheckTexture(UnityEngine.Texture2D a_texture)
		{
			this.check_sprite.SetTexture(a_texture);
		}

		/** チェックテクスチャー矩形。設定。
		*/
		public void SetCheckTextureRect(ref Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.check_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** チェックテクスチャー色。設定。
		*/
		public void SetCheckTextureColor(ref UnityEngine.Color a_color)
		{
			this.check_sprite.SetColor(ref a_color);
		}

		/** チェックテクスチャー色。設定。
		*/
		public void SetCheckTextureColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.check_sprite.SetColor(a_r,a_g,a_b,a_a);
		}
	}
}

