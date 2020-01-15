

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
		/** [Fee.Pool.PoolItem_Base]プールアイテムをメモリから削除。
		*/
		void OnPoolItemDeleteFromMemory();
	}
}

