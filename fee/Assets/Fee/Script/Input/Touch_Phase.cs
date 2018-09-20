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
		/** index
		*/
		public int index;

		/** phase。
		*/
		public UnityEngine.Experimental.Input.PointerPhase phase;

		/** touch_id
		*/
		public int touch_id;

		/** phase_flag
		*/
		public bool phase_flag;

		/** value_x
		*/
		public int value_x;

		/** value_y
		*/
		public int value_y;

		/** リセット。
		*/
		public void Reset()
		{
			//index
			this.index = 0;

			//phase
			this.phase = UnityEngine.Experimental.Input.PointerPhase.None;

			//touch_id
			this.touch_id = 0;

			//phase_flag
			this.phase_flag = false;

			//value_x
			this.value_x = 0;
	
			//value_y
			this.value_y = 0;
		}

		/** 設定。
		*/
		public void Set(int a_index,UnityEngine.Experimental.Input.PointerPhase a_phase,int a_touch_id,int a_value_x,int a_value_y)
		{
			//index
			this.index = a_index;

			//phase
			this.phase = a_phase;

			//touch_id
			this.touch_id = a_touch_id;

			//value_x
			this.value_x = a_value_x;

			//value_y
			this.value_y = a_value_y;
		}

		/** 更新。
		*/
		public void Main()
		{
			switch(this.phase){
			case UnityEngine.Experimental.Input.PointerPhase.Began:
				{
					this.phase_flag = true;
				}break;
			case UnityEngine.Experimental.Input.PointerPhase.Stationary:
			case UnityEngine.Experimental.Input.PointerPhase.Moved:
				{
				}break;
			case UnityEngine.Experimental.Input.PointerPhase.Cancelled:
			case UnityEngine.Experimental.Input.PointerPhase.Ended:
			case UnityEngine.Experimental.Input.PointerPhase.None:
			default:
				{
					this.phase_flag = false;
					this.value_x = 0;
					this.value_y = 0;
				}break;
			}
		}
    }
}

