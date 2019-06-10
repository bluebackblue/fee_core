

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
		/** パス。
		*/
		private Fee.File.Path path;

		/** workbook
		*/
		#if(USE_DEF_NPOI)
		private NPOI.SS.UserModel.IWorkbook workbook;
		#endif

		/** sheet
		*/
		private Sheet sheet;

		/** constructor
		*/
		public Excel(Fee.File.Path a_path)
		{
			//path
			this.path = a_path;

			//workbook
			#if(USE_DEF_NPOI)
			this.workbook = null;
			#endif

			//sheet
			this.sheet = null;
		}

		/** 開く。
		*/
		public bool ReadOpen()
		{
			#if(USE_DEF_NPOI)
			{
				try{
					string t_path = this.path.GetPath();
					this.workbook = NPOI.SS.UserModel.WorkbookFactory.Create(t_path);
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}

				if(this.workbook != null){
					return true;
				}

				return false;
			}
			#else
			{
				return false;
			}
			#endif
		}

		/** 閉じる。
		*/
		public void Close()
		{
			if(this.sheet != null){
				this.sheet.Close();
				this.sheet = null;
			}

			#if(USE_DEF_NPOI)
			if(this.workbook != null){
				this.workbook = null;
			}
			#endif
		}

		/** シート数。取得。
		*/
		public int GetSheetMax()
		{
			#if(USE_DEF_NPOI)
			if(this.workbook != null){
				return this.workbook.NumberOfSheets;
			}
			#endif

			return 0;
		}

		/** シート名。取得。
		*/
		public string GetSheetName(int a_sheet_index)
		{
			#if(USE_DEF_NPOI)
			if(this.workbook != null){
				if((0 <= a_sheet_index)&&(a_sheet_index < this.workbook.NumberOfSheets)){
					return this.workbook.GetSheetName(a_sheet_index);
				}
			}
			#endif

			Tool.Assert(false);
			return null;
		}

		/** OpenSheet
		*/
		public bool OpenSheet(int a_sheet_index)
		{
			if(this.sheet != null){
				this.sheet.Close();
				this.sheet = null;
			}

			this.sheet = new Sheet(this,a_sheet_index);
			if(this.sheet.Open() == true){
				return true;
			}

			Tool.Assert(false);
			return false;
		}

		/** CloseSheet
		*/
		public void CloseSheet()
		{
			if(this.sheet != null){
				this.sheet.Close();
				this.sheet = null;
			}
		}

		/** GetCell
		*/
		public string GetCell(int a_x,int a_y)
		{
			if(this.sheet != null){
				return this.sheet.GetCell(a_x,a_y);
			}

			Tool.Assert(false);
			return "";
		}

		/** GetRawSheetInstance
		*/
		#if(USE_DEF_NPOI)
		public NPOI.SS.UserModel.ISheet GetRawSheetInstance(int a_sheet_index)
		{
			NPOI.SS.UserModel.ISheet t_sheet = this.workbook.GetSheetAt(a_sheet_index);
			if(t_sheet != null){
				return t_sheet;
			}

			Tool.Assert(false);
			return null;
		}
		#endif
	}
}

