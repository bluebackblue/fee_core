

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スクロール。ベース。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll_Base
	*/
	public abstract class Scroll_Base : Fee.Deleter.DeleteItem_Base , Fee.EventPlate.OnOverCallBack_Base
	{
		/** deleter
		*/
		protected Fee.Deleter.Deleter deleter;

		/** 表示位置。
		*/
		protected int view_position;

		/** アイテム幅。
		*/
		protected int item_length;

		/** 表示幅。
		*/
		protected int view_length;

		/** 表示インデックス。
		*/
		protected int viewindex_st;
		protected int viewindex_en;

		/** 矩形。
		*/
		protected Fee.Render2D.Rect2D_R<int> rect;

		/** eventplate
		*/
		protected Fee.EventPlate.Item eventplate;

		/** is_onover
		*/
		protected bool is_onover;

		/** ドラッグスクロール。
		*/
		protected bool dragscroll_flag;
		protected int dragscroll_start_viewposition;
		protected int dragscroll_start_pos;
		protected int dragscroll_old_pos;
		protected float dragscroll_speed;

		/** constructor
		*/
		public Scroll_Base(Fee.Deleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
		{
			//deleter
			this.deleter = new Fee.Deleter.Deleter();

			//表示位置。
			this.view_position = 0;

			//アイテム幅。
			this.item_length = a_item_length;

			//表示幅。
			this.view_length = 0;

			//表示インデックス。
			this.viewindex_st = -1;
			this.viewindex_en = -1;

			//矩形。
			this.rect.Set(0,0,0,0);

			//eventplate
			this.eventplate = new Fee.EventPlate.Item(this.deleter,Fee.EventPlate.EventType.View,a_drawpriority);
			this.eventplate.SetRect(0,0,0,0);
			this.eventplate.SetOnOverCallBack(this);

			//is_onover
			this.is_onover = false;

			//dragscroll
			this.dragscroll_flag = false;
			this.dragscroll_start_viewposition = 0;
			this.dragscroll_start_pos = 0;
			this.dragscroll_old_pos = 0;
			this.dragscroll_speed = 0.0f;

			//削除管理。
			if(a_deleter != null){
				a_deleter.Register(this);
			}
		}

		/** [Scroll_Base]コールバック。リスト数。取得。
		*/
		public abstract int GetListCount();

		/** [Scroll_Base]コールバック。スクロール方向の値。取得。
		*/
		protected abstract int GetDirectionValue(int a_x,int a_y);

		/** [Scroll_Base]コールバック。リスト数変更。
		*/
		protected abstract void OnChangeListCount();

		/** [Scroll_Base]コールバック。矩形変更。
		*/
		protected abstract void OnChangeRect();

		/** [Scroll_Base]コールバック。表示位置変更。
		*/
		protected abstract void OnChangeViewPosition();

		/** [Scroll_Base]コールバック。アイテム移動。
		*/
		protected abstract void OnMoveItem_FromBase(int a_index);

		/** [Scroll_Base]コールバック。表示開始。
		*/
		protected abstract void OnViewInItem_FromBase(int a_index);

		/** [Scroll_Base]コールバック。表示終了。
		*/
		protected abstract void OnViewOutItem_FromBase(int a_index);

		/** [Scroll_Base]コールバック。矩形変更。
		*/
		protected abstract void OnChangeRect_FromBase();

		/** 削除。
		*/
		public void Delete()
		{
			this.deleter.DeleteAll();
		}

		/** 全チェック。表示範囲更新。
		*/
		protected void UpdateView_AllCheck()
		{
			int t_list_count = this.GetListCount();

			//表示中のアイテムの位置を設定。
			for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
				this.OnMoveItem_FromBase(ii);
			}

			for(int ii=0;ii<this.viewindex_st;ii++){
				//表示終了。
				this.OnViewOutItem_FromBase(ii);
			}

			for(int ii=viewindex_en+1;ii<t_list_count;ii++){
				//表示終了。
				this.OnViewOutItem_FromBase(ii);
			}

			for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
				//表示開始。
				this.OnViewInItem_FromBase(ii);
			}
		}

		/** 表示位置変更後。表示範囲更新。
		*/
		protected void UpdateView_PositionChange()
		{
			int t_list_count = this.GetListCount();

			int t_st_old = this.viewindex_st;
			int t_en_old = this.viewindex_en;

			//表示範囲チェック。
			{
				if(t_list_count <= 0){
					this.viewindex_st = -1;
					this.viewindex_en = -1;
				}else{
					int t_index_st = this.view_position / this.item_length;
					int t_index_en = (this.view_position + this.view_length) / this.item_length;

					if(t_index_en > (t_list_count - 1)){
						t_index_en = t_list_count - 1;
					}

					this.viewindex_st = t_index_st;
					this.viewindex_en = t_index_en;
				}
			}

			//表示中のアイテムの位置を設定。
			for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
				this.OnMoveItem_FromBase(ii);
			}

			if((t_st_old == this.viewindex_st)&&(t_en_old == this.viewindex_en)){
				//変化なし。
			}else{
				//旧表示空間。
				for(int ii=t_st_old;ii<=t_en_old;ii++){
					if((ii<this.viewindex_st)||(this.viewindex_en<ii)){
						//表示終了。
						this.OnViewOutItem_FromBase(ii);
					}
				}
				//新表示空間。
				for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
					if((ii<t_st_old)||(t_en_old<ii)){
						//表示開始。
						this.OnViewInItem_FromBase(ii);
					}
				}
			}
		}

		/** アイテム追加後。表示範囲更新。
		*/
		protected void UpdateView_Insert(int a_insert_index)
		{
			int t_list_count = this.GetListCount();

			//表示範囲チェック。
			{
				if(t_list_count <= 0){
					this.viewindex_st = -1;
					this.viewindex_en = -1;
				}else{
					int t_index_st = this.view_position / this.item_length;
					int t_index_en = (this.view_position + this.view_length) / this.item_length;

					if(t_index_en > (t_list_count - 1)){
						t_index_en = t_list_count - 1;
					}

					this.viewindex_st = t_index_st;
					this.viewindex_en = t_index_en;
				}
			}

			//表示中のアイテムの位置を設定。
			for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
				this.OnMoveItem_FromBase(ii);
			}

			//表示範囲内チェック。
			for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
				this.OnViewInItem_FromBase(ii);
			}

			//表示範囲外チェック。
			{
				this.OnViewOutItem_FromBase(this.viewindex_st - 1);
				this.OnViewOutItem_FromBase(this.viewindex_en + 1);
			}
		}

		/** アイテム削除後。表示範囲更新。
		*/
		protected void UpdateView_Remove()
		{
			int t_list_count = this.GetListCount();

			//表示範囲チェック。
			{
				if(t_list_count <= 0){
					this.viewindex_st = -1;
					this.viewindex_en = -1;
				}else{
					if(this.item_length * t_list_count < this.view_length){
						if(this.view_position != 0){
							this.view_position = 0;
							this.OnChangeViewPosition();
						}
					}else{
						int t_position_max = this.item_length * t_list_count - this.view_length;
						if(this.view_position > t_position_max){
							if(this.view_position != t_position_max){
								this.view_position = t_position_max;
								this.OnChangeViewPosition();
							}
						}
					}

					int t_index_st = this.view_position / this.item_length;
					int t_index_en = (this.view_position + this.view_length) / this.item_length;

					if(t_index_en > (t_list_count - 1)){
						t_index_en = t_list_count - 1;
					}

					this.viewindex_st = t_index_st;
					this.viewindex_en = t_index_en;
				}
			}

			//表示中のアイテムの位置を設定。
			for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
				this.OnMoveItem_FromBase(ii);
			}

			//表示範囲内チェック。
			for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
				this.OnViewInItem_FromBase(ii);
			}

			//表示範囲外チェック。
			{
				this.OnViewOutItem_FromBase(this.viewindex_st - 1);
				this.OnViewOutItem_FromBase(this.viewindex_en + 1);
			}
		}

		/** 表示位置。設定。

		return : 変更あり。

		*/
		public void SetViewPosition(int a_view_position)
		{
			int t_view_position = a_view_position;
			int t_list_count = this.GetListCount();

			if(t_view_position < 0){
				t_view_position = 0;
			}else if(this.item_length * t_list_count < this.view_length){
				t_view_position = 0;
			}else{
				int t_position_max = this.item_length * t_list_count - this.view_length;
				if(t_view_position > t_position_max){
					t_view_position = t_position_max;
				}
			}

			if(this.view_position != t_view_position){
				this.view_position = t_view_position;
				this.OnChangeViewPosition();

				//表示範囲更新。
				this.UpdateView_PositionChange();
			}
		}

		/** 表示位置。取得。
		*/
		public int GetViewPosition()
		{
			return this.view_position;
		}

 		/** 矩形。設定。
		*/
		public void SetRect(int a_x,int a_y,int a_w,int a_h)
		{
			//rect
			this.rect.Set(a_x,a_y,a_w,a_h);

			//eventplate
			this.eventplate.SetRect(a_x,a_y,a_w,a_h);

			//コールバック。矩形変更。
			this.OnChangeRect_FromBase();
		}

 		/** 矩形。設定。
		*/
		public void SetX(int a_x)
		{
			//rect
			this.rect.x = a_x;

			//eventplate
			this.eventplate.SetX(a_x);

			//コールバック。矩形変更。
			this.OnChangeRect_FromBase();
		}

 		/** 矩形。設定。
		*/
		public void SetY(int a_y)
		{
			//rect
			this.rect.y = a_y;

			//eventplate
			this.eventplate.SetY(a_y);

			//コールバック。矩形変更。
			this.OnChangeRect_FromBase();
		}

		/** 矩形。取得。
		*/
		public int GetX()
		{
			return this.rect.x;
		}

		/** 矩形。取得。
		*/
		public int GetY()
		{
			return this.rect.y;
		}

		/** 矩形。取得。
		*/
		public int GetW()
		{
			return this.rect.w;
		}

		/** 矩形。取得。
		*/
		public int GetH()
		{
			return this.rect.h;
		}

		/** [Fee.EventPlateOnOverCallBack_Base]イベントプレートに入場。
		*/
		public void OnOverEnter(int a_value)
		{
			this.is_onover = true;	
		}

		/** [Fee.EventPlateOnOverCallBack_Base]イベントプレートから退場。
		*/
		public void OnOverLeave(int a_value)
		{
			this.is_onover = false;
		}

		/** オンオーバー。取得。
		*/
		public bool IsOnOver()
		{
			return this.is_onover;
		}

		/** ドラッグスクロール速度。設定。
		*/
		public void SetDragScrollSpeed(float a_speed)
		{
			this.dragscroll_speed = a_speed;
		}

		/** ドラッグスクロール速度。取得。
		*/
		public float GetDragScrollSpeed()
		{
			return this.dragscroll_speed;
		}

		/** ドラッグスクロール。更新。
		*/
		public void DragScrollUpdate(int a_x,int a_y,bool a_button)
		{
			if((this.dragscroll_flag == false)&&(a_button == true)){
				if((this.rect.x <= a_x)&&(a_x <= (this.rect.x + this.rect.w))&&(this.rect.y <= a_y)&&(a_y <= (this.rect.y + this.rect.h))){
					//ドラッグ開始。
					this.dragscroll_flag = true;
					this.dragscroll_start_viewposition = this.GetViewPosition();
					this.dragscroll_start_pos = this.GetDirectionValue(a_x,a_y);
					this.dragscroll_old_pos = this.dragscroll_start_pos;

					this.dragscroll_speed = 0.0f;

					//GetDirectionValue
				}
			}else if((this.dragscroll_flag == true)&&(a_button == true)){
				//ドラッグ中。
				this.SetViewPosition(this.dragscroll_start_viewposition + this.dragscroll_start_pos - this.GetDirectionValue(a_x,a_y));

				//慣性。
				int t_drag_new_pos = this.GetDirectionValue(a_x,a_y);
				this.dragscroll_speed = this.dragscroll_speed * 0.3f + (this.dragscroll_old_pos - t_drag_new_pos) * 0.7f;
				this.dragscroll_old_pos = t_drag_new_pos;
			}else if((this.dragscroll_flag == true)&&(a_button == false)){
				//ドラッグ終了。
				this.dragscroll_flag = false;
			}

			if((this.dragscroll_flag == false)&&(this.dragscroll_speed != 0.0f)){
				int t_move = (int)this.dragscroll_speed;
				this.dragscroll_speed /= 1.08f;
				if(t_move != 0){
					this.SetViewPosition(this.GetViewPosition() + t_move);
				}else{
					this.dragscroll_speed = 0.0f;
				}
			}
		}
	}
}

