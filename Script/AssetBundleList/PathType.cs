

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドル。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** PathType
	*/
	public enum PathType
	{
		#if(UNITY_EDITOR)
		AssetsAssetBundle,
		#endif

		UrlAssetBundle,
	}	
}

