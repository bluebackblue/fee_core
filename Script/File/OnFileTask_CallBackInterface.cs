

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
	/** OnFileTask_CallBackInterface
	*/
	public interface OnFileTask_CallBackInterface
	{
		/** [Fee.File.OnFileTask_CallBackInterface]タスク実行中。
		*/
		void OnFileTask(float a_progress);
	}
}

