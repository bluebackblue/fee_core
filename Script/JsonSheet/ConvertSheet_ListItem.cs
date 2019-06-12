

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮシート。コンフィグ。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** コンバートシート。リストアイテム。
	*/
	public class ConvertSheet_ListItem
	{
		/** command

			<enum> : ＥＮＵＭ出力。
			<json> : ＪＳＯＮ出力。

		*/
		public string command;

		/** output

			相対パス

		*/
		public string output;

		/** key

			連結ルート名

		*/
		public string key_0;
		public string key_1;
		public string key_2;
		public string key_3;
	}
}

