

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
	/** NodeBase
	*/
	public class NodeBase
	{
		/** リンク。
		*/
		public System.Collections.Generic.List<LinkBase> link;

		/** 到達コスト。
		*/
		public long total_cost;

		/** 隣接ノード計算済みフラグ。
		*/
		public bool fix;

		/** このノードへ到達するための隣接ノード。
		*/
		public NodeBase prev_node;

		/** constructor
		*/
		public NodeBase()
		{
			this.link = new System.Collections.Generic.List<LinkBase>();
			this.total_cost = -1;
			this.fix = false;
			this.prev_node = null;
		}

		/** 隣接ノードの追加。
		*/
		public void AddLink<LINK>(LINK a_link)
			where LINK : LinkBase
		{
			this.link.Add(a_link);
		}

		/** 計算フラグのリセット。
		*/
		public void ResetCalcFlag()
		{
			this.total_cost = -1;
			this.fix = false;
			this.prev_node = null;
		}

		/** 開始ノードに設定。
		*/
		public void SetStartNode()
		{
			this.total_cost = 0;
			this.fix = false;
			this.prev_node = null;
		}
	}
}

