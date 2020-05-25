

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
	/** UnityOnPostRender_MonoBehaviour
	*/
	public class UnityOnPostRender_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** callback_param
		*/
		private UnityOnPostRender_CallBackParam_Base callback_param;

		/** コールバック。設定。
		*/
		public void SetCallBack<T>(UnityOnPostRender_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_param = new UnityOnPostRender_CallBackParam_Generic<T>(a_callback_interface,a_id);
		}

		/** OnPostRender
		*/
		private void OnPostRender()
		{
			try{
				this.callback_param.UnityOnPostRender();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

