

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
	/** BufferNodeFind
	*/
	public class BufferNodeFind<NODE>
		where NODE : BufferNodeItem
	{
		/** a_indexより小さい、ノードを検索。

			a_node : 検索開始ノード。

		*/
		public static System.Collections.Generic.LinkedListNode<NODE> FindLessIndexNode(System.Collections.Generic.LinkedListNode<NODE> a_node,int a_index)
		{
			System.Collections.Generic.LinkedListNode<NODE> t_node = a_node;
			do{
				if(t_node.Value.GetBufferIndex() < a_index){
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
				if(t_node.Value.GetBufferIndex() > a_index){
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
				if(t_node.Value.GetBufferIndex() <= a_index){
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
				if(t_node.Value.GetBufferIndex() >= a_index){
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
				if(t_node.Value.GetBufferIndex() == a_index){
					return t_node;
				}

				t_node = t_node.Next;
			}

			return null;
		}
	}
}

