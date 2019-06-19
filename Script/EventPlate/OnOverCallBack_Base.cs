

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief イベントプレート。コールバック。
*/


/** Fee.EventPlate
*/
namespace Fee.EventPlate
{
	/** OnOverCallBack_Base
	*/
	public interface OnOverCallBack_Base
	{
		/** [Fee.EventPlateOnOverCallBack_Base]イベントプレートに入場。
		*/
		void OnOverEnter(int a_value);

		/** [Fee.EventPlateOnOverCallBack_Base]イベントプレートから退場。
		*/
		void OnOverLeave(int a_value);
	}
}

