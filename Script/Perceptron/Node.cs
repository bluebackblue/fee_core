

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パーセプトロン。ノード。
*/


/** Fee.Perceptron
*/
namespace Fee.Perceptron
{
	/** Node
	*/
	public class Node
	{
		/** layer_parent
		*/
		public Layer layer_parent;

		/** node_index
		*/
		public int node_index;

		/** value
		*/
		public float value;

		/** error
		*/
		public float error;

		/** is_bias
		*/
		public bool is_bias;

		/** 接続先リスト。
		*/
		public System.Collections.Generic.List<Link> link_list;

		/** 接続元リスト。
		*/
		public System.Collections.Generic.List<Link> link_list_prev;

		/** constructor
		*/
		public Node(Layer a_layer_parent,int a_node_index)
		{
			//layer_parent
			this.layer_parent = a_layer_parent;

			//node_index
			this.node_index = a_node_index;

			//value
			this.value = 0.0f;

			//error
			this.error = 0.0f;

			//is_bias
			this.is_bias = false;

			//link_list
			this.link_list = new System.Collections.Generic.List<Link>();

			//link_list_prev
			this.link_list_prev = new System.Collections.Generic.List<Link>();
		}
	}
}

