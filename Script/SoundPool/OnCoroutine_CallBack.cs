

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
	/** OnCoroutine_CallBack
	*/
	public interface OnCoroutine_CallBack
	{
		/** [Fee.SoundPool.OnCoroutine_CallBack]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnCoroutine(float a_progress_up,float a_progress_down);
	}
}

