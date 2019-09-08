

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
	/** Perceptron
	*/
	public class Perceptron
	{
		/** layer_list
		*/
		public System.Collections.Generic.List<Layer> layer_list;

		/** constructor
		*/
		public Perceptron(int a_in_count,int a_middle_count,int a_out_out)
		{
			this.layer_list = new System.Collections.Generic.List<Layer>();

			//バイアスノードを追加。
			const int t_bias_node_count = 1;

			//入力層。
			this.layer_list.Add(new Layer(a_in_count + t_bias_node_count));

			//中間層。
			this.layer_list.Add(new Layer(a_middle_count + t_bias_node_count));

			//出力層。
			this.layer_list.Add(new Layer(a_out_out + t_bias_node_count));

			//接続。
			{
				for(int t_layer_index=0;t_layer_index<(this.layer_list.Count - 1);t_layer_index++){
					//レイヤーのループ。

					//接続元レイヤー。
					Layer t_layer_from = this.layer_list[t_layer_index];

					//接続先レイヤー。
					Layer t_layer_to = this.layer_list[t_layer_index + 1];

					for(int t_node_index_from=0;t_node_index_from<t_layer_from.node_list.Count;t_node_index_from++){
						//接続元ノードのループ。

						Node t_node_from = t_layer_from.node_list[t_node_index_from];

						for(int t_node_index_to=0;t_node_index_to<t_layer_to.node_list.Count;t_node_index_to++){
							//接続先ノードのループ。

							Node t_node_to = t_layer_to.node_list[t_node_index_to];

							//接続。。
							Link t_link = new Link(t_node_from,t_node_to);
							t_node_from.link_list_from.Add(t_link);
							t_node_to.link_list_to.Add(t_link);
						}
					}
				}
			}

			//バイアスノードの設定。
			{
				for(int t_layer_index=0;t_layer_index<this.layer_list.Count;t_layer_index++){
					int t_count = this.layer_list[t_layer_index].node_list.Count;
					Node t_node = this.layer_list[t_layer_index].node_list[t_count-1];
					t_node.value = 1.0f;
				}
			}
		}

		/** 順方向計算。
		*/
		public void ForwardCalculation()
		{
			for(int t_layer_index=1;t_layer_index<this.layer_list.Count;t_layer_index++){
				//layer
				Layer t_layer = this.layer_list[t_layer_index];

				//順方向計算。
				t_layer.ForwardCalculation();
			}
		}
	}
}

