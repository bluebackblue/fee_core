

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エディターツール。
*/


/** Fee.EditorTool
*/
#if(UNITY_EDITOR)
namespace Fee.EditorTool
{
	/** Tool
	*/
	public class Tool
	{
		/** EditorLog
		*/
		public static void EditorLog(string a_text)
		{
			UnityEngine.Debug.Log(a_text);
		}

		/** EditorLogError
		*/
		public static void EditorLogError(string a_text)
		{
			UnityEngine.Debug.LogError(a_text);
		}
	}
}
#endif

