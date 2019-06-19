

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。データアイテム。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** ListItem
	*/
	public class ListItem
	{
		/** path
		*/
		public Fee.File.Path path;

		/** constructor
		*/
		public ListItem(Fee.File.Path a_path)
		{
			//path
			this.path = a_path;
		}
	}
}

