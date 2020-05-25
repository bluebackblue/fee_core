

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エクセル。ツール。
*/


/** Unreachable code detected.
*/
#pragma warning disable 0162


/** Fee.Excel
*/
namespace Fee.Excel
{
	/** Tool
	*/
	public class Tool
	{
		/** Log
		*/
		public static void Log(string a_tag,string a_text)
		{
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			if(Config.LOG_ENABLE == true){
				UnityEngine.Debug.Log(a_tag + " : " + a_text);
			}
			#endif
		}

		/** LogError
		*/
		public static void LogError(string a_tag,string a_text)
		{
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			if(Config.LOGERROR_ENABLE == true){
				UnityEngine.Debug.LogError(a_tag + " : " + a_text);
			}else if(Config.LOG_ENABLE == true){
				UnityEngine.Debug.Log(a_tag + " : " + a_text);
			}
			#endif
		}

		/** DebugReThrow
		*/
		public static void DebugReThrow(System.Exception a_exception)
		{
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			if(Config.LOGERROR_ENABLE == true){
				UnityEngine.Debug.LogError(a_exception.StackTrace + "\n\n" + a_exception.Message);
			}else if(Config.LOG_ENABLE == true){
				UnityEngine.Debug.Log(a_exception.StackTrace + "\n\n" + a_exception.Message);
			}

			if(Config.DEBUGRETHROW_ENABLE == true){
				if(a_exception is Fee.ReThrowException.ReThrowException){
					throw a_exception;
				}else{
					throw new Fee.ReThrowException.ReThrowException(a_exception.Message,a_exception);
				}
			}
			#endif
		}

		/** Assert
		*/
		public static void Assert(bool a_flag)
		{
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)||(USE_DEF_FEE_DEBUGTOOL)
			if(a_flag == false){
				if(Config.ASSERT_ENABLE == true){
					UnityEngine.Debug.LogError("Assert");

					#if(UNITY_EDITOR)
					UnityEditor.EditorApplication.isPaused = true;
					#endif
				}
			}
			#endif
		}
	}
}

