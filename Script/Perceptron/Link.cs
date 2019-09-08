

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パーセプトロン。リンク。
*/


/** Fee.Perceptron
*/
namespace Fee.Perceptron
{
	/** Link
	*/
	public class Link
	{
		/** node_from
		*/
		public Node node_from;
	
		/** node_to
		*/
		public Node node_to;

		/** weight
		*/
		public float weight;

		/** weight_change_rate
		*/
		public float weight_change_rate;

		/** constructor。
		*/
		public Link(Node a_node_from,Node a_node_to)
		{
			//node_from
			this.node_from = a_node_from;

			//node_to
			this.node_to = a_node_to;

			//weight
			this.weight = 0.0f;

			//weight_change_rate
			this.weight_change_rate = 0.02f;
		}
	}
}

