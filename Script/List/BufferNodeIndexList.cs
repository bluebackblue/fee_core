

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
	/** BufferNodeIndexList
	*/
	public struct BufferNodeIndexList<NODE>
		where NODE : BufferNodeItem
	{
		/** list
		*/
		private System.Collections.Generic.LinkedListNode<NODE>[] list;

		/** constructor
		*/
		public BufferNodeIndexList(System.Collections.Generic.LinkedList<NODE> a_list)
		{
			//list
			this.list = new System.Collections.Generic.LinkedListNode<NODE>[a_list.Count];
			System.Collections.Generic.LinkedListNode<NODE> t_node = a_list.First;
			while(t_node != null){
				this.list[t_node.Value.GetBufferIndex()] = t_node;
				t_node = t_node.Next;
			}
		}

		/** GetList
		*/
		public System.Collections.Generic.LinkedListNode<NODE>[] GetList()
		{
			return this.list;
		}

		/** GetNode
		*/
		public System.Collections.Generic.LinkedListNode<NODE> GetNode(int a_index)
		{
			return this.list[a_index];
		}

		/** SetNode
		*/
		public void SetNode(int a_index,System.Collections.Generic.LinkedListNode<NODE> a_node)
		{
			this.list[a_index] = a_node;
		}

		/** GetCount
		*/
		public int GetCount()
		{
			return this.list.Length;
		}

		/** a_index から 後方向へ、a_listに所属するノードのインデックスを検索。
		*/
		public int FindInListIndexToEnd(System.Collections.Generic.LinkedList<NODE> a_list,int a_index)
		{
			for(int ii=a_index;ii<this.list.Length;ii++){
				if(this.list[ii].List == a_list){
					return ii;
				}
			}

			return -1;
		}

		/** a_index から 前方向へ、a_listに所属するノードのインデックスを検索。
		*/
		public int FindInListIndexToStart(System.Collections.Generic.LinkedList<NODE> a_list,int a_index)
		{
			for(int ii=a_index;ii>=0;ii--){
				if(this.list[ii].List == a_list){
					return ii;
				}
			}

			return -1;
		}
	}
}

