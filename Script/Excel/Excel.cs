

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief エクセル。
*/


/** USE_DEF_FEE_NPOI
*/
#if((UNITY_EDITOR)&&(USE_DEF_EDITOR_FEE_NPOI))
	#define USE_DEF_FEE_NPOI
#endif



/** USE_DEF_FEE_EDITOR_EXCELDATAREADER
*/
#if((UNITY_EDITOR)&&(USE_DEF_FEE_EDITOR_EXCELDATAREADER))
	#define USE_DEF_FEE_EXCELDATAREADER
#endif



/** Fee.Excel
*/
namespace Fee.Excel
{
	/** Excel
	*/
	public class Excel
	{
		#if(USE_DEF_FEE_NPOI)


		/** raw_excel
		*/
		private NPOI.SS.UserModel.IWorkbook raw_excel;

		/** raw_sheet
		*/
		private NPOI.SS.UserModel.ISheet raw_sheet;

		/** raw_line
		*/
		private NPOI.SS.UserModel.IRow raw_line;

		/** raw_cell
		*/
		private NPOI.SS.UserModel.ICell raw_cell;


		#elif(USE_DEF_FEE_EXCELDATAREADER)


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


		#endif

		/** constructor
		*/
		public Excel()
		{
			#if(USE_DEF_FEE_NPOI)
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
			#elif(USE_DEF_FEE_EXCELDATAREADER)
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
			#endif
		}

		/** 開く。
		*/
		public bool ReadOpen(Fee.File.Path a_path)
		{
			#if(USE_DEF_FEE_NPOI)
			{
				this.raw_excel = Excel_Npoi.Open(a_path);
				if(this.raw_excel == null){
					return false;
				}

				return true;
			}
			#elif(USE_DEF_FEE_EXCELDATAREADER)
			{
				this.raw_excel = Excel_ExcelDataReader.Open(a_path);
				if(this.raw_excel == null){
					return false;
				}

				return true;
			}
			#else
			{
				Tool.Assert(false);
				return false;
			}
			#endif
		}

		/** 閉じる。
		*/
		public void Close()
		{
			#if(USE_DEF_FEE_NPOI)
			{
				this.raw_excel = null;
				this.raw_sheet = null;
				this.raw_line = null;
				this.raw_cell = null;
			}
			#elif(USE_DEF_FEE_EXCELDATAREADER)
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
			#if(USE_DEF_FEE_NPOI)
			{
				return Excel_Npoi.GetSheetCount(this.raw_excel);
			}
			#elif(USE_DEF_FEE_EXCELDATAREADER)
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
			#if(USE_DEF_FEE_NPOI)
			{
				this.raw_sheet = Excel_Npoi.GetSheet(this.raw_excel,a_sheet_index);
				if(this.raw_sheet == null){
					Tool.Assert(false);
					return false;
				}

				return true;
			}
			#elif(USE_DEF_FEE_EXCELDATAREADER)
			{
				this.raw_sheet = Excel_ExcelDataReader.GetSheet(this.raw_excel,a_sheet_index);
				if(this.raw_sheet == null){
					Tool.Assert(false);
					return false;
				}

				return true;
			}
			#else
			{
				Tool.Assert(false);
				return false;
			}
			#endif
		}

		/** アクティブセル。設定。
		*/
		public bool SetActiveCell(int a_x,int a_y)
		{
			#if(USE_DEF_FEE_NPOI)
			{
				this.raw_line = Excel_Npoi.GetLine(this.raw_sheet,a_y);

				if(this.raw_line == null){
					//データのないライン。
					this.raw_cell = null;
				}else{
					this.raw_cell = Excel_Npoi.GetCell(this.raw_line,a_x);
				}

				return true;
			}
			#elif(USE_DEF_FEE_EXCELDATAREADER)
			{
				this.raw_line = Excel_ExcelDataReader.GetLine(this.raw_sheet,a_y);

				if(this.raw_line == null){
					//データのないライン。
					this.raw_cell = null;
				}else{
					this.raw_cell = Excel_ExcelDataReader.GetCell(this.raw_line,a_x);
				}

				return true;
			}
			#else
			{
				Tool.Assert(false);
				return false;
			}
			#endif
		}

		/** 文字列。取得。
		*/
		public bool GetTryCellString(out string a_result_value)
		{
			#if(USE_DEF_FEE_NPOI)
			{
				return Excel_Npoi.GetTryCellString(this.raw_cell,out a_result_value);
			}
			#elif(USE_DEF_FEE_EXCELDATAREADER)
			{
				return Excel_ExcelDataReader.GetTryCellString(this.raw_cell,out a_result_value);
			}
			#else
			{
				a_result_value = null;
				return false;
			}
			#endif
		}

		/** 文字列。取得。
		*/
		public bool GetTryCellNumeric(out double a_result_value)
		{
			#if(USE_DEF_FEE_NPOI)
			{
				return Excel_Npoi.GetTryCellNumeric(this.raw_cell,out a_result_value);
			}
			#elif(USE_DEF_FEE_EXCELDATAREADER)
			{
				return Excel_ExcelDataReader.GetTryCellNumeric(this.raw_cell,out a_result_value);
			}
			#else
			{
				a_result_value = 0.0;
				return false;
			}
			#endif
		}
	}
}

