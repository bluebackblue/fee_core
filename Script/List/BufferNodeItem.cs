

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief リスト。
*/


/** Fee.List
*/
namespace Fee.List
{
	/** BufferNodeItem
	*/
	public class BufferNodeItem
	{
		/** buffer_index
		*/
		private int buffer_index;

		/** GetBufferIndex
		*/
		public int GetBufferIndex()
		{
			return this.buffer_index;
		}

		/** SetBufferIndex
		*/
		public void SetBufferIndex(int a_index)
		{
			this.buffer_index = a_index;
		}
	}
}

