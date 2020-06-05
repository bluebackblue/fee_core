

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
	/** BufferNodeList
	*/
	public class BufferNodeList<NODE,BUFFER> : BufferNodeList_Base<NODE,BUFFER>
		where NODE : BufferNodeItem , new()
	{
		/** list_free
		*/
		private System.Collections.Generic.LinkedList<NODE> list_free;

		/** list_use
		*/
		private System.Collections.Generic.LinkedList<NODE> list_use;

		/** buffer
		*/
		private BUFFER[] buffer;

		/** constructor
		*/
		public BufferNodeList(BUFFER[] a_buffer)
		{
			//list_free
			this.list_free = new System.Collections.Generic.LinkedList<NODE>();
			for(int ii=0;ii<a_buffer.Length;ii++){
				NODE t_raw = new NODE();
				t_raw.SetBufferIndex(a_buffer.Length - ii - 1);
				System.Collections.Generic.LinkedListNode<NODE> t_node = new System.Collections.Generic.LinkedListNode<NODE>(t_raw);
				this.list_free.AddLast(t_node);
			}

			//use
			this.list_use = new System.Collections.Generic.LinkedList<NODE>();

			//buffer
			this.buffer = a_buffer;
		}

		/** Alloc
		*/
		public System.Collections.Generic.LinkedListNode<NODE> Alloc()
		{
			System.Collections.Generic.LinkedListNode<NODE> t_node = this.list_free.Last;
			if(t_node != null){
				this.list_free.Remove(t_node);
				this.list_use.AddLast(t_node);
				return t_node;
			}
			return null;
		}

		/** Free
		*/
		public void Free(System.Collections.Generic.LinkedListNode<NODE> a_item)
		{
			if(a_item != null){
				this.list_use.Remove(a_item);
				this.list_free.AddLast(a_item);
			}else{
				Tool.Assert(false);
			}
		}

		/** GetFreeList
		*/
		public System.Collections.Generic.LinkedList<NODE> GetFreeList()
		{
			return this.list_use;
		}

		/** GetUseList
		*/
		public System.Collections.Generic.LinkedList<NODE> GetUseList()
		{
			return this.list_use;
		}

		/** GetBuffer
		*/
		public BUFFER[] GetBuffer()
		{
			return this.buffer;
		}

		/** GetUseCount
		*/
		public int GetUseCount()
		{
			return this.list_use.Count;
		}

		/** GetFreeCount
		*/
		public int GetFreeCount()
		{
			return this.list_free.Count;
		}

		/** 隙間を埋める。
		*/
		public void GarbageCollection()
		{
			GarbageCollection(this.list_use,this.list_free,this.buffer);
		}

		/** 隙間を埋める。使用リストの順序に合わせてバッファを入れ替える。
		*/
		public void GarbageCollectionSwapUseListOder()
		{
			GarbageCollectionSwapUseListOder(this.list_use,this.list_free,this.buffer);
		}

		/** バッファをスワップする。
		*/
		private static void SwapBuffer(System.Collections.Generic.LinkedListNode<NODE> a_node_a,System.Collections.Generic.LinkedListNode<NODE> a_node_b,BUFFER[] a_buffer)
		{
			int t_index_a = a_node_a.Value.GetBufferIndex();
			int t_index_b = a_node_b.Value.GetBufferIndex();

			BUFFER t_temp = a_buffer[t_index_a];
			a_buffer[t_index_a] = a_buffer[t_index_b];
			a_buffer[t_index_b] = t_temp;

			a_node_a.Value.SetBufferIndex(t_index_b);
			a_node_b.Value.SetBufferIndex(t_index_a);
		}

		/** 隙間を埋める。
		*/
		private static void GarbageCollection(System.Collections.Generic.LinkedList<NODE> a_list_use,System.Collections.Generic.LinkedList<NODE> a_list_free,BUFFER[] a_buffer)
		{
			int t_max = a_list_use.Count;

			//node
			System.Collections.Generic.LinkedListNode<NODE> t_node_use = BufferNodeFind<NODE>.FindGreaterEequalIndexNode(a_list_use.First,t_max);
			System.Collections.Generic.LinkedListNode<NODE> t_node_free = a_list_free.First;

			while(t_node_use != null){
				//t_node_use : 未使用にすべきノード。
				//t_node_free : 使用にすべきノード。
				t_node_free = BufferNodeFind<NODE>.FindLessIndexNode(t_node_free,t_max);

				//バッファをスワップする。
				SwapBuffer(t_node_free,t_node_use,a_buffer);

				//次の未使用にすべきノード。
				t_node_use = BufferNodeFind<NODE>.FindGreaterEequalIndexNode(t_node_use,t_max);
			}
		}

		/** 隙間を埋める。使用リストの順序に合わせてバッファを入れ替える。
		*/
		private static void GarbageCollectionSwapUseListOder(System.Collections.Generic.LinkedList<NODE> a_list_use,System.Collections.Generic.LinkedList<NODE> a_list_free,BUFFER[] a_buffer)
		{
			int t_max = a_list_use.Count;

			System.Collections.Generic.LinkedListNode<NODE> t_node = a_list_use.First;
			for(int ii=0;ii<t_max;ii++){
				if(t_node.Value.GetBufferIndex() != ii){
					System.Collections.Generic.LinkedListNode<NODE> t_node_next = t_node.Next;

					//t_node : 未使用にすべきノード。
					//t_node_find : 使用にすべきノード。

					//インデックスがiiのノードを検索。使用リストから。
					System.Collections.Generic.LinkedListNode<NODE> t_node_find = BufferNodeFind<NODE>.FindEequalIndexNode(a_list_use.First,ii);
					if(t_node_find == null){
						//インデックスがiiのノードを検索。未使用リストから。
						t_node_find = BufferNodeFind<NODE>.FindEequalIndexNode(a_list_free.First,ii);
					}

					//バッファをスワップする。
					SwapBuffer(t_node,t_node_find,a_buffer);

					t_node = t_node_next;
				}
			}
		}
	}
}

