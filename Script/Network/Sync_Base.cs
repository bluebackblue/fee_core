

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。ストリーム。
*/


/** Fee.Network
*/
namespace Fee.Network
{
	/** Sync_Base
	*/
	public interface Sync_Base
	{
		/** [Fee.Network.Sync_Base.IsSelf]自分自身。
		*/
		bool IsSelf();

		/** [Fee.Network.Sync_Base.IsSelf]同期。設定。
		*/
		void SetSync(bool a_flag);
	}
}

