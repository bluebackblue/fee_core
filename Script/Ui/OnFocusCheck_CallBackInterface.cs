

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
	/** OnFocusCheck_CallBackInterface
	*/
	public interface OnFocusCheck_CallBackInterface<T>
	{
		/** [Fee.Ui.OnFocusCheck_CallBackInterface]フォーカスチェック。
		*/
		void OnFocusCheck(T a_id);
	}

	/** OnFocusCheck_CallBackParam
	*/
	public interface OnFocusCheck_CallBackParam
	{
		/** Call
		*/
		void Call();
	}

	/** OnFocusCheck_CallBackParam_Generic
	*/
	public readonly struct OnFocusCheck_CallBackParam_Generic<T> : OnFocusCheck_CallBackParam
	{
		/** callback_interface
		*/
		public readonly OnFocusCheck_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public OnFocusCheck_CallBackParam_Generic(OnFocusCheck_CallBackInterface<T> a_callback_interface,T a_id)
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
					this.callback_interface.OnFocusCheck(this.id);
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}
		}
	}
}

