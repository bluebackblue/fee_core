

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
		*/
		public static bool ConvertFromJsonSheet(Fee.JsonItem.JsonItem a_jsonitem,Fee.JsonSheet.ConvertParam a_convertparam)
		{
			if(a_jsonitem == null){
				Tool.Assert(false);
				return false;
			}

			if(a_jsonitem.IsAssociativeArray() == false){
				Tool.Assert(false);
				return false;
			}

			//コンバードシート。確認。
			if(a_jsonitem.IsExistItem(Config.SHEETNAME_CONVERT,Fee.JsonItem.ValueType.IndexArray) == false){
				Tool.Assert(false);
				return false;
			}

			//コンバートシート。取得。
			Fee.JsonItem.JsonItem t_jsonitem_convertsheet = a_jsonitem.GetItem(Config.SHEETNAME_CONVERT);
			if(t_jsonitem_convertsheet == null){
				Tool.Assert(false);
				return false;
			}

			//コンバートシート。取得。
			System.Collections.Generic.List<ConvertListItem> t_list_convert = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ConvertListItem>>(t_jsonitem_convertsheet);
			if(t_list_convert == null){
				return false;
			}

			//処理。
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

				switch(t_list_convert[ii].convert_command){
				case ConvertSheet_Enum.COMMAND:
					{
						//ＥＮＵＭ。
						ConvertSheet_Enum.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case ConvertSheet_Json.COMMAND:
					{
						//ＪＳＯＮ。
						ConvertSheet_Json.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case ConvertSheet_Prefab.COMMAND:
					{
						//プレハブ。
						ConvertSheet_Prefab.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case ConvertSheet_Data.COMMAND:
					{
						//データ。
						ConvertSheet_Data.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;



				case ConvertSheet_AudioPrefab.COMMAND:
					{
						//オーディオプレハブ。
						ConvertSheet_AudioPrefab.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case ConvertSheet_MaterialPrefab.COMMAND:
					{
						//マテリアルプレハブ。
						ConvertSheet_MaterialPrefab.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case ConvertSheet_VideoPrefab.COMMAND:
					{
						//ビデオプレハブ。
						ConvertSheet_VideoPrefab.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case ConvertSheet_TexturePrefab.COMMAND:
					{
						//テクスチャプレハブ。
						ConvertSheet_TexturePrefab.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case ConvertSheet_FontPrefab.COMMAND:
					{
						//フォントプレハブ。
						ConvertSheet_FontPrefab.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case ConvertSheet_TextAssetPrefab.COMMAND:
					{
						//テキストアセットプレハブ。
						ConvertSheet_TextAssetPrefab.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case ConvertSheet_RAControllerPrefab.COMMAND:
					{
						//ランタイムアニメータコントローラプレハブ。
						ConvertSheet_RAControllerPrefab.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;



				case ConvertSheet_AnimatorController.COMMAND:
					{
						//アニメータコントローラ。
						ConvertSheet_AnimatorController.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;



				default:
					{
						Tool.LogError("ConvertFromJsonSheet",t_list_convert[ii].convert_command);
					}break;
				}
			}

			Fee.EditorTool.AssetTool.Refresh();

			return true;
		}
	}
	#endif
}

