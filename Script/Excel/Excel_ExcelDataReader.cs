

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エクセル。エクセルデータリーダー。
*/


/** USE_DEF_FEE_EDITOR_EXCELDATAREADER
*/
#if((UNITY_EDITOR)&&(USE_DEF_FEE_EDITOR_EXCELDATAREADER))
	#define USE_DEF_FEE_EXCELDATAREADER
#endif


/** Fee.Excel
*/
#if(USE_DEF_FEE_EXCELDATAREADER)
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
				using(System.IO.FileStream t_stream = System.IO.File.Open(a_path.GetPath(),System.IO.FileMode.Open,System.IO.FileAccess.Read)){
					if(t_stream != null){
						using(ExcelDataReader.IExcelDataReader t_reader = ExcelDataReader.ExcelReaderFactory.CreateOpenXmlReader(t_stream)){
							if(t_reader != null){
								t_workbook = ExcelDataReader.ExcelDataReaderExtensions.AsDataSet(t_reader);
							}
						}
					}
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
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
					Tool.DebugReThrow(t_exception);
				}
			}else{
				Tool.Assert(false);
			}

			return t_count;
		}

		/** セル。取得。
		*/
		public static System.Object GetCell(System.Data.DataRow a_line,int a_x)
		{
			System.Object t_cell = null;

			if(a_line != null){
				if(0 <= a_x){
					try{
						if(a_x < a_line.ItemArray.Length){
							t_cell = a_line.ItemArray[a_x];	
						}else{
							//データのないセル。
						}
					}catch(System.Exception t_exception){
						Tool.DebugReThrow(t_exception);
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
						Tool.DebugReThrow(t_exception);
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
					Tool.DebugReThrow(t_exception);
				}
			}else{
				Tool.Assert(false);
			}

			return t_sheet;
		}

		/** 文字列。取得。
		*/
		public static bool GetTryCellString(System.Object a_cell,out string a_result_value)
		{
			try{
				if(a_cell != null){
					string t_value_string = a_cell.ToString();
					if(t_value_string != null){
						a_result_value = t_value_string;
						return true;
					}
				}else{
					//データのないセル。
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//失敗。
			a_result_value = null;
			return false;
		}

		/** 数値。取得。
		*/
		public static bool GetTryCellNumeric(System.Object a_cell,out double a_result_value)
		{
			try{
				if(a_cell != null){
					System.Type t_type = a_cell.GetType();
					if(t_type.IsValueType == true){
						//数値へのキャスト。
						a_result_value = (double)a_cell;
						return true;
					}else if(t_type == typeof(string)){
						string t_value_string = a_cell as string;
						if(t_value_string != null){
							if(double.TryParse(t_value_string,out double t_value_double) == true){
								//数値へのパース。
								a_result_value = t_value_double;
								return true;
							}
						}
					}else if(t_type == typeof(System.DBNull)){
						//データのないセル。
					}else{
						//不明。
						Tool.Assert(false);
					}
				}else{
					//データのないセル。
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}

			//失敗。
			a_result_value = 0.0;
			return false;
		}
	}
}
#endif

