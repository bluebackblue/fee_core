

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief グラフィック。UnityOnPreRender。
*/


/** Fee.Graphic
*/
namespace Fee.Graphic
{
	/** UnityOnPreRender_MonoBehaviour
	*/
	public class UnityOnPreRender_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** callback_param
		*/
		private UnityOnPreRender_CallBackParam_Base callback_param;

		/** コールバック。設定。
		*/
		public void SetCallBack<T>(UnityOnPreRender_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_param = new UnityOnPreRender_CallBackParam_Generic<T>(a_callback_interface,a_id);
		}

		/** OnPreRender
		*/
		private void OnPreRender()
		{
			try{
				this.callback_param.UnityOnPreRender();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

