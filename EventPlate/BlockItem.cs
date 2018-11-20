using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief イベントプレート。ブロックアイテム。
*/


/** NEventPlate
*/
namespace NEventPlate
{
	/** BlockItem
	*/
	public class BlockItem : NDeleter.DeleteItem_Base
	{
		/** deleter
		*/
		NDeleter.Deleter deleter;

		/** eventplate
		*/
		private Item eventplate_button;
		private Item eventplate_viewitem;
		private Item eventplate_view;

		/** constructor
		*/
		public BlockItem(NDeleter.Deleter a_deleter,long a_priority)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			this.eventplate_button = new Item(this.deleter,EventType.Button,a_priority);
			this.eventplate_viewitem = new Item(this.deleter,EventType.ViewItem,a_priority);
			this.eventplate_view = new Item(this.deleter,EventType.View,a_priority);

			this.eventplate_button.SetRect(0,0,NRender2D.Render2D.VIRTUAL_W,NRender2D.Render2D.VIRTUAL_H);
			this.eventplate_viewitem.SetRect(0,0,NRender2D.Render2D.VIRTUAL_W,NRender2D.Render2D.VIRTUAL_H);
			this.eventplate_view.SetRect(0,0,NRender2D.Render2D.VIRTUAL_W,NRender2D.Render2D.VIRTUAL_H);

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
	}
}

