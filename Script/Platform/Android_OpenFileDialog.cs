

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
	#if((!UNITY_EDITOR)&&(UNITY_ANDROID))

	/** Android_OpenFileDialog
	*/
	class Android_OpenFileDialog
	{
		/** OpenFileDialog
		*/
		public static void OpenFileDialog(MonoBehaviour_Root a_root_instance)
		{
			a_root_instance.openfiledialog_result = null;

			UnityEngine.AndroidJavaClass t_class_openfiledialog = new UnityEngine.AndroidJavaClass("fee.platform.Android_OpenFileDialogActivity");
			t_class_openfiledialog.CallStatic("DO","*/*");
		}

		/** GetOpenFileDialogResultBianry
		*/
		public static byte[] GetOpenFileDialogResultBianry(MonoBehaviour_Root a_root_instance)
		{
			UnityEngine.AndroidJavaClass t_class_openfiledialog = new UnityEngine.AndroidJavaClass("fee.platform.Android_OpenFileDialogActivity");
			UnityEngine.AndroidJavaObject t_array_object = t_class_openfiledialog.CallStatic<UnityEngine.AndroidJavaObject>("GET");
			byte[] t_result = UnityEngine.AndroidJNIHelper.ConvertFromJNIArray<byte[]>(t_array_object.GetRawObject());

			return t_result;
		}
	}

	#endif
}

