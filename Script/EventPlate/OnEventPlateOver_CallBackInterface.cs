

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief イベントプレート。コールバックインターフェイス。
*/


/** Fee.EventPlate
*/
namespace Fee.EventPlate
{
	/** OnEventPlateOver_CallBackInterface
	*/
	public interface OnEventPlateOver_CallBackInterface<T>
	{
		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートに入場。
		*/
		void OnEventPlateEnter(T a_id);

		/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートから退場。
		*/
		void OnEventPlateLeave(T a_id);
	}

	/** OnEventPlateOver_CallBackParam
	*/
	public interface OnEventPlateOver_CallBackParam
	{
		/** 入場。
		*/
		void CallEnter();

		/** 退場。
		*/
		void CallLeave();
	}

	/** OnEventPlateOver_CallBackParam_Generic
	*/
	public readonly struct OnEventPlateOver_CallBackParam_Generic<T> : OnEventPlateOver_CallBackParam
	{
		/** callback_interface
		*/
		public readonly OnEventPlateOver_CallBackInterface<T> callback_interface;

		/** id
		*/
		public readonly T id;

		/** constructor
		*/
		public OnEventPlateOver_CallBackParam_Generic(OnEventPlateOver_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_interface = a_callback_interface;
			this.id = a_id;
		}

		/** 入場。
		*/
		public void CallEnter()
		{
			if(this.callback_interface != null){
				this.callback_interface.OnEventPlateEnter(this.id);
			}
		}

		/** 退場。
		*/
		public void CallLeave()
		{
			if(this.callback_interface != null){
				try{
					this.callback_interface.OnEventPlateLeave(this.id);
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}
		}
	}
}

