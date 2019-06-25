

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
	/** OnCryptCoroutine_CallBackInterface
	*/
	public interface OnCryptCoroutine_CallBackInterface
	{
		/** [Fee.Crypt.OnCryptCoroutine_CallBackInterface]コルーチンからのコールバック。

			return == false : キャンセル。

		*/
		bool OnCryptCoroutine(float a_progress);
	}
}

