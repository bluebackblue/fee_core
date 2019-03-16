

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。位置。
*/


/** NFee.Rect2D
*/
namespace Fee.Render2D
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

