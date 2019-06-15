

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
	/** コンバートシート。リストアイテム。
	*/
	public class ConvertSheet_ListItem
	{
		/** convert_command

			<enum> : ＥＮＵＭ出力。
			<json> : ＪＳＯＮ出力。

		*/
		public string convert_command;

		/** convert_output

			相対パス

		*/
		public string convert_output;

		/** convert_sheet

			連結ルート名

		*/
		public string convert_sheet_0;
		public string convert_sheet_1;
		public string convert_sheet_2;
		public string convert_sheet_3;
	}
}

