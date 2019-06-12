

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＪＳＯＮシート。ツール。
*/


//Unreachable code detected.
#pragma warning disable 0162


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** Convert
	*/
	#if(UNITY_EDITOR)
	public class Convert
	{
		/** コンバート

			コンバートシート
				コンバート方法を記述。
				ConvertSheet_ListItemのインデックスリストＪＳＯＮ。
				コマンド
					<enum> : ＥＮＵＭシートを連結出力。
					<json> : ＪＳＯＮシートを連結出力。

			ＥＮＵＭシート
				ＥＮＵＭの定義。
				EnumSheet_ListItemのインデックスリストＪＳＯＮ。

			ＪＳＯＮシート
				中身はいろいろインデックスリストＪＳＯＮ。

			a_jsonitem == {

				//コンバートシート。
				"convert" : {
					[
						{
							"command" : <enum>,							//ＥＮＵＭシートを連結出力。
							"output" : "StreamingAssets/***.cs",		//出力先。
							"key_0" : "jjjj1",							//連結するＪＳＯＮシート名。
							"key_1" : "jjjj2",							//連結するＪＳＯＮシート名。
							"key_2" : "",								//連結するＪＳＯＮシート名。
							"key_3" : "",								//連結するＪＳＯＮシート名。
						},
						{
							"command" : <json>,							//ＪＳＯＮシートを連結出力。
							"output" : "StreamingAssets/***.json",		//出力先。
							"key_0" : "jjjj1",							//連結するＪＳＯＮシート名。
							"key_1" : "jjjj2",							//連結するＪＳＯＮシート名。
							"key_2" : "",								//連結するＪＳＯＮシート名。
							"key_3" : "",								//連結するＪＳＯＮシート名。
						},

						...略。
					]

				}
			
				//ＥＮＵＭシート。
				"eeee1" : {
					[
						"command" : "<namespace>"						//ネームスペース名の設定。
						"text" : "Xxx.Yyy"								//ネームスペース。
						"comment" : ""									//未使用。
					],
					[
						"command" : "<enumname>"						//ＥＮＵＭ型名の設定。
						"text" : "EnumType"								//ＥＮＵＭ型名。
						"comment" : "コメント"							//コメント。
					],
					[
						"command" : "<item>"							//ＥＮＵＭ要素名の設定。
						"text" : "EnumItemType1"						//ＥＮＵＭ要素名。
						"comment" : ""									//コメント。
					]
					[
						"command" : "<item>"							//ＥＮＵＭ要素名の設定。
						"text" : "EnumItemType2"						//ＥＮＵＭ要素名。
						"comment" : ""									//コメント。
					]

					...略。
				}

				//ＪＳＯＮシート。
				"jjjj1" : {
					"xxx" : ...略。
					"yyy" : ...略。

					...略。
				}


				...略
			}
		*/
		public static bool ConvertFromJsonSheet(Fee.JsonItem.JsonItem a_jsonitem)
		{
			if(a_jsonitem == null){
				Tool.Assert(false);
				return false;
			}

			if(a_jsonitem.IsAssociativeArray() == false){
				Tool.Assert(false);
				return false;
			}

			if(a_jsonitem.IsExistItem(Config.SHEETNAME_CONVERT, Fee.JsonItem.ValueType.IndexArray) == false){
				Tool.Assert(false);
				return false;
			}

			Fee.JsonItem.JsonItem t_jsonitem_convertsheet = a_jsonitem.GetItem(Config.SHEETNAME_CONVERT);
			if(t_jsonitem_convertsheet == null){
				Tool.Assert(false);
				return false;
			}

			System.Collections.Generic.List<ConvertSheet_ListItem> t_list_convert = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ConvertSheet_ListItem>>(t_jsonitem_convertsheet);
			if(t_list_convert == null){
				return false;
			}

			for(int ii=0;ii<t_list_convert.Count;ii++){
				if(t_list_convert[ii].command == Config.CONVERTSHEET_COMMAND_JSON){
					//ＪＳＯＮシートを連結出力。

					Fee.JsonItem.JsonItem[] t_jsonitem_list = new Fee.JsonItem.JsonItem[4];

					if(string.IsNullOrEmpty(t_list_convert[ii].key_0) == false){
						if(a_jsonitem.IsExistItem(t_list_convert[ii].key_0,Fee.JsonItem.ValueType.IndexArray) == true){
							t_jsonitem_list[0] = a_jsonitem.GetItem(t_list_convert[ii].key_0);
						}
					}

					if(string.IsNullOrEmpty(t_list_convert[ii].key_1) == false){
						if(a_jsonitem.IsExistItem(t_list_convert[ii].key_1,Fee.JsonItem.ValueType.IndexArray) == true){
							t_jsonitem_list[1] = a_jsonitem.GetItem(t_list_convert[ii].key_1);
						}
					}

					if(string.IsNullOrEmpty(t_list_convert[ii].key_2) == false){
						if(a_jsonitem.IsExistItem(t_list_convert[ii].key_2,Fee.JsonItem.ValueType.IndexArray) == true){
							t_jsonitem_list[2] = a_jsonitem.GetItem(t_list_convert[ii].key_2);
						}
					}

					if(string.IsNullOrEmpty(t_list_convert[ii].key_3) == false){
						if(a_jsonitem.IsExistItem(t_list_convert[ii].key_3,Fee.JsonItem.ValueType.IndexArray) == true){
							t_jsonitem_list[3] = a_jsonitem.GetItem(t_list_convert[ii].key_3);
						}
					}
					
					Convert.Convert_Write_JsonSheet(new Fee.File.Path(UnityEngine.Application.dataPath + "/" + t_list_convert[ii].output),t_jsonitem_list);
				}else if(t_list_convert[ii].command == Config.CONVERTSHEET_COMMAND_ENUM){
					//ＥＮＵＭシートを連結出力。

					Fee.JsonItem.JsonItem[] t_jsonitem_list = new Fee.JsonItem.JsonItem[4];

					if(string.IsNullOrEmpty(t_list_convert[ii].key_0) == false){
						if(a_jsonitem.IsExistItem(t_list_convert[ii].key_0,Fee.JsonItem.ValueType.IndexArray) == true){
							t_jsonitem_list[0] = a_jsonitem.GetItem(t_list_convert[ii].key_0);
						}
					}

					if(string.IsNullOrEmpty(t_list_convert[ii].key_1) == false){
						if(a_jsonitem.IsExistItem(t_list_convert[ii].key_1,Fee.JsonItem.ValueType.IndexArray) == true){
							t_jsonitem_list[1] = a_jsonitem.GetItem(t_list_convert[ii].key_1);
						}
					}

					if(string.IsNullOrEmpty(t_list_convert[ii].key_2) == false){
						if(a_jsonitem.IsExistItem(t_list_convert[ii].key_2,Fee.JsonItem.ValueType.IndexArray) == true){
							t_jsonitem_list[2] = a_jsonitem.GetItem(t_list_convert[ii].key_2);
						}
					}

					if(string.IsNullOrEmpty(t_list_convert[ii].key_3) == false){
						if(a_jsonitem.IsExistItem(t_list_convert[ii].key_3,Fee.JsonItem.ValueType.IndexArray) == true){
							t_jsonitem_list[3] = a_jsonitem.GetItem(t_list_convert[ii].key_3);
						}
					}

					Convert.Convert_Write_EnumSheet(new Fee.File.Path(UnityEngine.Application.dataPath + "/" + t_list_convert[ii].output),t_jsonitem_list);
				}
			}

			return true;
		}

		/** ＪＳＯＮシートを連結出力。

			a_output : 「Assets/**.json」

		*/
		public static void Convert_Write_JsonSheet(Fee.File.Path a_path,Fee.JsonItem.JsonItem[] a_json)
		{
			Fee.JsonItem.JsonItem t_jsonitem = new Fee.JsonItem.JsonItem(new Fee.JsonItem.Value_IndexArray());

			for(int ii=0;ii<a_json.Length;ii++){
				if(a_json[ii] != null){
					int jj_max = a_json[ii].GetListMax();
					for(int jj=0;jj<jj_max;jj++){
						t_jsonitem.AddItem(a_json[ii].GetItem(jj),false);
					}
				}
			}

			Fee.EditorTool.Utility.WriteTextFile(a_path.GetPath(),t_jsonitem.ConvertJsonString());
		}

		/** ＥＮＵＭシートを連結出力。
		*/
		public static void Convert_Write_EnumSheet(Fee.File.Path a_path,Fee.JsonItem.JsonItem[] a_json)
		{
			string t_text = Config.ENUMCONVERT_TEMPLATE_MAIN;

			for(int ii=0;ii<a_json.Length;ii++){
				if(a_json[ii] != null){
					System.Collections.Generic.List<EnumSheet_ListItem> t_enum_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<EnumSheet_ListItem>>(a_json[ii]);
					for(int jj=0;jj<t_enum_sheet.Count;jj++){

						if(Config.ENUMSHEET_COMMAND_NAMESPACE == t_enum_sheet[jj].command){
							//<namespace>

							//<<namespace>>の置換。
							t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_NAMESPACE,t_enum_sheet[jj].text);
						}else if(Config.ENUMSHEET_COMMAND_ENUMNAME == t_enum_sheet[jj].command){
							//<enumname>

							//<<enumname>>の置換。
							t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_ENUMNAME,t_enum_sheet[jj].text);

							//<<enumcomment>>の置換。
							t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_ENUMCOMMENT,t_enum_sheet[jj].comment);

						}else if(Config.ENUMSHEET_COMMAND_ITEM == t_enum_sheet[jj].command){
							//<item>

							string t_text_item = Config.ENUMCONVERT_TEMPLATE_ITEM;
							{
								//<<itemcomment>>
								t_text_item = t_text_item.Replace(Config.ENUMCONVERT_KEYWORD_ITEMCOMMENT,t_enum_sheet[jj].comment);

								//<<itemname>>
								t_text_item = t_text_item.Replace(Config.ENUMCONVERT_KEYWORD_ITEMNAME,t_enum_sheet[jj].text);
							}

							//<<itemroot>>の置換。<<itemroot>>の追記。
							t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_ITEMROOT,t_text_item + Config.ENUMCONVERT_KEYWORD_ITEMROOT);
						}else{
							//不明。
							Tool.Assert(false);
						}
					}
				}
			}

			//<<itemroot>>の置換。
			t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_ITEMROOT,"");

			Fee.EditorTool.Utility.WriteTextFile(a_path.GetPath(),t_text);
		}
	}
	#endif
}

