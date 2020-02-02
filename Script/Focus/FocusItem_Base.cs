

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
		*/
		void SetFocus(bool a_flag);

		/** [Fee.Focus.FocusItem_Base]フォーカス。チェック。
		*/
		bool IsFocus();

		/** [Fee.Focus.FocusItem_Base]フォーカス。設定。OnFocusCheckを呼び出す。
		*/
		void SetFocusCallOnFocusCheck(bool a_flag);
	}
}

