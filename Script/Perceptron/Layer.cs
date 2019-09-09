

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パーセプトロン。レイヤー。
*/


/** Fee.Perceptron
*/
namespace Fee.Perceptron
{
	/** Layer
	*/
	public class Layer
	{
		/** layer_index
		*/
		public int layer_index;

		/** node_list
		*/
		public System.Collections.Generic.List<Node> node_list;

		/** constructor
		*/
		public Layer(int a_count,int a_layer_index)
		{
			//layer_index
			this.layer_index = a_layer_index;

			//node_list
			this.node_list = new System.Collections.Generic.List<Node>();
			for(int ii=0;ii<a_count;ii++){
				this.node_list.Add(new Node(this,ii));
			}
		}
	}
}

