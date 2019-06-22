

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
	/** JsonListItem
	*/
	public class JsonListItem
	{
		/** path_type
		*/
		public PathType path_type;

		/** path
		*/
		public string path;

		/** assetbundle_name
		*/
		public string assetbundle_name;

		/** constructor

			JsonToObject

		*/
		public JsonListItem()
		{
			//path_type
			this.path_type = PathType.None;

			//path
			this.path = null;

			//assetbundle_name
			this.assetbundle_name = null;
		}

		/** constructor
		*/
		public JsonListItem(PathType a_path_type,string a_path,string a_assetbundle_name)
		{
			//path_type
			this.path_type = a_path_type;

			//path
			this.path = a_path;

			//assetbundle_name
			this.assetbundle_name = a_assetbundle_name;
		}
	}
}

