

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
	/** NodeItem
	*/
	public class NodeItem
	{
		/** index
		*/
		private int index;

		/** GetNodeIndex
		*/
		public int GetNodeIndex()
		{
			return this.index;
		}

		/** SetNodeIndex
		*/
		public void SetNodeIndex(int a_index)
		{
			this.index = a_index;
		}
	}

	/** NodeList
	*/
	public class NodeList<T,U>
		where T : NodeItem , new()
		where U : struct
	{
		/** free
		*/
		public System.Collections.Generic.LinkedList<T> free;
		public System.Collections.Generic.LinkedList<T> use;

		/** buffer
		*/
		public U[] buffer;

		/** constructor
		*/
		public NodeList(U[] a_buffer)
		{
			//free
			this.free = new System.Collections.Generic.LinkedList<T>();
			for(int ii=0;ii<a_buffer.Length;ii++){
				T t_raw = new T();
				t_raw.SetNodeIndex(a_buffer.Length - ii - 1);
				this.free.AddLast(new System.Collections.Generic.LinkedListNode<T>(t_raw));
			}

			//use
			this.use = new System.Collections.Generic.LinkedList<T>();

			//buffer
			this.buffer = a_buffer;
		}

		/** Alloc
		*/
		public System.Collections.Generic.LinkedListNode<T> Alloc()
		{
			System.Collections.Generic.LinkedListNode<T> t_node = this.free.Last;
			if(t_node != null){
				this.free.Remove(t_node);
				this.use.AddLast(t_node);
				return t_node;
			}
			return null;
		}

		/** Free
		*/
		public void Free(System.Collections.Generic.LinkedListNode<T> a_item)
		{
			if(a_item != null){
				this.use.Remove(a_item);
				this.free.AddLast(a_item);
			}else{
				Tool.Assert(false);
			}
		}

		/** GetUseCount
		*/
		public int GetUseCount()
		{
			return this.use.Count;
		}

		/** GetFreeCount
		*/
		public int GetFreeCount()
		{
			return this.free.Count;
		}

		/** FindLessIndex

			return < a_index : 検索

		*/
		public static System.Collections.Generic.LinkedListNode<T> FindLessIndex(System.Collections.Generic.LinkedListNode<T> a_node,int a_index)
		{
			System.Collections.Generic.LinkedListNode<T> t_node = a_node;
			do{
				if(t_node.Value.GetNodeIndex() < a_index){
					return t_node;
				}

				t_node = t_node.Next;
			}while(t_node != null);

			return null;
		}

		/** FindGreaterEequalIndex

			return >= a_index : 検索。

		*/
		public static System.Collections.Generic.LinkedListNode<T> FindGreaterEequalIndex(System.Collections.Generic.LinkedListNode<T> a_node,int a_index)
		{
			System.Collections.Generic.LinkedListNode<T> t_node = a_node;
			do{
				if(t_node.Value.GetNodeIndex() >= a_index){
					return t_node;
				}

				t_node = t_node.Next;
			}while(t_node != null);

			return null;
		}

		/** 隙間を埋める。
		*/
		public void GarbageCollection()
		{
			int t_max = this.use.Count;

			//未使用にすべきノード。
			System.Collections.Generic.LinkedListNode<T> t_node_use = Fee.List.NodeList<T,U>.FindGreaterEequalIndex(this.use.First,t_max);
			System.Collections.Generic.LinkedListNode<T> t_node_free = this.free.First;

			while(t_node_use != null){
				//使用にすべきノード。
				t_node_free = Fee.List.NodeList<T,U>.FindLessIndex(t_node_free,t_max);
				{
					int t_index_free = t_node_free.Value.GetNodeIndex();
					int t_index_use = t_node_use.Value.GetNodeIndex();

					//スワップ。
					U t_temp = this.buffer[t_index_free];
					this.buffer[t_index_free] = this.buffer[t_index_use];
					this.buffer[t_index_use] = t_temp;

					//スワップ。
					t_node_use.Value.SetNodeIndex(t_index_free);
					t_node_free.Value.SetNodeIndex(t_index_use);
				}

				//次の未使用にすべきノード。
				t_node_use = Fee.List.NodeList<T,U>.FindGreaterEequalIndex(t_node_use,t_max);
			}
		}
	}
}

