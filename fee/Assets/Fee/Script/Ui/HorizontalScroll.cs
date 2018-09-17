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
	public class HorizontalScroll<ITEM> : NDeleter.DeleteItem_Base
		where ITEM : class , ScrollItem_Base
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
		private List<ITEM> list;

		/** item_length
		*/
		private int item_length;

		/** 表示インデックス。
		*/
		private float viewindex_start;
		private float viewindex_end;

		/** constructor
		*/
		public HorizontalScroll(NDeleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
		{
			//deleter
			this.deleter = new NDeleter.Deleter();

			//rect
			this.rect.Set(0,0,0,0);

			//bg
			this.bg = new ClipSprite(this.deleter,null,a_drawpriority);
			this.bg.SetTexture(Texture2D.whiteTexture);
			this.bg.SetRect(ref this.rect);
			this.bg.SetTextureRect(ref NRender2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetColor(0.0f,0.0f,0.0f,0.1f);

			//位置。
			this.position = 0;

			//リスト。
			this.list = new List<ITEM>();

			//item_length
			this.item_length = a_item_length;

			//表示インデックス。
			this.viewindex_start = -1.0f;
			this.viewindex_end = -1.0f;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** Ｘ座標計算。
		*/
		private int CalcX(int a_index)
		{
			return this.rect.x - this.position + a_index * this.item_length;
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

			//矩形。設定。
			if(t_view_start >= 0){
				for(int ii=t_view_start;ii<=t_view_end;ii++){
					this.list[ii].SetX(this.CalcX(ii));
				}
			}

			if((t_oldview_start == t_view_start)&&(t_oldview_end == t_view_end)){
				//変化なし。
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

		/** リスト。取得。
		*/
		public List<ITEM> GetList()
		{
			return this.list;
		}

		/** 位置。設定。

		return : 変更あり。

		*/
		public bool SetPosition(int a_position)
		{
			int t_position = a_position;
			if(t_position < 0){
				t_position = 0;
			}else if(this.item_length * this.list.Count < this.rect.w){
				t_position = 0;
			}else{
				int t_position_max = this.item_length * this.list.Count - this.rect.w;
				if(t_position > t_position_max){
					t_position = t_position_max;
				}
			}

			if(this.position != t_position){
				this.position = t_position;

				//表示範囲変更。
				{
					float t_index_start = this.position / this.item_length;
					float t_index_end = (this.position + this.rect.w) / this.item_length;
					if(t_index_end >= (this.list.Count - 1)){
						t_index_end = (this.list.Count - 1);
					}
					if(this.list.Count == 0){
						t_index_start = -1.0f;
					}
					this.UpdateViewIndex(t_index_start,t_index_end);
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

		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			this.rect.Set(a_x,a_y,a_w,a_h);

			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].SetClipRect(ref this.rect);
			}

			this.bg.SetRect(ref this.rect);

			//表示範囲変更。
			{
				float t_index_start = this.position / this.item_length;
				float t_index_end = (this.position + this.rect.h) / this.item_length;
				if(t_index_end >= (this.list.Count - 1)){
					t_index_end = (this.list.Count - 1);
				}
				if(this.list.Count == 0){
					t_index_start = -1.0f;
				}
				this.UpdateViewIndex(t_index_start,t_index_end);
			}
		}

		/** リスト追加。
		*/
		public void AddList(ITEM a_new_item)
		{
			//クリップ設定。
			a_new_item.SetClipRect(ref this.rect);

			//リスト追加。
			int t_new_index = this.list.Count;
			this.list.Add(a_new_item);
			a_new_item.SetY(this.rect.y);

			//表示範囲変更。
			{
				float t_index_start = this.position / this.item_length;
				float t_index_end = (this.position + this.rect.w) / this.item_length;
				if(t_index_end >= (this.list.Count - 1)){
					t_index_end = (this.list.Count - 1);
				}
				if(this.list.Count == 0){
					t_index_start = -1.0f;
				}
				this.UpdateViewIndex(t_index_start,t_index_end);
			}
		}

		/** 削除。
		*/
		public void Delete()
		{
			this.deleter.DeleteAll();
		}
	}
}

