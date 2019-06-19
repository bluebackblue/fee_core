

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。アナログ。スティック。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Analog_Stick
	*/
	public struct Analog_Stick
	{
		/** 向き。
		*/
		public float x;
		public float y;
		public float x_old;
		public float y_old;

		/** リセット。
		*/
		public void Reset()
		{
			this.x = 0.0f;
			this.y = 0.0f;
			this.x_old = 0.0f;
			this.y_old = 0.0f;
		}

		/** 設定。
		*/
		public void Set(float a_x,float a_y)
		{
			this.x_old = this.x;
			this.y_old = this.y;
			this.x = a_x;
			this.y = a_y;
		}

		/** 更新。
		*/
		public void Main()
		{
		}
    }
}

