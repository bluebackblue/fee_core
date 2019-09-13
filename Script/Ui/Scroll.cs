

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
	public class Scroll<ITEM> : Scroll_Base<ITEM>
		where ITEM : ScrollItem_Base
	{
		/** 背景。
		*/
		private Fee.Render2D.Sprite2D bg;

		/** バー。
		*/
		private Fee.Render2D.Sprite2D bar;

		/** バー。
		*/
		private int bar_drawpriority_offset;

		/** constructor
		*/
		public Scroll(Fee.Deleter.Deleter a_deleter,long a_drawpriority,Scroll_Type a_scroll_type,int a_item_length)
			:
			base(a_deleter,a_drawpriority,a_scroll_type,a_item_length)
		{
			//背景。
			this.bg = Fee.Render2D.Render2D.GetInstance().Sprite2D_PoolNew(this.deleter,a_drawpriority);
			this.bg.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg.SetRect(0,0,0,0);
			this.bg.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetColor(0.0f,0.0f,0.0f,0.1f);
			this.bg.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);

			//バー。
			this.bar_drawpriority_offset = 1;

			//バー。
			this.bar = Fee.Render2D.Render2D.GetInstance().Sprite2D_PoolNew(this.deleter,a_drawpriority + this.bar_drawpriority_offset);
			this.bar.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bar.SetRect(0,0,5,5);
			this.bar.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.bar.SetColor(1.0f,1.0f,1.0f,0.3f);
			this.bar.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);
			this.bar.SetVisible(false);
		}

		/** [Scroll_Base]コールバック。矩形。設定。
		*/
		protected override void OnChangeRect()
		{
			this.bg.SetRect(in this.rect);
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
			this.bg.SetDrawPriority(this.drawpriority);
			this.bar.SetDrawPriority(this.drawpriority + this.bar_drawpriority_offset);
		}
		
		/** バー描画プライオリティオフセット。設定。
		*/
		public void SetBarDrawPriorityOffset(int a_offset)
		{
			this.bar_drawpriority_offset = a_offset;
			this.bar.SetDrawPriority(this.drawpriority + this.bar_drawpriority_offset);
		}

		/** 背景色。設定。
		*/
		public void SetBgColor(float a_r,float a_g,float a_b,float a_a)
		{
			this.bg.SetColor(a_r,a_g,a_b,a_a);
		}

		/** 背景色。設定。
		*/
		public void SetBgColor(in UnityEngine.Color a_color)
		{
			this.bg.SetColor(in a_color);
		}

		/** 更新。表示。
		*/
		private void UpdateView()
		{
			int t_position_max = this.scroll_value.GetItemLength() * this.scroll_value.GetListCount() - this.scroll_value.GetViewLength();

			if(t_position_max <= 0){
				this.bar.SetVisible(false);
			}else{
				this.bar.SetVisible(true);

				float t_offset_per = (float)this.scroll_value.GetViewPosition() / t_position_max;
				float t_length_per = (float)this.scroll_value.GetViewLength() / (this.scroll_value.GetItemLength() * this.scroll_value.GetListCount());

				if(this.GetScrollType() == Scroll_Type.Vertical){
					//縦。
					int t_bar_length = (int)(this.rect.h * t_length_per);
					int t_bar_offset = (int)(t_offset_per * (this.scroll_value.GetViewLength() - t_bar_length));
					this.bar.SetY(this.rect.y + t_bar_offset);
					this.bar.SetX(this.rect.x + this.rect.w - 5);
					this.bar.SetH(t_bar_length);
				}else{
					//横。
					int t_bar_length = (int)(this.rect.w * t_length_per);
					int t_bar_offset = (int)(t_offset_per * (this.scroll_value.GetViewLength() - t_bar_length));
					this.bar.SetX(this.rect.x + t_bar_offset);
					this.bar.SetY(this.rect.y + this.rect.h - 5);
					this.bar.SetW(t_bar_length);
				}
			}
		}
	}
}

