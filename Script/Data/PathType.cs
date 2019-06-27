

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

		/** リソース。プレハブ。アセットバンドル化可能。
		*/
		Resources_Prefab,

		/** リソース。テクスチャ。アセットバンドル化可能。
		*/
		Resources_Texture,

		/** リソース。テキスト。アセットバンドル化可能。
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

	}
}

