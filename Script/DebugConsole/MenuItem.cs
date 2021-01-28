

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
	/** MenuItem
	*/
	public class MenuItem
	{
		/** 表示。
		*/
		#if(!NOUSE_DEF_FEE_EDITORMENU)
		[UnityEditor.MenuItem("Fee/Console/Show")]
		private static void MenuItem_Console_Show()
		{
			Window.GetWindow<Window>("DebugConsole");
		}
		#endif
	}
}

