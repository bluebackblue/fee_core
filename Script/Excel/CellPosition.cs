

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エクセル。セルの位置。
*/


/** Fee.Excel
*/
namespace Fee.Excel
{
	/** セルの位置。
	*/
	public readonly struct CellPosition
	{
		/** xy
		*/
		public readonly int x;
		public readonly int y;

		/** constructor
		*/
		public CellPosition(int a_x,int a_y)
		{
			this.x = a_x;
			this.y = a_y;
		}
	}
}

