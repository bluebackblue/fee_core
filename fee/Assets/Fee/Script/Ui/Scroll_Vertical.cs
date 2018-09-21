﻿using System.Collections;
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
			base(a_deleter,a_item_length)
		{
			//背景。
			this.bg = new NRender2D.Sprite2D(a_deleter,null,a_drawpriority);
			this.bg.SetTexture(Texture2D.whiteTexture);
			this.bg.SetRect(0,0,0,0);
			this.bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetColor(0.0f,0.0f,0.0f,0.1f);
			this.bg.SetMaterialType(NRender2D.Config.MaterialType.Alpha);

			//バー。
			this.bar = new NRender2D.Sprite2D(a_deleter,null,a_drawpriority + 1);
			this.bar.SetTexture(Texture2D.whiteTexture);
			this.bar.SetRect(0,0,5,5);
			this.bar.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bar.SetColor(1.0f,1.0f,1.0f,0.3f);
			this.bar.SetMaterialType(NRender2D.Config.MaterialType.Alpha);

			this.bar.SetVisible(false);
		}

		/** [Scroll_Vertical_Base]コールバック。矩形。設定。
		*/
		protected override void OnChangeRect()
		{
			this.bg.SetRect(ref this.rect);
			this.bar.SetX(this.rect.x - 10);
		}

		/** [Scroll_Vertical_Base]コールバック。表示位置変更。
		*/
		protected override void OnChangeViewPosition()
		{
			this.UpdateBar();
		}

		/** [Scroll_Vertical_Base]コールバック。リスト数変更。
		*/
		protected override void OnChangeListCount()
		{
			this.UpdateBar();
		}

		/** 更新。バー表示。
		*/
		private void UpdateBar()
		{
			int t_position_max = this.item_length * this.list.Count - this.view_length;

			if(t_position_max <= 0){
				this.bar.SetVisible(false);
			}else{
				this.bar.SetVisible(true);

				float t_offset_per = (float)this.view_position / t_position_max;
				float t_length_per = (float)this.view_length / (this.item_length * this.list.Count);

				int t_offset = (int)(t_offset_per * (this.view_length - this.bar.GetH()));

				this.bar.SetY(this.rect.y + t_offset);
				this.bar.SetH((int)(this.rect.h * t_length_per));
			}
		}
	}
}

