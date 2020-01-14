

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
	/** アセットバンドルパスタイプ。
	*/
	public enum AssetBundlePathType
	{
		/** ＵＲＬ。
		*/
		UrlAssetBundle,

		/** ストリーミングアセット。
		*/
		StreamingAssetsAssetBundle,

		/** ローカル。
		*/
		LocalAssetBundle,

		/** フルパス。
		*/
		FullPathAssetBundle,

		/** アセットフォルダにあるアセットバンドルの相対パス。
		*/
		#if(UNITY_EDITOR)
		AssetsPathAssetBundle,
		#endif

		/** アセットフォルダにあるダミーアセットバンドルの相対パス。
		*/
		#if(UNITY_EDITOR)
		AssetsPathDummyAssetBundle,
		#endif
	}
}

