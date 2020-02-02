

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief フォーカス。グループ。
*/


/** Fee.Focus
*/
namespace Fee.Focus
{
	/** FocusGroup_Item_CallBackParam
	*/
	public interface FocusGroup_Item_CallBackParam
	{
		/** フォーカスＯＮ
		*/
		void OnFocusOn();

		/** フォーカスＯＦＦ。
		*/
		void OnFocusOff();
	}

	/** FocusGroup_Item_CallBackParam_Generic
	*/
	public class FocusGroup_Item_CallBackParam_Generic<T> : FocusGroup_Item_CallBackParam
	{
		/** item
		*/
		public T item;

		/** フォーカスＯＮ時に呼び出すコールバック。
		*/
		public System.Action<T> callback_on;

		/** フォーカスＯＦＦ時に呼び出すコールバック。
		*/
		public System.Action<T> callback_off;

		/** constructor
		*/
		public FocusGroup_Item_CallBackParam_Generic(T a_item,System.Action<T> a_callback_on,System.Action<T> a_callback_off)
		{
			//item
			this.item = a_item;

			//callback_on
			this.callback_on = a_callback_on;

			//callback_off
			this.callback_off = a_callback_off;
		}

		/** フォーカスＯＮ。
		*/
		public void OnFocusOn()
		{
			if(this.callback_on != null){
				this.callback_on.Invoke(this.item);
			}
		}

		/** フォーカスＯＦＦ。
		*/
		public void OnFocusOff()
		{
			if(this.callback_off != null){
				this.callback_off.Invoke(this.item);
			}
		}
	}

	/** FocusGroup_Item
	*/
	public class FocusGroup_Item
	{
		/** item
		*/
		public FocusItem_Base item;

		/** callbackparam
		*/
		public FocusGroup_Item_CallBackParam callbackparam;

		/** Create
		*/
		public static FocusGroup_Item Create<T>(T a_item,System.Action<T> a_callback_on,System.Action<T> a_callback_off)
			where T : FocusItem_Base
		{
			FocusGroup_Item t_item = new FocusGroup_Item();
			{
				t_item.item = a_item;
				t_item.callbackparam = new FocusGroup_Item_CallBackParam_Generic<T>(a_item,a_callback_on,a_callback_off);
			}
			return t_item;
		}

		/** フォーカスＯＮ。
		*/
		public void OnFocusOn()
		{
			this.callbackparam.OnFocusOn();
		}

		/** フォーカスＯＦＦ。
		*/
		public void OnFocusOff()
		{
			this.callbackparam.OnFocusOff();
		}
	}
}

