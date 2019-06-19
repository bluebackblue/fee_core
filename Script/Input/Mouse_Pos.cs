

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。位置。
*/


/** Fee.Input
*/
namespace Fee.Input
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
		public bool action;

		/** リセット。
		*/
		public void Reset()
		{
			this.x = 0;
			this.y = 0;
			this.x_old = 0;
			this.y_old = 0;
			this.action = false;
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

		/** 更新。
		*/
		public void Main()
		{
			if((this.x != this.x_old)||(this.y != this.y_old)){
				this.action = true;
			}else{
				this.action = false;
			}
		}
	}
}

