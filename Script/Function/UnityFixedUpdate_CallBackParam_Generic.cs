

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
	/** UnityFixedUpdate_CallBackParam_Generic
	*/
	public readonly struct UnityFixedUpdate_CallBackParam_Generic<T> : UnityFixedUpdate_CallBackParam_Base
	{
		/** callback_interface
		*/
		public readonly UnityFixedUpdate_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public UnityFixedUpdate_CallBackParam_Generic(UnityFixedUpdate_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** UnityFixedUpdate
		*/
		public void UnityFixedUpdate()
		{
			if(this.callback_interface != null){
				this.callback_interface.UnityFixedUpdate(this.id);
			}
		}
	}
}

