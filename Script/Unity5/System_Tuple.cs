

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＮＩＴＹ５。
*/


/** Fee.Unity5
*/
#if(UNITY_5)
namespace Fee.Unity5
{
	/** Tuple
	*/
	class Tuple<A,B>
	{
		/** Item1
		*/
		public A Item1;

		/** Item2
		*/
		public B Item2;

		/** constructor
		*/
		public Tuple(A a_item_1,B a_item_2)
		{
			this.Item1 = a_item_1;
			this.Item2 = a_item_2;
		}
	}
}
#endif

