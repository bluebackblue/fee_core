

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
	/** UnityOnDestroy_CallBackParam_Generic
	*/
	public readonly struct UnityOnDestroy_CallBackParam_Generic<T> : UnityOnDestroy_CallBackParam_Base
	{
		/** callback_interface
		*/
		public readonly UnityOnDestroy_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public UnityOnDestroy_CallBackParam_Generic(UnityOnDestroy_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** UnityOnDestroy
		*/
		public void UnityOnDestroy()
		{
			if(this.callback_interface != null){
				this.callback_interface.UnityOnDestroy(this.id);
			}
		}
	}
}

