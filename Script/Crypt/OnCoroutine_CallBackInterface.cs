

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 暗号。コールバックインターフェイス。
*/


/** Fee.Crypt
*/
namespace Fee.Crypt
{
	/** OnCoroutine_CallBackInterface
	*/
	public interface OnCoroutine_CallBackInterface
	{
		/** [Fee.Crypt.OnCoroutine_CallBack]コルーチンからのコールバック。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress);
	}
}

