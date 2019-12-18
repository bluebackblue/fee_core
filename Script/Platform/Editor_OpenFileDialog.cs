

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
		public static void OpenFileDialog(MonoBehaviour_Root a_root_instance)
		{
			a_root_instance.openfiledialog_result = null;

			try{
				a_root_instance.openfiledialog_result = UnityEditor.EditorUtility.OpenFilePanel("ファイルを開く",UnityEngine.Application.streamingAssetsPath,"");
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				a_root_instance.openfiledialog_result = "";
			}
		}
	}
	#endif
}

