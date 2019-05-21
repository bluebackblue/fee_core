

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** OnCoroutine_CallBack
	*/
	public interface OnCoroutine_CallBack
	{
		/** [Fee.File.OnCoroutine_CallBack]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress_up,float a_progress_down);
	}
}

