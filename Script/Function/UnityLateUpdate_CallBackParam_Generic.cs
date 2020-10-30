

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
	/** UnityLateUpdate_CallBackParam_Generic
	*/
	public readonly struct UnityLateUpdate_CallBackParam_Generic<T> : UnityLateUpdate_CallBackParam_Base
	{
		/** callback_interface
		*/
		public readonly UnityLateUpdate_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public UnityLateUpdate_CallBackParam_Generic(UnityLateUpdate_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** UnityLateUpdate
		*/
		public void UnityLateUpdate()
		{
			if(this.callback_interface != null){
				this.callback_interface.UnityLateUpdate(this.id);
			}
		}
	}
}

