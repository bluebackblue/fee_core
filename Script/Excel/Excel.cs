

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
		/** raw_npoi_excel
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.IWorkbook raw_npoi_excel;
		#endif

		/** raw_npoi_sheet
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.ISheet raw_npoi_sheet;
		#endif

		/** raw_npoi_line
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.IRow raw_npoi_line;
		#endif

		/** raw_npoi_cell
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.ICell raw_npoi_cell;
		#endif

		/** constructor
		*/
		public Excel()
		{
			//raw_npoi_workbook
			#if(USE_DEF_NPOI)
			this.raw_npoi_excel = null;
			#endif

			//raw_npoi_sheet
			#if(USE_DEF_NPOI)
			this.raw_npoi_sheet = null;
			#endif

			//raw_npoi_line
			#if(USE_DEF_NPOI)
			this.raw_npoi_line = null;
			#endif

			//raw_npoi_cell
			#if(USE_DEF_NPOI)
			this.raw_npoi_cell = null;
			#endif
		}

		/** 開く。
		*/
		public bool ReadOpen(Fee.File.Path a_path)
		{
			#if(USE_DEF_NPOI)
			{
				this.raw_npoi_excel = Excel_Npoi.Open(a_path);
				if(this.raw_npoi_excel != null){
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
				this.raw_npoi_excel = null;
				this.raw_npoi_sheet = null;
				this.raw_npoi_line = null;
				this.raw_npoi_cell = null;
			}
			#endif
		}

		/** シート数。取得。
		*/
		public int GetSheetCount()
		{
			#if(USE_DEF_NPOI)
			{
				return Excel_Npoi.GetSheetCount(this.raw_npoi_excel);
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
				this.raw_npoi_sheet = Excel_Npoi.GetSheet(this.raw_npoi_excel,a_sheet_index);
				if(this.raw_npoi_sheet == null){
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
				this.raw_npoi_line = Excel_Npoi.GetLine(this.raw_npoi_sheet,a_y);
			}
			#else
			{
				Tool.Assert(false);
				return false;
			}
			#endif

			#if(USE_DEF_NPOI)
			{
				if(this.raw_npoi_line == null){
					//データのないライン。
					this.raw_npoi_cell = null;
				}else{
					this.raw_npoi_cell = Excel_Npoi.GetCell(this.raw_npoi_line,a_x);
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
		public Excel_Npoi.CellType GetCellType()
		{
			Excel_Npoi.CellType t_celltype = Excel_Npoi.CellType.None;

			#if(USE_DEF_NPOI)
			if(this.raw_npoi_cell != null){
				t_celltype = Excel_Npoi.GetCellType(this.raw_npoi_cell);
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
			if(this.raw_npoi_cell != null){
				t_value = Excel_Npoi.GetCellString(this.raw_npoi_cell);
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
			if(this.raw_npoi_cell != null){
				t_value = Excel_Npoi.GetCellNumeric(this.raw_npoi_cell);
			}
			#endif

			return t_value;
		}
	}
}

