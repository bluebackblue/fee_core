

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プール。プールアイテム。
*/


/** Fee.Pool
*/
namespace Fee.Pool
{
	/** PoolItem_Base
	*/
	public interface PoolItem_Base
	{
		/** [Fee.Pool.PoolItem_Base]プールへ削除。

			タスクから呼び出される。

		*/
		void OnPoolDelete();

		/** [Fee.Pool.PoolItem_Base]メモリから削除。
		*/
		void OnMemoryDelete();
	}
}

