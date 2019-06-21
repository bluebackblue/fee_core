

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
	/** エディターデータシート。リストアイテム。
	*/
	public class EditorDataSheet_ListItem
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
	}
}

