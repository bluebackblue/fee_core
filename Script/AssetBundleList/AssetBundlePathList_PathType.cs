

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
	/** AssetBundlePathList_PathType
	*/
	public enum AssetBundlePathList_PathType
	{
		/** アセット。アセットバンドル。
		*/
		#if(UNITY_EDITOR)
		AssetsAssetBundle,
		#endif

		/** ＵＲＬ。アセットバンドル。
		*/
		UrlAssetBundle,

		/** アセット。ダミーアセットバンドル。
		*/
		AssetsDummyAssetBundle,
	}
}

