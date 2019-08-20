

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ジオメトリ。位置。
*/


/** Fee.Geometry
*/
namespace Fee.Geometry
{
	/** Pos2D
	*/
	public struct Pos2D<Type>
	{
		/** xy
		*/
		public Type x;
		public Type y;

		/** constructor
		*/
		public Pos2D(Type a_x,Type a_y)
		{
			this.x = a_x;
			this.y = a_y;
		}

		/** Set
		*/
		public void Set(Type a_x,Type a_y)
		{
			this.x = a_x;
			this.y = a_y;
		}
	}
}

