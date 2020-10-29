

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。ＥＮＵＭ。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** ConvertSheet_Enum
	*/
	#if(UNITY_EDITOR)
	public class ConvertSheet_Enum
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
		private const string TEMPLATE_MAIN = 
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
		private const string TEMPLATE_ITEM = 
@"		/** <<itemcomment>>
		*/
		<<itemname>>,

";

		/** 置換キーワード。
		*/
		private const string KEYWORD_NAMESPACE = "<<namespace>>";

		/** 置換キーワード。
		*/
		private const string KEYWORD_ENUMCOMMENT = "<<enumcomment>>";

		/** 置換キーワード。
		*/
		private const string KEYWORD_ENUMNAME = "<<enumname>>";

		/** 置換キーワード。
		*/
		private const string KEYWORD_ITEMROOT = "<<itemroot>>";

		/** 置換キーワード。
		*/
		private const string KEYWORD_ITEMCOMMENT = "<<itemcomment>>";

		/** 置換キーワード。
		*/
		private const string KEYWORD_ITEMNAME = "<<itemname>>";


		/** コマンド。
		*/
		private const string COMMAND_NAMESPACE = "<namespace>";

		/** コマンド。
		*/
		private const string COMMAND_ENUMNAME = "<enumname>";

		/** コマンド。
		*/
		private const string COMMAND_ITEM = "<item>";



		/** コンバート。
		*/
		public static void Convert(string a_convert_param,Fee.File.Path a_assets_path,Fee.JsonItem.JsonItem[] a_sheet,Fee.JsonSheet.ConvertParam a_convertparam)
		{
			try{
				if(a_sheet != null){
					string t_text = ConvertSheet_Enum.TEMPLATE_MAIN;
					bool t_exist_namespace = false;
					bool t_exist_enumname = false;

					for(int ii=0;ii<a_sheet.Length;ii++){
						if(a_sheet[ii] != null){
							System.Collections.Generic.List<ListItem> t_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ListItem>>(a_sheet[ii]);
							if(t_sheet != null){
								for(int jj=0;jj<t_sheet.Count;jj++){

									if(ConvertSheet_Enum.COMMAND_NAMESPACE == t_sheet[jj].enum_command){
										//<namespace>

										t_text = t_text.Replace(ConvertSheet_Enum.KEYWORD_NAMESPACE,t_sheet[jj].enum_name);
										t_exist_namespace = true;
									}else if(ConvertSheet_Enum.COMMAND_ENUMNAME == t_sheet[jj].enum_command){
										//<enumname>

										t_text = t_text.Replace(ConvertSheet_Enum.KEYWORD_ENUMNAME,t_sheet[jj].enum_name);
										t_text = t_text.Replace(ConvertSheet_Enum.KEYWORD_ENUMCOMMENT,t_sheet[jj].enum_comment);
										t_exist_enumname = true;
									}else if(ConvertSheet_Enum.COMMAND_ITEM == t_sheet[jj].enum_command){
										//<item>

										string t_text_item = ConvertSheet_Enum.TEMPLATE_ITEM;
										{
											t_text_item = t_text_item.Replace(ConvertSheet_Enum.KEYWORD_ITEMCOMMENT,t_sheet[jj].enum_comment);
											t_text_item = t_text_item.Replace(ConvertSheet_Enum.KEYWORD_ITEMNAME,t_sheet[jj].enum_name);
										}

										t_text = t_text.Replace(ConvertSheet_Enum.KEYWORD_ITEMROOT,t_text_item + ConvertSheet_Enum.KEYWORD_ITEMROOT);
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
					t_text = t_text.Replace(ConvertSheet_Enum.KEYWORD_ITEMROOT,"");

					//ネームスペースの指定がない。
					if(t_exist_namespace == false){
						Tool.Assert(false);
						t_text = t_text.Replace(ConvertSheet_Enum.KEYWORD_NAMESPACE,"NameSpace");
					}

					//ＥＮＵＭ名の指定がない。
					if(t_exist_enumname == false){
						Tool.Assert(false);
						t_text = t_text.Replace(ConvertSheet_Enum.KEYWORD_ENUMNAME,"EnumName");
						t_text = t_text.Replace(ConvertSheet_Enum.KEYWORD_ENUMCOMMENT,"NoComment");
					}

					Fee.EditorTool.AssetTool.WriteTextFile(a_assets_path,t_text);
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

