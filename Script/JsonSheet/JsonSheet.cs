

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** JsonSheet
	*/
	#if(UNITY_EDITOR)
	public class JsonSheet
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
							"convert_command" : <json>,							//ＪＳＯＮシートを連結出力。
							"convert_output" : "StreamingAssets/***.json",		//出力先。
							"convert_sheet_0" : "jjjj1",						//連結するＪＳＯＮシート名。
							"convert_sheet_1" : "jjjj2",						//連結するＪＳＯＮシート名。
							"convert_sheet_2" : "",								//連結するＪＳＯＮシート名。
							"convert_sheet_3" : "",								//連結するＪＳＯＮシート名。
						},
						{
							"convert_command" : <enum>,							//ＥＮＵＭシートを連結出力。
							"convert_output" : "StreamingAssets/***.cs",		//出力先。
							"convert_sheet_0" : "jjjj1",						//連結するＪＳＯＮシート名。
							"convert_sheet_1" : "jjjj2",						//連結するＪＳＯＮシート名。
							"convert_sheet_2" : "",								//連結するＪＳＯＮシート名。
							"convert_sheet_3" : "",								//連結するＪＳＯＮシート名。
						},
						{
							"convert_command" : <audio>,						//オーディオシートを連結出力。
							...略。
						},
						{
							"convert_command" : <editordata>,					//エディターデータシートを連結出力。
							...略。
						}.
						{
							"convert_command" : <releasedata>,					//リリースデータシートを連結出力。
							...略。
						}.

						...略。
					]

				}

				//ＪＳＯＮシート。
				"jjjj1" : {
					"xxx" : ...略。
					"yyy" : ...略。

					...略。
				}
			
				//ＥＮＵＭシート。
				"eeee1" : {
					[
						"enum_command" : "<namespace>",		//ネームスペース名の設定。
						"enum_name" : "Xxx.Yyy",			//ネームスペース。
						"enum_comment" : "",				//未使用。
					],
					[
						"enum_command" : "<enumname>",		//ＥＮＵＭ型名の設定。
						"enum_name" : "EnumType",			//ＥＮＵＭ型名。
						"enum_comment" : "コメント",		//コメント。
					],
					[
						"enum_command" : "<item>"			//ＥＮＵＭ要素名の設定。
						"enum_name" : "EnumItemType1"		//ＥＮＵＭ要素名。
						"enum_comment" : ""					//コメント。
					]

					...略。
				}

				//オーディオシート。
				"sssss1" : {
					[
						"audio_command" : "<item>",		//オーディオ要素の設定。
						"audio_file" : "xxxx.mp3",		//オーディオファイルパス。
						"audio_volume" : 1.0,			//オーディオボリューム。
					]

					...略。
				}

				//エディターデータシート。
				"ddddd1" : {
					[
						"data_command" : "<resources_prefab>"			//リソース。プレハブ。
						"data_id" : "xxxx"								//データＩＤ。
						"data_path" : "xxx/xxx/xxx"						//リソースパス。
					],
					[
						"data_command" : "<streamingassets_texture>"	//ストリーミングアセット。テクスチャ。
						"data_id" : "xxxx"								//データＩＤ。
						"data_path" : "xxx/xxx/xxx.png"					//リソースパス。
					],
					[
						"data_command" : "<url_text>"					//ＵＲＬ。テキスト。
						"data_id" : "xxxx"								//データＩＤ。
						"data_path" : "https://xxx.xx/text.txt"			//リソースパス。
					],

					...略。
				}

				//データシート。
				"ddddd1" : {
					[
						"data_command" : "<resources_prefab>"			//リソース。プレハブ。
						"data_id" : "xxxx"								//データＩＤ。
						"data_path" : "xxx/xxx/xxx"						//リソースパス。
						"data_packname" : ""							//パック名。
					],
					[
						"data_command" : "<streamingassets_texture>"	//ストリーミングアセット。テクスチャ。
						"data_id" : "xxxx"								//データＩＤ。
						"data_path" : "xxx/xxx/xxx.png"					//リソースパス。
						"data_packname" : ""							//パック名。
					],
					[
						"data_command" : "<url_text>"					//ＵＲＬ。テキスト。
						"data_id" : "xxxx"								//データＩＤ。
						"data_path" : "https://xxx.xx/text.txt"			//リソースパス。
						"data_packname" : ""							//パック名。
					],

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

				Fee.JsonItem.JsonItem[] t_jsonitem_list = new Fee.JsonItem.JsonItem[4]{null,null,null,null};

				if(string.IsNullOrEmpty(t_list_convert[ii].convert_sheet_0) == false){
					if(a_jsonitem.IsExistItem(t_list_convert[ii].convert_sheet_0,Fee.JsonItem.ValueType.IndexArray) == true){
						t_jsonitem_list[0] = a_jsonitem.GetItem(t_list_convert[ii].convert_sheet_0);
					}else{
						Tool.Assert(false);
					}
				}

				if(string.IsNullOrEmpty(t_list_convert[ii].convert_sheet_1) == false){
					if(a_jsonitem.IsExistItem(t_list_convert[ii].convert_sheet_1,Fee.JsonItem.ValueType.IndexArray) == true){
						t_jsonitem_list[1] = a_jsonitem.GetItem(t_list_convert[ii].convert_sheet_1);
					}else{
						Tool.Assert(false);
					}
				}

				if(string.IsNullOrEmpty(t_list_convert[ii].convert_sheet_2) == false){
					if(a_jsonitem.IsExistItem(t_list_convert[ii].convert_sheet_2,Fee.JsonItem.ValueType.IndexArray) == true){
						t_jsonitem_list[2] = a_jsonitem.GetItem(t_list_convert[ii].convert_sheet_2);
					}else{
						Tool.Assert(false);
					}
				}

				if(string.IsNullOrEmpty(t_list_convert[ii].convert_sheet_3) == false){
					if(a_jsonitem.IsExistItem(t_list_convert[ii].convert_sheet_3,Fee.JsonItem.ValueType.IndexArray) == true){
						t_jsonitem_list[3] = a_jsonitem.GetItem(t_list_convert[ii].convert_sheet_3);
					}else{
						Tool.Assert(false);
					}
				}

				if(t_list_convert[ii].convert_command == Convert_JsonSheet.COMMAND){
					//ＪＳＯＮシート。
					Convert_JsonSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list);
				}else if(t_list_convert[ii].convert_command == Convert_EnumSheet.COMMAND){
					//ＥＮＵＭシート。
					Convert_EnumSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list);
				}else if(t_list_convert[ii].convert_command == Convert_AudioSheet.COMMAND){
					//オーディオシート。
					Convert_AudioSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list);
				}else if(t_list_convert[ii].convert_command == Convert_DataSheet.COMMAND){
					//データシート。
					Convert_DataSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list);
				}
			}

			return true;
		}
	}
	#endif
}

