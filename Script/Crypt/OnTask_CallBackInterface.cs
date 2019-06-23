

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
	/** OnTask_CallBackInterface
	*/
	public interface OnTask_CallBackInterface
	{
		/** [Fee.Crypt.OnTask_CallBackInterface]タスク実行中。
		*/
		void OnTask(float a_progress);
	}
}

