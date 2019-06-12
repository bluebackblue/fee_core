

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
	/** Config
	*/
	public class Config
	{
		/** ログ。
		*/
		public static bool LOG_ENABLE = false;

		/** ログエラー。
		*/
		public static bool LOGERROR_ENABLE = true;

		/** アサート。
		*/
		public static bool ASSERT_ENABLE = true;



		/** シート名。コンバート。
		*/
		public static string SHEETNAME_CONVERT = "convert";



		/** コンバートシート。コマンド。

			TODO

		*/
		public static string CONVERTSHEET_COMMAND_JSON = "<json>";

		/** コンバートシート。コマンド。

			TODO

		*/
		public static string CONVERTSHEET_COMMAND_ENUM = "<enum>";

		/** ＥＮＵＭシート。コマンド。

			text : xxx.xxx

		*/
		public static string ENUMSHEET_COMMAND_NAMESPACE = "<namespace>";

		/** ＥＮＵＭシート。コマンド。

			text : enumの型名。
			comment : コメント

		*/
		public static string ENUMSHEET_COMMAND_ENUMNAME = "<enumname>";

		/** ＥＮＵＭシート。コマンド。

			text : enumの要素名。
			comment : コメント

		*/
		public static string ENUMSHEET_COMMAND_ITEM = "<item>";




		/** ＥＮＵＭコンバート。テンプレート。
		*/
		public static string ENUMCONVERT_TEMPLATE_MAIN = 
@"

/** <<namespace>>
*/
namespace <<namespace>>
{
	/** <<enumcomment>>
	*/
	public enum <<enumname>>
	{
<<itemroot>>	}
}

";

		/** ＥＮＵＭコンバート。テンプレート。
		*/
		public static string ENUMCONVERT_TEMPLATE_ITEM = 
@"		/** <<itemcomment>>
		*/
		<<itemname>>,

";


		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_NAMESPACE = "<<namespace>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ENUMCOMMENT = "<<enumcomment>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ENUMNAME = "<<enumname>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ITEMROOT = "<<itemroot>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ITEMCOMMENT = "<<itemcomment>>";

		/** ＥＮＵＭコンバート。置換キーワード。
		*/
		public static string ENUMCONVERT_KEYWORD_ITEMNAME = "<<itemname>>";

	}
}

