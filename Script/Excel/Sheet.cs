

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief エクセル。シート。
*/


/** Fee.Excel
*/
namespace Fee.Excel
{
	/** シート。
	*/
	public class Sheet
	{
		/** excel
		*/
		private Excel excel;

		/** sheet_index
		*/
		private int sheet_index;

		/** raw_sheet_instance
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.ISheet raw_sheet_instance;
		#endif

		/** constructor
		*/
		public Sheet(Excel a_excel,int a_sheet_index)
		{
			//excel
			this.excel = a_excel;
			
			//sheet_index
			this.sheet_index = a_sheet_index;

			//raw_sheet_instance
			#if(USE_DEF_NPOI)
			this.raw_sheet_instance = a_excel.GetRawSheetInstance(a_sheet_index);
			#endif
		}

		/** Close
		*/
		public void Close()
		{
			//excel
			this.excel = null;

			//sheet_index
			this.sheet_index = -1;

			//raw_sheet_instance
			#if(USE_DEF_NPOI)
			this.raw_sheet_instance = null;
			#endif
		}

		/** GetCell
		*/
		public string GetCell(int a_x,int a_y)
		{
			#if(USE_DEF_NPOI)
			if(this.raw_sheet_instance != null){
				try{
					NPOI.SS.UserModel.IRow t_row = this.raw_sheet_instance.GetRow(a_y);
					if(t_row != null){
						NPOI.SS.UserModel.ICell t_cell = t_row.GetCell(a_x);
						if(t_cell != null){
							if(t_cell.StringCellValue != null){
								return t_cell.StringCellValue;
							}
						}
					}
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}
			}
			#endif

			Tool.Assert(false);
			return "";
		}
	}
}

