

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。コルーチン。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** OnCoroutine_CallBackInterface
	*/
	public interface OnCoroutine_CallBackInterface
	{
		/** [Fee.SoundPool.OnCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress_up,float a_progress_down);
	}
}

