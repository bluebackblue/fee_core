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
	/** ScrollItem
	*/
	public class ScrollItem : NDeleter.DeleteItem_Base
	{
		/** deleter
		*/
		private NDeleter.Deleter deleter;

		/** sprite
		*/
		private NUi.ClipSprite sprite;

		/** constructor
		*/
		public ScrollItem(NDeleter.Deleter a_deleter)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//drawpriority
			long t_drawpriority = 1;

			//sprite
			this.sprite = new ClipSprite(this.deleter,null,t_drawpriority);
			this.sprite.SetRect(0,0,100,50);
			this.sprite.SetTexture(Texture2D.whiteTexture);
			this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite.SetClip(true);
			this.sprite.SetClipRect(0,0,0,0);
			this.sprite.SetColor(Random.value,Random.value,Random.value,1.0f);

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

		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			this.sprite.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.sprite.SetX(a_x);
		}

		/** クリック。矩形。
		*/
		public void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.sprite.SetClipRect(ref a_rect);
		}
	};

	/** VerticalScroll
	*/
	public class VerticalScroll : NDeleter.DeleteItem_Base
	{
		/** deleter
		*/
		private NDeleter.Deleter deleter;

		/** rect
		*/
		private NRender2D.Rect2D_R<int> rect;

		/** bg
		*/
		private NUi.ClipSprite bg;

		/** 位置。
		*/
		private int position;

		/** リスト。
		*/
		private List<ScrollItem> list;

		/** item_height
		*/
		private int item_height;

		/** constructor
		*/
		public VerticalScroll(NDeleter.Deleter a_deleter)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//rect
			this.rect.Set(0,0,0,0);

			//bg
			this.bg = new ClipSprite(this.deleter,null,0);
			this.bg.SetTexture(Texture2D.whiteTexture);
			this.bg.SetRect(ref this.rect);
			this.bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetColor(0.0f,0.0f,0.0f,0.1f);

			//位置。
			this.position = 0;

			//リスト。
			this.list = new List<ScrollItem>();

			//item_height
			this.item_height = 0;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** 位置。設定。
		*/
		public void SetPosition(int a_position)
		{
			this.position = a_position;

			//位置再計算。
			int t_y = this.rect.y - this.position;
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetY(t_y);
				t_y += this.item_height;
			}
		}

		/** 位置。取得。
		*/
		public int GetPosition()
		{
			return this.position;
		}

		/** アイテム縦幅。設定。
		*/
		public void SetItemHight(int a_hight)
		{
			this.item_height = a_hight;
		}

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.rect.Set(a_x,a_y,a_w,a_h);

			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetClipRect(ref this.rect);
			}

			//位置再計算。
			int t_y = this.rect.y - this.position;
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetY(t_y);
				t_y += this.item_height;
			}

			//bg
			this.bg.SetRect(ref this.rect);
		}

		/** リスト追加。
		*/
		public void AddList()
		{
			ScrollItem t_new_item = new ScrollItem(this.deleter);
			t_new_item.SetClipRect(ref this.rect);

			int t_new_index = this.list.Count;
			int t_y = t_new_index * this.item_height - this.position;
			t_new_item.SetY(t_y);
			t_new_item.SetX(this.rect.x);

			this.list.Add(t_new_item);
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.deleter.DeleteAll();
		}

		/** Main
		*/
		public void Main()
		{
		}
	}
}

