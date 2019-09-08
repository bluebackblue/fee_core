

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パーセプトロン。
*/


/** Fee.Perceptron
*/
namespace Fee.Perceptron
{
	/** Layer
	*/
	public class Layer
	{
		public System.Collections.Generic.List<Node> node_list;

		/** constructor
		*/
		public Layer(int a_count)
		{
			//ノード作成。
			this.node_list = new System.Collections.Generic.List<Node>(a_count);
			for(int ii=0;ii<a_count;ii++){
				Node t_node = new Node();
				this.node_list.Add(t_node);
			}
		}

		/** 順方向計算。
		*/
		public void ForwardCalculation()
		{
			for(int t_node_index=0;t_node_index<this.node_list.Count;t_node_index++){
				//ノードのループ。

				//順方向計算。
				this.node_list[t_node_index].ForwardCalculation();
			}
		}

		/** 誤差逆伝播。
		*/
		public void BackPropagate()
		{
		}
	}
}

