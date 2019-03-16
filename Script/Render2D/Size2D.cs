

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。サイズ。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
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

