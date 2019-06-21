

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

		/** packname
		*/
		public string packname;

		/** constructor
		*/
		public ListItem(PathType a_path_type,Fee.File.Path a_path,string a_packname)
		{
			//path_type
			this.path_type = a_path_type;

			//path
			this.path = a_path;

			//packname
			this.packname = a_packname;
		}
	}
}

