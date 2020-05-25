

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。ネットワークオブジェクト。プレイヤー。
*/


/** Fee.Network
*/
namespace Fee.Network
{
	/** NetworkObject_Player_MonoBehaviour_Base
	*/
	public abstract class NetworkObject_Player_MonoBehaviour_Base : UnityEngine.MonoBehaviour
	{
		/** Sync
		*/
		private Sync sync;

		/** 設定。
		*/
		public void SetSync(Sync a_sync)
		{
			this.sync = a_sync;
		}

		/** IsSelf
		*/
		public bool IsSelf()
		{
			return this.sync.IsSelf();
		}

		/** Start

			CreatePlayer 或いは PrefabPool から作成される。

		*/
		private void Start()
		{
			//sync
			this.sync.OnStartNetworkObject();

			//OnConnect
			this.OnConnect();
		}

		/** OnDestroy
		*/
		public void OnDestroy()
		{
			this.OnDisconnect();
		}

		/** [Fee.Network.NetworkObject_Player_Base.OnConnect]プレイヤーが接続。
		*/
		public abstract void OnConnect();

		/** [Fee.Network.NetworkObject_Player_Base.OnDisconnect]プレイヤーが切断。
		*/
		public abstract void OnDisconnect();

		/** [Fee.Network.NetworkObject_Player_Base.OnSendPlayer]送信。
		*/
		public abstract void OnSendPlayer(Fee.Network.Stream_Base a_stream);

		/** [Fee.Network.NetworkObject_Player_Base.OnRecvPlayer]受信。
		*/
		public abstract void OnRecvPlayer(Fee.Network.Stream_Base a_stream);

		/** [Fee.Network.NetworkObject_Player_Base.OnSendStatus]送信。

			低インターバル。

		*/
		public abstract void OnSendStatus(Fee.Network.Stream_Base a_stream);

		/** [Fee.Network.NetworkObject_Player_Base.OnRecvStatus]受信。
		*/
		public abstract void OnRecvStatus(Fee.Network.Stream_Base a_stream);
	}
}

