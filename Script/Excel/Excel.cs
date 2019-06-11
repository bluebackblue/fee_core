

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief エクセル。
*/


/** Fee.Excel
*/
namespace Fee.Excel
{
	/** Excel
	*/
	public class Excel
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

		/** raw_excel
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.IWorkbook raw_excel;
		#elif(USE_DEF_EXCELDATAREADER)
		private System.Data.DataSet raw_excel;
		#endif

		/** raw_sheet
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.ISheet raw_sheet;
		#elif(USE_DEF_EXCELDATAREADER)
		private System.Data.DataTable raw_sheet;
		#endif

		/** raw_line
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.IRow raw_line;
		#elif(USE_DEF_EXCELDATAREADER)
		private System.Data.DataRow raw_line;
		#endif

		/** raw_cell
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.ICell raw_cell;
		#elif(USE_DEF_EXCELDATAREADER)
		private object raw_cell;
		#endif

		/** constructor
		*/
		public Excel()
		{
			//raw_workbook
			#if(USE_DEF_NPOI)
			this.raw_excel = null;
			#elif(USE_DEF_EXCELDATAREADER)
			this.raw_excel = null;
			#endif

			//raw_sheet
			#if(USE_DEF_NPOI)
			this.raw_sheet = null;
			#elif(USE_DEF_EXCELDATAREADER)
			this.raw_sheet = null;
			#endif

			//raw_line
			#if(USE_DEF_NPOI)
			this.raw_line = null;
			#elif(USE_DEF_EXCELDATAREADER)
			this.raw_line = null;
			#endif

			//raw_cell
			#if(USE_DEF_NPOI)
			this.raw_cell = null;
			#elif(USE_DEF_EXCELDATAREADER)
			this.raw_cell = null;
			#endif
		}

		/** 開く。
		*/
		public bool ReadOpen(Fee.File.Path a_path)
		{
			#if(USE_DEF_NPOI)
			{
				this.raw_excel = Excel_Npoi.Open(a_path);
				if(this.raw_excel != null){
					return true;
				}
			}
			#elif(USE_DEF_EXCELDATAREADER)
			{
				this.raw_excel = Excel_ExcelDataReader.Open(a_path);
				if(this.raw_excel != null){
					return true;
				}
			}
			#else
			{
				Tool.Assert(false);
				return false;
			}
			#endif

			return false;
		}

		/** 閉じる。
		*/
		public void Close()
		{
			#if(USE_DEF_NPOI)
			{
				this.raw_excel = null;
				this.raw_sheet = null;
				this.raw_line = null;
				this.raw_cell = null;
			}
			#elif(USE_DEF_EXCELDATAREADER)
			{
				this.raw_excel = null;
				this.raw_sheet = null;
				this.raw_line = null;
				this.raw_cell = null;
			}
			#endif
		}

		/** シート数。取得。
		*/
		public int GetSheetCount()
		{
			#if(USE_DEF_NPOI)
			{
				return Excel_Npoi.GetSheetCount(this.raw_excel);
			}
			#elif(USE_DEF_EXCELDATAREADER)
			{
				return Excel_ExcelDataReader.GetSheetCount(this.raw_excel);
			}
			#else
			{
				Tool.Assert(false);
				return 0;
			}
			#endif
		}

		/** アクティブシート。設定。
		*/
		public bool SetActiveSheet(int a_sheet_index)
		{
			#if(USE_DEF_NPOI)
			{
				this.raw_sheet = Excel_Npoi.GetSheet(this.raw_excel,a_sheet_index);
				if(this.raw_sheet == null){
					Tool.Assert(false);
					return false;
				}
			}
			#elif(USE_DEF_EXCELDATAREADER)
			{
				this.raw_sheet = Excel_ExcelDataReader.GetSheet(this.raw_excel,a_sheet_index);
				if(this.raw_sheet == null){
					Tool.Assert(false);
					return false;
				}
			}
			#else
			{
				Tool.Assert(false);
				return false;
			}
			#endif

			return true;
		}

		/** アクティブセル。設定。
		*/
		public bool SetActiveCell(int a_x,int a_y)
		{
			#if(USE_DEF_NPOI)
			{
				this.raw_line = Excel_Npoi.GetLine(this.raw_sheet,a_y);
			}
			#elif(USE_DEF_EXCELDATAREADER)
			{
				this.raw_line = Excel_ExcelDataReader.GetLine(this.raw_sheet,a_y);
			}
			#else
			{
				Tool.Assert(false);
				return false;
			}
			#endif

			#if(USE_DEF_NPOI)
			{
				if(this.raw_line == null){
					//データのないライン。
					this.raw_cell = null;
				}else{
					this.raw_cell = Excel_Npoi.GetCell(this.raw_line,a_x);
				}
			}
			#elif(USE_DEF_EXCELDATAREADER)
			{
				if(this.raw_line == null){
					//データのないライン。
					this.raw_cell = null;
				}else{
					this.raw_cell = Excel_ExcelDataReader.GetCell(this.raw_line,a_x);
				}
			}
			#else
			{
				Tool.Assert(false);
				return false;
			}
			#endif

			return true;
		}

		/** セルタイプ。取得。
		*/
		public CellType GetCellType()
		{
			CellType t_celltype = CellType.None;

			#if(USE_DEF_NPOI)
			{
				if(this.raw_cell != null){
					t_celltype = Excel_Npoi.GetCellType(this.raw_cell);
				}
			}
			#elif(USE_DEF_EXCELDATAREADER)
			{
				if(this.raw_cell != null){
					t_celltype = Excel_ExcelDataReader.GetCellType(this.raw_cell);
				}
			}
			#endif

			return t_celltype;
		}

		/** 文字列。取得。
		*/
		public string GetCellString()
		{
			string t_value = null;

			#if(USE_DEF_NPOI)
			{
				if(this.raw_cell != null){
					t_value = Excel_Npoi.GetCellString(this.raw_cell);
				}
			}
			#elif(USE_DEF_EXCELDATAREADER)
			{
				if(this.raw_cell != null){
					t_value = Excel_ExcelDataReader.GetCellString(this.raw_cell);
				}
			}
			#endif

			return t_value;
		}

		/** 数値。取得。
		*/
		public double GetCellNumeric()
		{
			double t_value = 0.0;

			#if(USE_DEF_NPOI)
			{
				if(this.raw_cell != null){
					t_value = Excel_Npoi.GetCellNumeric(this.raw_cell);
				}
			}
			#elif(USE_DEF_EXCELDATAREADER)
			{
				if(this.raw_cell != null){
					t_value = Excel_ExcelDataReader.GetCellNumeric(this.raw_cell);
				}
			}
			#endif

			return t_value;
		}
	}
}

