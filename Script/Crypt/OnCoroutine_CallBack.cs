

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 暗号。コルーチン。
*/


/** Fee.Crypt
*/
namespace Fee.Crypt
{
	/** OnCoroutine_CallBack
	*/
	public interface OnCoroutine_CallBack
	{
		/** [Fee.File.OnCoroutine_CallBack]コルーチンからのコールバック。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress);
	}
}

