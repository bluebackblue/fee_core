

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
		/** id
		*/
		public string id;

		/** path_type
		*/
		public PathType path_type;

		/** path
		*/
		public Fee.File.Path path;

		/** assetbundle_name
		*/
		public string assetbundle_name;

		/** constructor

			JsonToObject

		*/
		public ListItem()
		{
			//id
			this.id = null;

			//path_type
			this.path_type = PathType.None;

			//path
			this.path = null;

			//assetbundle_name
			this.assetbundle_name = null;
		}

		/** constructor
		*/
		public ListItem(string a_id,PathType a_path_type,Fee.File.Path a_path,string a_assetbundle_name)
		{
			//id
			this.id = a_id;

			//path_type
			this.path_type = a_path_type;

			//path
			this.path = a_path;

			//assetbundle_name
			this.assetbundle_name = a_assetbundle_name;
		}
	}
}

