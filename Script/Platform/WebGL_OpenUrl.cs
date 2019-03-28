

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief プラットフォーム。ＵＲＬを開く。
*/


/** Fee.Platform
*/
namespace Fee.Platform
{
	/** WebGL_OpenUrl
	*/
	#if((!UNITY_EDITOR)&&(UNITY_WEBGL))
	class WebGL_OpenUrl
	{
		/** Fee_Platform_WebGLPlugin_OpenUrl

		Plugins/Platform/openurl:Fee_Platform_WebGLPlugin_OpenUrl

		*/
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern void Fee_Platform_WebGLPlugin_OpenUrl(string a_url);

		/** OpenUrl
		*/
		public static void OpenUrl(string  a_url)
		{
			try{
				WebGL_OpenUrl.Fee_Platform_WebGLPlugin_OpenUrl(a_url);
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
	#endif
}

