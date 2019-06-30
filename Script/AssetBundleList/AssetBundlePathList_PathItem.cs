

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** AssetBundlePathList_PathItem
	*/
	public class AssetBundlePathList_PathItem
	{
		/** pathtype
		*/
		public AssetBundlePathList_PathType pathtype;

		/** path
		*/
		public Fee.File.Path path;

		/** constructor
		*/
		public AssetBundlePathList_PathItem(AssetBundlePathList_PathType a_pathtype,Fee.File.Path a_path)
		{
			//pathtype
			this.pathtype = a_pathtype;

			//path
			this.path = a_path;
		}
	}
}

