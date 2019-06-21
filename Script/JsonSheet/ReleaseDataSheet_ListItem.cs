

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。インデックスリストアイテム。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** リリースデータシート。リストアイテム。
	*/
	public class ReleaseDataSheet_ListItem
	{
		/** data_command

			<resources_prefab>			: リソース。プレハブ。
			<resources_texture>			: リソース。テクスチャー。
			<streamingassets_texture>	: ストリーミングアセット。テクスチャー。
			<url_text>					: ＵＲＬ。テキスト。

		*/
		public string data_command;

		/** data_id
		*/
		public string data_id;

		/** data_path
		*/
		public string data_path;

		/** data_packname
		*/
		public string data_packname;
	}
}

