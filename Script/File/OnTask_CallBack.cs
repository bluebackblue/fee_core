

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。コルーチン。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** OnTask_CallBack
	*/
	public interface OnTask_CallBack
	{
		/** [Fee.File.OnTask_CallBack]タスク実行中。
		*/
		void OnTask(float a_progress);
	}
}

