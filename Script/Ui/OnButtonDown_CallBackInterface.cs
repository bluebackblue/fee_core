

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＩ。コールバックインターフェイス。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** OnButtonDown_CallBackInterface
	*/
	public interface OnButtonDown_CallBackInterface<T>
	{
		/** [Fee.Ui.OnButtonDown_CallBackInterface]ダウン。
		*/
		void OnButtonDown(T a_id);
	}

	/** OnButtonDown_CallBackParam
	*/
	public interface OnButtonDown_CallBackParam
	{
		/** Call
		*/
		void Call();
	}

	/** OnButtonDown_CallBackParam_Generic
	*/
	public readonly struct OnButtonDown_CallBackParam_Generic<T> : OnButtonDown_CallBackParam
	{
		/** callback_interface
		*/
		public readonly OnButtonDown_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public OnButtonDown_CallBackParam_Generic(OnButtonDown_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** Call
		*/
		public void Call()
		{
			if(this.callback_interface != null){
				this.callback_interface.OnButtonDown(this.id);
			}
		}
	}
}

