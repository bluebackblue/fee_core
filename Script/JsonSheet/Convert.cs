

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
							"convert_command" : <enum>,							//ＥＮＵＭシートを連結出力。
							"convert_output" : "StreamingAssets/***.cs",		//出力先。
							"convert_sheet_0" : "jjjj1",						//連結するＪＳＯＮシート名。
							"convert_sheet_1" : "jjjj2",						//連結するＪＳＯＮシート名。
							"convert_sheet_2" : "",								//連結するＪＳＯＮシート名。
							"convert_sheet_3" : "",								//連結するＪＳＯＮシート名。
						},
						{
							"convert_command" : <json>,							//ＪＳＯＮシートを連結出力。
							"convert_output" : "StreamingAssets/***.json",		//出力先。
							"convert_sheet_0" : "jjjj1",						//連結するＪＳＯＮシート名。
							"convert_sheet_1" : "jjjj2",						//連結するＪＳＯＮシート名。
							"convert_sheet_2" : "",								//連結するＪＳＯＮシート名。
							"convert_sheet_3" : "",								//連結するＪＳＯＮシート名。
						},

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
						"enum_command" : "<namespace>"		//ネームスペース名の設定。
						"enum_name" : "Xxx.Yyy"				//ネームスペース。
						"enum_comment" : ""					//未使用。
					],
					[
						"enum_command" : "<enumname>"		//ＥＮＵＭ型名の設定。
						"enum_name" : "EnumType"			//ＥＮＵＭ型名。
						"enum_comment" : "コメント"			//コメント。
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
						"se_command" : "<item>"				//ＳＥ要素の設定。
						"se_file" : "xxxx.mp3"				//ＳＥファイルパス。
						"se_volume" :						//ＳＥボリューム。
					]

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

				Fee.JsonItem.JsonItem[] t_jsonitem_list = new Fee.JsonItem.JsonItem[4];

				if(string.IsNullOrEmpty(t_list_convert[ii].convert_sheet_0) == false){
					if(a_jsonitem.IsExistItem(t_list_convert[ii].convert_sheet_0,Fee.JsonItem.ValueType.IndexArray) == true){
						t_jsonitem_list[0] = a_jsonitem.GetItem(t_list_convert[ii].convert_sheet_0);
					}
				}

				if(string.IsNullOrEmpty(t_list_convert[ii].convert_sheet_1) == false){
					if(a_jsonitem.IsExistItem(t_list_convert[ii].convert_sheet_1,Fee.JsonItem.ValueType.IndexArray) == true){
						t_jsonitem_list[1] = a_jsonitem.GetItem(t_list_convert[ii].convert_sheet_1);
					}
				}

				if(string.IsNullOrEmpty(t_list_convert[ii].convert_sheet_2) == false){
					if(a_jsonitem.IsExistItem(t_list_convert[ii].convert_sheet_2,Fee.JsonItem.ValueType.IndexArray) == true){
						t_jsonitem_list[2] = a_jsonitem.GetItem(t_list_convert[ii].convert_sheet_2);
					}
				}

				if(string.IsNullOrEmpty(t_list_convert[ii].convert_sheet_3) == false){
					if(a_jsonitem.IsExistItem(t_list_convert[ii].convert_sheet_3,Fee.JsonItem.ValueType.IndexArray) == true){
						t_jsonitem_list[3] = a_jsonitem.GetItem(t_list_convert[ii].convert_sheet_3);
					}
				}

				if(t_list_convert[ii].convert_command == Config.CONVERTSHEET_COMMAND_JSON){
					//ＪＳＯＮシートを連結出力。
					Convert.Convert_Write_JsonSheet(new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list);
				}else if(t_list_convert[ii].convert_command == Config.CONVERTSHEET_COMMAND_ENUM){
					//ＥＮＵＭシートを連結出力。
					Convert.Convert_Write_EnumSheet(new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list);
				}else if(t_list_convert[ii].convert_command == Config.CONVERTSHEET_COMMAND_SE){
					//ＳＥシートを連結出力。
					Convert.Convert_Write_SeSheet(new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list);
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

			Fee.EditorTool.Utility.WriteTextFile(UnityEngine.Application.dataPath + "/" + a_path.GetPath(),t_jsonitem.ConvertJsonString());
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

						if(Config.ENUMSHEET_COMMAND_NAMESPACE == t_enum_sheet[jj].enum_command){
							//<namespace>

							//<<namespace>>の置換。
							t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_NAMESPACE,t_enum_sheet[jj].enum_name);
						}else if(Config.ENUMSHEET_COMMAND_ENUMNAME == t_enum_sheet[jj].enum_command){
							//<enumname>

							//<<enumname>>の置換。
							t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_ENUMNAME,t_enum_sheet[jj].enum_name);

							//<<enumcomment>>の置換。
							t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_ENUMCOMMENT,t_enum_sheet[jj].enum_comment);

						}else if(Config.ENUMSHEET_COMMAND_ITEM == t_enum_sheet[jj].enum_command){
							//<item>

							string t_text_item = Config.ENUMCONVERT_TEMPLATE_ITEM;
							{
								//<<itemcomment>>
								t_text_item = t_text_item.Replace(Config.ENUMCONVERT_KEYWORD_ITEMCOMMENT,t_enum_sheet[jj].enum_comment);

								//<<itemname>>
								t_text_item = t_text_item.Replace(Config.ENUMCONVERT_KEYWORD_ITEMNAME,t_enum_sheet[jj].enum_name);
							}

							//<<itemroot>>の置換。<<itemroot>>の追記。
							t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_ITEMROOT,t_text_item + Config.ENUMCONVERT_KEYWORD_ITEMROOT);
						}else{
							//無関係。
						}
					}
				}
			}

			//<<itemroot>>の置換。
			t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_ITEMROOT,"");

			Fee.EditorTool.Utility.WriteTextFile(UnityEngine.Application.dataPath + "/" + a_path.GetPath(),t_text);
		}

		/** ＳＥシートを連結出力。
		*/
		public static void Convert_Write_SeSheet(Fee.File.Path a_path,Fee.JsonItem.JsonItem[] a_json)
		{
			string t_prefab_name = null;
			System.Collections.Generic.List<System.Tuple<string,float>> t_list = new System.Collections.Generic.List<System.Tuple<string,float>>();

			for(int ii=0;ii<a_json.Length;ii++){
				if(a_json[ii] != null){
					System.Collections.Generic.List<SeSheet_ListItem> t_enum_sheet = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<SeSheet_ListItem>>(a_json[ii]);
					for(int jj=0;jj<t_enum_sheet.Count;jj++){

						if(Config.SESHEET_COMMAND_NAME == t_enum_sheet[jj].se_command){
							//<sename>

							t_prefab_name = t_enum_sheet[jj].se_name;

						}else if(Config.SESHEET_COMMAND_ITEM == t_enum_sheet[jj].se_command){
							//<item>

							t_list.Add(new System.Tuple<string,float>(
								 t_enum_sheet[jj].se_file,
								  t_enum_sheet[jj].se_volume
							));

						}else{
							//無関係。
						}
					}
				}
			}

			//保存。
			{
				UnityEngine.GameObject t_prefab = new UnityEngine.GameObject();
				Fee.Audio.Pack_AudioClip t_pack = t_prefab.AddComponent<Fee.Audio.Pack_AudioClip>();

				{
					for(int ii=0;ii<t_list.Count;ii++){
						UnityEngine.AudioClip t_audio_cilp = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.AudioClip>("Assets/" + t_list[ii].Item2);
						float t_volume = t_list[ii].Item2;

						t_pack.audioclip_list.Add(t_audio_cilp);
						t_pack.volume_list.Add(t_volume);
					}
				}

				{
					UnityEditor.PrefabUtility.SaveAsPrefabAsset(t_prefab,"Assets/" + a_path.GetPath(),out bool t_ret);
					Tool.Assert(t_ret);
				}

				UnityEngine.GameObject.DestroyImmediate(t_prefab);
			}

			/*
			//<<itemroot>>の置換。
			t_text = t_text.Replace(Config.ENUMCONVERT_KEYWORD_ITEMROOT,"");

			Fee.EditorTool.Utility.WriteTextFile(a_path.GetPath(),t_text);
			*/
		}
	}
	#endif
}

