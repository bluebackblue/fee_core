

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。ボタン。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Button
	*/
	public class Button : Button_Base
	{
		/** sprite
		*/
		private Fee.Ui.Slice9Sprite normal_sprite;
		private Fee.Ui.Slice9Sprite on_sprite;
		private Fee.Ui.Slice9Sprite down_sprite;
		private Fee.Ui.Slice9Sprite lock_sprite;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** constructor
		*/
		public Button(Fee.Deleter.Deleter a_deleter,long a_drawpriority,Button_Base.CallBack_Click a_callback_click,int a_callback_click_id)
			:
			base(a_deleter,a_drawpriority,a_callback_click,a_callback_click_id)
		{
			//sprite
			this.normal_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority);
			this.normal_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.normal_sprite.SetVisible(true);

			this.on_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority);
			this.on_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.on_sprite.SetVisible(false);

			this.down_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority);
			this.down_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.down_sprite.SetVisible(false);

			this.lock_sprite = new Fee.Ui.Slice9Sprite(this.deleter,a_drawpriority);
			this.lock_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.lock_sprite.SetVisible(false);

			//text
			this.text = new Fee.Render2D.Text2D(this.deleter,a_drawpriority);
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
			switch(this.mode){
			case Button_Mode.Normal:
				{
					this.normal_sprite.SetVisible(this.visible_flag);
					this.on_sprite.SetVisible(false);
					this.down_sprite.SetVisible(false);
					this.lock_sprite.SetVisible(false);
				}break;
			case Button_Mode.On:
				{
					this.normal_sprite.SetVisible(false);
					this.on_sprite.SetVisible(this.visible_flag);
					this.down_sprite.SetVisible(false);
					this.lock_sprite.SetVisible(false);
				}break;
			case Button_Mode.Down:
				{
					this.normal_sprite.SetVisible(false);
					this.on_sprite.SetVisible(false);
					this.down_sprite.SetVisible(this.visible_flag);
					this.lock_sprite.SetVisible(false);
				}break;
			case Button_Mode.Lock:
				{
					this.normal_sprite.SetVisible(false);
					this.on_sprite.SetVisible(false);
					this.down_sprite.SetVisible(false);
					this.lock_sprite.SetVisible(this.visible_flag);
				}break;
			}
		}

		/** [Button_Base]コールバック。クリップフラグ変更。
		*/
		protected override void OnChangeClipFlag()
		{
			//sprite
			this.normal_sprite.SetClip(this.clip_flag);
			this.on_sprite.SetClip(this.clip_flag);
			this.down_sprite.SetClip(this.clip_flag);
			this.lock_sprite.SetClip(this.clip_flag);

			//text
			this.text.SetClip(this.clip_flag);
		}

		/** [Button_Base]コールバック。クリップ矩形変更。
		*/
		protected override void OnChangeClipRect()
		{
			//sprite
			this.normal_sprite.SetClipRect(ref this.clip_rect);
			this.on_sprite.SetClipRect(ref this.clip_rect);
			this.down_sprite.SetClipRect(ref this.clip_rect);
			this.lock_sprite.SetClipRect(ref this.clip_rect);

			//text
			this.text.SetClipRect(ref this.clip_rect);
		}

		/** [Button_Base]コールバック。表示フラグ変更。
		*/
		protected override void OnChangeVisibleFlag()
		{
			//sprite
			switch(this.mode){
			case Button_Mode.Normal:
				{
					this.normal_sprite.SetVisible(this.visible_flag);
					this.on_sprite.SetVisible(false);
					this.down_sprite.SetVisible(false);
					this.lock_sprite.SetVisible(false);
				}break;
			case Button_Mode.On:
				{
					this.normal_sprite.SetVisible(false);
					this.on_sprite.SetVisible(this.visible_flag);
					this.down_sprite.SetVisible(false);
					this.lock_sprite.SetVisible(false);
				}break;
			case Button_Mode.Down:
				{
					this.normal_sprite.SetVisible(false);
					this.on_sprite.SetVisible(false);
					this.down_sprite.SetVisible(this.visible_flag);
					this.lock_sprite.SetVisible(false);
				}break;
			case Button_Mode.Lock:
				{
					this.normal_sprite.SetVisible(false);
					this.on_sprite.SetVisible(false);
					this.down_sprite.SetVisible(false);
					this.lock_sprite.SetVisible(this.visible_flag);
				}break;
			}

			//text
			this.text.SetVisible(this.visible_flag);
		}

		/** [Button_Base]コールバック。描画プライオリティ変更。
		*/
		protected override void OnChangeDrawPriority()
		{
			//sprite
			this.normal_sprite.SetDrawPriority(this.drawpriority);
			this.on_sprite.SetDrawPriority(this.drawpriority);
			this.down_sprite.SetDrawPriority(this.drawpriority);
			this.lock_sprite.SetDrawPriority(this.drawpriority);

			//text
			this.text.SetDrawPriority(this.drawpriority);
		}

		/** テキスト。
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

		/**　テクスチャーコーナーサイズ。設定。
		*/
		public void SetTextureCornerSize(int a_corner_size)
		{
			//sprite
			this.normal_sprite.SetCornerSize(a_corner_size);
			this.on_sprite.SetCornerSize(a_corner_size);
			this.down_sprite.SetCornerSize(a_corner_size);
			this.lock_sprite.SetCornerSize(a_corner_size);
		}

		/** 更新。表示。
		*/
		public void UpdateView()
		{
			//sprite
			this.normal_sprite.SetRect(ref this.rect);
			this.on_sprite.SetRect(ref this.rect);
			this.down_sprite.SetRect(ref this.rect);
			this.lock_sprite.SetRect(ref this.rect);

			//text
			this.text.SetRect(this.rect.x+this.rect.w/2,this.rect.y+this.rect.h/2,0,0);
		}

		/** ノーマルテクスチャ。設定。
		*/
		public void SetNormalTexture(UnityEngine.Texture2D a_texture)
		{
			#if(UNITY_EDITOR)
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.filterMode != UnityEngine.FilterMode.Point)){
				Tool.Assert(false);
			}
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.mipmapCount != 1)){
				Tool.Assert(false);
			}
			#endif

			this.normal_sprite.SetTexture(a_texture);
		}

		/** オンテクスチャ。設定。
		*/
		public void SetOnTexture(UnityEngine.Texture2D a_texture)
		{
			#if(UNITY_EDITOR)
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.filterMode != UnityEngine.FilterMode.Point)){
				Tool.Assert(false);
			}
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.mipmapCount != 1)){
				Tool.Assert(false);
			}
			#endif

			this.on_sprite.SetTexture(a_texture);
		}

		/** ダウンテクスチャ。設定。
		*/
		public void SetDownTexture(UnityEngine.Texture2D a_texture)
		{
			#if(UNITY_EDITOR)
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.filterMode != UnityEngine.FilterMode.Point)){
				Tool.Assert(false);
			}
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.mipmapCount != 1)){
				Tool.Assert(false);
			}
			#endif

			this.down_sprite.SetTexture(a_texture);
		}

		/** ロックテクスチャ。設定。
		*/
		public void SetLockTexture(UnityEngine.Texture2D a_texture)
		{
			#if(UNITY_EDITOR)
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.filterMode != UnityEngine.FilterMode.Point)){
				Tool.Assert(false);
			}
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.mipmapCount != 1)){
				Tool.Assert(false);
			}
			#endif

			this.lock_sprite.SetTexture(a_texture);
		}

		/** ノーマルテクスチャ矩形。設定。
		*/
		public void SetNormalTextureRect(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.normal_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** オンテクスチャ矩形。設定。
		*/
		public void SetOnTextureRect(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.on_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** ダウンテクスチャ矩形。設定。
		*/
		public void SetDownTextureRect(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.down_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** ロックテクスチャ矩形。設定。
		*/
		public void SetLockTextureRect(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.lock_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** ノーマル色。設定。
		*/
		public void SetNormalColor(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.normal_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** オン色。設定。
		*/
		public void SetOnColor(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.on_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** ダウン色。設定。
		*/
		public void SetDownColor(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.down_sprite.SetTextureRect(ref a_texture_rect);
		}

		/** ロック色。設定。
		*/
		public void SetLockColor(ref Fee.Render2D.Rect2D_R<float> a_texture_rect)
		{
			this.lock_sprite.SetTextureRect(ref a_texture_rect);
		}
	}
}

