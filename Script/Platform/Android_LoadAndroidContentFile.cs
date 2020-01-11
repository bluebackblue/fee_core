

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

	/** Android_LoadAndroidContentFile
	*/
	class Android_LoadAndroidContentFile
	{
		/** 開始。
		*/
		public static bool Start(Root_MonoBehaviour a_root_monobehaviour,Fee.File.Path a_path)
		{
			if(a_root_monobehaviour.loadandroidcontentfile == null){
				UnityEngine.AndroidJavaObject t_async_object = null;

				try{
					using(UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadAndroidContentFile_AsyncObject")){
						t_async_object = t_class_loadcontentfile.CallStatic<UnityEngine.AndroidJavaObject>("Start",a_path.GetPath());
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}

				if(t_async_object != null){
					a_root_monobehaviour.loadandroidcontentfile = t_async_object;
					return true;
				}
			}

			return false;
		}

		/** 終了。
		*/
		public static void End(Root_MonoBehaviour a_root_monobehaviour)
		{
			if(a_root_monobehaviour.loadandroidcontentfile != null){
				try{
					using(UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadAndroidContentFile_AsyncObject")){
						t_class_loadcontentfile.CallStatic("End",a_root_monobehaviour.loadandroidcontentfile);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}

			if(a_root_monobehaviour.loadandroidcontentfile != null){
				try{
					a_root_monobehaviour.loadandroidcontentfile.Dispose();
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
				a_root_monobehaviour.loadandroidcontentfile = null;
			}
		}

		/** キャンセル。
		*/
		public static void Cancel(Root_MonoBehaviour a_root_monobehaviour)
		{
			if(a_root_monobehaviour.loadandroidcontentfile != null){
				try{
					using(UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadAndroidContentFile_AsyncObject")){
						t_class_loadcontentfile.CallStatic("Cancel",a_root_monobehaviour.loadandroidcontentfile);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}
		}

		/** IsComplate。
		*/
		public static bool IsComplate(Root_MonoBehaviour a_root_monobehaviour)
		{
			bool t_ret = false;

			if(a_root_monobehaviour.loadandroidcontentfile != null){
				try{
					using(UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadAndroidContentFile_AsyncObject")){
						t_ret = t_class_loadcontentfile.CallStatic<bool>("IsComplate",a_root_monobehaviour.loadandroidcontentfile);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
					t_ret = false;
				}
			}

			return t_ret;
		}

		/** GetResult。
		*/
		public static byte[] GetResult(Root_MonoBehaviour a_root_monobehaviour)
		{
			byte[] t_ret = null;

			if(a_root_monobehaviour.loadandroidcontentfile != null){
				try{
					using(UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadAndroidContentFile_AsyncObject")){
						using(UnityEngine.AndroidJavaObject t_array_object = t_class_loadcontentfile.CallStatic<UnityEngine.AndroidJavaObject>("GetResult",a_root_monobehaviour.loadandroidcontentfile)){
							if(t_array_object != null){
								System.IntPtr t_array_object_pointer = t_array_object.GetRawObject();
								t_ret = UnityEngine.AndroidJNIHelper.ConvertFromJNIArray<byte[]>(t_array_object_pointer);
							}
						}
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
					t_ret = null;
				}
			}

			return t_ret;
		}

		/** GetThreadStatus。
		*/
		public static int GetThreadStatus(Root_MonoBehaviour a_root_monobehaviour)
		{
			int t_ret = -1;

			if(a_root_monobehaviour.loadandroidcontentfile != null){
				try{
					using(UnityEngine.AndroidJavaClass t_class_loadcontentfile = new UnityEngine.AndroidJavaClass("fee.platform.Android_LoadAndroidContentFile_AsyncObject")){
						t_ret = t_class_loadcontentfile.CallStatic<int>("GetThreadStatus",a_root_monobehaviour.loadandroidcontentfile);
					}
				}catch(System.Exception t_exception){
					Tool.DebugReThrow(t_exception);
				}
			}

			return t_ret;
		}
	}

	#endif
}

