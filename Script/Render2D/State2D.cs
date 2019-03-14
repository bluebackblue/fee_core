using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。状態。
*/


/** Render2D
*/
namespace NRender2D
{
	/** State2D
	*/
	public class State2D
	{
		/** 表示。
		*/
		private bool visible;

		/** constructor
		*/
		public State2D(bool a_visible)
		{
			this.visible = a_visible;
		}

		/** 表示。取得。
		*/
		public bool IsVisible()
		{
			return this.visible;
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			this.visible = a_flag;
		}
	}
}

