

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
	/** Scroll_Value_CallBack

		Scroll_Baseが継承。

	*/
	public interface Scroll_Value_CallBack
	{
		/** [Scroll_Value_CallBack]コールバック。位置変更。
		*/
		void OnItemPositionChange(int a_index);

		/** [Scroll_Value_CallBack]コールバック。表示変更。
		*/
		void OnItemVisibleChange(int a_index,bool a_flag);
	}
}

