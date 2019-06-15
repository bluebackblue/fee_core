

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
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
		private Fee.Ui.Button_Sprite2D sprite;

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
			this.sprite = new Fee.Ui.Button_Sprite2D(this.deleter,a_drawpriority);
			this.sprite.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);

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
		public void SetTexture(UnityEngine.Texture2D a_texture)
		{
			#if(UNITY_EDITOR)
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.filterMode != UnityEngine.FilterMode.Point)){
				Tool.Assert(false);
			}
			if((Config.BUTTONTEXTURE_CHECK_FILTERMODE_ENABLE == true)&&(a_texture.mipmapCount != 1)){
				Tool.Assert(false);
			}
			#endif

			this.sprite.SetTexture(a_texture);
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

