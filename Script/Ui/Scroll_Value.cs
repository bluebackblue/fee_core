

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スクロール。値。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll_Value
	*/
	public struct Scroll_Value
	{
		/** callback
		*/
		private Scroll_Value_CallBack callabck;

		/** 表示位置。
		*/
		private float view_position_float;

		/** アイテム幅。
		*/
		private int item_length;

		/** 表示幅。
		*/
		private int view_length;

		/** 表示インデックス。
		*/
		private int viewindex_st;
		private int viewindex_en;

		/** リスト数。
		*/
		private int listcount;

		/** 初期化。
		*/
		public void Initialize()
		{
			//コールバック。
			this.callabck = null;

			//表示位置。
			this.view_position_float = 0.0f;

			//アイテム幅。
			this.item_length = 1;

			//表示幅。
			this.view_length = 0;

			//表示インデックス。
			this.viewindex_st = -1;
			this.viewindex_en = -1;

			//リスト数。
			this.listcount = 0;
		}

		/** コールバック。設定。
		*/
		public void SetCallBack(Scroll_Value_CallBack a_callback)
		{
			this.callabck = a_callback;
		}

		/** 表示位置。取得。
		*/
		public float GetViewPositionFloat()
		{
			return this.view_position_float;
		}

		/** 表示位置。取得。
		*/
		public int GetViewPositionInt()
		{
			return (int)this.view_position_float;
		}

		/** 表示位置。設定。
		*/
		public void SetViewPosition(float a_view_position)
		{
			float t_view_position;
			if((a_view_position < 0)||(this.item_length * this.listcount < this.view_length)){
				//上に吸着。
				t_view_position = 0;
			}else{
				int t_position_max = this.item_length * this.listcount - this.view_length;
				if(a_view_position > t_position_max){
					//下に吸着。
					t_view_position = t_position_max;
				}else{
					t_view_position = a_view_position;
				}
			}

			if(t_view_position != this.view_position_float){
				this.view_position_float = t_view_position;

				int t_old_viewindex_st = this.viewindex_st;
				int t_old_viewindex_en = this.viewindex_en;
				this.viewindex_st = this.GetViewPositionInt() / this.item_length;
				this.viewindex_en = (this.GetViewPositionInt() + this.view_length) / this.item_length;
				if(this.viewindex_en > this.listcount - 1){
					this.viewindex_en = this.listcount - 1;
				}

				//移動。
				for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
					this.callabck.OnItemPositionChange(ii);
				}

				if((this.viewindex_st != t_old_viewindex_st)||(this.viewindex_en != t_old_viewindex_en)){
					//範囲内から範囲外。
					for(int ii=t_old_viewindex_st;ii<=t_old_viewindex_en;ii++){
						if((ii<this.viewindex_st)||(this.viewindex_en<ii)){
							this.callabck.OnItemVisibleChange(ii,false);
						}
					}

					//範囲外から範囲内。
					for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
						if((ii<t_old_viewindex_st)||(t_old_viewindex_en<ii)){
							this.callabck.OnItemVisibleChange(ii,true);
						}
					}
				}
			}
		}

		/** アイテム幅。取得。
		*/
		public int GetItemLength()
		{
			return this.item_length;
		}

		/** アイテム幅。設定。
		*/
		public void SetItemLength(int a_item_length)
		{
			int t_item_length;
			if(a_item_length < 1){
				t_item_length = 1;
			}else{
				t_item_length = a_item_length;
			}

			if(t_item_length != this.item_length){
				this.item_length = t_item_length;
				if(this.listcount > 0){
					int t_old_viewindex_st = this.viewindex_st;
					int t_old_viewindex_en = this.viewindex_en;

					int t_position_max = this.item_length * this.listcount - this.view_length;
					if(this.view_position_float > t_position_max){
						this.view_position_float = t_position_max;
					}

					this.viewindex_st = this.GetViewPositionInt() / this.item_length;
					this.viewindex_en = (this.GetViewPositionInt() + this.view_length) / this.item_length;
					if(this.viewindex_en > (this.listcount - 1)){
						this.viewindex_en = this.listcount - 1;
					}

					//範囲内から範囲外。
					for(int ii=t_old_viewindex_st;ii<=t_old_viewindex_en;ii++){
						if((ii<this.viewindex_st)||(this.viewindex_en<ii)){
							this.callabck.OnItemVisibleChange(ii,false);
						}
					}

					//範囲外から範囲内。
					for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
						if((ii<t_old_viewindex_st)||(t_old_viewindex_en<ii)){
							this.callabck.OnItemVisibleChange(ii,true);
						}
					}
				}
			}
		}

		/** 表示幅。取得。
		*/
		public int GetViewLength()
		{
			return this.view_length;
		}

		/** 表示幅。設定。
		*/
		public void SetViewLength(int a_view_length)
		{
			int t_view_length;
			if(a_view_length < 0){
				t_view_length = 0;
			}else{
				t_view_length = a_view_length;
			}

			if(t_view_length != this.view_length){
				this.view_length = t_view_length;
				if(this.listcount > 0){
					int t_old_viewindex_st = this.viewindex_st;
					int t_old_viewindex_en = this.viewindex_en;

					if(this.view_length <= 0){
						this.viewindex_st = -1;
						this.viewindex_en = -1;
					}else{
						int t_position_max = this.item_length * this.listcount - this.view_length;
						if(t_position_max < 0){
							t_position_max = 0;
						}
						if(this.view_position_float > t_position_max){
							this.view_position_float = t_position_max;
						}

						this.viewindex_st = this.GetViewPositionInt() / this.item_length;
						this.viewindex_en = (this.GetViewPositionInt() + this.view_length) / this.item_length;
						if(this.viewindex_en > (this.listcount - 1)){
							this.viewindex_en = this.listcount - 1;
						}
					}

					//範囲内から範囲外。
					if(t_old_viewindex_st >= 0){
						for(int ii=t_old_viewindex_st;ii<=t_old_viewindex_en;ii++){
							if((ii<this.viewindex_st)||(this.viewindex_en<ii)){
								this.callabck.OnItemVisibleChange(ii,false);
							}
						}
					}

					//範囲外から範囲内。
					if(this.viewindex_st >= 0){
						for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
							if((ii<t_old_viewindex_st)||(t_old_viewindex_en<ii)){
								this.callabck.OnItemVisibleChange(ii,true);
							}
						}
					}
				}
			}
		}

		/** 表示開始インデックス。取得。
		*/
		public int GetViewStartIndex()
		{
			return this.viewindex_st;
		}

		/** 表示終了インデックス。取得。
		*/
		public int GetViewEndIndex()
		{
			return this.viewindex_en;
		}

		/** リスト数。取得。
		*/
		public int GetListCount()
		{
			return this.listcount;
		}

		/** 全アイテム削除。
		*/
		public void RemoveAllItem()
		{
			this.view_position_float = 0.0f;
			this.listcount = 0;
			this.viewindex_st = -1;
			this.viewindex_en = -1;
		}

		/** アイテム追加。
		*/
		public void InsertItem(int a_insert_index)
		{
			this.listcount++;

			if(this.view_length > 0){
				//表示範囲更新。
				int t_old_viewindex_end = this.viewindex_en;

				if(this.viewindex_st < 0){
					this.viewindex_st = 0;
					this.viewindex_en = 0;
				}

				if((this.listcount - 1) * this.item_length < this.view_length){
					this.viewindex_en = this.listcount - 1;
				}

				if(a_insert_index <= this.viewindex_en){
					//[位置変更]追加項目より下の表示範囲内の項目。
					{
						int t_st = a_insert_index;
						if(t_st < this.viewindex_st){
							t_st = this.viewindex_st;
						}
						for(int ii=t_st;ii<=this.viewindex_en;ii++){
							this.callabck.OnItemPositionChange(ii);
						}
					}

					//[表示変更]範囲内だったものが範囲外に。
					if(t_old_viewindex_end == this.viewindex_en){
						if((this.viewindex_en + 1) < this.listcount){
							this.callabck.OnItemVisibleChange(this.viewindex_en + 1,false);
						}
					}

					if((this.viewindex_st <= a_insert_index)&&(a_insert_index <= this.viewindex_en)){
						//[表示変更]表示範囲内への追加。
						this.callabck.OnItemVisibleChange(a_insert_index,true);
					}else if(this.viewindex_st > a_insert_index){
						//[表示変更]一番上を表示範囲内へ追加。
						this.callabck.OnItemVisibleChange(this.viewindex_st,true);
					}
				}else{
					//表示に変化なし。
				}
			}
		}

		/** アイテム削除。
		*/
		public void RemoveItem(int a_remove_index)
		{
			this.listcount--;

			if(this.viewindex_en < a_remove_index){
				//表示範囲より下の項目を削除。
			}else if(this.listcount == 0){
				//空。

				this.viewindex_st = -1;
				this.viewindex_en = -1;
			}else if(this.item_length * this.listcount < this.view_length){
				//上に吸着。

				if(this.view_position_float != 0){
					this.view_position_float = 0;

					this.viewindex_st = 0;
					this.viewindex_en = this.listcount - 1;

					for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
						this.callabck.OnItemPositionChange(ii);
					}
				}else{
					this.viewindex_en = this.listcount - 1;

					for(int ii=a_remove_index;ii<=this.viewindex_en;ii++){
						this.callabck.OnItemPositionChange(ii);
					}
				}
			}else if(this.listcount == this.viewindex_en){
				//下に吸着。

				int t_viewindex_st_old = this.viewindex_st;

				this.view_position_float = this.item_length * this.listcount - this.view_length;

				this.viewindex_en = this.listcount - 1;
				this.viewindex_st = this.GetViewPositionInt() / this.item_length;

				for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
					this.callabck.OnItemPositionChange(ii);
				}

				if(t_viewindex_st_old != this.viewindex_st){
					this.callabck.OnItemVisibleChange(this.viewindex_st,true);
				}
			}else{

				int t_old_viewindex_st = this.viewindex_st;
				int t_old_viewindex_en = this.viewindex_en;
				
				this.viewindex_st = this.GetViewPositionInt() / this.item_length;
				this.viewindex_en = (this.GetViewPositionInt() + this.view_length) / this.item_length;

				for(int ii=this.viewindex_st;ii<=this.viewindex_en;ii++){
					this.callabck.OnItemPositionChange(ii);
				}

				{
					//「this.viewindex_en」の旧インデックス。
					int t_en_oldindex = this.viewindex_en + 1;
					if(t_en_oldindex > t_old_viewindex_en){
						//範囲外から範囲内に。
						this.callabck.OnItemVisibleChange(this.viewindex_en,true);
					}
				}

				{
					//「this.viewindex_st - 1」の旧インデックス。
					int t_stout_oldindex = this.viewindex_st - 1;
					if(t_stout_oldindex >= a_remove_index){
						t_stout_oldindex++;
					}
					if(t_stout_oldindex >= t_old_viewindex_st){
						//範囲内から範囲外に。
						this.callabck.OnItemVisibleChange(this.viewindex_st - 1,false);
					}
				}
			}
		}
	}
}

