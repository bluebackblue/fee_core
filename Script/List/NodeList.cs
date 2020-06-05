

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
		/** indexlist
		*/
		public System.Collections.Generic.LinkedListNode<NODE>[] indexlist;

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
		public NodeList(BUFFER[] a_buffer,bool a_indexlist)
		{
			//list_free
			this.list_free = new System.Collections.Generic.LinkedList<NODE>();
			for(int ii=0;ii<a_buffer.Length;ii++){
				NODE t_raw = new NODE();
				t_raw.SetNodeIndex(a_buffer.Length - ii - 1);
				System.Collections.Generic.LinkedListNode<NODE> t_node = new System.Collections.Generic.LinkedListNode<NODE>(t_raw);
				this.list_free.AddLast(t_node);
			}

			//indexlist
			if(a_indexlist == true){
				this.indexlist = new System.Collections.Generic.LinkedListNode<NODE>[a_buffer.Length];
				System.Collections.Generic.LinkedListNode<NODE> t_node = this.list_free.First;
				while(t_node != null){
					this.indexlist[t_node.Value.GetNodeIndex()] = t_node;
					t_node = t_node.Next;
				}
			}else{
				this.indexlist = null;
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

		/** 隙間を埋める。インデックスリストあり。
		*/
		public void GarbageCollectionWithIndexList()
		{
			GarbageCollectionWithIndexList(this.indexlist,this.list_use,this.list_free,this.buffer);
		}

		/** 隙間を埋める。使用リストの順序に合わせてバッファを入れ替える。インデックスリストあり。
		*/
		public void GarbageCollectionSwapUseListOderWithIndexList()
		{
			GarbageCollectionSwapUseListOderWithIndexList(this.indexlist,this.list_use,this.list_free,this.buffer);
		}

		/** 隙間を埋める。インデックスリストなし。
		*/
		public void GarbageCollection()
		{
			GarbageCollection(this.list_use,this.list_free,this.buffer);
		}

		/** 隙間を埋める。使用リストの順序に合わせてバッファを入れ替える。インデックスリストなし。
		*/
		public void GarbageCollectionSwapUseListOder()
		{
			GarbageCollectionSwapUseListOder(this.list_use,this.list_free,this.buffer);
		}

		/** a_indexより小さい、ノードを検索。

			a_node : 検索開始ノード。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindLessIndexNode(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
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

		/** a_indexより大きい、ノードを検索。

			a_node : 検索開始ノード。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindGreaterIndexNode(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
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

		/** a_index以下の、ノードを検索。

			a_node : 検索開始ノード。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindLessEequalIndexNode(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
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

		/** a_index以上の、ノードを検索。
		
			a_node : 検索開始ノード。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindGreaterEequalIndexNode(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
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

		/** a_indexの、ノードを検索。

			a_node : 検索開始ノード。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindEequalIndexNode(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
		{
			System.Collections.Generic.LinkedListNode<NODE> t_node = a_node;

			while(t_node != null){
				if(t_node.Value.GetNodeIndex() == a_index){
					return t_node;
				}

				t_node = t_node.Next;
			}

			return null;
		}

		/** a_index から 後方向へ、a_listに所属するノードのインデックスを検索。
		*/
		public static int FindInListIndexToEnd(System.Collections.Generic.LinkedListNode<NODE>[] a_indexlist,System.Collections.Generic.LinkedList<NODE> a_list,int a_index)
		{
			for(int ii=a_index;ii<a_indexlist.Length;ii++){
				if(a_indexlist[ii].List == a_list){
					return ii;
				}
			}
			return -1;
		}

		/** a_index から 前方向へ、a_listに所属するノードのインデックスを検索。
		*/
		public static int FindInListIndexToStart(System.Collections.Generic.LinkedListNode<NODE>[] a_indexlist,System.Collections.Generic.LinkedList<NODE> a_list,int a_index)
		{
			for(int ii=a_index;ii>=0;ii--){
				if(a_indexlist[ii].List == a_list){
					return ii;
				}
			}
			return -1;
		}

		/** バッファをスワップする。インデックスリストなし。
		*/
		public static void SwapBuffer(System.Collections.Generic.LinkedListNode<NODE> a_node_a,System.Collections.Generic.LinkedListNode<NODE> a_node_b,BUFFER[] a_buffer)
		{
			int t_index_a = a_node_a.Value.GetNodeIndex();
			int t_index_b = a_node_b.Value.GetNodeIndex();

			BUFFER t_temp = a_buffer[t_index_a];
			a_buffer[t_index_a] = a_buffer[t_index_b];
			a_buffer[t_index_b] = t_temp;

			a_node_a.Value.SetNodeIndex(t_index_b);
			a_node_b.Value.SetNodeIndex(t_index_a);
		}

		/** バッファをスワップする。インデックスリストあり。
		*/
		public static void SwapBufferWithIndexList(System.Collections.Generic.LinkedListNode<NODE>[] a_indexlist,System.Collections.Generic.LinkedListNode<NODE> a_node_a,System.Collections.Generic.LinkedListNode<NODE> a_node_b,BUFFER[] a_buffer)
		{
			int t_index_a = a_node_a.Value.GetNodeIndex();
			int t_index_b = a_node_b.Value.GetNodeIndex();

			BUFFER t_temp = a_buffer[t_index_a];
			a_buffer[t_index_a] = a_buffer[t_index_b];
			a_buffer[t_index_b] = t_temp;

			a_node_a.Value.SetNodeIndex(t_index_b);
			a_node_b.Value.SetNodeIndex(t_index_a);

			a_indexlist[t_index_b] = a_node_a;
			a_indexlist[t_index_a] = a_node_b;
		}

		/** 隙間を埋める。インデックスリストなし。
		*/
		public static void GarbageCollection(System.Collections.Generic.LinkedList<NODE> a_list_use,System.Collections.Generic.LinkedList<NODE> a_list_free,BUFFER[] a_buffer)
		{
			int t_max = a_list_use.Count;

			//node
			System.Collections.Generic.LinkedListNode<NODE> t_node_use = FindGreaterEequalIndexNode(a_list_use.First,t_max);
			System.Collections.Generic.LinkedListNode<NODE> t_node_free = a_list_free.First;

			while(t_node_use != null){
				//t_node_use : 未使用にすべきノード。
				//t_node_free : 使用にすべきノード。
				t_node_free = FindLessIndexNode(t_node_free,t_max);

				//バッファをスワップする。
				SwapBuffer(t_node_free,t_node_use,a_buffer);

				//次の未使用にすべきノード。
				t_node_use = FindGreaterEequalIndexNode(t_node_use,t_max);
			}
		}

		/** 隙間を埋める。インデックスリストあり。
		*/
		public static void GarbageCollectionWithIndexList(System.Collections.Generic.LinkedListNode<NODE>[] a_indexlist,System.Collections.Generic.LinkedList<NODE> a_list_use,System.Collections.Generic.LinkedList<NODE> a_list_free,BUFFER[] a_buffer)
		{
			int t_max = a_list_use.Count;

			int t_index_use = FindInListIndexToStart(a_indexlist,a_list_use,a_indexlist.Length - 1);
			int t_index_free = 0;

			while(t_index_use >= t_max){
				//t_index_use : 未使用にすべきノード。
				//t_index_free : 使用にすべきノード。
				t_index_free = FindInListIndexToEnd(a_indexlist,a_list_free,t_index_free);

				//バッファをスワップする。
				SwapBufferWithIndexList(a_indexlist,a_indexlist[t_index_free],a_indexlist[t_index_use],a_buffer);
			}
		}

		/** 隙間を埋める。使用リストの順序に合わせてバッファを入れ替える。インデックスリストなし。
		*/
		public static void GarbageCollectionSwapUseListOder(System.Collections.Generic.LinkedList<NODE> a_list_use,System.Collections.Generic.LinkedList<NODE> a_list_free,BUFFER[] a_buffer)
		{
			int t_max = a_list_use.Count;

			System.Collections.Generic.LinkedListNode<NODE> t_node = a_list_use.First;
			for(int ii=0;ii<t_max;ii++){
				if(t_node.Value.GetNodeIndex() != ii){
					System.Collections.Generic.LinkedListNode<NODE> t_node_next = t_node.Next;

					//t_node : 未使用にすべきノード。
					//t_node_find : 使用にすべきノード。

					//インデックスがiiのノードを検索。使用リストから。
					System.Collections.Generic.LinkedListNode<NODE> t_node_find = FindEequalIndexNode(a_list_use.First,ii);
					if(t_node_find == null){
						//インデックスがiiのノードを検索。未使用リストから。
						t_node_find = FindEequalIndexNode(a_list_free.First,ii);
					}

					//バッファをスワップする。
					SwapBuffer(t_node,t_node_find,a_buffer);

					t_node = t_node_next;
				}
			}
		}
		
		/** 隙間を埋める。使用リストの順序に合わせてバッファを入れ替える。インデックスリストあり。
		*/
		public static void GarbageCollectionSwapUseListOderWithIndexList(System.Collections.Generic.LinkedListNode<NODE>[] a_indexlist,System.Collections.Generic.LinkedList<NODE> a_list_use,System.Collections.Generic.LinkedList<NODE> a_list_free,BUFFER[] a_buffer)
		{
			int t_max = a_list_use.Count;

			System.Collections.Generic.LinkedListNode<NODE> t_node = a_list_use.First;
			for(int ii=0;ii<t_max;ii++){
				if(t_node.Value.GetNodeIndex() != ii){
					System.Collections.Generic.LinkedListNode<NODE> t_node_next = t_node.Next;

					//t_node : 未使用にすべきノード。
					//t_node_find : 使用にすべきノード。
					System.Collections.Generic.LinkedListNode<NODE> t_node_find = a_indexlist[ii];

					//バッファをスワップする。
					SwapBufferWithIndexList(a_indexlist,t_node,t_node_find,a_buffer);

					t_node = t_node_next;
				}
			}
		}
	}
}

