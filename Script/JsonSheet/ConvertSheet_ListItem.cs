

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
	/** コンバートシート。リストアイテム。
	*/
	public class ConvertSheet_ListItem
	{
		/** convert_command

			<enum>			: ＥＮＵＭ出力。
			<json>			: ＪＳＯＮ出力。
			<se>			: ＳＥプレハブ出力。
			<data>			: データＪＳＯＮ出力。
			<assetbundle>	: アセットバンドル出力。

		*/
		public string convert_command;

		/** convert_param

			<data>
				<editor>	: エディタ。
				<release>	: リリース。

		*/
		public string convert_param;

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

