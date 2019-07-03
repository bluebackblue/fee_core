

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。アセットバンドルパスリスト。
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
		#if(UNITY_EDITOR)
		AssetsDummyAssetBundle,
		#endif
	}
}

