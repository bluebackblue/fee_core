

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief リンクベース。
*/


/** Fee.Dijkstra
*/
namespace Fee.Dijkstra
{
	/** LinkBase
	*/
	public class LinkBase
	{
		/** 接続先ノード。
		*/
		public NodeBase to_node;

		/** 接続先ノードへの到達コスト。
		*/
		public long to_cost;

		/** constructor
		*/
		public LinkBase(NodeBase a_to_node,long a_to_cost)
		{
			this.to_node = a_to_node;
			this.to_cost = a_to_cost;
		}
	}
}

