﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。横スクロール。
*/


/** NUi
*/
namespace NUi
{
	/** Scroll_Horizontal_Base
	*/
	public abstract class Scroll_Horizontal_Base<ITEM> : Scroll_List_Base<ITEM>
		where ITEM : ScrollItem_Base
	{
		/** constructor
		*/
		public Scroll_Horizontal_Base(NDeleter.Deleter a_deleter,int a_item_length)
			:
			base(a_deleter,a_item_length)
		{
		}

		/** アイテムの位置更新。スクロール方向の座標。
		*/
		protected override void UpdateItemPos(ITEM a_item,int a_index)
		{
			int t_x = this.rect.x + a_index * this.item_length - this.view_position;
			a_item.SetX(t_x);
		}

		/** アイテムの位置更新。スクロール方向では座標。
		*/
		protected override void UpdateItemOtherPos(ITEM a_item)
		{
			a_item.SetY(this.rect.y);
		}

		/** 表示幅更新。
		*/
		protected override void UpdateViewLength()
		{
			this.view_length = this.rect.w;
		}
	}
}

