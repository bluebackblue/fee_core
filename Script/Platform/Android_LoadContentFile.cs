

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
	#if((!UNITY_EDITOR)&&(UNITY_ANDROID))||true

	/** Android_LoadContentFile
	*/
	class Android_LoadContentFile
	{
		/** OpenFileDialog
		*/
		/*
		public static byte[] LoadContentFile(Root_MonoBehaviour a_root_monobehaviour,Fee.File.Path a_path)
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
		*/

		/** 開始。
		*/
		public static bool Start(Root_MonoBehaviour a_root_monobehaviour,Fee.File.Path a_path)
		{
			UnityEngine.AndroidJavaObject t_async_object = null;

			try{
				UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadContentFile_AsyncObject");
				t_async_object = t_class_loadcontentfile.CallStatic<UnityEngine.AndroidJavaObject>("Start",a_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			if(t_async_object != null){
				a_root_monobehaviour.loadcontentfile_asyncobject = t_async_object;
				return true;
			}

			return false;
		}

		/** 終了。
		*/
		public static void End(Root_MonoBehaviour a_root_monobehaviour)
		{
			try{
				UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadContentFile_AsyncObject");
				t_class_loadcontentfile.CallStatic("End",a_root_monobehaviour.loadcontentfile_asyncobject);

				//a_root_monobehaviour.loadcontentfile_asyncobject.Dispose();
				a_root_monobehaviour.loadcontentfile_asyncobject = null;

			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** IsComplate。
		*/
		public static bool IsComplate(Root_MonoBehaviour a_root_monobehaviour)
		{
			bool t_ret = false;

			try{
				UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadContentFile_AsyncObject");
				t_ret = t_class_loadcontentfile.CallStatic<bool>("IsComplate",a_root_monobehaviour.loadcontentfile_asyncobject);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				t_ret = false;
			}

			return t_ret;
		}

		/** GetResult。
		*/
		public static byte[] GetResult(Root_MonoBehaviour a_root_monobehaviour)
		{
			byte[] t_ret = null;

			try{
				UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadContentFile_AsyncObject");
				UnityEngine.AndroidJavaObject t_array_object = t_class_loadcontentfile.CallStatic<UnityEngine.AndroidJavaObject>("GetResult",a_root_monobehaviour.loadcontentfile_asyncobject);

				if(t_array_object != null){
					System.IntPtr t_array_object_pointer = t_array_object.GetRawObject();
					t_ret = UnityEngine.AndroidJNIHelper.ConvertFromJNIArray<byte[]>(t_array_object_pointer);
				}

			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
				t_ret = null;
			}

			return t_ret;
		}

		/** GetThreadStatus。
		*/
		public static int GetThreadStatus(Root_MonoBehaviour a_root_monobehaviour)
		{
			int t_ret = -1;

			try{
				UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadContentFile_AsyncObject");
				t_ret = t_class_loadcontentfile.CallStatic<int>("GetThreadStatus",a_root_monobehaviour.loadcontentfile_asyncobject);
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			return t_ret;
		}
	}

	#endif
}

