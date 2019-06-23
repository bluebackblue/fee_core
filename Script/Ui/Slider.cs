

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
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
		/** bg_normal_sprite
		*/
		private Fee.Ui.Slice9Sprite bg_normal_sprite;

		/** bg_lock_sprite
		*/
		private Fee.Ui.Slice9Sprite bg_lock_sprite;

		/** value_normal_sprite
		*/
		private Fee.Ui.Slice9Sprite value_normal_sprite;

		/** value_lock_sprite
		*/
		private Fee.Ui.Slice9Sprite value_lock_sprite;

		/** button_sprite
		*/
		private Fee.Ui.Slider_Button_Sprite2D button_sprite;

		/** constructor
		*/
		public Slider(Fee.Deleter.Deleter a_deleter,long a_drawpriority,Slider_Base.CallBack_Change a_callback_change,int a_callback_id)
			:
			base(a_deleter,a_drawpriority,a_callback_change,a_callback_id)
		{
			//bg_normal_sprite
			this.bg_normal_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority + 0);
			this.bg_normal_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg_normal_sprite.SetVisible(true);

			//bg_lock_sprite
			this.bg_lock_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority + 0);
			this.bg_lock_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg_lock_sprite.SetVisible(false);

			//value_normal_sprite
			this.value_normal_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority + 1);
			this.value_normal_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.value_normal_sprite.SetVisible(true);

			//value_lock_sprite
			this.value_lock_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority + 1);
			this.value_lock_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.value_lock_sprite.SetVisible(false);

			/*
			//sprite_value
			this.sprite_value = new Fee.Ui.Slider_Value_Sprite2D(this.deleter,a_drawpriority + 1);
			this.sprite_value.SetTexture(UnityEngine.Texture2D.whiteTexture);
			*/

			//button_sprite
			this.button_sprite = new Slider_Button_Sprite2D(this.deleter,a_drawpriority + 2);
			this.button_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
		}

		/** [Slider_Base]コールバック。ロックフラグ変更。
		*/
		protected override void OnChangeLockFlag()
		{
			if(this.lock_flag == true){
				//bg
				this.bg_normal_sprite.SetVisible(false);
				this.bg_lock_sprite.SetVisible(this.visible_flag);

				//value
				this.value_normal_sprite.SetVisible(false);
				this.value_lock_sprite.SetVisible(this.visible_flag);
			}else{
				//bg
				this.bg_normal_sprite.SetVisible(this.visible_flag);
				this.bg_lock_sprite.SetVisible(false);

				//value
				this.value_normal_sprite.SetVisible(this.visible_flag);
				this.value_lock_sprite.SetVisible(false);
			}

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
			//bg
			this.bg_normal_sprite.SetClip(this.clip_flag);
			this.bg_lock_sprite.SetClip(this.clip_flag);
			
			//value
			this.value_normal_sprite.SetClip(this.clip_flag);
			this.value_lock_sprite.SetClip(this.clip_flag);

			this.button_sprite.SetClip(this.clip_flag);
		}

		/** [Slider_Base]コールバック。クリップ矩形変更。
		*/
		protected override void OnChangeClipRect()
		{
			//bg
			this.bg_normal_sprite.SetClipRect(ref this.clip_rect);
			this.bg_lock_sprite.SetClipRect(ref this.clip_rect);

			//value
			this.value_normal_sprite.SetClipRect(ref this.clip_rect);
			this.value_lock_sprite.SetClipRect(ref this.clip_rect);

			this.button_sprite.SetClipRect(ref this.clip_rect);
		}

		/** [Slider_Base]コールバック。表示フラグ変更。
		*/
		protected override void OnChangeVisibleFlag()
		{
			if(this.lock_flag == true){
				//bg
				this.bg_normal_sprite.SetVisible(false);
				this.bg_lock_sprite.SetVisible(this.visible_flag);

				//value
				this.value_normal_sprite.SetVisible(false);
				this.value_lock_sprite.SetVisible(this.visible_flag);
			}else{
				//bg
				this.bg_normal_sprite.SetVisible(this.visible_flag);
				this.bg_lock_sprite.SetVisible(false);

				//value
				this.value_normal_sprite.SetVisible(false);
				this.value_lock_sprite.SetVisible(this.visible_flag);
			}

			this.button_sprite.SetVisible(this.visible_flag);
		}

		/** [Slider_Base]コールバック。描画プライオリティ変更。
		*/
		protected override void OnChangeDrawPriority()
		{
			//bg
			this.bg_normal_sprite.SetDrawPriority(this.drawpriority + 0);
			this.bg_lock_sprite.SetDrawPriority(this.drawpriority + 0);

			//value
			this.value_normal_sprite.SetDrawPriority(this.drawpriority + 1);
			this.value_lock_sprite.SetDrawPriority(this.drawpriority + 1);

			this.button_sprite.SetDrawPriority(this.drawpriority + 2);
		}

		/** 更新。表示。
		*/
		public void UpdateView()
		{
			int t_value_w = (int)(this.rect.w * this.value / this.value_scale);

			//bg
			this.bg_normal_sprite.SetRect(ref this.rect);
			this.bg_lock_sprite.SetRect(ref this.rect);

			//value
			this.value_normal_sprite.SetRect(this.rect.x,this.rect.y,t_value_w,this.rect.h);
			this.value_lock_sprite.SetRect(this.rect.x,this.rect.y,t_value_w,this.rect.h);

			this.button_sprite.SetRect(this.eventplate_button.GetX(),this.eventplate_button.GetY(),this.eventplate_button.GetW(),this.eventplate_button.GetH());
		}

		/**　ボタンテクスチャーコーナーサイズ。設定。
		*/
		public void SetTextureCornerSize(int a_corner_size)
		{
			//bg
			this.bg_normal_sprite.SetCornerSize(a_corner_size);
			this.bg_lock_sprite.SetCornerSize(a_corner_size);

			//value
			this.value_normal_sprite.SetCornerSize(a_corner_size);
			this.value_lock_sprite.SetCornerSize(a_corner_size);
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

		/** パックテクスチャ。設定。（ＢＧ。バリュー）。
		*/
		#if(false)
		public void SetPacTexture_BgValue(UnityEngine.Texture2D a_texture)
		{
			Render2D.Rect2D_R<float> t_rect_normal = new Render2D.Rect2D_R<float>(0,0,Render2D.Config.TEXTURE_W / 2,Render2D.Config.TEXTURE_H / 2);
			Render2D.Rect2D_R<float> t_rect_lcok = new Render2D.Rect2D_R<float>(Render2D.Config.TEXTURE_W / 2,Render2D.Config.TEXTURE_H / 2,Render2D.Config.TEXTURE_W / 2,Render2D.Config.TEXTURE_H / 2);
			
			this.bg_normal_sprite.SetTexture(a_texture);
			this.bg_lock_sprite.SetTexture(a_texture);
			this.bg_normal_sprite.SetTextureRect2(ref t_rect_normal);
			this.bg_lock_sprite.SetTextureRect2(ref t_rect_lcok);
			
			this.sprite_value.SetTexture(a_texture);
		}
		#endif

		/** ＢＧノーマルテクスチャー。設定。
		*/
		public void SetBgNormalTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_normal_sprite.SetTexture(a_texture);
		}

		/** ＢＧロックテクスチャー。設定。
		*/
		public void SetBgLockTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_lock_sprite.SetTexture(a_texture);
		}

		/** ＢＧノーマルテクスチャー矩形。設定。
		*/
		public void SetBgNormalTextureRect(ref Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.bg_normal_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** ＢＧロックテクスチャー矩形。設定。
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

		/** ＢＧロック色。設定。
		*/
		public void SetBgLockColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.bg_lock_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** バリューノーマルテクスチャー。設定。
		*/
		public void SetValueNormalTexture(UnityEngine.Texture2D a_texture)
		{
			this.value_normal_sprite.SetTexture(a_texture);
		}

		/** バリューロックテクスチャー。設定。
		*/
		public void SetValueLockTexture(UnityEngine.Texture2D a_texture)
		{
			this.value_lock_sprite.SetTexture(a_texture);
		}

		/** バリューノーマルテクスチャー矩形。設定。
		*/
		public void SetValueNormalTextureRect(ref Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.value_normal_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** バリューロックテクスチャー矩形。設定。
		*/
		public void SetValueLockTextureRect(ref Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.value_lock_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** バリューノーマル色。設定。
		*/
		public void SetValueNormalColor(ref UnityEngine.Color a_color)
		{
			this.value_normal_sprite.SetColor(ref a_color);
		}

		/** バリューロック色。設定。
		*/
		public void SetValueLockColor(ref UnityEngine.Color a_color)
		{
			this.value_lock_sprite.SetColor(ref a_color);
		}

		/** バリューノーマル色。設定。
		*/
		public void SetValueNormalColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.value_normal_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** バリューロック色。設定。
		*/
		public void SetValueLockColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.value_lock_sprite.SetColor(a_r,a_g,a_b,a_a);
		}
	}
}

