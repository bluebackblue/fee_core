

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プラットフォーム。オープンファイルダイアログ。
*/


/** Fee.Platform
*/
namespace Fee.Platform
{
	/** WebGL_OpenFileDialog
	*/
	#if((!UNITY_EDITOR)&&(UNITY_WEBGL))
	class WebGL_OpenFileDialog
	{
		/** Fee_Platform_WebGLPlugin_OpenFileDialog_Register

		Plugins/Platform/openfiledialog:Fee_Platform_WebGLPlugin_OpenFileDialog_Register

		*/
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern void Fee_Platform_WebGLPlugin_OpenFileDialog_Register();

		/** Fee_Platform_WebGLPlugin_OpenFileDialog_Open

		Plugins/Platform/openfiledialog:Fee_Platform_WebGLPlugin_OpenFileDialog_Open

		*/
		[System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern void Fee_Platform_WebGLPlugin_OpenFileDialog_Open();

		/** 登録。

			インプットフィールド追加のために、一度呼び出す必要がある。

		*/
		public static void Register()
		{
			try{
				WebGL_OpenFileDialog.Fee_Platform_WebGLPlugin_OpenFileDialog_Register();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** OpenFileDialog
		*/
		public static void OpenFileDialog(Root_MonoBehaviour a_root_monobehaviour)
		{
			//a_root_monobehaviour.openfiledialog_result = null;

			try{
				WebGL_OpenFileDialog.Fee_Platform_WebGLPlugin_OpenFileDialog_Open();
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				//a_root_monobehaviour.openfiledialog_result = "";
			}
		}
	}
	#endif
}

