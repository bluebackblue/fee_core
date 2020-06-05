

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
	/** BufferNodeListWithIndexList
	*/
	public class BufferNodeListWithIndexList<NODE,BUFFER> : BufferNodeList_Base<NODE,BUFFER>
		where NODE : BufferNodeItem , new()
	{
		/** indexlist
		*/
		private BufferNodeIndexList<NODE> indexlist;

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
		public BufferNodeListWithIndexList(BUFFER[] a_buffer)
		{
			//list_free
			this.list_free = new System.Collections.Generic.LinkedList<NODE>();
			for(int ii=0;ii<a_buffer.Length;ii++){
				NODE t_raw = new NODE();
				t_raw.SetBufferIndex(a_buffer.Length - ii - 1);
				System.Collections.Generic.LinkedListNode<NODE> t_node = new System.Collections.Generic.LinkedListNode<NODE>(t_raw);
				this.list_free.AddLast(t_node);
			}

			//indexlist
			this.indexlist = new BufferNodeIndexList<NODE>(this.list_free);

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
			GarbageCollectionWithIndexList(this.indexlist,this.list_use,this.list_free,this.buffer);
		}

		/** 隙間を埋める。使用リストの順序に合わせてバッファを入れ替える。
		*/
		public void GarbageCollectionSwapUseListOder()
		{
			GarbageCollectionSwapUseListOderWithIndexList(this.indexlist,this.list_use,this.list_free,this.buffer);
		}

		/** バッファをスワップする。
		*/
		private static void SwapBuffer(in BufferNodeIndexList<NODE> a_indexlist,System.Collections.Generic.LinkedListNode<NODE> a_node_a,System.Collections.Generic.LinkedListNode<NODE> a_node_b,BUFFER[] a_buffer)
		{
			int t_index_a = a_node_a.Value.GetBufferIndex();
			int t_index_b = a_node_b.Value.GetBufferIndex();

			BUFFER t_temp = a_buffer[t_index_a];
			a_buffer[t_index_a] = a_buffer[t_index_b];
			a_buffer[t_index_b] = t_temp;

			a_node_a.Value.SetBufferIndex(t_index_b);
			a_node_b.Value.SetBufferIndex(t_index_a);

			a_indexlist.SetNode(t_index_b,a_node_a);
			a_indexlist.SetNode(t_index_a,a_node_b);
		}

		/** 隙間を埋める。
		*/
		private static void GarbageCollectionWithIndexList(in BufferNodeIndexList<NODE> a_indexlist,System.Collections.Generic.LinkedList<NODE> a_list_use,System.Collections.Generic.LinkedList<NODE> a_list_free,BUFFER[] a_buffer)
		{
			int t_max = a_list_use.Count;

			int t_index_use = a_indexlist.FindInListIndexToStart(a_list_use,a_indexlist.GetCount() - 1);
			int t_index_free = 0;

			while(t_index_use >= t_max){
				//t_index_use : 未使用にすべきノード。
				//t_index_free : 使用にすべきノード。
				t_index_free = a_indexlist.FindInListIndexToEnd(a_list_free,t_index_free);

				//バッファをスワップする。
				SwapBuffer(a_indexlist,a_indexlist.GetNode(t_index_free),a_indexlist.GetNode(t_index_use),a_buffer);
			}
		}

	
		/** 隙間を埋める。使用リストの順序に合わせてバッファを入れ替える。
		*/
		private static void GarbageCollectionSwapUseListOderWithIndexList(in BufferNodeIndexList<NODE> a_indexlist,System.Collections.Generic.LinkedList<NODE> a_list_use,System.Collections.Generic.LinkedList<NODE> a_list_free,BUFFER[] a_buffer)
		{
			int t_max = a_list_use.Count;

			System.Collections.Generic.LinkedListNode<NODE> t_node = a_list_use.First;
			for(int ii=0;ii<t_max;ii++){
				if(t_node.Value.GetBufferIndex() != ii){
					System.Collections.Generic.LinkedListNode<NODE> t_node_next = t_node.Next;

					//t_node : 未使用にすべきノード。
					//t_node_find : 使用にすべきノード。
					System.Collections.Generic.LinkedListNode<NODE> t_node_find = a_indexlist.GetNode(ii);

					//バッファをスワップする。
					SwapBuffer(a_indexlist,t_node,t_node_find,a_buffer);

					t_node = t_node_next;
				}
			}
		}
	}
}

