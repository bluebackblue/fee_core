using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。縦スクロール。
*/


/** NUi
*/
namespace NUi
{
	/** Scroll_Vertical
	*/
	public class Scroll_Vertical<ITEM> : Scroll_Vertical_Base<ITEM>
		where ITEM : ScrollItem_Base
	{
		/** 背景。
		*/
		private NRender2D.Sprite2D bg;

		/** バー。
		*/
		private NRender2D.Sprite2D bar;

		/** constructor
		*/
		public Scroll_Vertical(NDeleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
			:
			base(a_deleter,a_drawpriority,a_item_length)
		{
			//背景。
			this.bg = new NRender2D.Sprite2D(a_deleter,null,a_drawpriority);
			this.bg.SetTexture(Texture2D.whiteTexture);
			this.bg.SetRect(0,0,0,0);
			this.bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetColor(0.0f,0.0f,0.0f,0.1f);

			//バー。
			this.bar = new NRender2D.Sprite2D(a_deleter,null,a_drawpriority + 1);
			this.bar.SetTexture(Texture2D.whiteTexture);
			this.bar.SetRect(0,0,5,5);
			this.bar.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bar.SetColor(1.0f,1.0f,1.0f,0.1f);

			this.bar.SetVisible(false);
		}

		/** [Scroll_Vertical_Base]コールバック。矩形。設定。
		*/
		protected override void OnSetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.bg.SetRect(a_x,a_y,a_w,a_h);
			this.bar.SetX(this.rect.x - 10);
		}

		/** [Scroll_Base]コールバック。表示位置変更。
		*/
		public override void OnChangeViewPosition()
		{
			this.UpdateBar();
		}

		/** [Scroll_Horizontal_Base]コールバック。
		*/
		public override void OnChangeListCount()
		{
			this.UpdateBar();
		}

		/** 更新。バー表示。
		*/
		private void UpdateBar()
		{
			if(this.list.Count <= 0){
				this.bar.SetVisible(false);
			}else{
				this.bar.SetVisible(true);

				int t_position_max = this.item_length * this.list.Count - this.view_length;
				float t_per = (float)this.view_position / t_position_max;
				int t_offset = (int)(t_per * (this.view_length - this.bar.GetH()));
				this.bar.SetY(this.rect.y + t_offset);
			}
		}
	}
}

