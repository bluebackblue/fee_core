using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スクロール。リスト。
*/


/** NUi
*/
namespace NUi
{
	/** Scroll_List_Base
	*/
	public abstract class Scroll_List_Base<ITEM> : Scroll_Base
		where ITEM : ScrollItem_Base
	{
		/** リスト。
		*/
		protected List<ITEM> list;

		/** 表示範囲内のアイテムをキャプチャしたリスト。
		*/
		protected List<ITEM> capture_list;

		/** constructor
		*/
		public Scroll_List_Base(NDeleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
			:
			base(a_deleter,a_drawpriority,a_item_length)
		{
			//リスト。
			this.list = new List<ITEM>();

			//表示範囲内のアイテムをキャプチャ。
			this.capture_list = new List<ITEM>();
		}

		/** [Scroll_List_Base]アイテムの位置更新。スクロール方向の座標。
		*/
		protected abstract void UpdateItemPos(ITEM a_item,int a_index);

		/** [Scroll_List_Base]アイテムの位置更新。スクロール方向ではない座標。
		*/
		protected abstract void UpdateItemOtherPos(ITEM a_item,int a_index);

		/** [Scroll_List_Base]表示幅更新。
		*/
		protected abstract void UpdateViewLength();

		/** アイテム。取得。
		*/
		public ITEM GetItem(int a_index)
		{
			return this.list[a_index];
		}

		/** [Scroll_Base]リスト数。取得。
		*/
		public override int GetListCount()
		{
			return this.list.Count;
		}

		/** [Scroll_Base]コールバック。アイテム移動。
		*/
		protected override void OnMoveItem_FromBase(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				//アイテムの位置更新。スクロール方向の座標。
				this.UpdateItemPos(this.list[a_index],a_index);
			}
		}

		/** [Scroll_Base]コールバック。表示開始。
		*/
		protected override void OnViewInItem_FromBase(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				if(this.list[a_index].IsViewIn() == false){
					this.list[a_index].SetViewIn(true);
					this.list[a_index].OnViewIn();
				}
			}
		}

		/** [Scroll_Base]コールバック。表示終了。
		*/
		protected override void OnViewOutItem_FromBase(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				if(this.list[a_index].IsViewIn() == true){
					this.list[a_index].SetViewIn(false);
					this.list[a_index].OnViewOut();
				}
			}
		}

		/** [Scroll_Base]コールバック。矩形変更。
		*/
		protected override void OnChangeRect_FromBase()
		{
			//表示幅更新。
			this.UpdateViewLength();

			//list
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetClipRect(ref this.rect);
			}

			//表示範囲更新。
			this.UpdateView_PositionChange();

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** アイテム初期化。
		*/
		private void InitItem(ITEM a_item,int a_index)
		{
			//クリップ矩形。設定。
			a_item.SetClipRect(ref this.rect);

			//アイテムの位置更新。スクロール方向ではない座標。
			this.UpdateItemOtherPos(a_item,a_index);
		}

		/** 最後尾追加。
		*/
		public void PushItem(ITEM a_new_item)
		{
			//追加。
			int t_index = this.list.Count;
			this.InitItem(a_new_item,t_index);
			this.list.Add(a_new_item);

			//表示範囲更新。
			this.UpdateView_Insert(t_index);

			//コールバック。リスト数変更。
			this.OnChangeListCount();
		}

		/** 最後尾削除。
		*/
		public ITEM PopItem()
		{
			if(this.list.Count > 0){
				//削除。
				int t_list_index = this.list.Count - 1;
				ITEM t_item = this.list[t_list_index];
				this.list.RemoveAt(t_list_index);

				//表示範囲更新。
				this.UpdateView_Remove(t_list_index);

				//コールバック。リスト数変更。
				this.OnChangeListCount();

				return t_item;
			}

			return null;
		}

		/** 追加。
		*/
		public void AddItem(ITEM a_new_item,int a_index)
		{
			if((0<=a_index)&&(a_index<=this.list.Count)){
				//追加。
				this.InitItem(a_new_item,a_index);
				this.list.Insert(a_index,a_new_item);

				//表示範囲更新。
				this.UpdateView_Insert(a_index);

				//コールバック。リスト数変更。
				this.OnChangeListCount();
			}
		}

		/** 削除。
		*/
		public ITEM RemoveItem(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				//削除。
				ITEM t_item = this.list[a_index];
				this.list.RemoveAt(a_index);

				//表示範囲更新。
				this.UpdateView_Remove(a_index);

				//コールバック。リスト数変更。
				this.OnChangeListCount();

				return t_item;
			}
			return null;
		}

		/** 表示範囲内のアイテムをキャプチャ。
		*/
		public void CaptureViewList()
		{
			this.capture_list.Clear();

			if(this.viewindex_st >= 0){
				for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
					this.list[ii].SetCaptureViewOutFlag(true);
					this.capture_list.Add(this.list[ii]);
				}
			}
		}

		/** キャプチャから表示範囲更新。
		*/
		public void ViewUpdateFromCapture()
		{
			if(this.viewindex_st >= 0){
				for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
					//最新の表示範囲内のフラグを消す。
					this.list[ii].SetCaptureViewOutFlag(false);

					//アイテム移動。
					this.OnMoveItem_FromBase(ii);
				}
			}

			//表示範囲外移動チェック。
			for(int ii=0;ii<this.capture_list.Count;ii++){
				if(this.capture_list[ii].GetCaptureViewOutFlag() == true){
					if(this.capture_list[ii].IsViewIn() == true){
						this.capture_list[ii].SetViewIn(false);
						this.capture_list[ii].OnViewOut();
					}
				}
			}

			//表示範囲内移動チェック。
			if(this.viewindex_st >= 0){
				for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
					if(this.list[ii].IsViewIn() == false){
						this.list[ii].SetViewIn(true);
						this.list[ii].OnViewIn();
					}
				}
			}
		}

		/** インデックス検索。
		*/
		public int FindIndex(ITEM a_item)
		{
			return this.list.IndexOf(a_item);
		}

		/** ソート。
		*/
		public void Sort(System.Comparison<ITEM> a_comparison)
		{
			//表示範囲内のアイテムをキャプチャ。
			this.CaptureViewList();

			//ソート。
			this.list.Sort(a_comparison);

			//キャプチャから表示範囲更新。
			this.ViewUpdateFromCapture();
		}

		/** 入れ替え。
		*/
		public void Swap(int a_index_a,int a_index_b)
		{
			if((0<=a_index_a)&&(0<=a_index_b)&&(a_index_a<this.list.Count)&&(a_index_b<this.list.Count)){
				//表示範囲内のアイテムをキャプチャ。
				this.CaptureViewList();

				//ソート。
				ITEM t_item = this.list[a_index_a];
				this.list[a_index_a] = this.list[a_index_b];
				this.list[a_index_b] = t_item;

				//キャプチャから表示範囲更新。
				this.ViewUpdateFromCapture();
			}
		}
	}
}

