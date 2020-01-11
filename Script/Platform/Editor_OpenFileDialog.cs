

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
	/** Editor_OpenFileDialog
	*/
	#if(UNITY_EDITOR)
	class Editor_OpenFileDialog
	{
		/** OpenFileDialog
		*/
		public static void OpenFileDialog(Root_MonoBehaviour a_root_monobehaviour,string a_title,string a_extension)
		{
			a_root_monobehaviour.openfiledialog_result = null;

			try{
				a_root_monobehaviour.openfiledialog_result = UnityEditor.EditorUtility.OpenFilePanel(a_title,UnityEngine.Application.streamingAssetsPath,a_extension);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				a_root_monobehaviour.openfiledialog_result = "";
			}
		}
	}
	#endif
}

