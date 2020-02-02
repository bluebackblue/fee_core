

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。スクロール。ドラッグ。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Scroll_Drag_CallBack

		Scroll_Baseが継承。

	*/
	public interface Scroll_Drag_CallBack
	{
		/** [Scroll_Drag_CallBack]コールバック。表示位置。取得。
		*/
		float GetViewPosition();

		/** [Scroll_Drag_CallBack]コールバック。表示位置。設定。
		*/
		void SetViewPosition(float a_view_position);

		/** [Scroll_Drag_CallBack]コールバック。範囲チェック。
		*/
		bool IsRectIn(in Fee.Geometry.Pos2D<int> a_position);

		/** [Scroll_Drag_CallBack]コールバック。スクロール方向の値。取得。
		*/
		int GetScrollDirectionValue(in Fee.Geometry.Pos2D<int> a_position);
	}
}

