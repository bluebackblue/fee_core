

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
	/** UnityOnPreRender_CallBackParam_Generic
	*/
	public readonly struct UnityOnPreRender_CallBackParam_Generic<T> : UnityOnPreRender_CallBackParam_Base
	{
		/** callback_interface
		*/
		public readonly UnityOnPreRender_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public UnityOnPreRender_CallBackParam_Generic(UnityOnPreRender_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** UnityOnPreRender
		*/
		public void UnityOnPreRender()
		{
			if(this.callback_interface != null){
				this.callback_interface.UnityOnPreRender(this.id);
			}
		}
	}
}

