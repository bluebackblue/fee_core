

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。矩形。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Rect2D_R
	*/
	public struct Rect2D_R<Type>
	{
		/** xywh
		*/
		public Type x;
		public Type y;
		public Type w;
		public Type h;

		/** constructor
		*/
		public Rect2D_R(Type a_x,Type a_y,Type a_w,Type a_h)
		{
			this.x = a_x;
			this.y = a_y;
			this.w = a_w;
			this.h = a_h;
		}

		/** Set
		*/
		public void Set(Type a_x,Type a_y,Type a_w,Type a_h)
		{
			this.x = a_x;
			this.y = a_y;
			this.w = a_w;
			this.h = a_h;
		}
	}

	/** Rect2D_A
	*/
	public struct Rect2D_A<Type>
	{
		/** x1y1x2y2
		*/
		public Type x1;
		public Type y1;
		public Type x2;
		public Type y2;

		/** constructor
		*/
		public Rect2D_A(Type a_x1,Type a_y1,Type a_x2,Type a_y2)
		{
			this.x1 = a_x1;
			this.y1 = a_y1;
			this.x2 = a_x2;
			this.y2 = a_y2;
		}

		/** Set
		*/
		public void Set(Type a_x1,Type a_y1,Type a_x2,Type a_y2)
		{
			this.x1 = a_x1;
			this.y1 = a_y1;
			this.x2 = a_x2;
			this.y2 = a_y2;
		}
	}
}

