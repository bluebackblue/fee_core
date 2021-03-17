

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
	/** UnityOnRenderImage_MonoBehaviour
	*/
	public class UnityOnRenderImage_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** callback_param
		*/
		private UnityOnRenderImage_CallBackParam_Base callback_param;

		/** コールバック。設定。
		*/
		public void SetCallBack<T>(UnityOnRenderImage_CallBackInterface<T> a_callback_interface,T a_id)
		{
			this.callback_param = new UnityOnRenderImage_CallBackParam_Generic<T>(a_callback_interface,a_id);
		}

		/** OnRenderImage

			すべてのレンダリングが RenderImage へと完了したときに呼び出されます。

		*/
		private void OnRenderImage(UnityEngine.RenderTexture a_source,UnityEngine.RenderTexture a_dest)
		{
			#if(UNITY_EDITOR)
			if(this.callback_param == null){
				return;
			}
			#endif

			try{
				this.callback_param.UnityOnRenderImage(a_source,a_dest);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
}

