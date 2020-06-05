

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
	/** NodePool
	*/
	public class NodePool<NODE>
		where NODE : class , new()
	{
		/** list
		*/
		private System.Collections.Generic.LinkedList<NODE> list;

		/** constructor
		*/
		public NodePool(int a_capacity)
		{
			//list
			this.list = new System.Collections.Generic.LinkedList<NODE>();
			for(int ii=0;ii<a_capacity;ii++){
				NODE t_raw = new NODE();
				System.Collections.Generic.LinkedListNode<NODE> t_node = new System.Collections.Generic.LinkedListNode<NODE>(t_raw);
				this.list.AddLast(t_node);
			}
		}

		/** Alloc
		*/
		public System.Collections.Generic.LinkedListNode<NODE> Alloc()
		{
			{
				System.Collections.Generic.LinkedListNode<NODE> t_node = this.list.Last;
				if(t_node != null){
					this.list.Remove(t_node);
					return t_node;
				}
			}

			{
				Tool.Assert(false);
				NODE t_raw = new NODE();
				return new System.Collections.Generic.LinkedListNode<NODE>(t_raw);
			}
		}

		/** Free
		*/
		public void Free(System.Collections.Generic.LinkedListNode<NODE> a_item)
		{
			if(a_item != null){
				this.list.AddLast(a_item);
			}else{
				Tool.Assert(false);
			}
		}

		/** GetList
		*/
		public System.Collections.Generic.LinkedList<NODE> GetList()
		{
			return this.list;
		}

		/** GetCount
		*/
		public int GetCount()
		{
			return this.list.Count;
		}
	}
}

