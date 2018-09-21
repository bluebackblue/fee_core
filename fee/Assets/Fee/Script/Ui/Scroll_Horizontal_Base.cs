using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。横スクロール。
*/


/** NUi
*/
namespace NUi
{
	/** Scroll_Horizontal_Base
	*/
	public abstract class Scroll_Horizontal_Base<ITEM> : Scroll_Base , NDeleter.DeleteItem_Base
		where ITEM : ScrollItem_Base
	{
		/** deleter
		*/
		protected NDeleter.Deleter deleter;

		/** リスト。
		*/
		protected List<ITEM> list;

		/** 表示範囲内のアイテムをキャプチャしたリスト。
		*/
		protected List<ITEM> capture_list;

		/** constructor
		*/
		public Scroll_Horizontal_Base(NDeleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
			:
			base(a_item_length)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//リスト。
			this.list = new List<ITEM>();

			//表示範囲内のアイテムをキャプチャ。
			this.capture_list = new List<ITEM>();

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.deleter.DeleteAll();
		}

		/** アイテム。取得。
		*/
		public ITEM GetItem(int a_index)
		{
			return this.list[a_index];
		}

		/** アイテム。初期化。
		*/
		private void InitItem(ITEM a_item)
		{
			//クリック矩形。設定。
			a_item.SetClipRect(ref this.rect);

			//矩形。設定。
			a_item.SetY(this.rect.y);
		}

		/** [Scroll_Base]リスト数。取得。
		*/
		public override int GetListCount()
		{
			return this.list.Count;
		}

		/** [Scroll_Base]コールバック。アイテム移動。
		*/
		protected override void OnMoveItem_FromScrollBase(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				int t_x = this.rect.x + a_index * this.item_length - this.view_position;
				this.list[a_index].SetX(t_x);
			}
		}

		/** [Scroll_Base]コールバック。表示開始。
		*/
		protected override void OnViewInItem_FromScrollBase(int a_index)
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
		protected override void OnViewOutItem_FromScrollBase(int a_index)
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
		protected override void OnChangeRect_FromScrollBase()
		{
			//表示幅。
			this.view_length = this.rect.w;

			//list
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetClipRect(ref this.rect);
			}

			//表示範囲更新。
			this.UpdateView_PositionChange();

			//コールバック。矩形変更。
			this.OnChangeRect();
		}

		/** [Scroll_Base]コールバック。表示位置変更。
		*/
		protected override void OnChangeViewPosition_FromScrollBase()
		{
			this.OnChangeViewPosition();
		}

		/** [Scroll_Horizontal_Base]コールバック。リスト数変更。
		*/
		protected abstract void OnChangeListCount();

		/** [Scroll_Horizontal_Base]コールバック。矩形変更。
		*/
		protected abstract void OnChangeRect();

		/** [Scroll_Horizontal_Base]コールバック。表示位置変更。
		*/
		protected abstract void OnChangeViewPosition();

		/** 最後尾追加。
		*/
		public void PushItem(ITEM a_new_item)
		{
			//追加。
			this.InitItem(a_new_item);
			this.list.Add(a_new_item);

			//表示範囲更新。
			this.UpdateView_Insert(this.list.Count - 1);

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
				this.InitItem(a_new_item);
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
					this.OnMoveItem_FromScrollBase(ii);
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
			int t_index = this.list.FindIndex((ITEM a_test) => {return a_test == a_item;});

			Debug.Log(t_index.ToString());

			return t_index;
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

