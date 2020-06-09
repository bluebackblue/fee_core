

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief 関数呼び出し。
*/


/** Fee.Function
*/
namespace Fee.Function
{
	/** UnityStart_CallBackParam_Generic
	*/
	public readonly struct UnityStart_CallBackParam_Generic<T> : UnityStart_CallBackParam_Base
	{
		/** callback_interface
		*/
		public readonly UnityStart_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public UnityStart_CallBackParam_Generic(UnityStart_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** UnityStart
		*/
		public void UnityStart()
		{
			if(this.callback_interface != null){
				this.callback_interface.UnityStart(this.id);
			}
		}
	}
}

