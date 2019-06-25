

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
	/** OnCryptTask_CallBackInterface
	*/
	public interface OnCryptTask_CallBackInterface
	{
		/** [Fee.Crypt.OnCryptTask_CallBackInterface]タスク実行中。
		*/
		void OnCryptTask(float a_progress);
	}
}

