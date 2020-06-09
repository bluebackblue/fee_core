

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
	/** UnityStart_MonoBehaviour
	*/
	public class UnityStart_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** callback_param
		*/
		private UnityStart_CallBackParam_Base callback_param;

		/** コールバック。設定。
		*/
		public void SetCallBack<T>(UnityStart_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_param = new UnityStart_CallBackParam_Generic<T>(a_callback_interface,a_id);
		}

		/** Start
		*/
		private void Start()
		{
			try{
				this.callback_param.UnityStart();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

