

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＪＳＯＮシート。リストアイテム。
*/


/** Fee.JsonSheet
*/
namespace Fee.JsonSheet
{
	/** コンバートパラメータ。
	*/
	public class ConvertParam
	{
		/** アセットバンドル。作成。
		*/
		public bool create_assetbundle;

		/** ダミーアセットバンドル。作成。
		*/
		public bool create_dummy_assetbundle;

		/** リセット。
		*/
		public void Reset()
		{
			this.create_assetbundle = false;
			this.create_dummy_assetbundle = false;
		}
	}
}

