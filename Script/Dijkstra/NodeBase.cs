

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ノードベース。
*/


/** Fee.Dijkstra
*/
namespace Fee.Dijkstra
{
	/** Node
	*/
	public class NodeEx<NODEKEY,NODEDATA,LINKDATA>
		where NODEDATA : struct
		where LINKDATA : struct
	{
		/** キー。
		*/
		public NODEKEY key;

		/** ノード追加情報。
		*/
		public NODEDATA nodedata;

		/** リンク。
		*/
		private System.Collections.Generic.List<LinkEx<NODEKEY,NODEDATA,LINKDATA>> link_list;

		/** 到達コスト。
		*/
		private long total_cost;

		/** このノードへ到達するための隣接ノード。
		*/
		private NodeEx<NODEKEY,NODEDATA,LINKDATA> prev_node;

		/** constructor
		*/
		public NodeEx(NODEKEY a_key,NODEDATA a_nodedata)
		{
			this.key = a_key;
			this.nodedata = a_nodedata;
			this.link_list = new System.Collections.Generic.List<LinkEx<NODEKEY,NODEDATA,LINKDATA>>();
			this.total_cost = -1;
			this.prev_node = null;
		}

		/** 計算フラグのリセット。
		*/
		public void ResetCalcFlag()
		{
			this.total_cost = -1;
			this.prev_node = null;
		}

		/** 開始ノードに設定。
		*/
		public void SetStartNode()
		{
			this.total_cost = 0;
			this.prev_node = null;
		}

		/** 到達コスト。取得。
		*/
		public long GetTotalCost()
		{
			return this.total_cost;
		}

		/** 到達コスト。設定。
		*/
		public void SetTotalCost(long a_totalcost)
		{
			this.total_cost = a_totalcost;
		}

		/** 隣接ノードの追加。
		*/
		public void AddLink(LinkEx<NODEKEY,NODEDATA,LINKDATA> a_link)
		{
			this.link_list.Add(a_link);
		}

		/** リンクリスト。取得。
		*/
		public System.Collections.Generic.List<LinkEx<NODEKEY,NODEDATA,LINKDATA>> GetLinkList()
		{
			return this.link_list;
		}

		/** このノードへ到達するための隣接ノード。取得。
		*/
		public NodeEx<NODEKEY,NODEDATA,LINKDATA> GetPrevNode()
		{
			return this.prev_node;
		}

		/** このノードへ到達するための隣接ノード。設定。
		*/
		public void SetPrevNode(NodeEx<NODEKEY,NODEDATA,LINKDATA> a_prev_node)
		{
			this.prev_node = a_prev_node;
		}
	}
}

