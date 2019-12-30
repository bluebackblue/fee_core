

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
	/** Excel_Base
	*/
	public interface Excel_Base
	{
		/** 開く。
		*/
		bool ReadOpen(Fee.File.Path a_path);

		/** 閉じる。
		*/
		void Close();

		/** シート数。取得。
		*/
		int GetSheetCount();

		/** アクティブシート。設定。
		*/
		bool SetActiveSheet(int a_sheet_index);

		/** アクティブセル。設定。
		*/
		bool SetActiveCell(int a_x,int a_y);

		/** 文字列。取得。
		*/
		bool GetTryCellString(out string a_result_value);

		/** 文字列。取得。
		*/
		bool GetTryCellNumeric(out double a_result_value);
	}
}

