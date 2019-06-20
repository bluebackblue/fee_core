

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。コルーチン。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** OnCoroutine_CallBack
	*/
	public interface OnCoroutine_CallBack
	{
		/** [Fee.Data.OnCoroutine_CallBack]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress);
	}
}

