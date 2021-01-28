

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief デバッグコンソール。アイテム。
*/


/** Fee.DebugConsole
*/
namespace Fee.DebugConsole
{
	/** Item
	*/
	[System.Serializable]
	public class Item
	{
		/** text
		*/
		public string text;

		/** Item
		*/
		public Item(string a_text)
		{
			this.text = a_text;
		}
	}
}

