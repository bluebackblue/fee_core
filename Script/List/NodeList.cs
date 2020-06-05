

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
	public class NodeList<NODE,BUFFER>
		where NODE : NodeItem , new()
	{
		/** list_free
		*/
		public System.Collections.Generic.LinkedList<NODE> list_free;

		/** list_use
		*/
		public System.Collections.Generic.LinkedList<NODE> list_use;

		/** buffer
		*/
		public BUFFER[] buffer;

		/** constructor
		*/
		public NodeList(BUFFER[] a_buffer)
		{
			//list_free
			this.list_free = new System.Collections.Generic.LinkedList<NODE>();
			for(int ii=0;ii<a_buffer.Length;ii++){
				NODE t_raw = new NODE();
				t_raw.SetNodeIndex(a_buffer.Length - ii - 1);
				this.list_free.AddLast(new System.Collections.Generic.LinkedListNode<NODE>(t_raw));
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

		/** FindLessIndex

			a_indexより小さい。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindLessIndex(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
		{
			System.Collections.Generic.LinkedListNode<NODE> t_node = a_node;
			do{
				if(t_node.Value.GetNodeIndex() < a_index){
					return t_node;
				}

				t_node = t_node.Next;
			}while(t_node != null);

			return null;
		}

		/** FindGreaterIndex

			a_indexより大きい。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindGreaterIndex(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
		{
			System.Collections.Generic.LinkedListNode<NODE> t_node = a_node;
			do{
				if(t_node.Value.GetNodeIndex() > a_index){
					return t_node;
				}

				t_node = t_node.Next;
			}while(t_node != null);

			return null;
		}

		/** FindLessEequalIndex

			a_index以下。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindLessEequalIndex(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
		{
			System.Collections.Generic.LinkedListNode<NODE> t_node = a_node;
			do{
				if(t_node.Value.GetNodeIndex() <= a_index){
					return t_node;
				}

				t_node = t_node.Next;
			}while(t_node != null);

			return null;
		}

		/** FindGreaterEequalIndex

			a_index以上。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindGreaterEequalIndex(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
		{
			System.Collections.Generic.LinkedListNode<NODE> t_node = a_node;

			while(t_node != null){
				if(t_node.Value.GetNodeIndex() >= a_index){
					return t_node;
				}

				t_node = t_node.Next;
			}

			return null;
		}

		/** 隙間を埋める。
		*/
		public void GarbageCollection()
		{
			int t_max = this.list_use.Count;

			//未使用にすべきノード。
			System.Collections.Generic.LinkedListNode<NODE> t_node_use = FindGreaterEequalIndex(this.list_use.First,t_max);
			System.Collections.Generic.LinkedListNode<NODE> t_node_free = this.list_free.First;

			while(t_node_use != null){
				//使用にすべきノード。
				t_node_free = FindLessIndex(t_node_free,t_max);
				{
					int t_index_free = t_node_free.Value.GetNodeIndex();
					int t_index_use = t_node_use.Value.GetNodeIndex();

					//スワップ。
					BUFFER t_temp = this.buffer[t_index_free];
					this.buffer[t_index_free] = this.buffer[t_index_use];
					this.buffer[t_index_use] = t_temp;

					//スワップ。
					t_node_use.Value.SetNodeIndex(t_index_free);
					t_node_free.Value.SetNodeIndex(t_index_use);
				}

				//次の未使用にすべきノード。
				t_node_use = FindGreaterEequalIndex(t_node_use,t_max);
			}
		}
	}
}

