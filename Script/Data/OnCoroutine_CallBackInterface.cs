

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
	/** OnCoroutine_CallBackInterface
	*/
	public interface OnCoroutine_CallBackInterface
	{
		/** [Fee.Data.OnCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress);
	}
}

