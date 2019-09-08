

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

		/** layer_teacher
		*/
		public Layer layer_teacher;

		/** constructor
		*/
		public Perceptron(int a_in_count,int a_middle_count,int a_out_out)
		{
			this.layer_list = new System.Collections.Generic.List<Layer>();

			//バイアスノードを追加。
			const int t_bias_node_count = 1;

			//入力レイヤー。
			this.layer_list.Add(new Layer(a_in_count + t_bias_node_count));

			//中間レイヤー。
			this.layer_list.Add(new Layer(a_middle_count + t_bias_node_count));

			//出力レイヤー。
			this.layer_list.Add(new Layer(a_out_out));

			//教師レイヤー。
			this.layer_teacher = new Layer(a_out_out);

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
				for(int t_layer_index=0;t_layer_index<(this.layer_list.Count - 1);t_layer_index++){
					int t_count = this.layer_list[t_layer_index].node_list.Count;
					Node t_node = this.layer_list[t_layer_index].node_list[t_count-1];
					t_node.value = 1.0f;
					t_node.is_bias = true;
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

		/** 誤差逆伝播。
		*/
		public void BackPropagate()
		{
			//誤差値をリセット。
			{
				for(int t_layer_index=0;t_layer_index<this.layer_list.Count;t_layer_index++){
					//layer
					Layer t_layer = this.layer_list[t_layer_index];
					
					for(int t_node_index=0;t_node_index<t_layer.node_list.Count;t_node_index++){
						//node
						Node t_node = t_layer.node_list[t_node_index];

						t_node.eee_value = 0.0f;
					}
				}
			}

			//出力レイヤーの誤差値を教師レイヤーから計算。
			{
				Layer t_layer_out = this.layer_list[this.layer_list.Count - 1];
				Layer t_layer_teacher = this.layer_teacher;
				
				for(int t_node_index=0;t_node_index<t_layer_teacher.node_list.Count;t_node_index++){
					Node t_node_teacher = t_layer_teacher.node_list[t_node_index];
					Node t_node_out = t_layer_out.node_list[t_node_index];
					
					//誤差。
					float t_eee_value = t_node_teacher.value - t_node_out.value;

					//値が0.5fの場合に0.25で最大になる。
					float t_rate = t_node_out.value * (1.0f - t_node_out.value);

					t_node_out.eee_value = t_eee_value * t_rate;
				}
			}

			//中間レイヤーの誤差を計算。
			{
				for(int t_layer_index = this.layer_list.Count - 2;t_layer_index >= 1;t_layer_index--){
					//誤差を受け取るレイヤー。
					Layer t_layer = this.layer_list[t_layer_index];

					for(int t_node_index=0;t_node_index<t_layer.node_list.Count;t_node_index++){

						//誤差を受け取るノード。
						Node t_node = t_layer.node_list[t_node_index];

						//（接続先誤差 * 接続先重み）の合計。
						float t_eee_value = 0.0f;
						{
							for(int t_link_index=0;t_link_index<t_node.link_list_to.Count;t_link_index++){
								Link t_link_to = t_node.link_list_to[t_link_index];
								t_eee_value += t_link_to.node_to.eee_value * t_link_to.weight;
							}
						}

						//値が0.5fの場合に0.25で最大になる。
						float t_rate = t_node.value * (1.0f - t_node.value);

						t_node.eee_value = t_eee_value * t_rate;
					}
				}
			}

			//重み修正。
			{
				for(int t_layer_index=0;t_layer_index<(this.layer_list.Count-1);t_layer_index++){
					Layer t_layer = this.layer_list[t_layer_index];
					for(int t_node_index=0;t_node_index<t_layer.node_list.Count;t_node_index++){
						Node t_node = t_layer.node_list[t_node_index];
						for(int t_link_index=0;t_link_index<t_node.link_list_to.Count;t_link_index++){
							Link t_link = t_node.link_list_to[t_link_index];
							t_link.weight += t_node.value * t_link.node_to.eee_value * t_link.weight_change_rate;
						}
					}
				}
			}

		}
	}
}

