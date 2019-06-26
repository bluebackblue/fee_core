

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief アセットバンドル。コールバックインターフェイス。
*/


/** Fee.AssetBundleList
*/
namespace Fee.AssetBundleList
{
	/** OnAssetBundleCoroutine_CallBackInterface
	*/
	public interface OnAssetBundleCoroutine_CallBackInterface
	{
		/** [Fee.AssetBundleList.OnAssetBundleCoroutine_CallBackInterface]コルーチン実行中。

			return == false : キャンセル。

		*/
		bool OnAssetBundleCoroutine(float a_progress);
	}
}

