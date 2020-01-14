

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
		private Fee.Ui.Sprite2D_Slice9 normal_sprite;
		private Fee.Ui.Sprite2D_Slice9 on_sprite;
		private Fee.Ui.Sprite2D_Slice9 down_sprite;
		private Fee.Ui.Sprite2D_Slice9 lock_sprite;

		/** text
		*/
		private Fee.Render2D.Text2D text;

		/** textcolor
		*/
		private UnityEngine.Color nomal_textcolor;
		private UnityEngine.Color on_textcolor;
		private UnityEngine.Color down_textcolor;
		private UnityEngine.Color lock_textcolor;

		/** constructor
		*/
		public Button(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
			:
			base(a_deleter,a_drawpriority)
		{
			//sprite
			this.normal_sprite = new Fee.Ui.Sprite2D_Slice9(this.deleter,a_drawpriority);
			this.normal_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.normal_sprite.SetVisible(true);

			this.on_sprite = new Fee.Ui.Sprite2D_Slice9(this.deleter,a_drawpriority);
			this.on_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.on_sprite.SetVisible(false);

			this.down_sprite = new Fee.Ui.Sprite2D_Slice9(this.deleter,a_drawpriority);
			this.down_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.down_sprite.SetVisible(false);

			this.lock_sprite = new Fee.Ui.Sprite2D_Slice9(this.deleter,a_drawpriority);
			this.lock_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.lock_sprite.SetVisible(false);

			//text
			this.text = Fee.Render2D.Text2D.Create(this.deleter,a_drawpriority);
			this.text.SetAlignmentType(Render2D.Text2D_HorizontalAlignmentType.Center,Render2D.Text2D_VerticalAlignmentType.Middle);

			//lock_textcolor
			this.nomal_textcolor = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
			this.on_textcolor = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
			this.down_textcolor = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
			this.lock_textcolor = new UnityEngine.Color(1.0f,1.0f,1.0f,1.0f);
		}

		/** 作成。
		*/
		public static Button Create(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			return new Button(a_deleter,a_drawpriority);
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

			this.UpdateTextColor();
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
			this.normal_sprite.SetClipRect(in this.clip_rect);
			this.on_sprite.SetClipRect(in this.clip_rect);
			this.down_sprite.SetClipRect(in this.clip_rect);
			this.lock_sprite.SetClipRect(in this.clip_rect);

			//text
			this.text.SetClipRect(in this.clip_rect);
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
			this.UpdateTextColor();
		}

		/** UpdateTextColor
		*/
		private void UpdateTextColor()
		{
			//sprite
			switch(this.mode){
			case Button_Mode.Normal:
				{
					this.text.SetColor(in this.nomal_textcolor);
				}break;
			case Button_Mode.On:
				{
					this.text.SetColor(in this.on_textcolor);
				}break;
			case Button_Mode.Down:
				{
					this.text.SetColor(in this.down_textcolor);
				}break;
			case Button_Mode.Lock:
				{
					this.text.SetColor(in this.lock_textcolor);
				}break;
			}
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

		/** ノーマルテキスト色。設定。
		*/
		public void SetNormalTextColor(in UnityEngine.Color a_color)
		{
			this.nomal_textcolor = a_color;
			this.UpdateTextColor();
		}

		/** ノーマルテキスト色。設定。
		*/
		public void SetNormalTextColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.nomal_textcolor.r = a_r;
			this.nomal_textcolor.g = a_g;
			this.nomal_textcolor.b = a_b;
			this.nomal_textcolor.a = a_a;
			this.UpdateTextColor();
		}

		/** オンテキスト色。設定。
		*/
		public void SetOnTextColor(in UnityEngine.Color a_color)
		{
			this.on_textcolor = a_color;
			this.UpdateTextColor();
		}

		/** オンテキスト色。設定。
		*/
		public void SetOnTextColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.on_textcolor.r = a_r;
			this.on_textcolor.g = a_g;
			this.on_textcolor.b = a_b;
			this.on_textcolor.a = a_a;
			this.UpdateTextColor();
		}

		/** ダウンテキスト色。設定。
		*/
		public void SetDownTextColor(in UnityEngine.Color a_color)
		{
			this.down_textcolor = a_color;
			this.UpdateTextColor();
		}

		/** ダウンテキスト色。設定。
		*/
		public void SetDownTextColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.down_textcolor.r = a_r;
			this.down_textcolor.g = a_g;
			this.down_textcolor.b = a_b;
			this.down_textcolor.a = a_a;
			this.UpdateTextColor();
		}

		/** ロックテキスト色。設定。
		*/
		public void SetLockTextColor(in UnityEngine.Color a_color)
		{
			this.lock_textcolor = a_color;
			this.UpdateTextColor();
		}

		/** ロックテキスト色。設定。
		*/
		public void SetLockTextColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.lock_textcolor.r = a_r;
			this.lock_textcolor.g = a_g;
			this.lock_textcolor.b = a_b;
			this.lock_textcolor.a = a_a;
			this.UpdateTextColor();
		}

		/** テキストフォントサイズ。設定。
		*/
		public void SetFontSize(int a_fontsize)
		{
			this.text.SetFontSize(a_fontsize);	
		}

		/**　テクスチャコーナーサイズ。設定。
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
			this.normal_sprite.SetRect(in this.rect);
			this.on_sprite.SetRect(in this.rect);
			this.down_sprite.SetRect(in this.rect);
			this.lock_sprite.SetRect(in this.rect);

			//text
			this.text.SetRect(this.rect.x + this.rect.w / 2,this.rect.y + this.rect.h / 2,0,0);
		}

		/** ノーマルテクスチャ。設定。
		*/
		public void SetNormalTexture(UnityEngine.Texture2D a_texture)
		{
			#if(UNITY_EDITOR)
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.filterMode != UnityEngine.FilterMode.Point)){
				Tool.Log("SetNormalTexture","filterMode != Point");
			}
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.mipmapCount != 1)){
				Tool.Log("SetNormalTexture","mipmapCount != 1");
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
				Tool.Log("SetNormalTexture","filterMode != Point");
			}
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.mipmapCount != 1)){
				Tool.Log("SetNormalTexture","mipmapCount != 1");
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
				Tool.Log("SetNormalTexture","filterMode != Point");
			}
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.mipmapCount != 1)){
				Tool.Log("SetNormalTexture","mipmapCount != 1");
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
				Tool.Log("SetNormalTexture","filterMode != Point");
			}
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.mipmapCount != 1)){
				Tool.Log("SetNormalTexture","mipmapCount != 1");
			}
			#endif

			this.lock_sprite.SetTexture(a_texture);
		}

		/** ノーマルテクスチャ矩形。設定。
		*/
		public void SetNormalTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.normal_sprite.SetTextureRect(in a_texture_rect);
		}

		/** オンテクスチャ矩形。設定。
		*/
		public void SetOnTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.on_sprite.SetTextureRect(in a_texture_rect);
		}

		/** ダウンテクスチャ矩形。設定。
		*/
		public void SetDownTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.down_sprite.SetTextureRect(in a_texture_rect);
		}

		/** ロックテクスチャ矩形。設定。
		*/
		public void SetLockTextureRect(in Fee.Geometry.Rect2D_R<float> a_texture_rect)
		{
			this.lock_sprite.SetTextureRect(in a_texture_rect);
		}

		/** ノーマル色。設定。
		*/
		public void SetNormalColor(in UnityEngine.Color a_color)
		{
			this.normal_sprite.SetColor(in a_color);
		}

		/** オン色。設定。
		*/
		public void SetOnColor(in UnityEngine.Color a_color)
		{
			this.on_sprite.SetColor(in a_color);
		}

		/** ダウン色。設定。
		*/
		public void SetDownColor(in UnityEngine.Color a_color)
		{
			this.down_sprite.SetColor(in a_color);
		}

		/** ロック色。設定。
		*/
		public void SetLockColor(in UnityEngine.Color a_color)
		{
			this.lock_sprite.SetColor(in a_color);
		}

		/** ノーマル色。設定。
		*/
		public void SetNormalColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.normal_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** オン色。設定。
		*/
		public void SetOnColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.on_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** ダウン色。設定。
		*/
		public void SetDownColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.down_sprite.SetColor(a_r,a_g,a_b,a_a);
		}

		/** ロック色。設定。
		*/
		public void SetLockColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.lock_sprite.SetColor(a_r,a_g,a_b,a_a);
		}
	}
}

