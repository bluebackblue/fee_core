using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スクロールアイテム。
*/


/** NUi
*/
namespace NUi
{
	/** ScrollItem_Base
	*/
	public abstract class ScrollItem_Base
	{
		/** is_viewin
		*/
		private bool is_viewin;

		/** capture_viewout
		*/
		private bool capture_viewout;

		/** constructor
		*/
		public ScrollItem_Base()
		{
			this.is_viewin = false;
		}

		/** 表示。チェック。
		*/
		public bool IsViewIn()
		{
			return this.is_viewin;
		}

		/** 表示。設定。
		*/
		public void SetViewIn(bool a_flag)
		{
			this.is_viewin = a_flag;
		}

		/** キャプチャアイテム表示範囲外移動フラグ。設定。
		*/
		public void SetCaptureViewOutFlag(bool a_flag)
		{
			this.capture_viewout = a_flag;
		}

		/** キャプチャアイテム表示範囲外移動フラグ。取得。
		*/
		public bool GetCaptureViewOutFlag()
		{
			return this.capture_viewout;
		}

		/** [NUi.ScrollItem_Base]矩形。設定。
		*/
		public abstract void SetY(int a_y);

		/** [NUi.ScrollItem_Base]矩形。設定。
		*/
		public abstract void SetX(int a_x);

		/** [NUi.ScrollItem_Base]クリップ矩形。設定。
		*/
		public abstract void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect);

		/** [NUi.ScrollItem_Base]表示内。
		*/
		public abstract void OnViewIn();

		/** [NUi.ScrollItem_Base]表示外。
		*/
		public abstract void OnViewOut();
	}
}

