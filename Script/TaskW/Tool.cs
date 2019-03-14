

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief タスク。ツール。
*/


//Unreachable code detected.
#pragma warning disable 0162


/** Fee.TaskW
*/
namespace Fee.TaskW
{
	/** Tool
	*/
	public class Tool
	{
		/** Log
		*/
		public static void Log(string a_tag,string a_text)
		{
			if(Config.LOG_ENABLE == true){
				#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)
				UnityEngine.Debug.Log(a_tag + " : " + a_text);
				#endif
			}
		}

		/** LogError
		*/
		public static void LogError(string a_tag,string a_text)
		{
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)
			UnityEngine.Debug.LogError(a_tag + " : " + a_text);
			#endif
		}

		/** LogError
		*/
		public static void LogError(System.Exception a_exception)
		{
			#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)
			UnityEngine.Debug.LogError(a_exception.StackTrace + "\n\n" + a_exception.Message);
			#endif
		}

		/** Assert
		*/
		public static void Assert(bool a_flag)
		{
			if(Config.ASSERT_ENABLE == true){
				if(a_flag == false){

					#if(UNITY_EDITOR)||(DEVELOPMENT_BUILD)
					UnityEngine.Debug.Log("Assert");
					#endif

					#if(UNITY_EDITOR)
					UnityEditor.EditorApplication.isPaused = true;
					#endif
				}
			}
		}
	}
}

