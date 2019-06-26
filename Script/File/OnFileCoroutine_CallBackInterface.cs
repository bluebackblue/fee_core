

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
	/** OnFileCoroutine_CallBackInterface
	*/
	public interface OnFileCoroutine_CallBackInterface
	{
		/** [Fee.File.OnFileCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnFileCoroutine(float a_progress);
	}
}

