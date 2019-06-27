

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
							"convert_command" : <se>,							//ＳＥシートを連結出力。
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

				//ＳＥシート。
				"sssss1" : {
					[
						"se_command" : "<item>",				//ＳＥ要素の設定。
						"se_file" : "xxxx.mp3",					//ＳＥファイルパス。
						"se_volume" : 1.0,						//ＳＥボリューム。
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
				}else if(t_list_convert[ii].convert_command == Convert_SeSheet.COMMAND){
					//ＳＥシート。
					Convert_SeSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list);
				}else if(t_list_convert[ii].convert_command == Convert_DataSheet.COMMAND){
					//データシート。
					Convert_DataSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list);
				}
			}

			return true;
		}

		/** リリースデータシートを連続出力。
		*/
		/*
		public static void Convert_Write_ReleaseDataSheet(Fee.File.Path a_path,Fee.JsonItem.JsonItem[] a_json)
		{
			System.Collections.Generic.Dictionary<string,Data.JsonListItem> t_list = new System.Collections.Generic.Dictionary<string,Data.JsonListItem>();
			{
				for(int ii=0;ii<a_json.Length;ii++){
					if(a_json[ii] != null){
						System.Collections.Generic.List<ReleaseDataSheet_ListItem> t_enum_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ReleaseDataSheet_ListItem>>(a_json[ii]);
						for(int jj=0;jj<t_enum_sheet.Count;jj++){

							if(Config.DATASHEET_COMMAND_RESOURCES_PREFAB == t_enum_sheet[jj].data_command){
								//<resources_prefab>

								string t_id = t_enum_sheet[jj].data_id;
								Data.JsonListItem t_item = new Data.JsonListItem(Data.PathType.Resources_Prefab,t_enum_sheet[jj].data_path,t_enum_sheet[jj].data_packfilename);
								t_list.Add(t_id,t_item);

							}else if(Config.DATASHEET_COMMAND_RESOURCES_TEXTURE == t_enum_sheet[jj].data_command){
								//<resources_texture>

								string t_id = t_enum_sheet[jj].data_id;
								Data.JsonListItem t_item = new Data.JsonListItem(Data.PathType.Resources_Texture,t_enum_sheet[jj].data_path,t_enum_sheet[jj].data_packfilename);
								t_list.Add(t_id,t_item);

							}else if(Config.DATASHEET_COMMAND_STREAMINGASSETS_TEXTURE == t_enum_sheet[jj].data_command){
								//<streamingassets_texture>

								string t_id = t_enum_sheet[jj].data_id;
								Data.JsonListItem t_item = new Data.JsonListItem(Data.PathType.StreamingAssets_Texture,t_enum_sheet[jj].data_path,t_enum_sheet[jj].data_packfilename);
								t_list.Add(t_id,t_item);

							}else{
								//無関係。
							}
						}
					}
				}

				//ＪＳＯＮ。出力。
				{
					Fee.JsonItem.JsonItem t_jsonitem = Fee.JsonItem.Convert.ObjectToJsonItem(t_list);
					string t_jsonstring = t_jsonitem.ConvertJsonString();
					Fee.EditorTool.Utility.WriteTextFile(Fee.File.Path.CreateAssetsPath(a_path),t_jsonstring);
				}
			}

			//アセットバンドルリスト作成。
			System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<string,Data.JsonListItem>> t_assetbundlelist = new System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<string,Data.JsonListItem>>();
			{
				foreach(System.Collections.Generic.KeyValuePair<string,Data.JsonListItem> t_pair in t_list){
					if(string.IsNullOrEmpty(t_pair.Value.packname) == false){
						System.Collections.Generic.Dictionary<string,Data.JsonListItem> t_item_list = null;

						if(t_assetbundlelist.TryGetValue(t_pair.Value.packname,out t_item_list) == false){
							t_item_list = new System.Collections.Generic.Dictionary<string,Data.JsonListItem>();
							t_assetbundlelist.Add(t_pair.Value.packname,t_item_list);
						}

						t_item_list.Add(t_pair.Key,t_pair.Value);
					}
				}
			}

			{
				//t_assetbundle_build
				UnityEditor.AssetBundleBuild[] t_assetbundle_build = new UnityEditor.AssetBundleBuild[t_assetbundlelist.Count];
				{
					int t_count = 0;

					foreach(System.Collections.Generic.KeyValuePair<string,System.Collections.Generic.Dictionary<string,Data.JsonListItem>> t_pair in t_assetbundlelist){

						//パック名。
						t_assetbundle_build[t_count].assetBundleName = t_pair.Key;

						//assetBundleVariant
						t_assetbundle_build[t_count].assetBundleVariant = null;

						//key_list
						System.Collections.Generic.List<string> t_key_list = new System.Collections.Generic.List<string>(t_pair.Value.Keys);
						t_assetbundle_build[t_count].assetNames = new string[t_key_list.Count];
						t_assetbundle_build[t_count].addressableNames = new string[t_key_list.Count];

						for(int ii=0;ii<t_key_list.Count;ii++){
							if(t_pair.Value.TryGetValue(t_key_list[ii],out Data.JsonListItem t_listitem) == true){

								string t_asset_path = null;
								{
									UnityEngine.Object t_object = null;

									switch(t_listitem.path_type){
									case Data.PathType.Resources_Prefab:
									case Data.PathType.Resources_Texture:
									case Data.PathType.StreamingAssets_Texture:
										{
											t_object = UnityEngine.Resources.Load(t_listitem.path);
										}break;
									default:
										{
											Tool.Assert(false);
										}break;
									}

									if(t_object != null){
										t_asset_path = UnityEditor.AssetDatabase.GetAssetPath(t_object);
									}else{
										Tool.Assert(false);
									}
								}

								//assetNames
								t_assetbundle_build[t_count].assetNames[ii] = t_asset_path;

								//addressableNames
								t_assetbundle_build[t_count].addressableNames[ii] = t_key_list[ii];

							}else{
								Tool.Assert(false);
							}
						}

						t_count++;
					}
				}

				//outputpath
				string t_output_path = "StandaloneWindows/";

				//option
				UnityEditor.BuildAssetBundleOptions t_option = UnityEditor.BuildAssetBundleOptions.None;

				UnityEditor.BuildPipeline.BuildAssetBundles(t_output_path,t_assetbundle_build,t_option,UnityEditor.BuildTarget.StandaloneWindows);
			}
		}
		*/

			/*
			//アセットバンドル。作成。
			{

					

			}
			*/

			/* TODO:アセットバンドル作成。
			{
				//object
				UnityEditor.AssetBundleBuild[] t_object = new UnityEditor.AssetBundleBuild[1];
				{
					t_object[0].assetBundleName = "se.assetbundle";
					t_object[0].assetBundleVariant = null;
					t_object[0].assetNames = new string[1]{
						"Assets/Data/ConvertFromExcel/excel_to_se_prefab.prefab"
					};
				}

				//outputpath
				string t_output_path = "Assets/Data/AssetBundle";

				//option
				UnityEditor.BuildAssetBundleOptions t_option = UnityEditor.BuildAssetBundleOptions.None;

				UnityEditor.BuildPipeline.BuildAssetBundles(t_output_path,t_object,t_option,UnityEditor.BuildTarget.StandaloneWindows);
			}
			*/
	}
	#endif
}

