

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
	/** NetworkObject_Player_Base
	*/
	public abstract class NetworkObject_Player_Base : UnityEngine.MonoBehaviour
	{
		/** self
		*/
		public bool self;

		/** sync
		*/
		public Sync_Base sync_player;
		public Sync_Base sync_status;

		/** Start
		*/
		private void Start()
		{
			this.self = this.sync_player.IsSelf();
			this.sync_player.SetSync(true);
			this.sync_status.SetSync(true);
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

