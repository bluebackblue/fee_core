

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フォーカス。グループ。
*/


/** Fee.Focus
*/
namespace Fee.Focus
{
	/** FocusItem_Base
	*/
	public interface FocusItem_Base
	{
		/** [Fee.Focus.FocusItem_Base]フォーカス。設定。

			OnFocusCheckを呼び出さない。。

		*/
		void SetFocus_NoCall(bool a_flag);

		/** [Fee.Focus.FocusItem_Base]フォーカス。設定。

			OnFocusCheckを呼び出す。

		*/
		void SetFocus_CallOnFocusCheck(bool a_flag);

		/** [Fee.Focus.FocusItem_Base]フォーカス。チェック。
		*/
		bool IsFocus();
	}
}

