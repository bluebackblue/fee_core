

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プラットフォーム。プロンプトを開く。
*/


/** Fee.Platform
*/
namespace Fee.Platform
{
	/** WebGL_OpenPrompt
	*/
	#if((!UNITY_EDITOR)&&(UNITY_WEBGL))
	class WebGL_OpenPrompt
	{
		/** Fee_Platform_WebGLPlugin_OpenPrompt

		Plugins/Platform/openprompt:Fee_Platform_WebGLPlugin_OpenPrompt

		*/
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern string Fee_Platform_WebGLPlugin_OpenPrompt(string a_title,string a_text);

		/** OpenPrompt
		*/
		public static string OpenPrompt(string a_title,string a_text)
		{
			try{
				return WebGL_OpenPrompt.Fee_Platform_WebGLPlugin_OpenPrompt(a_title,a_text);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				return null;
			}
		}
	}
	#endif
}

