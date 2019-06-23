

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ファイル。コールバックインターフェイス。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** OnCoroutine_CallBackInterface
	*/
	public interface OnCoroutine_CallBackInterface
	{
		/** [Fee.File.OnCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress_up,float a_progress_down);
	}
}

