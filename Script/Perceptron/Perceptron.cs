

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
		public Perceptron(int a_in_count,int a_middle_count,int a_out_count)
		{
			//layer_list
			this.layer_list = new System.Collections.Generic.List<Layer>();

			//教師レイヤー。
			this.layer_teacher = new Layer(a_out_count);

			//入力レイヤー。
			this.layer_list.Add(new Layer(a_in_count + 1));

			//中間レイヤー。
			this.layer_list.Add(new Layer(a_middle_count + 1));

			//出力レイヤー。
			this.layer_list.Add(new Layer(a_out_count));

			//接続。
			{
				for(int t_layer_index=0;t_layer_index<(this.layer_list.Count - 1);t_layer_index++){
					Layer t_layer_from = this.layer_list[t_layer_index];
					Layer t_layer_to = this.layer_list[t_layer_index + 1];

					for(int t_node_index_from=0;t_node_index_from<t_layer_from.node_list.Count;t_node_index_from++){
						Node t_node_from = t_layer_from.node_list[t_node_index_from];
						for(int t_node_index_to=0;t_node_index_to<t_layer_to.node_list.Count;t_node_index_to++){
							Node t_node_to = t_layer_to.node_list[t_node_index_to];
							if(t_node_to.is_bias == false){
								Link t_link = new Link(t_node_from,t_node_to);
								t_node_from.link_list.Add(t_link);
								t_node_to.link_list_prev.Add(t_link);
							}
						}
					}
				}
			}

			//バイアス。
			this.layer_list[0].node_list[this.layer_list[0].node_list.Count - 1].is_bias = true;
			this.layer_list[0].node_list[this.layer_list[0].node_list.Count - 1].value = 1.0f;

			//バイアス。
			this.layer_list[1].node_list[this.layer_list[1].node_list.Count - 1].is_bias = true;
			this.layer_list[1].node_list[this.layer_list[1].node_list.Count - 1].value = 1.0f;
		}
	
		/** ForwardCalculation
		*/
		public void ForwardCalculation()
		{
			//中間レイヤー。
			{
				for(int j = 0;j < this.layer_list[1].node_list.Count;j++){
					if(this.layer_list[1].node_list[j].is_bias == false){

						float t_sum = 0.0f;
						foreach(Link t_link in this.layer_list[1].node_list[j].link_list_prev){
							t_sum += t_link.node_from.value * t_link.weight;
						}

						//シグモイド。
						this.layer_list[1].node_list[j].value = 1.0f / (1.0f + UnityEngine.Mathf.Exp(-t_sum));
					}
				}
			}

			//出力レイヤー。
			{
				for(int j = 0;j < this.layer_list[2].node_list.Count;j++){
					if(this.layer_list[2].node_list[j].is_bias == false){
						float t_sum = 0.0f;
						foreach(Link t_link in this.layer_list[2].node_list[j].link_list_prev){
							t_sum += t_link.node_from.value * t_link.weight;
						}

						//シグモイド。
						this.layer_list[2].node_list[j].value = 1.0f / (1.0f + UnityEngine.Mathf.Exp(-t_sum));
					}
				}
			}
		}

		/** BackPropagation
		*/
		public void BackPropagation()
		{
			//出力レイヤー。
			{
				Layer t_layer_out = this.layer_list[2];
				Layer t_layer_teacher = this.layer_teacher;

				for(int t_node_index=0;t_node_index<t_layer_teacher.node_list.Count;t_node_index++){
					Node t_node_teacher = t_layer_teacher.node_list[t_node_index];
					Node t_node_out = t_layer_out.node_list[t_node_index];

					//誤差。
					float t_eee_value = t_node_teacher.value - t_node_out.value;

					//0.5fの時が最大。
					float t_rate = t_node_out.value * (1.0f - t_node_out.value);

					t_node_out.error = t_eee_value * t_rate;
				}
			}

			//中間レイヤー。
			for(int t_node_index = 0;t_node_index < this.layer_list[1].node_list.Count;t_node_index++){

				float t_error = 0.0f;
				foreach(Link t_link in this.layer_list[1].node_list[t_node_index].link_list){
					t_error += t_link.node_to.error * t_link.weight;
				}

				this.layer_list[1].node_list[t_node_index].error = t_error * this.layer_list[1].node_list[t_node_index].value * (1.0f - this.layer_list[1].node_list[t_node_index].value);
			}

			//中間レイヤー。重み。
			{
				for(int t_node_index = 0;t_node_index < this.layer_list[1].node_list.Count; t_node_index++){
					foreach(Link t_link in this.layer_list[1].node_list[t_node_index].link_list){
						float t_dw = 
						t_link.weight += t_link.weight_change_rate * t_link.node_to.error * t_link.node_from.value;
					}
				}
			}

			//入力レイヤー。重み。
			{
				for(int t_node_index = 0;t_node_index < this.layer_list[0].node_list.Count;t_node_index++){
					foreach(Link t_link in this.layer_list[0].node_list[t_node_index].link_list){
						t_link.weight += t_link.weight_change_rate * t_link.node_to.error * t_link.node_from.value;
					}
				}
			}
		}
	}
}

