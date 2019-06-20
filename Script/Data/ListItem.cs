

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。リストアイテム。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** ListItem
	*/
	public class ListItem
	{
		/** path_type
		*/
		public PathType path_type;

		/** path
		*/
		public Fee.File.Path path;

		/** constructor
		*/
		public ListItem(PathType a_path_type,Fee.File.Path a_path)
		{
			//path_type
			this.path_type = a_path_type;

			//path
			this.path = a_path;
		}
	}
}

