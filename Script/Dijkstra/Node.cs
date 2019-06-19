

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ダイクストラ法。ノード。
*/


/** Fee.Dijkstra
*/
namespace Fee.Dijkstra
{
	/** Instance_Base
	*/
	public class Instance_Base
	{
	}

	/** Node_Base
	*/
	public interface Node_Base
	{
		/** [Node_Base]計算フラグ。設定。
		*/
		void SetCalcFlag(Fee.Dijkstra.Instance_Base a_instance,bool a_flag);

		/** [Node_Base]計算フラグ。取得。
		*/
		bool GetCalcFlag(Fee.Dijkstra.Instance_Base a_instance);

		/** [Node_Base]到達コスト。設定。
		*/
		void SetTotalCost(Fee.Dijkstra.Instance_Base a_instance,long a_totalcost);

		/** [Node_Base]到達コスト。取得。
		*/
		long GetTotalCost(Fee.Dijkstra.Instance_Base a_instance);
	}

	/** Node
	*/
	public class Node<NODEKEY,NODEDATA,LINKDATA>
		where NODEDATA : Node_Base
		where LINKDATA : Link_Base
	{
		/** キー。
		*/
		public NODEKEY key;

		/** ノード追加情報。
		*/
		public NODEDATA nodedata;

		/** リンク。
		*/
		private System.Collections.Generic.List<Link<NODEKEY,NODEDATA,LINKDATA>> link_list;

		/** 計算フラグ。デフォルト。
		*/
		public const bool CALCFALG_DEFAULT = false;

		/** 到達コスト。デフォルト。
		*/
		public const long TOTALCOST_DEFAULT = -1;

		/** このノードへ到達するための隣接ノード。
		*/
		private Node<NODEKEY,NODEDATA,LINKDATA> prev_node;

		/** constructor
		*/
		public Node(NODEKEY a_key,NODEDATA a_nodedata)
		{
			this.key = a_key;
			this.nodedata = a_nodedata;
			this.link_list = new System.Collections.Generic.List<Link<NODEKEY,NODEDATA,LINKDATA>>();

			this.prev_node = null;
		}

		/** 計算フラグのリセット。
		*/
		public void ResetCalcFlag(Fee.Dijkstra.Instance_Base a_instance)
		{
			this.prev_node = null;

			this.nodedata.SetTotalCost(a_instance,-1);
			this.nodedata.SetCalcFlag(a_instance,false);
		}

		/** 開始ノードとしてコストを設定。
		*/
		public void SetStartCost(Fee.Dijkstra.Instance_Base a_instance)
		{
			this.nodedata.SetTotalCost(a_instance,0);
		}

		/** 到達コスト。取得。
		*/
		public long GetTotalCost(Fee.Dijkstra.Instance_Base a_instance)
		{
			return this.nodedata.GetTotalCost(a_instance);
		}

		/** 到達コスト。設定。
		*/
		public void SetTotalCost(Fee.Dijkstra.Instance_Base a_instance,long a_totalcost)
		{
			this.nodedata.SetTotalCost(a_instance,a_totalcost);
		}

		/** 計算フラグ。設定。
		*/
		public void SetCalcFlag(Fee.Dijkstra.Instance_Base a_instance,bool a_flag)
		{
			this.nodedata.SetCalcFlag(a_instance,a_flag);
		}

		/** 計算フラグ。取得。
		*/
		public bool GetCalcFlag(Fee.Dijkstra.Instance_Base a_instance)
		{
			return this.nodedata.GetCalcFlag(a_instance);
		}

		/** 隣接ノードの追加。
		*/
		public void AddLink(Link<NODEKEY,NODEDATA,LINKDATA> a_link)
		{
			this.link_list.Add(a_link);
		}

		/** リンクリスト。取得。
		*/
		public System.Collections.Generic.List<Link<NODEKEY,NODEDATA,LINKDATA>> GetLinkList()
		{
			return this.link_list;
		}

		/** このノードへ到達するための隣接ノード。取得。
		*/
		public Node<NODEKEY,NODEDATA,LINKDATA> GetPrevNode()
		{
			return this.prev_node;
		}

		/** このノードへ到達するための隣接ノード。設定。
		*/
		public void SetPrevNode(Node<NODEKEY,NODEDATA,LINKDATA> a_prev_node)
		{
			this.prev_node = a_prev_node;
		}
	}
}

