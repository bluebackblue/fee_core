

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
	/** OnTask_CallBackInterface
	*/
	public interface OnTask_CallBackInterface
	{
		/** [Fee.File.OnTask_CallBackInterface]タスク実行中。
		*/
		void OnTask(float a_progress);
	}
}

