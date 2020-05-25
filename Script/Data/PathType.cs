

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。パスタイプ。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** PathType
	*/
	public enum PathType
	{
		/** None
		*/
		None,

		/** アセットバンドル。プレハブ。
		*/
		AssetBundle_Prefab,

		/** アセットバンドル。テクスチャ。
		*/
		AssetBundle_Texture,

		/** アセットバンドル。テキスト。
		*/
		AssetBundle_Text,

		/** リソース。プレハブ。
		*/
		Resources_Prefab,

		/** リソース。テクスチャ。
		*/
		Resources_Texture,

		/** リソース。テキスト。
		*/
		Resources_Text,

		/** ストリーミングアセット。テクスチャ。
		*/
		StreamingAssets_Texture,

		/** ストリーミングアセット。テキスト。
		*/
		StreamingAssets_Text,

		/** ストリーミングアセット。バイナリ。
		*/
		StreamingAssets_Binary,

		/** ＵＲＬ。テクスチャ。
		*/
		Url_Texture,

		/** ＵＲＬ。テキスト。
		*/
		Url_Text,

		/** ＵＲＬ。バイナリ。
		*/
		Url_Binary,

		#if(UNITY_EDITOR)

		/** アセットパス。テキスト。
		*/
		AssetsPath_Text,

		#endif
	}
}

