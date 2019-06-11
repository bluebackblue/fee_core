

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
		/** CellType
		*/
		public enum CellType
		{
			/** None
			*/
			None,

			/** StringType
			*/
			StringType,

			/** NumericType
			*/
			NumericType,
		}

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

		/** セルタイプ。取得。
		*/
		public static CellType GetCellType(NPOI.SS.UserModel.ICell a_cell)
		{
			CellType t_celltype = CellType.None;

			if(a_cell != null){
				try{
					switch(a_cell.CellType){
					case NPOI.SS.UserModel.CellType.String:
						{
							t_celltype = CellType.StringType;
						}break;
					case NPOI.SS.UserModel.CellType.Numeric:
						{
							t_celltype = CellType.NumericType;
						}break;
					default:
						{
							t_celltype = CellType.None;
						}break;
					}
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}else{
				Tool.Assert(false);
			}
			
			return t_celltype;
		}

		/** 文字列。取得。
		*/
		public static string GetCellString(NPOI.SS.UserModel.ICell a_cell)
		{
			string t_value = null;

			if(a_cell != null){
				try{
					t_value = a_cell.StringCellValue;
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}else{
				Tool.Assert(false);
			}

			return t_value;
		}

		/** 数値。取得。
		*/
		public static double GetCellNumeric(NPOI.SS.UserModel.ICell a_cell)
		{
			double t_value = 0.0;

			if(a_cell != null){
				try{
					t_value = a_cell.NumericCellValue;
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}else{
				Tool.Assert(false);
			}

			return t_value;
		}
	}
}
#endif

