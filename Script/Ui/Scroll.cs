

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
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

		//TODO:IsOnOverは必要。 EventType.View

		/** constructor
		*/
		public Scroll(Fee.Deleter.Deleter a_deleter,long a_drawpriority,ScrollType a_scroll_type,int a_item_length)
			:
			base(a_scroll_type,a_item_length)
		{
			//背景。
			this.bg = new Fee.Render2D.Sprite2D(a_deleter,a_drawpriority);
			this.bg.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bg.SetRect(0,0,0,0);
			this.bg.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetColor(0.0f,0.0f,0.0f,0.1f);
			this.bg.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);

			//バー。
			this.bar = new Fee.Render2D.Sprite2D(a_deleter,a_drawpriority + 1);
			this.bar.SetTexture(UnityEngine.Texture2D.whiteTexture);
			this.bar.SetRect(0,0,5,5);
			this.bar.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.bar.SetColor(1.0f,1.0f,1.0f,0.3f);
			this.bar.SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);
			this.bar.SetVisible(false);
		}

		/** 描画プライオリティー。設定。
		*/
		public void SetDrawPriority(long a_drawpriority)
		{
			this.bg.SetDrawPriority(a_drawpriority);
			this.bar.SetDrawPriority(a_drawpriority);
		}

		/** [Scroll_Base]コールバック。矩形。設定。
		*/
		protected override void OnChangeRect()
		{
			this.bg.SetRect(ref this.rect);
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

				if(this.GetScrollType() == ScrollType.Vertical){
					int t_bar_length = (int)(this.rect.h * t_length_per);
					int t_bar_offset = (int)(t_offset_per * (this.scroll_value.GetViewLength() - t_bar_length));
					this.bar.SetY(this.rect.y + t_bar_offset);
					this.bar.SetX(this.rect.x - 10);
					this.bar.SetH(t_bar_length);
				}else{
					int t_bar_length = (int)(this.rect.w * t_length_per);
					int t_bar_offset = (int)(t_offset_per * (this.scroll_value.GetViewLength() - t_bar_length));
					this.bar.SetX(this.rect.x + t_bar_offset);
					this.bar.SetY(this.rect.y - 10);
					this.bar.SetW(t_bar_length);
				}
			}
		}
	}
}

