

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ダイクストラ法。
*/


/** Fee.Dijkstra
*/
namespace Fee.Dijkstra
{
	/** Dijkstra
	*/
	public class Dijkstra<NODEKEY,NODEDATA,LINKDATA>
		where NODEDATA : Node_Base
		where LINKDATA : Link_Base
	{
		/** ノードリスト。
		*/
		public System.Collections.Generic.Dictionary<NODEKEY,Node<NODEKEY,NODEDATA,LINKDATA>> node_list;

		/** 未計算リスト。
		*/
		public System.Collections.Generic.List<Node<NODEKEY,NODEDATA,LINKDATA>> calc_list;

		/** constructor
		*/
		public Dijkstra()
		{
			this.node_list = new System.Collections.Generic.Dictionary<NODEKEY,Node<NODEKEY,NODEDATA,LINKDATA>>();
			this.calc_list = new System.Collections.Generic.List<Node<NODEKEY,NODEDATA,LINKDATA>>();
		}

		/** 到達コストが最小のノードを検索。
		*/
		private static Node<NODEKEY,NODEDATA,LINKDATA> FindMinCostNode(Fee.Dijkstra.Instance_Base a_instance,System.Collections.Generic.List<Node<NODEKEY,NODEDATA,LINKDATA>> a_calc_list)
		{
			Node<NODEKEY,NODEDATA,LINKDATA> t_find_node = null;

			foreach(Node<NODEKEY,NODEDATA,LINKDATA> t_node in a_calc_list){
				if(t_find_node == null){
					t_find_node = t_node;
				}else if(t_node.GetTotalCost(a_instance) < t_find_node.GetTotalCost(a_instance)){
					t_find_node = t_node;
				}						
			}

			return t_find_node;
		}

		/** クリア。
		*/
		public void ClearAll()
		{
			this.node_list.Clear();
			this.calc_list.Clear();
		}

		/** ノード追加。
		*/
		public void AddNode(NODEKEY a_nodekey,Node<NODEKEY,NODEDATA,LINKDATA> a_node)
		{
			this.node_list.Add(a_nodekey,a_node);
		}

		/** ノード取得。
		*/
		public Node<NODEKEY,NODEDATA,LINKDATA> GetNode(NODEKEY a_nodekey)
		{
			Node<NODEKEY,NODEDATA,LINKDATA> t_node;

			if(this.node_list.TryGetValue(a_nodekey,out t_node) == true){
				return t_node;
			}

			return null;
		}

		/** ノード削除。
		*/
		public bool RemoveNode(NODEKEY a_nodekey)
		{
			return this.node_list.Remove(a_nodekey);
		}

		/** 計算フラグのリセット。
		*/
		public void ResetCalcFlag(Fee.Dijkstra.Instance_Base a_instance)
		{
			foreach(System.Collections.Generic.KeyValuePair<NODEKEY,Node<NODEKEY,NODEDATA,LINKDATA>> t_pair in this.node_list){
				t_pair.Value.ResetCalcFlag(a_instance);
			}
			this.calc_list.Clear();
		}

		/** 開始ノードに設定。
		*/
		public void SetStartNode(Fee.Dijkstra.Instance_Base a_instance,Node<NODEKEY,NODEDATA,LINKDATA> a_node_start)
		{
			//開始ノードとしてコストを設定。
			a_node_start.SetStartCost(a_instance);

			//計算リストに追加。
			this.calc_list.Add(a_node_start);
			a_node_start.SetCalcFlag(a_instance,true);
		}

		/** 計算。

			return == true : 継続。

		*/
		public bool Calc(Fee.Dijkstra.Instance_Base a_instance)
		{
			//到達コストが最小のノードを検索。
			Node<NODEKEY,NODEDATA,LINKDATA> t_node_current = Dijkstra<NODEKEY,NODEDATA,LINKDATA>.FindMinCostNode(a_instance,this.calc_list);
			if(t_node_current == null){
				return false;
			}else{
				//未計算リストから削除。
				this.calc_list.Remove(t_node_current);
				t_node_current.SetCalcFlag(a_instance,false);
			}

			//隣接ノード計算開始。
			System.Collections.Generic.List<Link<NODEKEY,NODEDATA,LINKDATA>> t_linklist_current = t_node_current.GetLinkList();
			for(int ii=0;ii<t_linklist_current.Count;ii++){
				Node<NODEKEY,NODEDATA,LINKDATA> t_node = t_linklist_current[ii].GetToNode();
				long t_total_cost = t_node_current.GetTotalCost(a_instance) + t_linklist_current[ii].GetToCost(a_instance);
				if((t_node.GetTotalCost(a_instance) < 0)||(t_node.GetTotalCost(a_instance) > t_total_cost)){
					//このノードへ到達するための隣接ノード。
					t_node.SetTotalCost(a_instance,t_total_cost);
					t_node.SetPrevNode(t_node_current);

					//計算リストに追加。
					if(t_node.GetCalcFlag(a_instance) == false){
						this.calc_list.Add(t_node);
						t_node.SetCalcFlag(a_instance,true);
					}
				}
			}

			return true;
		}

		/** 計算リスト。取得。
		*/
		public System.Collections.Generic.List<Node<NODEKEY,NODEDATA,LINKDATA>> GetCalcList()
		{
			return this.calc_list;
		}
	}
}

