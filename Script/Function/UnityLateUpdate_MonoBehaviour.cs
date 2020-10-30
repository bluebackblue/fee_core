

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
	/** UnityLateUpdate_MonoBehaviour
	*/
	public class UnityLateUpdate_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** callback_param
		*/
		private UnityLateUpdate_CallBackParam_Base callback_param;

		/** コールバック。設定。
		*/
		public void SetCallBack<T>(UnityLateUpdate_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_param = new UnityLateUpdate_CallBackParam_Generic<T>(a_callback_interface,a_id);
		}

		/** Update
		*/
		private void Update()
		{
			try{
				this.callback_param.UnityLateUpdate();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

