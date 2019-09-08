

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
	/** Node
	*/
	public class Node
	{
		/** 値。
		*/
		public float value;

		/** 自分が接続元のリスト。
		*/
		public System.Collections.Generic.List<Link> link_list_from;

		/** 自分が接続先のリスト。
		*/
		public System.Collections.Generic.List<Link> link_list_to;

		/** constructor
		*/
		public Node()
		{
			//value
			this.value = 0.0f;

			//link_list_from
			this.link_list_from = new System.Collections.Generic.List<Link>();

			//link_list_to
			this.link_list_to = new System.Collections.Generic.List<Link>();
		}

		/** 順方向計算。
		*/
		public void ForwardCalculation()
		{
			//自分が接続先のリスト。
			System.Collections.Generic.List<Link> t_link_list_to = this.link_list_to;

			//値。
			float t_value_to = 0.0f;

			for(int t_link_index=0;t_link_index<t_link_list_to.Count;t_link_index++){
				//リンクのループ。

				//リンク。
				Link t_link = t_link_list_to[t_link_index];
				Tool.Assert(this == t_link.node_to);//TODO:

				//加算。
				t_value_to += t_link.node_from.value * t_link.weight;
			}

			//シグモイド関数。（0.0f -- 1.0f）
			t_value_to = 1.0f / (1.0f + UnityEngine.Mathf.Exp(-t_value_to));

			//設定。
			this.value = t_value_to;
		}
	}
}

