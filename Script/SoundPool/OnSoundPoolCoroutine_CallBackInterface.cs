

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief サウンドプール。コールバックインターフェイス。
*/


/** Fee.SoundPool
*/
namespace Fee.SoundPool
{
	/** OnSoundPoolCoroutine_CallBackInterface
	*/
	public interface OnSoundPoolCoroutine_CallBackInterface
	{
		/** [Fee.SoundPool.OnSoundPoolCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnSoundPoolCoroutine(float a_progress_up,float a_progress_down);
	}
}

