

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief エクセル。エクセルＴＯＪＳＯＮ。
*/


/** Fee.Excel
*/
namespace Fee.Excel
{
	/** ExcelToJson
	*/
	public class ExcelToJson
	{
		/** excel
		*/
		private Excel excel;

		/** jsonitem
		*/
		private Fee.JsonItem.JsonItem jsonitem;

		/** constructor
		*/
		public ExcelToJson()
		{
			//excel
			this.excel = null;

			//jsonitem
			this.jsonitem = null;
		}

		/** コンバート。
		*/
		public bool Convert(File.Path a_path)
		{
			this.jsonitem = new JsonItem.JsonItem(new Fee.JsonItem.Value_AssociativeArray());

			{
				this.excel = new Excel();
				if(this.excel.ReadOpen(a_path) == true){
					int t_sheet_count = this.excel.GetSheetCount();
					for(int ii=0;ii<t_sheet_count;ii++){
						if(this.excel.SetActiveSheet(ii) == true){
							this.Convert_Sheet(ii);
						}
					}
					this.excel.Close();
				}else{
					//失敗。
					return false;
				}
			}

			return true;
		}

		/** ConvertJsonString
		*/
		public string ConvertJsonString()
		{
			return this.jsonitem.ConvertJsonString();
		}

		/** セルの文字列をチェック。
		*/
		private bool CellStringCheck(int a_x,int a_y,string a_text,ref CellPosition a_result_pos)
		{
			if(this.excel.SetActiveCell(a_x,a_y) == true){
				string t_value;
				this.excel.GetTryCellString(out t_value);
				if(t_value == a_text){
					a_result_pos = new CellPosition(a_x,a_y);
					return true;
				}
			}

			return false;
		}

		/** セルの文字列を取得。
		*/
		private string GetTryCellString(int a_x,int a_y)
		{
			if(this.excel.SetActiveCell(a_x,a_y) == true){
				if(this.excel.GetTryCellString(out string t_value) == true){
					return t_value;
				}
			}
			return null;
		}

		/** セルの数値を取得。
		*/
		private double GetTryCellNumeric(int a_x,int a_y)
		{
			if(this.excel.SetActiveCell(a_x,a_y) == true){
				if(this.excel.GetTryCellNumeric(out double t_value) == true){
					return t_value;
				}
			}
			return 0.0;
		}

		/** ボックス内を検索。
		*/
		private bool FindCellBox(int a_x,int a_y,int a_size,string a_text,ref CellPosition a_result_pos)
		{
			for(int yy=0;yy<a_size;yy++){
				for(int xx=0;xx<a_size;xx++){
					int t_x = a_x + xx;
					int t_y = a_y + yy;
					if(this.CellStringCheck(t_x,t_y,a_text,ref a_result_pos) == true){
						return true;
					}
				}
			}

			return false;
		}

		/** Ｘ軸方向に検索。
		*/
		private bool FindCellXLine(int a_x,int a_y,int a_size,string a_text,ref CellPosition a_result_pos)
		{
			for(int xx=0;xx<a_size;xx++){
				int t_x = a_x + xx;
				int t_y = a_y;
				if(this.CellStringCheck(t_x,t_y,a_text,ref a_result_pos) == true){
					return true;
				}
			}

			return false;
		}

		/** Ｙ軸方向に検索。
		*/
		private bool FindCellYLine(int a_x,int a_y,int a_size,string a_text,ref CellPosition a_result_pos)
		{
			for(int yy=0;yy<a_size;yy++){
				int t_x = a_x;
				int t_y = a_y + yy;
				if(this.CellStringCheck(t_x,t_y,a_text,ref a_result_pos) == true){
					return true;
				}
			}

			return false;
		}

		/** ルートを検索。
		*/
		private bool FindCell_Root(ref CellPosition a_result_pos)
		{
			int t_block_size = 16;
			for(int yy=0;yy<10;yy++){
				for(int xx=0;xx<10;xx++){
					if(this.FindCellBox(xx * t_block_size,yy * t_block_size,t_block_size,Config.COMMAND_PARAM_ROOT,ref a_result_pos) == true){
						return true;
					}
				}
			}

			return false;
		}

		/** パラメータタイプリスト。作成。
		*/
		private System.Collections.Generic.List<ParamListItem> CreateParamTypeList(int a_param_type_y,int a_param_name_y,int a_start_x,int a_end_x)
		{
			System.Collections.Generic.List<ParamListItem> t_list = new System.Collections.Generic.List<ParamListItem>();

			for(int xx = a_start_x;xx < a_end_x;xx++){
				string t_param_type = this.GetTryCellString(xx,a_param_type_y);
				string t_param_name = this.GetTryCellString(xx,a_param_name_y);

				switch(t_param_type){
				case Config.PARAMTYPE_STRING:
					{
						t_list.Add(new ParamListItem(ParamType.StringType,t_param_name,xx));
					}break;
				case Config.PARAMTYPE_INT:
					{
						t_list.Add(new ParamListItem(ParamType.IntType,t_param_name,xx));
					}break;
				case Config.PARAMTYPE_FLOAT:
					{
						t_list.Add(new ParamListItem(ParamType.FloatType,t_param_name,xx));
					}break;
				case Config.PARAMTYPE_COMMENT:
					{
						//スキップ。
					}break;
				default:
					{
						//不明なパラメータタイプ。
						Tool.Assert(false);
					}break;
				}
			}

			return t_list;
		}

		/** コンバート。シート。
		*/
		private void Convert_Sheet(int a_index)
		{
			/** pos
			*/
			CellPosition t_pos_root = new CellPosition(0,0);
			CellPosition t_pos_param_type = new CellPosition(0,0);
			CellPosition t_pos_param_name = new CellPosition(0,0);
			CellPosition t_pos_end_y = new CellPosition(0,0);
			CellPosition t_pos_end_x = new CellPosition(0,0);

			if(this.FindCell_Root(ref t_pos_root) == false){
				return;
			}

			//パラメータタイプ。検索。
			if(this.CellStringCheck(t_pos_root.x,t_pos_root.y + 1,Config.COMMAND_PARAM_TYPE,ref t_pos_param_type) == false){
				return;
			}

			//パラメータ名。検索。
			if(this.CellStringCheck(t_pos_root.x,t_pos_root.y + 2,Config.COMMAND_PARAM_NAME,ref t_pos_param_name) == false){
				return;
			}

			//Ｘ軸方向。終端検索。
			if(this.FindCellXLine(t_pos_param_type.x + 1,t_pos_param_type.y,Config.END_SEARCH_WIDTH,Config.COMMAND_PARAM_END,ref t_pos_end_x) == false){
				return;
			}

			//Ｙ軸方向。終端検索。
			if(this.FindCellYLine(t_pos_param_type.x,t_pos_param_type.y + 1,Config.END_SEARCH_HEIGHT,Config.COMMAND_PARAM_END,ref t_pos_end_y) == false){
				return;
			}

			//パラメータリスト。作成。
			System.Collections.Generic.List<ParamListItem> t_param_list = this.CreateParamTypeList(t_pos_param_type.y,t_pos_param_name.y,t_pos_param_type.x + 1,t_pos_end_x.x);

			{
				Fee.JsonItem.JsonItem t_jsonitem_list = new JsonItem.JsonItem(new Fee.JsonItem.Value_IndexArray());
				for(int yy=t_pos_param_name.y+1;yy<t_pos_end_y.y;yy++){
					string t_flag = this.GetTryCellString(t_pos_param_name.x,yy);
					if(t_flag == Config.COMMAND_ON){
						Fee.JsonItem.JsonItem t_jsonitem_item = new JsonItem.JsonItem(new Fee.JsonItem.Value_AssociativeArray());
						for(int ii=0;ii<t_param_list.Count;ii++){
							switch(t_param_list[ii].param_type){
							case ParamType.StringType:
								{
									//string

									string t_value = this.GetTryCellString(t_param_list[ii].pos_x,yy);
									t_jsonitem_item.AddItem(t_param_list[ii].param_name,new JsonItem.JsonItem(new  Fee.JsonItem.Value_StringData(t_value)),false);
								}break;
							case ParamType.IntType:
								{
									//int

									double t_value = this.GetTryCellNumeric(t_param_list[ii].pos_x,yy);
									t_jsonitem_item.AddItem(t_param_list[ii].param_name,new JsonItem.JsonItem(new  Fee.JsonItem.Value_Int((int)t_value)),false);
								}break;
							case ParamType.FloatType:
								{
									//float

									double t_value = this.GetTryCellNumeric(t_param_list[ii].pos_x,yy);
									t_jsonitem_item.AddItem(t_param_list[ii].param_name,new JsonItem.JsonItem(new  Fee.JsonItem.Value_Float((float)t_value)),false);
								}break;
							default:
								{
									Tool.Assert(false);
								}break;
							}
						}
						t_jsonitem_list.AddItem(t_jsonitem_item,false);
					}
				}
				string t_root_name = this.GetTryCellString(t_pos_root.x + 1,t_pos_root.y);
				this.jsonitem.AddItem(t_root_name,t_jsonitem_list,false);
			}
		}
	}
}

