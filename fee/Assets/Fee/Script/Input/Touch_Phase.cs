using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。タッチ。フェイズ。
*/


/** NInput
*/
namespace NInput
{
	/** Touch_Phase
	*/
	public class Touch_Phase
	{
		/** value_x
		*/
		public int value_x;

		/** value_y
		*/
		public int value_y;

		/** 更新。
		*/
		public bool update;

		/** リセット。
		*/
		public void Reset()
		{
			//update
			this.update = false;

			//value_x
			this.value_x = 0;
	
			//value_y
			this.value_y = 0;
		}

		/** 設定。
		*/
		public void Set(int a_value_x,int a_value_y)
		{
			//value_x
			this.value_x = a_value_x;

			//value_y
			this.value_y = a_value_y;
		}

		/** 更新。
		*/
		public void Main()
		{
		}
    }
}

