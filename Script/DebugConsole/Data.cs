

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief デバッグコンソール。データ。
*/


/** Fee.DebugConsole
*/
#if(UNITY_EDITOR)
namespace Fee.DebugConsole
{
	/** Data
	*/
	public class Data : UnityEditor.ScriptableSingleton<Data>
	{
		/** list
		*/
		public System.Collections.Generic.List<Item> list = new System.Collections.Generic.List<Item>();

		/** GetList
		*/
		public static System.Collections.Generic.List<Item> GetList()
		{
			return UnityEditor.ScriptableSingleton<Data>.instance.list;
		}

		/** Clear
		*/
		public static void Clear()
		{
			UnityEditor.ScriptableSingleton<Data>.instance.list.Clear();
			Window.ClearList();
		}

		/** Log
		*/
		public static void Log(string a_text)
		{
			UnityEditor.ScriptableSingleton<Data>.instance.list.Add(new Item(a_text));
			Window.ReCreateList();
		}
	}
}
#endif

