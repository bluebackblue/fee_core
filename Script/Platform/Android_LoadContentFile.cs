

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

	/** Android_LoadContentFile
	*/
	class Android_LoadContentFile
	{
		/** OpenFileDialog
		*/
		public static byte[] LoadContentFile(Fee.File.Path a_path)
		{
			byte[] t_binary = null;

			try{
				UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadContentFile");
				UnityEngine.AndroidJavaObject t_array_object = t_class_loadcontentfile.CallStatic<UnityEngine.AndroidJavaObject>("LoadContentFile",a_path.GetPath());
				if(t_array_object != null){
					System.IntPtr t_array_object_pointer = t_array_object.GetRawObject();
					t_binary = UnityEngine.AndroidJNIHelper.ConvertFromJNIArray<byte[]>(t_array_object_pointer);
				}else{
					Tool.Assert(false);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_binary;
		}
	}

	#endif
}

