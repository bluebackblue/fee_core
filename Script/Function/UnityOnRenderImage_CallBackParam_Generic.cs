

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
	/** UnityOnRenderImage_CallBackParam_Generic
	*/
	public readonly struct UnityOnRenderImage_CallBackParam_Generic<T> : UnityOnRenderImage_CallBackParam_Base
	{
		/** callback_interface
		*/
		public readonly UnityOnRenderImage_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public UnityOnRenderImage_CallBackParam_Generic(UnityOnRenderImage_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** UnityOnRenderImage
		*/
		public void UnityOnRenderImage(UnityEngine.RenderTexture a_source,UnityEngine.RenderTexture a_dest)
		{
			if(this.callback_interface != null){
				this.callback_interface.UnityOnRenderImage(this.id,a_source,a_dest);
			}
		}
	}
}

