

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。コールバックインターフェイス。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** OnDataCoroutine_CallBackInterface
	*/
	public interface OnDataCoroutine_CallBackInterface
	{
		/** [Fee.Data.OnDataCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnDataCoroutine(float a_progress);
	}
}

