

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
			System.Collections.Generic.List<ConvertSheet_ListItem> t_list_convert = Fee.JsonItem.Convert.JsonItemToObject<System.Collections.Generic.List<ConvertSheet_ListItem>>(t_jsonitem_convertsheet);
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
				case Convert_JsonSheet.COMMAND:
					{
						//ＪＳＯＮシート。
						Convert_JsonSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case Convert_EnumSheet.COMMAND:
					{
						//ＥＮＵＭシート。
						Convert_EnumSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case Convert_AudioSheet.COMMAND:
					{
						//オーディオシート。
						Convert_AudioSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case Convert_DataSheet.COMMAND:
					{
						//データシート。
						Convert_DataSheet.Convert(t_list_convert[ii].convert_param,new Fee.File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case Convert_TextureSheet.COMMAND:
					{
						//テクスチャーシート。
						Convert_TextureSheet.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case Convert_FontSheet.COMMAND:
					{
						//フォントシート。
						Convert_FontSheet.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case Convert_TextAssetSheet.COMMAND:
					{
						//テキストアセットシート。
						Convert_TextAssetSheet.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case Convert_Videoheet.COMMAND:
					{
						//ムービーシート。
						Convert_Videoheet.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case Convert_PrefabSheet.COMMAND:
					{
						//プレハブシート。
						Convert_PrefabSheet.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				case Convert_AnimationSheet.COMMAND:
					{
						//アニメーションシート。
						Convert_AnimationSheet.Convert(t_list_convert[ii].convert_param,new File.Path(t_list_convert[ii].convert_output),t_jsonitem_list,a_convertparam);
					}break;
				default:
					{
						Tool.Assert(false);
					}break;
				}
			}

			return true;
		}
	}
	#endif
}

