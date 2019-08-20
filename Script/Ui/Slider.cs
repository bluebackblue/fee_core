

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

		/** button_normal_sprite
		*/
		private Fee.Ui.Slice9Sprite button_normal_sprite;

		/** button_lock_sprite
		*/
		private Fee.Ui.Slice9Sprite button_lock_sprite;

		/** constructor
		*/
		public Slider(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
			:
			base(a_deleter,a_drawpriority)
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

			//button_normal_sprite
			this.button_normal_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority + 2);
			this.button_normal_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.button_normal_sprite.SetVisible(true);

			//button_lock_sprite
			this.button_lock_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority + 2);
			this.button_lock_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.button_lock_sprite.SetVisible(false);
		}

		/** [Slider_Base]コールバック。ロックフラグ変更。

			TODO:大量にスプライトを登録するとループが重いので、ここで切り替えたほうがよういかも。

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

				//button
				this.button_normal_sprite.SetVisible(false);
				this.button_lock_sprite.SetVisible(this.visible_flag);
			}else{
				//bg
				this.bg_normal_sprite.SetVisible(this.visible_flag);
				this.bg_lock_sprite.SetVisible(false);

				//value
				this.value_normal_sprite.SetVisible(this.visible_flag);
				this.value_lock_sprite.SetVisible(false);

				//button
				this.button_normal_sprite.SetVisible(this.visible_flag);
				this.button_lock_sprite.SetVisible(false);
			}
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

			//button
			this.button_normal_sprite.SetClip(this.clip_flag);
			this.button_lock_sprite.SetClip(this.clip_flag);
		}

		/** [Slider_Base]コールバック。クリップ矩形変更。
		*/
		protected override void OnChangeClipRect()
		{
			//bg
			this.bg_normal_sprite.SetClipRect(in this.clip_rect);
			this.bg_lock_sprite.SetClipRect(in this.clip_rect);

			//value
			this.value_normal_sprite.SetClipRect(in this.clip_rect);
			this.value_lock_sprite.SetClipRect(in this.clip_rect);

			//button
			this.button_normal_sprite.SetClipRect(in this.clip_rect);
			this.button_lock_sprite.SetClipRect(in this.clip_rect);
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

				//button
				this.button_normal_sprite.SetVisible(false);
				this.button_lock_sprite.SetVisible(this.visible_flag);
			}else{
				//bg
				this.bg_normal_sprite.SetVisible(this.visible_flag);
				this.bg_lock_sprite.SetVisible(false);

				//value
				this.value_normal_sprite.SetVisible(this.visible_flag);
				this.value_lock_sprite.SetVisible(false);

				//button
				this.button_normal_sprite.SetVisible(this.visible_flag);
				this.button_lock_sprite.SetVisible(false);
			}
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

			//button
			this.button_normal_sprite.SetDrawPriority(this.drawpriority + 2);
			this.button_lock_sprite.SetDrawPriority(this.drawpriority + 2);
		}

		/** 更新。表示。
		*/
		public void UpdateView()
		{
			int t_value_w = (int)(this.rect.w * this.value / this.value_scale);

			//bg
			this.bg_normal_sprite.SetRect(in this.rect);
			this.bg_lock_sprite.SetRect(in this.rect);

			//value
			this.value_normal_sprite.SetRect(this.rect.x,this.rect.y,t_value_w,this.rect.h);
			this.value_lock_sprite.SetRect(this.rect.x,this.rect.y,t_value_w,this.rect.h);

			//button
			this.button_normal_sprite.SetRect(in this.button_rect);
			this.button_lock_sprite.SetRect(in this.button_rect);
		}

		/**　ボタンテクスチャコーナーサイズ。設定。
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

		/**　ボタンテクスチャコーナーサイズ。設定。
		*/
		public void SetButtonTextureCornerSize(int a_corner_size)
		{
			this.button_normal_sprite.SetCornerSize(a_corner_size);
			this.button_lock_sprite.SetCornerSize(a_corner_size);
		}

		/** ボタンノーマルテクスチャ。設定。
		*/
		public void SetButtonNormalTexture(UnityEngine.Texture2D a_texture)
		{
			this.button_normal_sprite.SetTexture(a_texture);
		}

		/** ボタンロックテクスチャ。設定。
		*/
		public void SetButtonLockTexture(UnityEngine.Texture2D a_texture)
		{
			this.button_lock_sprite.SetTexture(a_texture);
		}

		/** ボタンノーマルテクスチャ矩形。設定。
		*/
		public void SetButtonNormalTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.button_normal_sprite.SetTextureRect(in a_texture_rect);
		}

		/** ボタンロックテクスチャ矩形。設定。
		*/
		public void SetButtonLockTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.button_lock_sprite.SetTextureRect(in a_texture_rect);
		}

		/** ボタンノーマル色。設定。
		*/
		public void SetButtonNormalColor(in UnityEngine.Color a_color)
		{
			this.button_normal_sprite.SetColor(in a_color);
		}

		/** ボタンロック色。設定。
		*/
		public void SetButtonLockColor(in UnityEngine.Color a_color)
		{
			this.button_lock_sprite.SetColor(in a_color);
		}

		/** ボタンノーマル色。設定。
		*/
		public void SetButtonNormalColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.button_normal_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** ボタンロック色。設定。
		*/
		public void SetButtonLockColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.button_lock_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** ＢＧノーマルテクスチャ。設定。
		*/
		public void SetBgNormalTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_normal_sprite.SetTexture(a_texture);
		}

		/** ＢＧロックテクスチャ。設定。
		*/
		public void SetBgLockTexture(UnityEngine.Texture2D a_texture)
		{
			this.bg_lock_sprite.SetTexture(a_texture);
		}

		/** ＢＧノーマルテクスチャ矩形。設定。
		*/
		public void SetBgNormalTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.bg_normal_sprite.SetTextureRect(in a_texture_rect);
		}

		/** ＢＧロックテクスチャ矩形。設定。
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

		/** ＢＧロック色。設定。
		*/
		public void SetBgLockColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.bg_lock_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** バリューノーマルテクスチャ。設定。
		*/
		public void SetValueNormalTexture(UnityEngine.Texture2D a_texture)
		{
			this.value_normal_sprite.SetTexture(a_texture);
		}

		/** バリューロックテクスチャ。設定。
		*/
		public void SetValueLockTexture(UnityEngine.Texture2D a_texture)
		{
			this.value_lock_sprite.SetTexture(a_texture);
		}

		/** バリューノーマルテクスチャ矩形。設定。
		*/
		public void SetValueNormalTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.value_normal_sprite.SetTextureRect(in a_texture_rect);
		}

		/** バリューロックテクスチャ矩形。設定。
		*/
		public void SetValueLockTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.value_lock_sprite.SetTextureRect(in a_texture_rect);
		}

		/** バリューノーマル色。設定。
		*/
		public void SetValueNormalColor(in UnityEngine.Color a_color)
		{
			this.value_normal_sprite.SetColor(in a_color);
		}

		/** バリューロック色。設定。
		*/
		public void SetValueLockColor(in UnityEngine.Color a_color)
		{
			this.value_lock_sprite.SetColor(in a_color);
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

