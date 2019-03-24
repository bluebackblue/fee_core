

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief プラットフォーム。仮想DBからIDBへの書き込み。
*/


/** Fee.Platform
*/
namespace Fee.Platform
{
	/** WebGL_SyncFs
	*/
	class WebGL_SyncFs
	{
		/** Fee_Platform_WebGLPlugin_SyncFs

		Plugins/Platform/syncfs:Fee_Platform_WebGLPlugin_SyncFs

		*/
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern bool Fee_Platform_WebGLPlugin_SyncFs();

		/** SyncFs
		*/
		public static void SyncFs()
		{
			try{
				bool t_ret = WebGL_SyncFs.Fee_Platform_WebGLPlugin_SyncFs();
				if(t_ret == false){
					Tool.LogError("Fee_Platform_WebGLPlugin_SyncFs","error");
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

