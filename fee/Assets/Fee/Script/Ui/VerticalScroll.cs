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

		/** text
		*/
		private NRender2D.Text2D text;

		/** call_count
		*/
		private int call_count;

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
			this.sprite.SetRect(0,0,100,30);
			this.sprite.SetTexture(Texture2D.whiteTexture);
			this.sprite.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.sprite.SetClip(true);
			this.sprite.SetClipRect(0,0,0,0);
			this.sprite.SetColor(Random.value,Random.value,Random.value,1.0f);

			//text
			this.text = new NRender2D.Text2D(this.deleter,null,t_drawpriority);
			this.text.SetRect(0,0,0,0);
			this.text.SetClip(false);
			this.text.SetClipRect(0,0,0,0);
			this.text.SetText("out");

			//
			this.call_count = 0;

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
			this.text.SetY(a_y);
		}

		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			this.sprite.SetX(a_x);
			this.text.SetX(a_x);
		}

		/** クリック。矩形。
		*/
		public void SetClipRect(ref NRender2D.Rect2D_R<int> a_rect)
		{
			this.sprite.SetClipRect(ref a_rect);
			this.text.SetClipRect(ref a_rect);
		}

		/** 表示内。
		*/
		public void OnViewIn()
		{
			this.call_count++;
			this.text.SetText("in" + this.call_count.ToString());
		}

		/** 表示外。
		*/
		public void OnViewOut()
		{
			this.call_count--;
			this.text.SetText("out" + this.call_count.ToString());
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

		/** 表示インデックス。
		*/
		private float viewindex_start;
		private float viewindex_end;

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

			//表示インデックス。
			this.viewindex_start = -1.0f;
			this.viewindex_end = -1.0f;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** Ｙ座標計算。
		*/
		private int CalcY(int a_index)
		{
			return this.rect.y - this.position + a_index * this.item_height;
		}

		/** 表示範囲更新。
		*/
		private void UpdateViewIndex(float a_viewindex_start,float a_viewindex_end)
		{
			float t_viewindex_start_old = this.viewindex_start;
			float t_viewindex_end_old = this.viewindex_end;
			this.viewindex_start = a_viewindex_start;
			this.viewindex_end = a_viewindex_end;

			int t_oldview_start = (int)(t_viewindex_start_old);
			int t_oldview_end = (int)(t_viewindex_end_old);
			int t_view_start = (int)(this.viewindex_start);
			int t_view_end = (int)(this.viewindex_end);

			if((t_oldview_start == t_view_start)&&(t_oldview_end == t_view_end)){
				//変化なし。
				if(t_view_start >= 0){
					for(int ii=t_view_start;ii<=t_view_end;ii++){
						this.list[ii].SetY(this.CalcY(ii));
					}
				}
			}else{
				//旧表示空間。
				if(t_oldview_start >= 0){
					for(int ii=t_oldview_start;ii<=t_oldview_end;ii++){
						if((ii<t_view_start)||(t_view_end<ii)){
							//表示終了。
							this.list[ii].OnViewOut();
						}
					}
				}

				//新表示空間。
				if(t_view_start >= 0){
					for(int ii=t_view_start;ii<=t_view_end;ii++){
						if((ii<t_oldview_start)||(t_oldview_end<ii)){
							//表示開始。
							this.list[ii].OnViewIn();
						}
					}
				}
			}
		}

		/** 位置。設定。

		return : 変更あり。

		*/
		public bool SetPosition(int a_position)
		{
			int t_position = a_position;
			if(t_position < 0){
				t_position = 0;
			}else if(this.item_height * this.list.Count < this.rect.h){
				t_position = 0;
			}else{
				int t_position_max = this.item_height * this.list.Count - this.rect.h;
				if(t_position > t_position_max){
					t_position = t_position_max;
				}
			}

			if(this.position != t_position){
				this.position = t_position;

				//表示範囲変更。
				{
					float t_index_start = this.position / this.item_height;
					float t_index_end = (this.position + this.rect.h) / this.item_height;
					if(t_index_end >= (this.list.Count - 1)){
						t_index_end = (this.list.Count - 1);
					}
					if(this.list.Count == 0){
						t_index_start = -1.0f;
					}
					this.UpdateViewIndex(t_index_start,t_index_end);
				}

				//TODO:全位置再計算。
				for(int ii=0;ii<this.list.Count;ii++){
					this.list[ii].SetY(this.CalcY(ii));
				}

				return true;
			}
			return false;
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
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetY(this.CalcY(ii));
			}

			//bg
			this.bg.SetRect(ref this.rect);
		}

		/** リスト追加。
		*/
		public void AddList()
		{
			ScrollItem t_new_item = new ScrollItem(this.deleter);

			//クリップ設定。
			t_new_item.SetClipRect(ref this.rect);

			//リスト追加。
			int t_new_index = this.list.Count;
			this.list.Add(t_new_item);
			t_new_item.SetX(this.rect.x);

			//表示範囲変更。
			{
				float t_index_start = this.position / this.item_height;
				float t_index_end = (this.position + this.rect.h) / this.item_height;
				if(t_index_end >= (this.list.Count - 1)){
					t_index_end = (this.list.Count - 1);
				}
				if(this.list.Count == 0){
					t_index_start = -1.0f;
				}
				this.UpdateViewIndex(t_index_start,t_index_end);
			}

			//TODO:全位置再計算。
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetY(this.CalcY(ii));
			}
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

