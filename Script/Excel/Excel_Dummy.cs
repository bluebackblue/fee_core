

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
	/** Excel_Dummy
	*/
	public class Excel_Dummy : Excel_Base
	{
		/** constructor
		*/
		public Excel_Dummy()
		{
		}

		/** 開く。
		*/
		public bool ReadOpen(Fee.File.Path a_path)
		{
			Tool.Assert(false);
			return false;
		}

		/** 閉じる。
		*/
		public void Close()
		{
		}

		/** シート数。取得。
		*/
		public int GetSheetCount()
		{
			Tool.Assert(false);
			return 0;
		}

		/** アクティブシート。設定。
		*/
		public bool SetActiveSheet(int a_sheet_index)
		{
			Tool.Assert(false);
			return false;
		}

		/** アクティブセル。設定。
		*/
		public bool SetActiveCell(int a_x,int a_y)
		{
			Tool.Assert(false);
			return false;
		}

		/** 文字列。取得。
		*/
		public bool GetTryCellString(out string a_result_value)
		{
			a_result_value = null;
			return false;
		}

		/** 文字列。取得。
		*/
		public bool GetTryCellNumeric(out double a_result_value)
		{
			a_result_value = 0.0;
			return false;
		}
	}
}

