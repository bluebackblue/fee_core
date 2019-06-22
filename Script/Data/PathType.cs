

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

		/** リソース。テクスチャー。アセットバンドル化可能。
		*/
		Resources_Texture,

		/** リソース。テキスト。アセットバンドル化可能。
		*/
		Resources_Text,

		/** ストリーミングアセット。テクスチャー。
		*/
		StreamingAssets_Texture,

		/** ストリーミングアセット。テキスト。
		*/
		StreamingAssets_Text,

		/** ストリーミングアセット。バイナリー。
		*/
		StreamingAssets_Binary,

		/** ＵＲＬ。テクスチャー。
		*/
		Url_Texture,

		/** ＵＲＬ。テキスト。
		*/
		Url_Text,

		/** ＵＲＬ。バイナリー。
		*/
		Url_Binary,

	}
}

