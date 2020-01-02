

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スクロールアイテム。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** ScrollItem_Base
	*/
	public abstract class ScrollItem_Base
	{
		/** is_viewin
		*/
		private bool is_viewin;

		/** capture_viewout
		*/
		private bool capture_viewout;

		/** constructor
		*/
		public ScrollItem_Base()
		{
			this.is_viewin = false;
		}

		/** 表示。チェック。
		*/
		public bool IsViewIn()
		{
			return this.is_viewin;
		}

		/** 表示。設定。
		*/
		public void SetViewIn(bool a_flag)
		{
			this.is_viewin = a_flag;
		}

		/** キャプチャアイテム表示範囲外移動フラグ。設定。
		*/
		public void SetCaptureViewOutFlag(bool a_flag)
		{
			this.capture_viewout = a_flag;
		}

		/** キャプチャアイテム表示範囲外移動フラグ。取得。
		*/
		public bool GetCaptureViewOutFlag()
		{
			return this.capture_viewout;
		}

		/** [Fee.Ui.ScrollItem_Base]矩形変更。
		*/
		public abstract void OnChangeParentRectX(int a_parent_x);

		/** [Fee.Ui.ScrollItem_Base]矩形変更。
		*/
		public abstract void OnChangeParentRectY(int a_parent_y);

		/** [Fee.Ui.ScrollItem_Base]矩形変更。
		*/
		public abstract void OnChangeParentRectWH(int a_parent_w,int a_parent_h);

		/** [Fee.Ui.ScrollItem_Base]クリップ矩形変更。
		*/
		public abstract void OnChangeParentClipRect(in Fee.Geometry.Rect2D_R<int> a_parent_rect);

		/** [Fee.Ui.ScrollItem_Base]描画プライオリティ変更。
		*/
		public abstract void OnChangeParentDrawPriority(long a_parent_drawpriority);

		/** [Fee.Ui.ScrollItem_Base]表示内。
		*/
		public abstract void OnViewIn();

		/** [Fee.Ui.ScrollItem_Base]表示外。
		*/
		public abstract void OnViewOut();
	}
}

