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
		/** exist
		*/
		public bool exist;

		/** phase。
		*/
		public UnityEngine.Experimental.Input.PointerPhase phase;

		/** phase_flag
		*/
		public bool phase_flag;

		/** value_x
		*/
		public float value_x;

		/** value_y
		*/
		public float value_y;

		/** リセット。
		*/
		public void Reset()
		{
			//exist
			this.exist = false;

			//phase
			this.phase = UnityEngine.Experimental.Input.PointerPhase.None;

			//phase_flag
			this.phase_flag = false;

			//value_x
			this.value_x = 0.0f;
	
			//value_y
			this.value_y = 0;
		}

		/** 設定。
		*/
		public void Set(bool a_flag,UnityEngine.Experimental.Input.PointerPhase a_phase,float a_value_x,float a_value_y)
		{
			//exit
			this.exist = a_flag;

			//phase
			this.phase = a_phase;


			if(a_phase == UnityEngine.Experimental.Input.PointerPhase.Moved){
				//value_x
				this.value_x = a_value_x;

				//value_y
				this.value_y = a_value_y;
			}
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
			case UnityEngine.Experimental.Input.PointerPhase.Cancelled:
			case UnityEngine.Experimental.Input.PointerPhase.Ended:
			case UnityEngine.Experimental.Input.PointerPhase.None:
				{
					this.phase_flag = false;
					this.value_x = 0.0f;
					this.value_y = 0.0f;
				}break;
			}
		}
    }
}

