

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief エクセル。ＮＰＯＩ。
*/


/** Fee.Excel
*/
#if(USE_DEF_NPOI)
namespace Fee.Excel
{
	/** Excel_Npoi
	*/
	public class Excel_Npoi
	{
		/** 開く。
		*/
		public static NPOI.SS.UserModel.IWorkbook Open(Fee.File.Path a_path)
		{
			NPOI.SS.UserModel.IWorkbook t_workbook = null;

			try{
				t_workbook = NPOI.SS.UserModel.WorkbookFactory.Create(a_path.GetPath());
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			return t_workbook;
		}

		/** シート数。取得。
		*/
		public static int GetSheetCount(NPOI.SS.UserModel.IWorkbook a_workbook)
		{
			int t_count = 0;

			if(a_workbook != null){
				try{
					t_count = a_workbook.NumberOfSheets;
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}else{
				Tool.Assert(false);
			}

			return t_count;
		}

		/** セル。取得。
		*/
		public static NPOI.SS.UserModel.ICell GetCell(NPOI.SS.UserModel.IRow a_line,int a_x)
		{
			NPOI.SS.UserModel.ICell t_cell = null;

			if(a_line != null){
				if(0 <= a_x){
					try{
						t_cell = a_line.GetCell(a_x);
					}catch(System.Exception t_exception){
						Tool.LogError(t_exception);
					}
				}else{
					Tool.Assert(false);
				}
			}else{
				//データのないライン。
			}

			return t_cell;
		}

		/** ライン。取得。
		*/
		public static NPOI.SS.UserModel.IRow GetLine(NPOI.SS.UserModel.ISheet a_sheet,int a_y)
		{
			NPOI.SS.UserModel.IRow t_row = null;

			if(a_sheet != null){
				if(0 <= a_y){
					try{
						t_row = a_sheet.GetRow(a_y);
					}catch(System.Exception t_exception){
						Tool.LogError(t_exception);
					}
				}else{
					Tool.Assert(false);
				}
			}else{
				Tool.Assert(false);
			}

			return t_row;
		}

		/** シート。取得。
		*/
		public static NPOI.SS.UserModel.ISheet GetSheet(NPOI.SS.UserModel.IWorkbook a_workbook,int a_index)
		{
			NPOI.SS.UserModel.ISheet t_sheet = null;

			if(a_workbook != null){
				try{
					t_sheet = a_workbook.GetSheetAt(a_index);
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}else{
				Tool.Assert(false);
			}

			return t_sheet;
		}

		/** 文字列。取得。
		*/
		public static bool GetTryCellString(NPOI.SS.UserModel.ICell a_cell,out string a_result_value)
		{
			try{
				if(a_cell != null){
					if(a_cell.CellType == NPOI.SS.UserModel.CellType.String){
						string t_value_string = a_cell.StringCellValue;
						if(t_value_string != null){
							a_result_value = t_value_string;
							return true;
						}
					}else if(a_cell.CellType == NPOI.SS.UserModel.CellType.Numeric){
						double t_value_double = a_cell.NumericCellValue;
						a_result_value = t_value_double.ToString();
						return true;
					}else{
						//不明。
						Tool.Assert(false);
					}
				}else{
					//データのないセル。
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			//失敗。
			a_result_value = null;
			return false;
		}

		/** 数値。取得。
		*/
		public static bool GetTryCellNumeric(NPOI.SS.UserModel.ICell a_cell,out double a_result_value)
		{
			try{
				if(a_cell != null){
					if(a_cell.CellType == NPOI.SS.UserModel.CellType.Numeric){
						//数値へのキャッスト。
						a_result_value = a_cell.NumericCellValue;
						return true;
					}else if(a_cell.CellType == NPOI.SS.UserModel.CellType.String){
						string t_value_string = a_cell.StringCellValue;
						if(t_value_string != null){
							if(double.TryParse(t_value_string,out double t_value_double) == true){
								//数値へのパース。
								a_result_value = t_value_double;
								return true;
							}
						}
					}else{
						//不明。
						Tool.Assert(false);
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			//失敗。
			a_result_value = 0.0;
			return false;
		}
	}
}
#endif

