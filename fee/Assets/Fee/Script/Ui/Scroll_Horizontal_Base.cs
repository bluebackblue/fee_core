﻿using System.Collections;
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

		/** 矩形。
		*/
		protected NRender2D.Rect2D_R<int> rect;

		/** リスト。
		*/
		protected List<ITEM> list;

		/** constructor
		*/
		public Scroll_Horizontal_Base(NDeleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
			:
			base(a_item_length)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//矩形。
			this.rect.Set(0,0,0,0);

			//リスト。
			this.list = new List<ITEM>();

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** [Scroll_Horizontal_Base]コールバック。矩形。設定。
		*/
		protected abstract void OnSetRect(int a_x,int a_y,int a_w,int a_h);

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

		/** [Scroll_Base]アイテム移動。
		*/
		public override void OnMove(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				int t_x = this.rect.x + a_index * this.item_length - this.view_position;
				this.list[a_index].SetX(t_x);
			}
		}

		/** [Scroll_Base]表示開始。
		*/
		public override void OnViewIn(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				if(this.list[a_index].IsViewIn() == false){
					this.list[a_index].SetViewIn(true);
					this.list[a_index].OnViewIn();
				}
			}
		}

		/** [Scroll_Base]表示終了。
		*/
		public override void OnViewOut(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				if(this.list[a_index].IsViewIn() == true){
					this.list[a_index].SetViewIn(false);
					this.list[a_index].OnViewOut();
				}
			}
		}

		/** [Scroll_Base]コールバック。表示位置変更。
		*/
		/*
		public override void OnChangeViewPosition(int a_view_position)
		{
		}
		*/

		/** [Scroll_Horizontal_Base]コールバック。
		*/
		public abstract void OnChangeListCount();

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			//表示幅。
			this.view_length = a_w;

			//rect
			this.rect.Set(a_x,a_y,a_w,a_h);

			//list
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetClipRect(ref this.rect);
			}

			//表示範囲更新。
			this.UpdateView_PositionChange();

			//コールバック。
			this.OnSetRect(a_x,a_y,a_w,a_h);
		}

		/** 最後尾追加。
		*/
		public void PushItem(ITEM a_new_item)
		{
			//追加。
			this.InitItem(a_new_item);
			this.list.Add(a_new_item);

			//表示範囲更新。
			this.UpdateView_Insert(this.list.Count - 1);

			//コールバック。
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

				//コールバック。
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

				//コールバック。
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

				//コールバック。
				this.OnChangeListCount();

				return t_item;
			}
			return null;
		}
	}
}
