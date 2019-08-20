

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スクロール。ベース。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll2_Base
	*/
	public abstract class Scroll_Base<ITEM> : Scroll_Value_CallBack , Scroll_Drag_CallBack , Fee.Deleter.OnDelete_CallBackInterface , Fee.EventPlate.OnEventPlateOver_CallBackInterface<int>
		where ITEM : ScrollItem_Base
	{
		/** deleter
		*/
		protected Fee.Deleter.Deleter deleter;

		/** drawpriority
		*/
		protected long drawpriority;

		/** eventplate
		*/
		private Fee.EventPlate.Item eventplate;

		/** list
		*/
		private System.Collections.Generic.List<ITEM> list;

		/** scroll_value
		*/
		protected Scroll_Value scroll_value;

		/** scroll_drag
		*/
		protected Scroll_Drag scroll_drag;

		/** 矩形。
		*/
		protected Fee.Geometry.Rect2D_R<int> rect;

		/** scroll_type
		*/
		private Scroll_Type scroll_type;

		/** is_onover
		*/
		private bool is_onover;

		/** constructor
		*/
		public Scroll_Base(Fee.Deleter.Deleter a_deleter,long a_drawpriority,Scroll_Type a_scroll_type,int a_item_length)
		{
			//deleter
			this.deleter = new Deleter.Deleter();

			//drawpriority
			this.drawpriority = a_drawpriority;

			//eventplate
			this.eventplate = new EventPlate.Item(this.deleter,EventPlate.EventType.View,a_drawpriority);
			this.eventplate.SetOnEventPlateOver(this,-1);

			//list
			this.list = new System.Collections.Generic.List<ITEM>();

			//scroll_value
			this.scroll_value.Initialize();
			this.scroll_value.SetCallBack(this);
			this.scroll_value.SetItemLength(a_item_length);

			//scroll_drag
			this.scroll_drag.Initialize();
			this.scroll_drag.SetCallBack(this);

			//rect
			this.rect.Set(0,0,0,0);

			//scroll_type
			this.scroll_type = a_scroll_type;

			//is_onover
			this.is_onover = false;

			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** [Scroll_Base]コールバック。矩形。設定。
		*/
		protected abstract void OnChangeRect();

		/** [Scroll_Base]コールバック。表示位置変更。
		*/
		protected abstract void OnChangeViewPosition();

		/** [Scroll_Base]コールバック。リスト数変更。
		*/
		protected abstract void OnChangeListCount();

		/** [Scroll_Base]コールバック。描画プライオリティ変更。
		*/
		protected abstract void OnChangeDrawPriority();

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			this.deleter.DeleteAll();
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートに入場。
		*/
		public void OnEventPlateEnter(int a_id)
		{
			this.is_onover = true;
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートから退場。
		*/
		public void OnEventPlateLeave(int a_id)
		{
			this.is_onover = false;
		}

		/** 描画プライオリティー。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			if(this.drawpriority != a_drawpriority){
				this.drawpriority = a_drawpriority;

				this.eventplate.SetPriority(a_drawpriority);

				//コールバック。描画プライオリティ変更。
				this.OnChangeDrawPriority();
			}
		}

		/** オンオーバー。取得。
		*/
		public bool IsOnOver()
		{
			return this.is_onover;
		}

		/** アイテム。取得。
		*/
		public ITEM GetItem(int a_index)
		{
			if((0 <= a_index)&&(a_index < this.list.Count)){
				return this.list[a_index];
			}else{
				Tool.Assert(false);
				return null;
			}
		}

		/** スクロールタイプ。取得。
		*/
		public Scroll_Type GetScrollType()
		{
			return this.scroll_type;
		}

		/** リスト数。取得。
		*/
		public int GetListCount()
		{
			return this.scroll_value.GetListCount();
		}

		/** 表示位置。取得。

			[Scroll_Drag_CallBack]コールバック。表示位置。取得。

		*/
		public int GetViewPosition()
		{
			return this.scroll_value.GetViewPosition();
		}

		/** アイテム幅。取得。
		*/
		public int GetItemLength()
		{
			return this.scroll_value.GetItemLength();
		}

		/** 表示幅。取得。
		*/
		public int GetViewLength()
		{
			return this.scroll_value.GetViewLength();
		}

		/** 表示開始インデックス。取得。
		*/
		public int GetViewStartIndex()
		{
			return this.scroll_value.GetViewStartIndex();
		}

		/** 表示終了インデックス。取得。
		*/
		public int GetViewEndIndex()
		{
			return this.scroll_value.GetViewEndIndex();
		}

		/** 表示位置。設定。

		[Scroll_Drag_CallBack]コールバック。表示位置。設定。

		*/
		public void SetViewPosition(int a_view_position)
		{
			this.scroll_value.SetViewPosition(a_view_position);

			//[Scroll_Base]コールバック。表示位置変更。
			this.OnChangeViewPosition();
		}

 		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			//rect
			this.rect.Set(a_x,a_y,a_w,a_h);

			//eventplate
			this.eventplate.SetRect(in this.rect);

			//SetViewLength
			if(this.scroll_type == Scroll_Type.Vertical){
				this.scroll_value.SetViewLength(a_h);
			}else{
				this.scroll_value.SetViewLength(a_w);
			}

			//位置更新。
			if(this.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.scroll_value.GetViewStartIndex();ii<=this.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].SetClipRect(in this.rect);
					this.OnItemPositionChange(ii);
					this.OnItemOtherPositionChange(ii);

					this.OnItemWHChange(ii);
				}
			}

			//[Scroll_Base]コールバック。矩形。設定。
			this.OnChangeRect();
		}

 		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			//rect
			this.rect = a_rect;

			//eventplate
			this.eventplate.SetRect(in this.rect);

			//SetViewLength
			if(this.scroll_type == Scroll_Type.Vertical){
				this.scroll_value.SetViewLength(a_rect.h);
			}else{
				this.scroll_value.SetViewLength(a_rect.w);
			}

			//位置更新。
			if(this.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.scroll_value.GetViewStartIndex();ii<=this.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].SetClipRect(in this.rect);
					this.OnItemPositionChange(ii);
					this.OnItemOtherPositionChange(ii);

					this.OnItemWHChange(ii);
				}
			}

			//[Scroll_Base]コールバック。矩形。設定。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			//rect
			this.rect.x = a_x;

			//eventplate
			this.eventplate.SetX(a_x);

			//位置更新。
			if(this.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.scroll_value.GetViewStartIndex();ii<=this.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].SetClipRect(in this.rect);
					this.OnItemPositionChange(ii);
					this.OnItemOtherPositionChange(ii);
				}
			}

			//[Scroll_Base]コールバック。矩形。設定。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			//rect
			this.rect.y = a_y;

			//eventplate
			this.eventplate.SetY(a_y);

			//位置更新。
			if(this.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.scroll_value.GetViewStartIndex();ii<=this.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].SetClipRect(in this.rect);
					this.OnItemPositionChange(ii);
					this.OnItemOtherPositionChange(ii);
				}
			}

			//[Scroll_Base]コールバック。矩形。設定。
			this.OnChangeRect();
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			//rect
			this.rect.x = a_x;
			this.rect.y = a_y;

			//eventplate
			this.eventplate.SetXY(a_x,a_y);

			//位置更新。
			if(this.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.scroll_value.GetViewStartIndex();ii<=this.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].SetClipRect(in this.rect);
					this.OnItemPositionChange(ii);
					this.OnItemOtherPositionChange(ii);
				}
			}

			//[Scroll_Base]コールバック。矩形。設定。
			this.OnChangeRect();
		}

		/** 範囲内。チェック。

		[Scroll_Drag_CallBack]コールバック。範囲チェック。

		*/
		public bool IsRectIn(in Fee.Geometry.Pos2D<int> a_position)
		{
			return Fee.Geometry.Range.IsRectIn(in this.rect,in a_position);
		}

		/** [Scroll_Drag_CallBack]コールバック。スクロール方向の値。取得。
		*/
		public int GetScrollDirectionValue(in Fee.Geometry.Pos2D<int> a_position)
		{
			if(this.scroll_type == Scroll_Type.Vertical){
				return a_position.y;
			}else{
				return a_position.x;
			}
		}

		/** [Scroll_Value_CallBack]コールバック。位置変更。
		*/
		public void OnItemPositionChange(int a_index)
		{
			int t_pos = a_index * this.scroll_value.GetItemLength() - this.scroll_value.GetViewPosition();

			if(this.scroll_type == Scroll_Type.Vertical){
				this.list[a_index].SetY(t_pos + this.rect.y);
			}else{
				this.list[a_index].SetX(t_pos + this.rect.x);
			}
		}

		/** スクロール方向とは違う方向の位置変更。
		*/
		public void OnItemOtherPositionChange(int a_index)
		{
			if(this.scroll_type == Scroll_Type.Vertical){
				this.list[a_index].SetX(this.rect.x);
			}else{
				this.list[a_index].SetY(this.rect.y);
			}
		}

		/** スクロールのWH変更。
		*/
		public void OnItemWHChange(int a_index)
		{
			this.SetItemWH(this.list[a_index]);
		}

		/** SetItemWH
		*/
		private void SetItemWH(ITEM a_item)
		{
			if(this.scroll_type == Scroll_Type.Vertical){
				a_item.SetWH(this.rect.w,this.GetItemLength());
			}else{
				a_item.SetWH(this.GetItemLength(),this.rect.h);
			}
		}
		
		/** [Scroll_Value_CallBack]コールバック。表示変更。
		*/
		public void OnItemVisibleChange(int a_index,bool a_flag)
		{
			if(a_flag == true){

				//スクロール処理は表示中のみなので表示開始時に復元。
				{
					//スクロール方向とは違う方向の位置変更。
					if(this.scroll_type == Scroll_Type.Vertical){
						this.list[a_index].SetX(this.rect.x);
					}else{
						this.list[a_index].SetY(this.rect.y);
					}

					this.OnItemWHChange(a_index);

					//クリップ矩形。設定。
					this.list[a_index].SetClipRect(in this.rect);
				}

				this.list[a_index].OnViewIn();
			}else{
				this.list[a_index].OnViewOut();
			}
		}

		/** アイテム追加。最後尾。
		*/
		public void PushItem(ITEM a_new_item)
		{
			int t_index = this.list.Count;
		
			a_new_item.SetClipRect(in this.rect);

			//other direction
			if(this.scroll_type == Scroll_Type.Vertical){
				a_new_item.SetX(this.rect.x);
			}else{
				a_new_item.SetY(this.rect.y);
			}

			this.SetItemWH(a_new_item);

			this.list.Add(a_new_item);
			this.scroll_value.InsertItem(t_index);

			//[Scroll_Base]コールバック。リスト数変更。
			this.OnChangeListCount();
		}

		/** アイテム削除。最後尾。
		*/
		public ITEM PopItem()
		{
			if(this.list.Count > 0){
				int t_index = this.list.Count - 1;
				ITEM t_item = this.list[t_index];

				this.list.RemoveAt(t_index);
				this.scroll_value.RemoveItem(t_index);

				//[Scroll_Base]コールバック。リスト数変更。
				this.OnChangeListCount();

				return t_item;
			}

			return null;
		}

		/** アイテム追加。インデックス指定。

			a_index : リスト追加位置。

		*/
		public bool AddItem(ITEM a_new_item,int a_index)
		{
			if((0<=a_index)&&(a_index<=this.list.Count)){
				//追加。

				a_new_item.SetClipRect(in this.rect);

				//other direction
				if(this.scroll_type == Scroll_Type.Vertical){
					a_new_item.SetX(this.rect.x);
				}else{
					a_new_item.SetY(this.rect.y);
				}

				this.SetItemWH(a_new_item);

				this.list.Insert(a_index,a_new_item);
				this.scroll_value.InsertItem(a_index);

				//[Scroll_Base]コールバック。リスト数変更。
				this.OnChangeListCount();

				return true;
			}

			return false;
		}

		/** アイテム削除。インデックス指定。
		*/
		public ITEM RemoveItem(int a_index)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				//削除。
				ITEM t_item = this.list[a_index];
				
				this.list.RemoveAt(a_index);
				this.scroll_value.RemoveItem(a_index);

				//[Scroll_Base]コールバック。リスト数変更。
				this.OnChangeListCount();

				return t_item;
			}
			return null;
		}

		/** 全アイテム削除。
		*/
		public void RemoveAllItem()
		{
			this.list.Clear();
			this.scroll_value.RemoveAllItem();

			//[Scroll_Base]コールバック。リスト数変更。
			this.OnChangeListCount();
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
			System.Collections.Generic.List<ITEM> t_capture_list = new System.Collections.Generic.List<ITEM>();

			if(this.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.scroll_value.GetViewStartIndex();ii<=this.scroll_value.GetViewEndIndex();ii++){
					t_capture_list.Add(this.list[ii]);
				}
			}

			this.list.Sort(a_comparison);

			//位置。
			if(this.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.scroll_value.GetViewStartIndex();ii<=this.scroll_value.GetViewEndIndex();ii++){
					this.OnItemPositionChange(ii);
				}
			}

			//範囲外へ。
			for(int ii=0;ii<t_capture_list.Count;ii++){
				int t_now_index = this.list.IndexOf(t_capture_list[ii]);
				if(t_now_index < this.scroll_value.GetViewStartIndex()||(this.scroll_value.GetViewEndIndex() < t_now_index)){
					this.OnItemVisibleChange(t_now_index,false);
				}
			}

			//範囲内へ。
			if(this.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.scroll_value.GetViewStartIndex();ii<=this.scroll_value.GetViewEndIndex();ii++){
					int t_old_index = t_capture_list.IndexOf(this.list[ii]);
					if(t_old_index < 0){
						this.OnItemVisibleChange(ii,true);
					}
				}
			}
		}

		/** 入れ替え。
		*/
		public void Swap(int a_index_a,int a_index_b)
		{
			if((0<=a_index_a)&&(0<=a_index_b)&&(a_index_a<this.list.Count)&&(a_index_b<this.list.Count)){

				ITEM t_item = this.list[a_index_a];
				this.list[a_index_a] = this.list[a_index_b];
				this.list[a_index_b] = t_item;

				bool t_a = false;
				bool t_b = false;
				if((this.scroll_value.GetViewStartIndex() <= a_index_a)&&(a_index_a <= this.scroll_value.GetViewEndIndex())){
					this.OnItemPositionChange(a_index_a);
					t_a = true;
				}
				if((this.scroll_value.GetViewStartIndex() <= a_index_b)&&(a_index_b <= this.scroll_value.GetViewEndIndex())){
					this.OnItemPositionChange(a_index_b);
					t_b = true;
				}

				if((t_a == true)&&(t_b == false)){
					this.OnItemVisibleChange(a_index_a,true);
					this.OnItemVisibleChange(a_index_b,false);
				}else if((t_a == false)&&(t_b == true)){
					this.OnItemVisibleChange(a_index_a,false);
					this.OnItemVisibleChange(a_index_b,true);
				}
			}
		}

		/** ドラッグスクロール速度。設定。
		*/
		public void SetDragScrollSpeed(float a_speed)
		{
			this.scroll_drag.SetSpeed(a_speed);
		}

		/** ドラッグスクロール速度。取得。
		*/
		public float GetDragScrollSpeed()
		{
			return this.scroll_drag.GetSpeed();
		}

		/** ドラッグスクロール。更新。
		*/
		public void DragScrollUpdate()
		{
			this.scroll_drag.Main(this.is_onover);
		}
	}
}

