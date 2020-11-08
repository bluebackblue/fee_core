

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スクロール。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll
	*/
	public class Scroll<ITEM> : Scroll_Base<ITEM> , Fee.Deleter.OnDelete_CallBackInterface
		where ITEM : ScrollItem_Base
	{
		/** 背景。
		*/
		private Fee.Render2D.Sprite2D bg;
		private bool bg_enable;

		/** バー。
		*/
		private Fee.Render2D.Sprite2D bar;
		private bool bar_enable;

		/** バー。
		*/
		private int bar_drawpriority_offset;
		private int bar_size;
		private int bar_offset;

		/** constructor

			プール用に作成。

		*/
		public Scroll()
			:
			base()
		{
		}

		/** constructor
		*/
		public static Scroll<ITEM> Create(Fee.Deleter.Deleter a_deleter,long a_drawpriority,Scroll_Type a_scroll_type,int a_item_length)
		{
			//Scroll t_this = Fee.Ui.Ui.GetInstance().GetPoolList_Scroll().PoolNew();
			Scroll<ITEM> t_this = new Scroll<ITEM>();
			{
				//プールから作成。
				t_this.InitializeFromPool(a_drawpriority,a_scroll_type,a_item_length);
				
				//背景。
				t_this.bg = Fee.Render2D.Sprite2D.Create(null,a_drawpriority);
				t_this.bg.SetTexture(UnityEngine.Texture2D.whiteTexture);
				t_this.bg.SetRect(0,0,0,0);
				t_this.bg.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_this.bg.SetColor(0.0f,0.0f,0.0f,1.0f);
				t_this.bg.SetMaterialType(Fee.Render2D.MaterialType.Alpha);
			
				//背景。
				t_this.bg_enable = true;

				//バー。
				t_this.bar_drawpriority_offset = 1;
				t_this.bar_size = 5;
				t_this.bar_offset = 1;

				//バー。
				t_this.bar = Fee.Render2D.Sprite2D.Create(null,a_drawpriority + t_this.bar_drawpriority_offset);
				t_this.bar.SetTexture(UnityEngine.Texture2D.whiteTexture);
				t_this.bar.SetRect(0,0,0,0);
				t_this.bar.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_this.bar.SetColor(1.0f,1.0f,1.0f,1.0f);
				t_this.bar.SetMaterialType(Fee.Render2D.MaterialType.Alpha);
				t_this.bar.SetVisible(false);

				//バー。
				t_this.bar_enable = true;

				if(a_deleter != null){
					a_deleter.Regist(t_this);
				}
			}
			return t_this;
		}

		/** [Fee.Deleter.OnDelete_CallBackInterface]削除。
		*/
		public void OnDelete()
		{
			//OnDelete
			this.bg.OnDelete();
			this.bar.OnDelete();

			//プールへ削除。
			this.DeleteToPool();

			//プールへ変換。
			//Fee.Ui.Ui.GetInstance().GetPoolList_Scroll().PoolDelete(this);
		}

		/** [Scroll_Base]コールバック。矩形。設定。
		*/
		protected override void OnChangeRect()
		{
			this.bg.SetRect(in this.param.rect);
			this.UpdateView();
		}

		/** [Scroll_Base]コールバック。表示位置変更。
		*/
		protected override void OnChangeViewPosition()
		{
			this.UpdateView();
		}

		/** [Scroll_Base]コールバック。リスト数変更。
		*/
		protected override void OnChangeListCount()
		{
			this.UpdateView();
		}

		/** [Scroll_Base]コールバック。
		*/
		protected override void OnChangeDrawPriority()
		{
			this.bg.SetDrawPriority(this.param.drawpriority);
			this.bar.SetDrawPriority(this.param.drawpriority + this.bar_drawpriority_offset);
		}
		
		/** [Scroll_Base]コールバック。表示フラグ変更。
		*/
		protected override void OnChangeVisibleFlag()
		{
			this.UpdateView();
		}

		/** バー描画プライオリティオフセット。設定。
		*/
		public void SetBarDrawPriorityOffset(int a_offset)
		{
			this.bar_drawpriority_offset = a_offset;
			this.bar.SetDrawPriority(this.param.drawpriority + this.bar_drawpriority_offset);
		}

		/** バーサイズ。設定。
		*/
		public void SetBarSize(int a_size)
		{
			if(this.bar_size != a_size){
				this.bar_size = a_size;
				this.UpdateView();
			}
		}

		/** バーサイズ。取得。
		*/
		public int GetBarSize()
		{
			return this.bar_size;
		}

		/** SetBarOffset
		*/
		public void SetBarOffset(int a_offset)
		{
			this.bar_offset = a_offset;
		}

		/** GetBarOffset
		*/
		public int GetBarOffset()
		{
			return this.bar_offset;
		}

		/** 背景。有効。設定。
		*/
		public void SetBgEnable(bool a_flag)
		{
			if(this.bg_enable != a_flag){
				this.bg_enable = a_flag;
				if(this.bg_enable == true){
					this.UpdateView();
				}else{
					this.bg.SetVisible(false);
				}
			}
		}

		/** 背景。色。設定。
		*/
		public void SetBgColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.bg.SetColor(a_r,a_g,a_b,a_a);
		}

		/** 背景。色。設定。
		*/
		public void SetBgColor(in UnityEngine.Color a_color)
		{
			this.bg.SetColor(in a_color);
		}

		/** バー。有効。設定。
		*/
		public void SetBarEnable(bool a_flag)
		{
			if(this.bar_enable != a_flag){
				this.bar_enable = a_flag;
				if(this.bar_enable == true){
					this.UpdateView();
				}else{
					this.bar.SetVisible(false);
				}
			}
		}

		/** バー。色。設定。
		*/
		public void SetBarColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.bar.SetColor(a_r,a_g,a_b,a_a);
		}

		/** バー。色。設定。
		*/
		public void SetBarColor(in UnityEngine.Color a_color)
		{
			this.bar.SetColor(in a_color);
		}

		/** 更新。表示。
		*/
		private void UpdateView()
		{
			//bg
			if(this.bg_enable == true){
				this.bg.SetVisible(this.param.visible_flag);
			}

			//bar
			if(this.bar_enable == true){
				//position_max
				int t_position_max = this.param.scroll_value.GetItemLength() * this.param.scroll_value.GetListCount() - this.param.scroll_value.GetViewLength();

				if(t_position_max <= 0){
					this.bar.SetVisible(false);
				}else{
					this.bar.SetVisible(this.param.visible_flag);

					float t_offset_per = (float)this.param.scroll_value.GetViewPositionInt() / t_position_max;
					float t_length_per = (float)this.param.scroll_value.GetViewLength() / (this.param.scroll_value.GetItemLength() * this.param.scroll_value.GetListCount());

					if(this.GetScrollType() == Scroll_Type.Vertical){
						//縦。
						int t_bar_length = (int)(this.param.rect.h * t_length_per);
						int t_bar_offset = (int)(t_offset_per * (this.param.scroll_value.GetViewLength() - t_bar_length));
						this.bar.SetY(this.param.rect.y + t_bar_offset);
						this.bar.SetX(this.param.rect.x + this.param.rect.w - this.bar_size - this.bar_offset);
						this.bar.SetH(t_bar_length);
						this.bar.SetW(this.bar_size);
					}else{
						//横。
						int t_bar_length = (int)(this.param.rect.w * t_length_per);
						int t_bar_offset = (int)(t_offset_per * (this.param.scroll_value.GetViewLength() - t_bar_length));
						this.bar.SetX(this.param.rect.x + t_bar_offset);
						this.bar.SetY(this.param.rect.y + this.param.rect.h - this.bar_size - this.bar_offset);
						this.bar.SetW(t_bar_length);
						this.bar.SetH(this.bar_size);
					}
				}
			}
		}
	}
}

