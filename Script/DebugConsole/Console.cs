

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief デバッグコンソール。
*/


/** Fee.DebugConsole
*/
namespace Fee.DebugConsole
{
	/** DebugConsole
	*/
	public class DebugConsole
	{
		/** Clear
		*/
		public static void Clear()
		{
			#if(UNITY_EDITOR)
			Data.Clear();
			#endif
		}

		/** Log
		*/
		public static void Log(string a_text)
		{
			#if(UNITY_EDITOR)
			Data.Log(a_text);
			#endif
		}
	}
}

