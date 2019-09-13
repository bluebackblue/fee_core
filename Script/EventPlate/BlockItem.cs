

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief イベントプレート。ブロックアイテム。
*/


/** Fee.EventPlate
*/
namespace Fee.EventPlate
{
	/** BlockItem
	*/
	public class BlockItem : Fee.Deleter.OnDelete_CallBackInterface
	{
		/** deleter
		*/
		Fee.Deleter.Deleter deleter;

		/** eventplate
		*/
		private Item eventplate_window;
		private Item eventplate_view;
		private Item eventplate_viewitem;
		private Item eventplate_button;

		/** constructor
		*/
		public BlockItem(Fee.Deleter.Deleter a_deleter,long a_priority,EventTypeMask a_eventtype_mask)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			if((a_eventtype_mask&EventTypeMask.Window) > 0){
				this.eventplate_window = new Item(this.deleter,EventType.Window,a_priority);
				this.eventplate_window.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			}else{
				this.eventplate_window = null;
			}

			if((a_eventtype_mask&EventTypeMask.View) > 0){
				this.eventplate_view = new Item(this.deleter,EventType.View,a_priority);
				this.eventplate_view.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			}else{
				this.eventplate_view = null;
			}

			if((a_eventtype_mask&EventTypeMask.ViewItem) > 0){
				this.eventplate_viewitem = new Item(this.deleter,EventType.ViewItem,a_priority);
				this.eventplate_viewitem.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			}else{
				this.eventplate_viewitem = null;
			}

			if((a_eventtype_mask&EventTypeMask.Button) > 0){
				this.eventplate_button = new Item(this.deleter,EventType.Button,a_priority);
				this.eventplate_button.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			}else{
				this.eventplate_button = null;
			}

			//削除管理。
			if(a_deleter != null){
				a_deleter.Regist(this);
			}
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			this.deleter.DeleteAll();
		}

		/** 有効。設定。
		*/
		public void SetEnable(bool a_flag)
		{
			if(this.eventplate_window != null){
				this.eventplate_window.SetEnable(a_flag);
			}
			if(this.eventplate_view != null){
				this.eventplate_view.SetEnable(a_flag);
			}
			if(this.eventplate_viewitem != null){
				this.eventplate_viewitem.SetEnable(a_flag);
			}
			if(this.eventplate_button != null){
				this.eventplate_button.SetEnable(a_flag);
			}
		}

		/** プライオリティ。設定。
		*/
		public void SetPriority(long a_priority)
		{
			if(this.eventplate_window != null){
				this.eventplate_window.SetPriority(a_priority);
			}
			if(this.eventplate_view != null){
				this.eventplate_view.SetPriority(a_priority);
			}
			if(this.eventplate_viewitem != null){
				this.eventplate_viewitem.SetPriority(a_priority);
			}
			if(this.eventplate_button != null){
				this.eventplate_button.SetPriority(a_priority);
			}
		}

		/** 矩形。設定。
		*/
		public void SetRectMax()
		{
			if(this.eventplate_window != null){
				this.eventplate_window.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			}
			if(this.eventplate_view != null){
				this.eventplate_view.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			}
			if(this.eventplate_viewitem != null){
				this.eventplate_viewitem.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			}
			if(this.eventplate_button != null){
				this.eventplate_button.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			}
		}

		/** 矩形。設定。
		*/
		public void SetRect(in Fee.Geometry.Rect2D_R<int> a_rect)
		{
			if(this.eventplate_window != null){
				this.eventplate_window.SetRect(in a_rect);
			}
			if(this.eventplate_view != null){
				this.eventplate_view.SetRect(in a_rect);
			}
			if(this.eventplate_viewitem != null){
				this.eventplate_viewitem.SetRect(in a_rect);
			}
			if(this.eventplate_button != null){
				this.eventplate_button.SetRect(in a_rect);
			}
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			if(this.eventplate_window != null){
				this.eventplate_window.SetRect(a_x,a_y,a_w,a_h);
			}
			if(this.eventplate_view != null){
				this.eventplate_view.SetRect(a_x,a_y,a_w,a_h);
			}
			if(this.eventplate_viewitem != null){
				this.eventplate_viewitem.SetRect(a_x,a_y,a_w,a_h);
			}
			if(this.eventplate_button != null){
				this.eventplate_button.SetRect(a_x,a_y,a_w,a_h);
			}
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			if(this.eventplate_window != null){
				this.eventplate_window.SetXY(a_x,a_y);
			}
			if(this.eventplate_view != null){
				this.eventplate_view.SetXY(a_x,a_y);
			}
			if(this.eventplate_viewitem != null){
				this.eventplate_viewitem.SetXY(a_x,a_y);
			}
			if(this.eventplate_button != null){
				this.eventplate_button.SetXY(a_x,a_y);
			}
		}
	}
}

