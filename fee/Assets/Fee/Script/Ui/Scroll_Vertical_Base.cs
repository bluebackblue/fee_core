using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。縦スクロール。
*/


/** NUi
*/
namespace NUi
{
	/** Scroll_Vertical_Base
	*/
	public abstract class Scroll_Vertical_Base<ITEM> : Scroll_Base , NDeleter.DeleteItem_Base
		where ITEM : ScrollItem_Base
	{
		/** deleter
		*/
		protected NDeleter.Deleter deleter;

		/** リスト。
		*/
		protected List<ITEM> list;

		/** constructor
		*/
		public Scroll_Vertical_Base(NDeleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
			:
			base(a_item_length)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//リスト。
			this.list = new List<ITEM>();

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
			a_item.SetX(this.rect.x);
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
				int t_y = this.rect.y + a_index * this.item_length - this.view_position;
				this.list[a_index].SetY(t_y);
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
			this.view_length = this.rect.h;

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

		/** [Scroll_Vertical_Base]コールバック。リスト数変更。
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
	}
}

