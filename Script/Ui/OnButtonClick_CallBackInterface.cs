

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
	/** OnButtonClick_CallBackInterface
	*/
	public interface OnButtonClick_CallBackInterface<T>
	{
		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		void OnButtonClick(T a_id);
	}

	/** OnButtonClick_CallBackParam
	*/
	public interface OnButtonClick_CallBackParam
	{
		/** Call
		*/
		void Call();
	}

	/** OnButtonClick_CallBackParam_Generic
	*/
	public class OnButtonClick_CallBackParam_Generic<T> : OnButtonClick_CallBackParam
	{
		/** callback_interface
		*/
		public OnButtonClick_CallBackInterface<T> callback_interface;

		/** id
		*/
		public T id;

		/** constructor
		*/
		public OnButtonClick_CallBackParam_Generic(OnButtonClick_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** Call
		*/
		public void Call()
		{
			if(this.callback_interface != null){
				this.callback_interface.OnButtonClick(this.id);
			}
		}
	}
}

