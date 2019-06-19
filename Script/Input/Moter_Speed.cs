

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。アナログ。ボタン。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Moter_Speed
	*/
	public struct Moter_Speed
	{
		/** raw_value
		*/
		private float raw_value;

		/** totalframe
		*/
		private int totalframe;

		/** value
		*/
		private float value;

		/** リセット。
		*/
		public void Reset()
		{
			//raw_value
			this.raw_value = 0.0f;

			//totalframe
			this.totalframe = 0;

			//value
			this.value = 0.0f;
		}

		/** モータへの設定値。設定。
		*/
		public void SetRawValue(float a_value)
		{
			this.raw_value = a_value;
		}

		/** モータへの設定値。取得。
		*/
		public float GetRawValue()
		{
			return this.raw_value;
		}

		/** リクエスト。
		*/
		public void Request(float a_value)
		{
			this.value = a_value;
		}

		/** 更新。
		*/
		public void Main(int a_frame)
		{
			if(this.raw_value > 0.0f){
				this.totalframe += a_frame;

			}
		}

		/** 合計フレーム。
		*/
		public int GetTotalTime()
		{
			return this.totalframe;
		}

		/** 取得。
		*/
		public float GetValue()
		{
			return this.value;
		}
	}
}

