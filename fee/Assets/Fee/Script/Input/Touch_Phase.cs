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
	public struct Touch_Phase
	{
		/** 向き。
		*/
		public bool phase_flag;

		/** リセット。
		*/
		public void Reset()
		{
			this.phase_flag = false;
		}

		/** 設定。
		*/
		public void Set(bool a_phase_flag)
		{
			this.phase_flag = a_phase_flag;
		}

		/** 更新。
		*/
		public void Main()
		{
		}
    }
}

