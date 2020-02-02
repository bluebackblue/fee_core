

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief エクセル。
*/


/** USE_DEF_FEE_EDITOR_EXCELDATAREADER
*/
#if((UNITY_EDITOR)&&(USE_DEF_FEE_EDITOR_EXCELDATAREADER))
	#define USE_DEF_FEE_EXCELDATAREADER
#endif


/** Fee.Excel
*/
namespace Fee.Excel
{
	/** Excel_ExcelDataReader
	*/
	#if(USE_DEF_FEE_EXCELDATAREADER)
	public class Excel_ExcelDataReader : Excel_Base
	{
		/** raw_excel
		*/
		private System.Data.DataSet raw_excel;

		/** raw_sheet
		*/
		private System.Data.DataTable raw_sheet;

		/** raw_line
		*/
		private System.Data.DataRow raw_line;

		/** raw_cell
		*/
		private System.Object raw_cell;

		/** constructor
		*/
		public Excel_ExcelDataReader()
		{
			//raw_excel
			this.raw_excel = null;

			//raw_sheet
			this.raw_sheet = null;

			//raw_line
			this.raw_line = null;

			//raw_cell
			this.raw_cell = null;
		}

		/** 開く。
		*/
		public bool ReadOpen(Fee.File.Path a_assets_path)
		{
			this.raw_excel = Excel_ExcelDataReader_Tool.Open(a_assets_path);
			if(this.raw_excel == null){
				return false;
			}

			return true;
		}

		/** 閉じる。
		*/
		public void Close()
		{
			this.raw_excel = null;
			this.raw_sheet = null;
			this.raw_line = null;
			this.raw_cell = null;
		}

		/** シート数。取得。
		*/
		public int GetSheetCount()
		{
			return Excel_ExcelDataReader_Tool.GetSheetCount(this.raw_excel);
		}

		/** アクティブシート。設定。
		*/
		public bool SetActiveSheet(int a_sheet_index)
		{
			this.raw_sheet = Excel_ExcelDataReader_Tool.GetSheet(this.raw_excel,a_sheet_index);
			if(this.raw_sheet == null){
				Tool.Assert(false);
				return false;
			}

			return true;
		}

		/** アクティブセル。設定。
		*/
		public bool SetActiveCell(int a_x,int a_y)
		{
			this.raw_line = Excel_ExcelDataReader_Tool.GetLine(this.raw_sheet,a_y);

			if(this.raw_line == null){
				//データのないライン。
				this.raw_cell = null;
			}else{
				this.raw_cell = Excel_ExcelDataReader_Tool.GetCell(this.raw_line,a_x);
			}

			return true;
		}

		/** 文字列。取得。
		*/
		public bool GetTryCellString(out string a_result_value)
		{
			return Excel_ExcelDataReader_Tool.GetTryCellString(this.raw_cell,out a_result_value);
		}

		/** 文字列。取得。
		*/
		public bool GetTryCellNumeric(out double a_result_value)
		{
			return Excel_ExcelDataReader_Tool.GetTryCellNumeric(this.raw_cell,out a_result_value);
		}
	}
	#endif
}

