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
		/** [NUi.ScrollItem_Base]矩形。設定。
		*/
		void SetY(int a_y);

		/** [NUi.ScrollItem_Base]矩形。設定。
		*/
		void SetX(int a_x);

		/** [NUi.ScrollItem_Base]クリック。矩形。
		*/
		void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect);

		/** [NUi.ScrollItem_Base]表示内。
		*/
		void OnViewIn();

		/** [NUi.ScrollItem_Base]表示外。
		*/
		void OnViewOut();
	}
}

