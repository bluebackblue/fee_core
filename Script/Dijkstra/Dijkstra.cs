

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
	public class Dijkstra<NODEKEY,NODE,LINK>
		where NODE : NodeBase
		where LINK : LinkBase
	{
		/** ノードリスト。
		*/
		public System.Collections.Generic.Dictionary<NODEKEY,NODE> node_list;

		/** constructor
		*/
		public Dijkstra()
		{
			this.node_list = new System.Collections.Generic.Dictionary<NODEKEY,NODE>();
		}

		/** クリア。
		*/
		public void ClearAll()
		{
			this.node_list.Clear();
		}

		/** ノード追加。
		*/
		public void AddNode(NODEKEY a_nodekey,NODE a_node)
		{
			this.node_list.Add(a_nodekey,a_node);
		}

		/** ノード取得。
		*/
		public NODE GetNode(NODEKEY a_nodekey)
		{
			NODE t_node;

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
		public void ResetCalcFlag()
		{
			foreach(System.Collections.Generic.KeyValuePair<NODEKEY,NODE> t_pair in this.node_list){
				t_pair.Value.ResetCalcFlag();
			}
		}

		/** 隣接ノード未計算で到達コストが最小のノードを検索。
		*/
		public NODE SearchNoFixNodeMinCost()
		{
			NODE t_find = null;

			foreach(System.Collections.Generic.KeyValuePair<NODEKEY,NODE> t_pair in this.node_list){
				NODE t_node = t_pair.Value;

				if((t_node.fix == false)&&(t_node.total_cost >= 0)){
					//到達コストあり、隣接ノード未計算。
					if(t_find == null){
						t_find = t_node;
					}else if(t_node.total_cost < t_find.total_cost){
						t_find = t_node;
					}						
				}
			}

			return t_find;
		}

		/** 開始ノードに設定。
		*/
		public void SetStartNode(NODE a_node_start)
		{
			a_node_start.SetStartNode();
		}

		/** 計算。

			戻り値 == true : 継続。

		*/
		public bool Calc()
		{
			//隣接ノード未計算で到達コストが最小のノードを検索。
			NODE t_node_current = this.SearchNoFixNodeMinCost();
			if(t_node_current == null){
				return false;
			}

			//隣接ノード計算開始。
			t_node_current.fix = true;
			for(int ii=0;ii<t_node_current.link.Count;ii++){
				NodeBase t_node = t_node_current.link[ii].to_node;
				long t_total_cost = t_node_current.total_cost + t_node_current.link[ii].to_cost;
				if((t_node.total_cost < 0)||(t_node.total_cost > t_total_cost)){
					//このノードへ到達するための隣接ノード。
					t_node.total_cost = t_total_cost;
					t_node.prev_node = t_node_current;
				}
			}

			return true;
		}
	}
}

