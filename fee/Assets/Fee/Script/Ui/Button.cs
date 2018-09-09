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
		private Button_Sprite2D sprite;

		/** constructor
		*/
		public Button(NDeleter.Deleter a_deleter,NRender2D.State2D a_state,long a_drawpriority,Button_Base.CallBack_Click a_callback_click,int a_callback_click_value)
			:
			base(a_deleter,a_state,a_drawpriority,a_callback_click,a_callback_click_value)
		{
			//sprite
			this.sprite = new Button_Sprite2D(this.deleter,a_state,a_drawpriority);
			this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
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
			this.sprite.SetRect(a_x,a_y,a_w,a_h);
		}

		/** コールバック。矩形。設定。
		*/
		protected override void OnSetRectCallBack(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.sprite.SetRect(ref a_rect);
		}

		/** コールバック。モード。設定。
		*/
		protected override void OnSetModeCallBack(Button_Mode a_mode)
		{
			this.sprite.SetMode(a_mode);
		}

		/** コールバック。クリップ。設定。
		*/
		protected override void OnSetClipCallBack(bool a_flag)
		{
			this.sprite.SetClip(a_flag);
		}

		/** コールバック。クリップ矩形。設定。
		*/
		protected override void OnSetClipRectCallBack(int a_x,int a_y,int a_w,int a_h)
		{
			this.sprite.SetClipRect(a_x,a_y,a_w,a_h);
		}

		/** コールバック。クリップ矩形。設定。
		*/
		protected override void OnSetClipRectCallBack(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.sprite.SetClipRect(ref a_rect);
		}

		/** コーブラック。表示。設定。
		*/
		protected override void OnSetVisibleCallBack(bool a_flag)
		{
			this.sprite.SetVisible(a_flag);
		}

		/** テクスチャ設定。
		*/
		public void SetTexture(Texture2D a_texture)
		{
			this.sprite.SetTexture(a_texture);
		}
	}
}

