

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。
*/


/** Fee.Network
*/
#if(USE_DEF_FEE_PUN)
namespace Fee.Network
{
	/** Pun_MonoBehaviour
	*/
	public class Pun_MonoBehaviour : Photon.Pun.MonoBehaviourPunCallbacks
	{
		/** pun
		*/
		public Pun pun;

		/** マスター。接続。リージョン受信。
		*/
		public override void OnRegionListReceived(Photon.Realtime.RegionHandler a_region_handler)
		{
			this.pun.OnPunRegionListReceived(a_region_handler);
		}

		/** マスター。接続。低レベル接続。
		*/
		public override void OnConnected()
		{
			//this.pun.OnPunConnected();
		}

		/** マスター。接続。成功。
		*/
		public override void OnConnectedToMaster()
		{
			this.pun.OnPunConnectedToMaster();
		}

		/** マスター。切断。失敗。
		*/
		public override void OnDisconnected(Photon.Realtime.DisconnectCause a_cause)
		{
			this.pun.OnPunDisconnected(a_cause);
		}

		/** ロビー。接続。成功。
		*/
		public override void OnJoinedLobby()
		{
			this.pun.OnPunJoinedLobby();
		}
 
		/** ロビー。切断。失敗。
		*/
		public override void OnLeftLobby()
		{
			this.pun.OnPunLeftLobby();
		}

		/** ロビー。ルームリスト。取得。
		*/
		public override void OnRoomListUpdate(System.Collections.Generic.List<Photon.Realtime.RoomInfo> a_room_list)
		{
			this.pun.OnPunRoomListUpdate(a_room_list);
		}

		/** ルーム。作成。成功。
		*/
		public override void OnCreatedRoom()
		{
			//this.pun.OnPunCreatedRoom();
		}

		/** ルーム。作成。失敗。
		*/ 
		public override void OnCreateRoomFailed(short a_return_code,string a_message)
		{
			this.pun.OnPunCreateRoomFailed(a_return_code,a_message);
		}

		/** ルーム。接続。成功。
		*/
		public override void OnJoinedRoom()
		{
			this.pun.OnPunJoinedRoom();
		}
 
		/** ルーム。接続。失敗。
		*/
		public override void OnJoinRoomFailed(short a_return_code,string a_message)
		{
			this.pun.OnPunJoinRoomFailed(a_return_code,a_message);
		}

		/** ルーム。接続。失敗。
		*/ 
		public override void OnJoinRandomFailed(short a_return_code, string message)
		{
			this.pun.OnPunJoinRandomFailed(a_return_code,message);
		}

		/** ルーム。切断。失敗。
		*/ 
		public override void OnLeftRoom()
		{
			this.pun.OnPunLeftRoom();
		}

		/** カスタム認証。レスポンス。
		*/
		public override void OnCustomAuthenticationResponse(System.Collections.Generic.Dictionary<string,object> a_data)
		{
			//this.pun.OnPunCustomAuthenticationResponse(a_data);
		}
 
		/** カスタム認証。失敗。
		*/
		public override void OnCustomAuthenticationFailed(string a_message)
		{
			//this.pun.OnPunCustomAuthenticationFailed(a_message);
		}

		/** ロビー。更新。
		*/
		public override void OnLobbyStatisticsUpdate(System.Collections.Generic.List<Photon.Realtime.TypedLobbyInfo> a_lobby_statistics)
		{
			//this.pun.OnPunLobbyStatisticsUpdate(a_lobby_statistics);
		}
 
		/** 部屋。プレイヤー。入場。
		*/
		public override void OnPlayerEnteredRoom(Photon.Realtime.Player a_player)
		{
			//this.pun.OnPunPlayerEnteredRoom(a_player);
		}
 
		/** 部屋。プレイヤー。退場。
		*/
		public override void OnPlayerLeftRoom(Photon.Realtime.Player a_player)
		{
			//this.pun.OnPunPlayerLeftRoom(a_player);
		}
 
		/** 部屋。マスタークライアント。変更。
		*/
		public override void OnMasterClientSwitched(Photon.Realtime.Player a_player)
		{
			//this.pun.OnPunMasterClientSwitched(a_player);
		}

		/** 部屋。ルームプロパティ。変更。
		*/
		public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable a_properties_that_changed)
		{
			//this.pun.OnPunRoomPropertiesUpdate(a_properties_that_changed);
		}
 
		/** 部屋。プレイヤープロパティ。変更。
		*/
		public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player a_player,ExitGames.Client.Photon.Hashtable a_changed_props)
		{
			//this.pun.OnPunPlayerPropertiesUpdate(a_player,a_changed_props);
		}
 
		/** フレンドリスト。変更。
		*/
		public override void OnFriendListUpdate(System.Collections.Generic.List<Photon.Realtime.FriendInfo> a_friend_list)
		{
			//this.pun.OnPunFriendListUpdate(a_friend_list);
		}

		/** RPC
		*/ 
		public override void OnWebRpcResponse(ExitGames.Client.Photon.OperationResponse a_response)
		{
			//this.pun.OnPunWebRpcResponse(a_response);
		}
	}
}
#endif

