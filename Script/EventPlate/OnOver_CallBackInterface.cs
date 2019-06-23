

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief イベントプレート。コールバックインターフェイス。
*/


/** Fee.EventPlate
*/
namespace Fee.EventPlate
{
	/** OnOver_CallBackInterface
	*/
	public interface OnOver_CallBackInterface
	{
		/** [Fee.EventPlate.OnOver_CallBackInterface]イベントプレートに入場。
		*/
		void OnOverEnter(int a_value);

		/** [Fee.EventPlate.OnOver_CallBackInterface]イベントプレートから退場。
		*/
		void OnOverLeave(int a_value);
	}
}

