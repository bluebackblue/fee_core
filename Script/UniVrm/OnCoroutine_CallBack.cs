

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
	/** OnCoroutine_CallBack
	*/
	public interface OnCoroutine_CallBack
	{
		/** [Fee.UniVrm.OnCoroutine_CallBack]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress);
	}
}

