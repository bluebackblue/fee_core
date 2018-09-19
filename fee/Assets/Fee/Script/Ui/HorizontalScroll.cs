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
	/** HorizontalScroll
	*/
	public class HorizontalScroll<ITEM> : Scroll_Base , NDeleter.DeleteItem_Base
		where ITEM : ScrollItem_Base
	{
		/** deleter
		*/
		private NDeleter.Deleter deleter;

		/** 矩形。
		*/
		private NRender2D.Rect2D_R<int> rect;

		/** bg
		*/
		private NUi.ClipSprite bg;

		/** リスト。
		*/
		private List<ITEM> list;

		/** constructor
		*/
		public HorizontalScroll(NDeleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
			:
			base(a_item_length)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//矩形。
			this.rect.Set(0,0,0,0);

			//bg
			this.bg = new ClipSprite(this.deleter,null,a_drawpriority);
			this.bg.SetTexture(Texture2D.whiteTexture);
			this.bg.SetRect(ref this.rect);
			this.bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetColor(0.0f,0.0f,0.0f,0.1f);

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
			a_item.SetY(this.rect.y);
		}

		/** リスト数。取得。
		*/
		public override int GetListCount()
		{
			return this.list.Count;
		}

		/** アイテム移動。
		*/
		public override void OnMove(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				int t_x = this.rect.x + a_index * this.item_length - this.view_position;
				this.list[a_index].SetX(t_x);
			}
		}

		/** 表示開始。
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

		/** 表示終了。
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

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			//表示幅。
			this.view_length = a_w;

			//rect
			this.rect.Set(a_x,a_y,a_w,a_h);

			//bg
			this.bg.SetRect(ref this.rect);

			//list
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetClipRect(ref this.rect);
			}

			//表示範囲更新。
			this.UpdateView_PositionChange();
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

				return t_item;
			}
			return null;
		}
	}
}

