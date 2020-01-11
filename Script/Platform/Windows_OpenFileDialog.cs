

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
	/** Windows_OpenFileDialog
	*/
	#if((!UNITY_EDITOR)&&(UNITY_STANDALONE_WIN))
	class Windows_OpenFileDialog
	{
		/** OpenFileDialog
		*/
		public static void OpenFileDialog(Root_MonoBehaviour a_root_monobehaviour,string a_title,string a_extension)
		{
			a_root_monobehaviour.openfiledialog_result = null;

			try{
				a_root_monobehaviour.openfiledialog_result = FeeCsDll.OpenFileDialog_Comdlg32.Open(a_title,a_extension);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				a_root_monobehaviour.openfiledialog_result = "";
			}
		}
	}
	#endif
}

