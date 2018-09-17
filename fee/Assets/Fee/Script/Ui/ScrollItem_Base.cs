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
	public interface ScrollItem_Base
	{
		/** 矩形。設定。
		*/
		void SetY(int a_y);

		/** 矩形。設定。
		*/
		void SetX(int a_x);

		/** クリック。矩形。
		*/
		void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect);

		/** 表示内。
		*/
		void OnViewIn();

		/** 表示外。
		*/
		void OnViewOut();
	}
}

