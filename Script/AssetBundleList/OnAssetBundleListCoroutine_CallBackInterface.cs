

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドルリスト。コールバックインターフェイス。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** OnAssetBundleListCoroutine_CallBackInterface
	*/
	public interface OnAssetBundleListCoroutine_CallBackInterface
	{
		/** [Fee.AssetBundleList.OnAssetBundleListCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnAssetBundleListCoroutine(float a_progress);
	}
}

