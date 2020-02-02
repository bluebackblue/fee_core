

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。ＥＮＵＭシート。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** Convert_EnumSheet
	*/
	#if(UNITY_EDITOR)
	public class Convert_EnumSheet
	{
		/** COMMAND
		*/
		public const string COMMAND = "<enum>";

		/** リストアイテム。
		*/
		public class ListItem
		{
			/** enum_command
			*/
			public string enum_command;

			/** enum_name
			*/
			public string enum_name;

			/** enum_comment
			*/
			public string enum_comment;
		}

		/** テンプレート。
		*/
		public static string TEMPLATE_MAIN = 
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

		/** テンプレート。
		*/
		public static string TEMPLATE_ITEM = 
@"		/** <<itemcomment>>
		*/
		<<itemname>>,

";

		/** 置換キーワード。
		*/
		public static string KEYWORD_NAMESPACE = "<<namespace>>";

		/** 置換キーワード。
		*/
		public static string KEYWORD_ENUMCOMMENT = "<<enumcomment>>";

		/** 置換キーワード。
		*/
		public static string KEYWORD_ENUMNAME = "<<enumname>>";

		/** 置換キーワード。
		*/
		public static string KEYWORD_ITEMROOT = "<<itemroot>>";

		/** 置換キーワード。
		*/
		public static string KEYWORD_ITEMCOMMENT = "<<itemcomment>>";

		/** 置換キーワード。
		*/
		public static string KEYWORD_ITEMNAME = "<<itemname>>";


		/** ＥＮＵＭコマンド。
		*/
		public static string ENUMCOMMAND_NAMESPACE = "<namespace>";

		/** ＥＮＵＭコマンド。
		*/
		public static string ENUMCOMMAND_ENUMNAME = "<enumname>";

		/** ＥＮＵＭコマンド。
		*/
		public static string ENUMCOMMAND_ITEM = "<item>";



		/** コンバート。

			a_param			: パラメータ。
			a_assets_path	: アセットフォルダからの相対パス。
			a_sheet			: ＥＮＵＭシート。

		*/
		public static void Convert(string a_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet,Fee.JsonSheet.ConvertParam a_convertparam)
		{
			try{
				if(a_sheet != null){
					string t_text = Convert_EnumSheet.TEMPLATE_MAIN;
					bool t_exist_namespace = false;
					bool t_exist_enumname = false;

					for(int ii=0;ii<a_sheet.Length;ii++){
						if(a_sheet[ii] != null){
							System.Collections.Generic.List<ListItem> t_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ListItem>>(a_sheet[ii]);
							if(t_sheet != null){
								for(int jj=0;jj<t_sheet.Count;jj++){

									if(Convert_EnumSheet.ENUMCOMMAND_NAMESPACE == t_sheet[jj].enum_command){
										//<namespace>

										t_text = t_text.Replace(Convert_EnumSheet.KEYWORD_NAMESPACE,t_sheet[jj].enum_name);
										t_exist_namespace = true;
									}else if(Convert_EnumSheet.ENUMCOMMAND_ENUMNAME == t_sheet[jj].enum_command){
										//<enumname>

										t_text = t_text.Replace(Convert_EnumSheet.KEYWORD_ENUMNAME,t_sheet[jj].enum_name);
										t_text = t_text.Replace(Convert_EnumSheet.KEYWORD_ENUMCOMMENT,t_sheet[jj].enum_comment);
										t_exist_enumname = true;
									}else if(Convert_EnumSheet.ENUMCOMMAND_ITEM == t_sheet[jj].enum_command){
										//<item>

										string t_text_item = Convert_EnumSheet.TEMPLATE_ITEM;
										{
											t_text_item = t_text_item.Replace(Convert_EnumSheet.KEYWORD_ITEMCOMMENT,t_sheet[jj].enum_comment);
											t_text_item = t_text_item.Replace(Convert_EnumSheet.KEYWORD_ITEMNAME,t_sheet[jj].enum_name);
										}

										t_text = t_text.Replace(Convert_EnumSheet.KEYWORD_ITEMROOT,t_text_item + Convert_EnumSheet.KEYWORD_ITEMROOT);
									}else{
										//無関係。複合シート。
									}
								}
							}else{
								Tool.Assert(false);
							}
						}
					}

					//<<itemroot>>の置換。
					t_text = t_text.Replace(Convert_EnumSheet.KEYWORD_ITEMROOT,"");

					//ネームスペースの指定がない。
					if(t_exist_namespace == false){
						Tool.Assert(false);
						t_text = t_text.Replace(Convert_EnumSheet.KEYWORD_NAMESPACE,"NameSpace");
					}

					//ＥＮＵＭ名の指定がない。
					if(t_exist_enumname == false){
						Tool.Assert(false);
						t_text = t_text.Replace(Convert_EnumSheet.KEYWORD_ENUMNAME,"EnumName");
						t_text = t_text.Replace(Convert_EnumSheet.KEYWORD_ENUMCOMMENT,"NoComment");
					}

					Fee.EditorTool.Utility.WriteTextFile(a_assets_path,t_text,true);
				}else{
					Tool.Assert(false);
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}
	}
	#endif
}

