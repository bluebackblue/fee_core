

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮシート。インデックスリストアイテム。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** データシート。リストアイテム。
	*/
	public class DataSheet_ListItem
	{
		/** data_command

			<packitem>		: パックアイテム。
			<resouceitem>	: リソースアイテム。

		*/
		public string data_command;

		/** data_id
		*/
		public string data_id;

		/** data_packname
		*/
		public string data_packname;

		/** data_path
		*/
		public string data_path;
	}
}

