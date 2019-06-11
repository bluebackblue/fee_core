

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief エクセル。ＮＰＯＩ。
*/


/** Fee.Excel
*/
#if(USE_DEF_EXCELDATAREADER)
namespace Fee.Excel
{
	/** Excel_ExcelDataReader
	*/
	public class Excel_ExcelDataReader
	{
		/** 開く。
		*/
		public static System.Data.DataSet Open(Fee.File.Path a_path)
		{
			System.Data.DataSet t_workbook = null;

			try{
				System.IO.FileStream t_stream = System.IO.File.Open(a_path.GetPath(),System.IO.FileMode.Open,System.IO.FileAccess.Read);
				if(t_stream != null){
					ExcelDataReader.IExcelDataReader t_reader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(t_stream);
					if(t_reader != null){
						t_workbook = ExcelDataReader.ExcelDataReaderExtensions.AsDataSet(t_reader);
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			return t_workbook;
		}

		/** シート数。取得。
		*/
		public static int GetSheetCount(System.Data.DataSet a_workbook)
		{
			int t_count = 0;

			if(a_workbook != null){
				try{
					t_count = a_workbook.Tables.Count;
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
		public static object GetCell(System.Data.DataRow a_line,int a_x)
		{
			object t_cell = null;

			if(a_line != null){
				if(0 <= a_x){
					try{
						t_cell = a_line.ItemArray[a_x];
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
		public static System.Data.DataRow GetLine(System.Data.DataTable a_sheet,int a_y)
		{
			System.Data.DataRow t_line = null;

			if(a_sheet != null){
				if(0 <= a_y){
					try{
						t_line = a_sheet.Rows[a_y];
					}catch(System.Exception t_exception){
						Tool.LogError(t_exception);
					}
				}else{
					Tool.Assert(false);
				}
			}else{
				Tool.Assert(false);
			}

			return t_line;
		}

		/** シート。取得。
		*/
		public static System.Data.DataTable GetSheet(System.Data.DataSet a_workbook,int a_index)
		{
			System.Data.DataTable t_sheet = null;

			if(a_workbook != null){
				try{
					t_sheet = a_workbook.Tables[a_index];
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
		public static Excel.CellType GetCellType(object a_cell)
		{
			return Excel.CellType.StringType;
		}

		/** 文字列。取得。
		*/
		public static string GetCellString(object a_cell)
		{
			string t_value = null;

			if(a_cell != null){
				try{
					t_value = a_cell.ToString();
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}else{
				//データのないセル。
			}

			return t_value;
		}

		/** 数値。取得。
		*/
		public static double GetCellNumeric(object a_cell)
		{
			//TODO:
			Tool.Assert(false);
			return 0.0;
		}
	}
}
#endif

