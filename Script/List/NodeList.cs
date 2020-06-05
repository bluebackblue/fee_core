

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
	/** NodeList
	*/
	public class NodeList<NODE>
		where NODE : class , new()
	{
		/** list_free
		*/
		private System.Collections.Generic.LinkedList<NODE> list_free;

		/** list_use
		*/
		private System.Collections.Generic.LinkedList<NODE> list_use;

		/** constructor
		*/
		public NodeList(int a_capacity)
		{
			//list_free
			this.list_free = new System.Collections.Generic.LinkedList<NODE>();
			for(int ii=0;ii<a_capacity;ii++){
				NODE t_raw = new NODE();
				System.Collections.Generic.LinkedListNode<NODE> t_node = new System.Collections.Generic.LinkedListNode<NODE>(t_raw);
				this.list_free.AddLast(t_node);
			}

			//use
			this.list_use = new System.Collections.Generic.LinkedList<NODE>();
		}

		/** Alloc
		*/
		public System.Collections.Generic.LinkedListNode<NODE> Alloc()
		{
			{
				System.Collections.Generic.LinkedListNode<NODE> t_node = this.list_free.Last;
				if(t_node != null){
					this.list_free.Remove(t_node);
					this.list_use.AddLast(t_node);
					return t_node;
				}
			}

			{
				Tool.Assert(false);
				NODE t_raw = new NODE();
				System.Collections.Generic.LinkedListNode<NODE> t_node = new System.Collections.Generic.LinkedListNode<NODE>(t_raw);
				this.list_use.AddLast(t_node);
				return t_node;
			}
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
	}
}

