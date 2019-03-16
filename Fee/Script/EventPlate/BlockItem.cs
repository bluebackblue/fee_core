

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief イベントプレート。ブロックアイテム。
*/


/** Fee.EventPlate
*/
namespace Fee.EventPlate
{
	/** BlockItem
	*/
	public class BlockItem : Fee.Deleter.DeleteItem_Base
	{
		/** deleter
		*/
		Fee.Deleter.Deleter deleter;

		/** eventplate
		*/
		private Item eventplate_button;
		private Item eventplate_viewitem;
		private Item eventplate_view;

		/** constructor
		*/
		public BlockItem(Fee.Deleter.Deleter a_deleter,long a_priority)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			this.eventplate_button = new Item(this.deleter,EventType.Button,a_priority);
			this.eventplate_viewitem = new Item(this.deleter,EventType.ViewItem,a_priority);
			this.eventplate_view = new Item(this.deleter,EventType.View,a_priority);

			this.eventplate_button.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			this.eventplate_viewitem.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			this.eventplate_view.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);

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

		/** 有効。設定。
		*/
		public void SetEnable(bool a_flag)
		{
			this.eventplate_button.SetEnable(a_flag);
			this.eventplate_viewitem.SetEnable(a_flag);
			this.eventplate_view.SetEnable(a_flag);
		}

		/** プライオリティ。設定。
		*/
		public void SetPriority(long a_priority)
		{
			this.eventplate_button.SetPriority(a_priority);
			this.eventplate_viewitem.SetPriority(a_priority);
			this.eventplate_view.SetPriority(a_priority);
		}

		/** 矩形。設定。
		*/
		public void SetRectMax()
		{
			this.eventplate_button.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			this.eventplate_viewitem.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
			this.eventplate_view.SetRect(0,0,Fee.Render2D.Render2D.VIRTUAL_W,Fee.Render2D.Render2D.VIRTUAL_H);
		}

		/** 矩形。設定。
		*/
		public void SetRect(ref Fee.Render2D.Rect2D_R<int> a_rect)
		{
			this.eventplate_button.SetRect(ref a_rect);
			this.eventplate_viewitem.SetRect(ref a_rect);
			this.eventplate_view.SetRect(ref a_rect);
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.eventplate_button.SetRect(a_x,a_y,a_w,a_h);
			this.eventplate_viewitem.SetRect(a_x,a_y,a_w,a_h);
			this.eventplate_view.SetRect(a_x,a_y,a_w,a_h);
		}

		/** 矩形。設定。
		*/
		public void SetXY(int a_x,int a_y)
		{
			this.eventplate_button.SetXY(a_x,a_y);
			this.eventplate_viewitem.SetXY(a_x,a_y);
			this.eventplate_view.SetXY(a_x,a_y);
		}
	}
}

