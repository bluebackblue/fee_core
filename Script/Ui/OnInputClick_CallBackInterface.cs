

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
	/** OnInputClick_CallBackInterface
	*/
	public interface OnInputClick_CallBackInterface<T>
	{
		/** [Fee.Ui.OnInputClick_CallBackInterface]クリック。
		*/
		void OnInputClick(T a_id);
	}

	/** OnInputClick_CallBackParam
	*/
	public interface OnInputClick_CallBackParam
	{
		/** Call
		*/
		void Call();
	}

	/** OnInputClick_CallBackParam_Generic
	*/
	public readonly struct OnInputClick_CallBackParam_Generic<T> : OnInputClick_CallBackParam
	{
		/** callback_interface
		*/
		public readonly OnInputClick_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public OnInputClick_CallBackParam_Generic(OnInputClick_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** Call
		*/
		public void Call()
		{
			if(this.callback_interface != null){
				try{
					this.callback_interface.OnInputClick(this.id);
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}
		}
	}
}

