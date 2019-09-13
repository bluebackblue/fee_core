

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。パスリスト。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** PathItem
	*/
	public class PathItem
	{
		/** assetbundle_pathtype
		*/
		public AssetBundlePathType assetbundle_pathtype;

		/** アセットバンドルのパス。
		*/
		public Fee.File.Path assetbundle_path;

		/** constructor
		*/
		public PathItem(AssetBundlePathType a_assetbundle_pathtype,Fee.File.Path a_assetbundle_path)
		{
			//assetbundle_pathtype
			this.assetbundle_pathtype = a_assetbundle_pathtype;

			//assetbundle_path
			this.assetbundle_path = a_assetbundle_path;
		}
	}
}

