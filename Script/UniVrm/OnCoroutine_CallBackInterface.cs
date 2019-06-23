

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＮＩＶＲＭ。コルーチン。
*/


/** Fee.UniVrm
*/
namespace Fee.UniVrm
{
	/** OnCoroutine_CallBackInterface
	*/
	public interface OnCoroutine_CallBackInterface
	{
		/** [Fee.UniVrm.OnCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress);
	}
}

