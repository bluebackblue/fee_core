

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
	/** BufferNodeList_Base
	*/
	public interface BufferNodeList_Base<NODE,BUFFER>
	{
		/** Alloc
		*/
		System.Collections.Generic.LinkedListNode<NODE> Alloc();

		/** Free
		*/
		void Free(System.Collections.Generic.LinkedListNode<NODE> a_item);

		/** GetFreeList
		*/
		System.Collections.Generic.LinkedList<NODE> GetFreeList();

		/** GetUseList
		*/
		System.Collections.Generic.LinkedList<NODE> GetUseList();

		/** GetBuffer
		*/
		BUFFER[] GetBuffer();

		/** GetUseCount
		*/
		int GetUseCount();

		/** GetFreeCount
		*/
		int GetFreeCount();

		/** 隙間を埋める。
		*/
		void GarbageCollection();

		/** 隙間を埋める。使用リストの順序に合わせてバッファを入れ替える。
		*/
		void GarbageCollectionSwapUseListOder();
	}
}

