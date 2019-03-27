

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。スクロール。横スクロール。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll_Horizontal
	*/
	public class Scroll_Horizontal<ITEM> : Scroll_Horizontal_Base<ITEM>
		where ITEM : ScrollItem_Base
	{
		/** 背景。
		*/
		private Fee.Render2D.Sprite2D bg;

		/** バー。
		*/
		private Fee.Render2D.Sprite2D bar;

		/** constructor
		*/
		public Scroll_Horizontal(Fee.Deleter.Deleter a_deleter,long a_drawpriority,int a_item_length)
			:
			base(a_deleter,a_drawpriority,a_item_length)
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
			int t_position_max = this.item_length * this.list.Count - this.view_length;

			if(t_position_max <= 0){
				this.bar.SetVisible(false);
			}else{
				this.bar.SetVisible(true);

				float t_offset_per = (float)this.view_position / t_position_max;
				float t_length_per = (float)this.view_length / (this.item_length * this.list.Count);

				int t_offset = (int)(t_offset_per * (this.view_length - this.bar.GetW()));

				this.bar.SetX(this.rect.x + t_offset);
				this.bar.SetY(this.rect.y - 10);
				this.bar.SetW((int)(this.rect.w * t_length_per));
			}
		}
	}
}

