

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
	/** Size2D
	*/
	public struct Size2D<Type>
	{
		/** wh
		*/
		public Type w;
		public Type h;

		/** constructor
		*/
		public Size2D(Type a_w,Type a_h)
		{
			this.w = a_w;
			this.h = a_h;
		}

		/** Set
		*/
		public void Set(Type a_w,Type a_h)
		{
			this.w = a_w;
			this.h = a_h;
		}
	}
}

