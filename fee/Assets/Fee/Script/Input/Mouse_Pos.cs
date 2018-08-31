using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。位置。
*/


/** NInput
*/
namespace NInput
{
	/** Mouse_Pos
	*/
	public struct Mouse_Pos
	{
		/** 位置。
		*/
		public int x;
		public int y;
		public int x_old;
		public int y_old;

		/** リセット。
		*/
		public void Reset()
		{
			this.x = 0;
			this.y = 0;
			this.x_old = 0;
			this.y_old = 0;
		}

		/** 設定。
		*/
		public void Set(int a_x,int a_y)
		{
			this.x_old = this.x;
			this.y_old = this.y;
			this.x = a_x;
			this.y = a_y;
		}
	}
}

