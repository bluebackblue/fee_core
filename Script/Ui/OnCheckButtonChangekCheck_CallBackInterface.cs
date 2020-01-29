

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
	/** OnCheckButtonChangekCheck_CallBackInterface
	*/
	public interface OnCheckButtonChangekCheck_CallBackInterface<T>
	{
		/** [Fee.Ui.OnCheckButtonChangekCheck_CallBackInterface]チェック変更。
		*/
		void OnCheckButtonChangeCheck(T a_id,bool a_check_flag);
	}

	/** OnCheckButtonChangekCheck_CallBackParam
	*/
	public interface OnCheckButtonChangekCheck_CallBackParam
	{
		/** Call
		*/
		void Call(bool a_check_flag);
	}

	/** OnCheckButtonChangekCheck_CallBackParam_Generic
	*/
	public readonly struct OnCheckButtonChangekCheck_CallBackParam_Generic<T> : OnCheckButtonChangekCheck_CallBackParam
	{
		/** callback_interface
		*/
		public readonly OnCheckButtonChangekCheck_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public OnCheckButtonChangekCheck_CallBackParam_Generic(OnCheckButtonChangekCheck_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** Call
		*/
		public void Call(bool a_check_flag)
		{
			if(this.callback_interface != null){
				this.callback_interface.OnCheckButtonChangeCheck(this.id,a_check_flag);
			}
		}
	}
}

