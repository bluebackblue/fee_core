

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
	/** UnityUpdate_CallBackParam_Generic
	*/
	public readonly struct UnityUpdate_CallBackParam_Generic<T> : UnityUpdate_CallBackParam_Base
	{
		/** callback_interface
		*/
		public readonly UnityUpdate_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public UnityUpdate_CallBackParam_Generic(UnityUpdate_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** UnityUpdate
		*/
		public void UnityUpdate()
		{
			if(this.callback_interface != null){
				this.callback_interface.UnityUpdate(this.id);
			}
		}
	}
}

