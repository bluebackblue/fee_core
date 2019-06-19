

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 入力。タッチ。フェイズ。
*/


/** Fee.Input
*/
namespace Fee.Input
{
	/** Touch_Phase_Key_Base
	*/
	public interface Touch_Phase_Key_Base
	{
		/** [Touch_Phase_Key_Base]更新。
		*/
		void OnUpdate();

		/** [Touch_Phase_Key_Base]リストから削除。
		*/
		void OnRemove();
	}
}

