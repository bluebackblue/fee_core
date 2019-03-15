

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
	/** LinkEx
	*/
	public class LinkEx<NODEKEY,NODEDATA,LINKDATA>
		where NODEDATA : struct
		where LINKDATA : struct
	{
		/** リンク追加情報。
		*/
		public LINKDATA linkdata;

		/** 接続先ノード。
		*/
		private NodeEx<NODEKEY,NODEDATA,LINKDATA> to_node;

		/** 接続先ノードへの到達コスト。
		*/
		private long to_cost;

		/** constructor
		*/
		public LinkEx(LINKDATA a_linkdata,NodeEx<NODEKEY,NODEDATA,LINKDATA> a_to_node,long a_to_cost)
		{
			this.linkdata = a_linkdata;
			this.to_node = a_to_node;
			this.to_cost = a_to_cost;
		}

		/** 接続先ノードへの到達コスト。設定。
		*/
		public void SetToCost(long a_to_cost)
		{
			this.to_cost = a_to_cost;
		}

		/** 接続先ノードへの到達コスト。取得。
		*/
		public long GetToCost()
		{
			return this.to_cost;
		}

		/** 接続先ノード。設定。
		*/
		public void SetToNode(NodeEx<NODEKEY,NODEDATA,LINKDATA> a_to_node)
		{
			this.to_node = a_to_node;
		}

		/** 接続先ノード。取得。
		*/
		public NodeEx<NODEKEY,NODEDATA,LINKDATA> GetToNode()
		{
			return this.to_node;
		}
	}
}

