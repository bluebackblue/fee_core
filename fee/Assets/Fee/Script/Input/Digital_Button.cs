using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief 入力。デジタル。ボタン。
*/


/** NInput
*/
namespace NInput
{
	/** Digital_Button
	*/
	public struct Digital_Button
	{
		/** flag
		*/
		public bool on_old;
		public bool on;
		public bool down;
		public bool up;

		/** リセット。
		*/
		public void Reset()
		{
			this.on_old = false;
			this.on = false;
			this.down = false;
			this.up = false;
		}

		/** 設定。
		*/
		public void Set(bool a_flag)
		{
			this.on_old = this.on;
			this.on = a_flag;
		}

		/** 更新。
		*/
		public void Main()
		{
			if((this.on == true)&&(this.on_old == false)){
				//ダウン。
				this.down = true;
			}else if((this.on == false)&&(this.on_old == true)){
				//アップ。
				this.up = true;
			}else{
				this.down = false;
				this.up = false;
			}
		}
	}
}

