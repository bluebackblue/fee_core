

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
	/** UnityOnDestroy_MonoBehaviour
	*/
	public class UnityOnDestroy_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** callback_param
		*/
		private UnityOnDestroy_CallBackParam_Base callback_param;

		/** コールバック。設定。
		*/
		public void SetCallBack<T>(UnityOnDestroy_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_param = new UnityOnDestroy_CallBackParam_Generic<T>(a_callback_interface,a_id);
		}

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			#if(UNITY_EDITOR)
			if(this.callback_param == null){
				return;
			}
			#endif

			try{
				this.callback_param.UnityOnDestroy();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

