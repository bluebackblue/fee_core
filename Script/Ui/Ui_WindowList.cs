

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。ウィンドウリスト。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Ui_WindowList
	*/
	public class Ui_WindowList
	{
		//リスト。
		private System.Collections.Generic.List<Window_Base> list;

		//レイヤーインデックス。変更フラグ。
		private bool change_layerindex;

		//開始レイヤーインデックス。
		private int layerindex_start;

		/** constructor
		*/
		public Ui_WindowList()
		{
			//リスト。
			this.list = new System.Collections.Generic.List<Window_Base>();

			//レイヤーインデックス。変更フラグ。
			this.change_layerindex = false;

			//開始レイヤーインデックス。
			this.layerindex_start = Config.DEFAULT_WINDOW_LAYER_INDEX_START;
		}

		/** 更新。
		*/
		public void Main()
		{
			if(this.change_layerindex == true){
				this.change_layerindex = false;

				for(int ii=0;ii<this.list.Count;ii++){
					//layerindex
					int t_layerindex = this.layerindex_start + ii;
					if(t_layerindex >= Fee.Render2D.Render2D.MAX_LAYER){
						t_layerindex =  Fee.Render2D.Render2D.MAX_LAYER - 1;
						Tool.Assert(false);
					}

					//レイヤーインデックス。変更。
					this.list[ii].ChangeLayerIndex(t_layerindex);
				}
			}
		}

		/** 開始レイヤーインデックス。設定。
		*/
		public void SetStartLayerIndex(int a_layerindex)
		{
			this.layerindex_start = a_layerindex;
		}

		/** 登録。
		*/
		public void Register(Window_Base a_window)
		{
			this.change_layerindex = true;
			this.list.Add(a_window);
		}

		/** 解除。
		*/
		public void UnRegister(Window_Base a_window)
		{
			this.change_layerindex = true;
			this.list.Remove(a_window);
		}

		/** ウィンドウを最前面にする。
		*/
		public void SetWindowPriorityTopMost(Window_Base a_window)
		{
			this.change_layerindex = true;

			int t_index_a = this.list.IndexOf(a_window);
			int t_index_b = this.list.Count - 1;

			if((t_index_a != t_index_b)&&(t_index_a >= 0)&&(t_index_b >= 0)){
				Window_Base t_item = this.list[t_index_a];
				this.list[t_index_a] = list[t_index_b];
				this.list[t_index_b] = t_item;				
			}
		}
	}
}

