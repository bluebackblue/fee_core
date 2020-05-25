

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief グラフィック。UnityOnPostRender。
*/


/** Fee.Graphic
*/
namespace Fee.Graphic
{
	/** UnityOnPostRender_CallBackParam_Generic
	*/
	public readonly struct UnityOnPostRender_CallBackParam_Generic<T> : UnityOnPostRender_CallBackParam_Base
	{
		/** callback_interface
		*/
		public readonly UnityOnPostRender_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public UnityOnPostRender_CallBackParam_Generic(UnityOnPostRender_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** UnityOnPostRender。
		*/
		public void UnityOnPostRender()
		{
			if(this.callback_interface != null){
				this.callback_interface.UnityOnPostRender(this.id);
			}
		}
	}
}

