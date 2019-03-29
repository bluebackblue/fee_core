

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダイクストラ法。リンク。
*/


/** Fee.Dijkstra
*/
namespace Fee.Dijkstra
{
	/** Link_Base
	*/
	public interface Link_Base
	{
		/** [Link_Base]接続先ノードへの到達コスト。取得。
		*/
		long GetToCost();

		/** [Link_Base]接続先ノードへの到達コスト。設定。
		*/
		void SetToCost(long a_to_cost);
	}

	/** Link
	*/
	public class Link<NODEKEY,NODEDATA,LINKDATA>
		where NODEDATA : Node_Base
		where LINKDATA : Link_Base
	{
		/** リンク追加情報。
		*/
		public LINKDATA linkdata;

		/** 接続先ノード。
		*/
		private Node<NODEKEY,NODEDATA,LINKDATA> to_node;

		/** 接続先ノードへの到達コスト。
		*/
		/*
		private long to_cost;
		*/

		/** constructor
		*/
		public Link(LINKDATA a_linkdata,Node<NODEKEY,NODEDATA,LINKDATA> a_to_node)
		{
			this.linkdata = a_linkdata;
			this.to_node = a_to_node;

			//this.to_cost = a_to_cost;
			//this.linkdata.SetToCost(a_to_cost);
		}

		/** 接続先ノードへの到達コスト。設定。
		*/
		public void SetToCost(long a_to_cost)
		{
			//this.to_cost = a_to_cost;
			this.linkdata.SetToCost(a_to_cost);
		}

		/** 接続先ノードへの到達コスト。取得。
		*/
		public long GetToCost()
		{
			//return this.to_cost;
			return this.linkdata.GetToCost();
		}

		/** 接続先ノード。設定。
		*/
		public void SetToNode(Node<NODEKEY,NODEDATA,LINKDATA> a_to_node)
		{
			this.to_node = a_to_node;
		}

		/** 接続先ノード。取得。
		*/
		public Node<NODEKEY,NODEDATA,LINKDATA> GetToNode()
		{
			return this.to_node;
		}
	}
}

