

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
	/** Scroll_Param
	*/
	public struct Scroll_Param
	{
		/** drawpriority
		*/
		public long drawpriority;

		/** scroll_value
		*/
		public Scroll_Value scroll_value;

		/** scroll_drag
		*/
		public Scroll_Drag scroll_drag;

		/** 矩形。
		*/
		public Fee.Geometry.Rect2D_R<int> rect;

		/** scroll_type
		*/
		public Scroll_Type scroll_type;

		/** is_onover
		*/
		public bool is_onover;

		/** visible_flag
		*/
		public bool visible_flag;
	};

	/** Scroll_Base
	*/
	public abstract class Scroll_Base<ITEM> : Scroll_Value_CallBack , Scroll_Drag_CallBack , Fee.EventPlate.OnEventPlateOver_CallBackInterface<int> , System.Collections.IEnumerable
		where ITEM : ScrollItem_Base
	{
		/** eventplate
		*/
		public Fee.EventPlate.Item eventplate;

		/** list
		*/
		private System.Collections.Generic.List<ITEM> list;

		/** param
		*/
		protected Scroll_Param param;

		/** constructor

			プール用に作成。

		*/
		public Scroll_Base()
		{
		}

		/** プールから作成。
		*/
		public void InitializeFromPool(long a_drawpriority,Scroll_Type a_scroll_type,int a_item_length)
		{
			//eventplate
			this.eventplate = new EventPlate.Item(null,EventPlate.EventType.View,a_drawpriority);
			this.eventplate.SetOnEventPlateOver(this,-1);

			//list
			this.list = new System.Collections.Generic.List<ITEM>();

			//drawpriority
			this.param.drawpriority = a_drawpriority;

			//scroll_value
			this.param.scroll_value.Initialize();
			this.param.scroll_value.SetCallBack(this);
			this.param.scroll_value.SetItemLength(a_item_length);

			//scroll_drag
			this.param.scroll_drag.Initialize();
			this.param.scroll_drag.SetCallBack(this);

			//rect
			this.param.rect.Set(0,0,0,0);

			//scroll_type
			this.param.scroll_type = a_scroll_type;

			//is_onover
			this.param.is_onover = false;

			//visible_flag
			this.param.visible_flag = true;
		}

		/** プールへ削除。
		*/
		public void DeleteToPool()
		{
			//OnDelete
			this.eventplate.OnDelete();

			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].OnDelete();
			}
			this.list.Clear();
		}

		/** メモリから削除。
		*/
		public void DeleteFromMemory()
		{
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

		/** [Scroll_Base]コールバック。表示フラグ変更。
		*/
		protected abstract void OnChangeVisibleFlag();

		/** GetEnumerator
		*/
		public System.Collections.IEnumerator GetEnumerator()
		{
			return new Scroll_Enumerator<ITEM>(this);
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートに入場。
		*/
		public void OnEventPlateEnter(int a_id)
		{
			this.param.is_onover = true;
		}

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートから退場。
		*/
		public void OnEventPlateLeave(int a_id)
		{
			this.param.is_onover = false;
		}

		/** 描画プライオリティー。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			if(this.param.drawpriority != a_drawpriority){
				this.param.drawpriority = a_drawpriority;

				this.eventplate.SetPriority(a_drawpriority);

				if(this.param.scroll_value.GetViewStartIndex() >= 0){
					for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
						this.list[ii].OnChangeParentDrawPriority(a_drawpriority);
					}
				}

				//コールバック。描画プライオリティ変更。
				this.OnChangeDrawPriority();
			}
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			if(this.param.visible_flag != a_flag){
				this.param.visible_flag = a_flag;
				this.eventplate.SetEnable(a_flag);

				if(a_flag == true){
					if(this.param.scroll_value.GetViewStartIndex() >= 0){
						for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
							this.list[ii].OnViewIn();
						}
					}
				}else{
					if(this.param.scroll_value.GetViewStartIndex() >= 0){
						for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
							this.list[ii].OnViewOut();
						}
					}
				}

				//コールバック。表示フラグ変更。
				this.OnChangeVisibleFlag();
			}
		}

		/** オンオーバー。取得。
		*/
		public bool IsOnOver()
		{
			return this.param.is_onover;
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
			return this.param.scroll_type;
		}

		/** リスト数。取得。
		*/
		public int GetListCount()
		{
			return this.param.scroll_value.GetListCount();
		}

		/** 表示位置。取得。

			[Scroll_Drag_CallBack]コールバック。表示位置。取得。

		*/
		public float GetViewPosition()
		{
			return this.param.scroll_value.GetViewPositionFloat();
		}

		/** アイテム幅。取得。
		*/
		public int GetItemLength()
		{
			return this.param.scroll_value.GetItemLength();
		}

		/** 表示幅。取得。
		*/
		public int GetViewLength()
		{
			return this.param.scroll_value.GetViewLength();
		}

		/** 表示開始インデックス。取得。
		*/
		public int GetViewStartIndex()
		{
			return this.param.scroll_value.GetViewStartIndex();
		}

		/** 表示終了インデックス。取得。
		*/
		public int GetViewEndIndex()
		{
			return this.param.scroll_value.GetViewEndIndex();
		}

		/** 表示位置。設定。

		[Scroll_Drag_CallBack]コールバック。表示位置。設定。

		*/
		public void SetViewPosition(float a_view_position)
		{
			this.param.scroll_value.SetViewPosition(a_view_position);

			//[Scroll_Base]コールバック。表示位置変更。
			this.OnChangeViewPosition();
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			//rect
			this.param.rect.Set(a_x,a_y,a_w,a_h);

			//eventplate
			this.eventplate.SetRect(in this.param.rect);

			//SetViewLength
			if(this.param.scroll_type == Scroll_Type.Vertical){
				this.param.scroll_value.SetViewLength(a_h);
			}else{
				this.param.scroll_value.SetViewLength(a_w);
			}

			//位置更新。
			if(this.param.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].OnChangeParentClipRect(in this.param.rect);
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
			this.param.rect = a_rect;

			//eventplate
			this.eventplate.SetRect(in this.param.rect);

			//SetViewLength
			if(this.param.scroll_type == Scroll_Type.Vertical){
				this.param.scroll_value.SetViewLength(a_rect.h);
			}else{
				this.param.scroll_value.SetViewLength(a_rect.w);
			}

			//位置更新。
			if(this.param.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].OnChangeParentClipRect(in this.param.rect);
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
			this.param.rect.x = a_x;

			//eventplate
			this.eventplate.SetX(a_x);

			//位置更新。
			if(this.param.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].OnChangeParentClipRect(in this.param.rect);
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
			this.param.rect.y = a_y;

			//eventplate
			this.eventplate.SetY(a_y);

			//位置更新。
			if(this.param.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].OnChangeParentClipRect(in this.param.rect);
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
			this.param.rect.x = a_x;
			this.param.rect.y = a_y;

			//eventplate
			this.eventplate.SetXY(a_x,a_y);

			//位置更新。
			if(this.param.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
					this.list[ii].OnChangeParentClipRect(in this.param.rect);
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
			return Fee.Geometry.Range.IsRectIn(in this.param.rect,in a_position);
		}

		/** [Scroll_Drag_CallBack]コールバック。スクロール方向の値。取得。
		*/
		public int GetScrollDirectionValue(in Fee.Geometry.Pos2D<int> a_position)
		{
			if(this.param.scroll_type == Scroll_Type.Vertical){
				return a_position.y;
			}else{
				return a_position.x;
			}
		}

		/** [Scroll_Value_CallBack]コールバック。位置変更。
		*/
		public void OnItemPositionChange(int a_index)
		{
			int t_pos = a_index * this.param.scroll_value.GetItemLength() - this.param.scroll_value.GetViewPositionInt();

			if(this.param.scroll_type == Scroll_Type.Vertical){
				this.list[a_index].OnChangeRectY(t_pos + this.param.rect.y);
			}else{
				this.list[a_index].OnChangeRectX(t_pos + this.param.rect.x);
			}
		}

		/** スクロール方向とは違う方向の位置変更。
		*/
		public void OnItemOtherPositionChange(int a_index)
		{
			if(this.param.scroll_type == Scroll_Type.Vertical){
				this.list[a_index].OnChangeRectX(this.param.rect.x);
			}else{
				this.list[a_index].OnChangeRectY(this.param.rect.y);
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
			if(this.param.scroll_type == Scroll_Type.Vertical){
				a_item.OnChangeParentRectWH(this.param.rect.w,this.GetItemLength());
			}else{
				a_item.OnChangeParentRectWH(this.GetItemLength(),this.param.rect.h);
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
					if(this.param.scroll_type == Scroll_Type.Vertical){
						this.list[a_index].OnChangeRectX(this.param.rect.x);
					}else{
						this.list[a_index].OnChangeRectY(this.param.rect.y);
					}

					this.OnItemWHChange(a_index);

					//クリップ矩形。設定。
					this.list[a_index].OnChangeParentClipRect(in this.param.rect);
				}

				if(this.param.visible_flag == true){
					this.list[a_index].OnViewIn();
				}
			}else{
				if(this.param.visible_flag == true){
					this.list[a_index].OnViewOut();
				}
			}
		}

		/** アイテム追加。最後尾。
		*/
		public void PushItem(ITEM a_new_item)
		{
			int t_index = this.list.Count;
		
			//rect
			a_new_item.OnChangeParentClipRect(in this.param.rect);

			//drawpriority
			a_new_item.OnChangeParentDrawPriority(this.param.drawpriority);

			//other direction
			if(this.param.scroll_type == Scroll_Type.Vertical){
				a_new_item.OnChangeRectX(this.param.rect.x);
			}else{
				a_new_item.OnChangeRectY(this.param.rect.y);
			}

			this.SetItemWH(a_new_item);

			this.list.Add(a_new_item);
			this.param.scroll_value.InsertItem(t_index);

			//[Scroll_Base]コールバック。リスト数変更。
			this.OnChangeListCount();
		}

		/** アイテム削除。最後尾。
		*/
		public ITEM PopItem(bool a_call_ondelete)
		{
			if(this.list.Count > 0){
				int t_index = this.list.Count - 1;
				ITEM t_item = this.list[t_index];

				this.list.RemoveAt(t_index);
				this.param.scroll_value.RemoveItem(t_index);

				//[Scroll_Base]コールバック。リスト数変更。
				this.OnChangeListCount();

				if(a_call_ondelete == true){
					t_item.OnDelete();
				}

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

				//rect
				a_new_item.OnChangeParentClipRect(in this.param.rect);

				//drawpriority
				a_new_item.OnChangeParentDrawPriority(this.param.drawpriority);

				//other direction
				if(this.param.scroll_type == Scroll_Type.Vertical){
					a_new_item.OnChangeRectX(this.param.rect.x);
				}else{
					a_new_item.OnChangeRectY(this.param.rect.y);
				}

				this.SetItemWH(a_new_item);

				this.list.Insert(a_index,a_new_item);
				this.param.scroll_value.InsertItem(a_index);

				//[Scroll_Base]コールバック。リスト数変更。
				this.OnChangeListCount();

				return true;
			}

			return false;
		}

		/** アイテム削除。インデックス指定。
		*/
		public ITEM RemoveItem(int a_index,bool a_call_ondelete)
		{
			if((0<=a_index)&&(a_index<this.list.Count)){
				//削除。
				ITEM t_item = this.list[a_index];
				
				this.list.RemoveAt(a_index);
				this.param.scroll_value.RemoveItem(a_index);

				//[Scroll_Base]コールバック。リスト数変更。
				this.OnChangeListCount();

				if(a_call_ondelete == true){
					t_item.OnDelete();
				}

				return t_item;
			}
			return null;
		}

		/** 全アイテム削除。
		*/
		public void RemoveAllItem(bool a_call_ondelete)
		{
			if(a_call_ondelete == true){
				for(int ii=0;ii<this.list.Count;ii++){
					this.list[ii].OnDelete();
				}
			}

			this.list.Clear();
			this.param.scroll_value.RemoveAllItem();

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

			if(this.param.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
					t_capture_list.Add(this.list[ii]);
				}
			}

			this.list.Sort(a_comparison);

			//位置。
			if(this.param.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
					this.OnItemPositionChange(ii);
				}
			}

			//範囲外へ。
			for(int ii=0;ii<t_capture_list.Count;ii++){
				int t_now_index = this.list.IndexOf(t_capture_list[ii]);
				if(t_now_index < this.param.scroll_value.GetViewStartIndex()||(this.param.scroll_value.GetViewEndIndex() < t_now_index)){
					this.OnItemVisibleChange(t_now_index,false);
				}
			}

			//範囲内へ。
			if(this.param.scroll_value.GetViewStartIndex() >= 0){
				for(int ii=this.param.scroll_value.GetViewStartIndex();ii<=this.param.scroll_value.GetViewEndIndex();ii++){
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
				if((this.param.scroll_value.GetViewStartIndex() <= a_index_a)&&(a_index_a <= this.param.scroll_value.GetViewEndIndex())){
					this.OnItemPositionChange(a_index_a);
					t_a = true;
				}
				if((this.param.scroll_value.GetViewStartIndex() <= a_index_b)&&(a_index_b <= this.param.scroll_value.GetViewEndIndex())){
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
			this.param.scroll_drag.SetSpeed(a_speed);
		}

		/** ドラッグスクロール速度。取得。
		*/
		public float GetDragScrollSpeed()
		{
			return this.param.scroll_drag.GetSpeed();
		}

		/** ドラッグスクロール。更新。
		*/
		public void DragScrollUpdate(float a_eceleration,float a_delta)
		{
			this.param.scroll_drag.Main(this.param.is_onover,a_eceleration,a_delta);
		}
	}
}

