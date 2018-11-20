using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ネットワーク。自動接続。
*/


/** NNetwork
*/
#if USE_PUN
namespace NNetwork
{
	/** MonoBehaviour_Connect_Auto
	*/
	public class MonoBehaviour_Connect_Auto : Photon.Pun.MonoBehaviourPunCallbacks
	{
		/** 接続。切断。
		*/
		public bool result_connected;
		public bool result_disconnected;

		/** マスターへの接続。
		*/
		public bool result_connected_to_master;

		/** 部屋参加。
		*/
		public bool result_joinroom_fix;
		public bool result_joinroom_failed;

		/** 部屋作成。
		*/
		public bool result_createroom_fix;
		public bool result_createroom_failed;

		/** Start
		*/
		private void Start()
		{
		}

		/** ResetAll
		*/
		public void ResetAll()
		{
			this.result_connected = false;
			this.result_disconnected = false;

			this.result_connected_to_master = false;

			this.result_joinroom_fix = false;
			this.result_joinroom_failed = false;

			this.result_createroom_fix = false;
			this.result_createroom_failed = false;
		}

		/** ResetCreateJoinRoom
		*/
		public void ResetCreateJoinRoom()
		{
			this.result_joinroom_fix = false;
			this.result_joinroom_failed = false;

			this.result_createroom_fix = false;
			this.result_createroom_failed = false;
		}

		/** 接続。
		*/
		public override void OnConnected()
		{
			Tool.Log("AutoJoinRandomRoom","OnConnected");

			this.result_connected = true;
		}

		/** 切断。
		*/
		public override void OnDisconnected(Photon.Realtime.DisconnectCause a_disconnect_cause)
		{
			Tool.Log("AutoJoinRandomRoom","OnDisconnected : " + a_disconnect_cause.ToString());

			this.result_disconnected = true;
		}

		//--------------------------------------

		/** マスターへの接続成功。
		*/
		public override void OnConnectedToMaster()
		{
			Tool.Log("AutoJoinRandomRoom","OnConnectedToMaster");

			this.result_connected_to_master = true;
		}

		//--------------------------------------

		/** 部屋参加成功。
		*/
		public override void OnJoinedRoom()
		{
			Tool.Log("AutoJoinRandomRoom","OnJoinedRoom");

			this.result_joinroom_fix = true;
		}

		/** 部屋参加失敗。
		*/
		public override void OnJoinRoomFailed(short a_return_code,string a_message)
		{
			Tool.Log("AutoJoinRandomRoom","OnJoinRoomFailed : " + a_return_code.ToString() + " : " + a_message);

			this.result_joinroom_failed = true;
		}

		/** ランダム部屋参加失敗。
		*/
		public override void OnJoinRandomFailed(short a_return_code,string a_message)
		{
			Tool.Log("AutoJoinRandomRoom","OnJoinRandomFailed : " + a_return_code.ToString() + " : " + a_message);

			this.result_joinroom_failed = true;

			/*
			if(Photon.Realtime.ErrorCode.NoRandomMatchFound == a_return_code){
				//空き部屋がなかった。
			}else{
				//不明。
			}
			*/
		}

		//--------------------------------------

		/** 部屋作成成功。
		*/
		public override void OnCreatedRoom()
		{
			Tool.Log("AutoJoinRandomRoom","OnCreatedRoom");

			this.result_createroom_fix = true;
		}

		/** 部屋作成失敗。
		*/
		public override void OnCreateRoomFailed(short a_return_code,string a_message)
		{
			Tool.Log("AutoJoinRandomRoom","OnCreateRoomFailed : " + a_return_code.ToString() + " : " + a_message);

			this.result_createroom_failed = true;
		}

		//--------------------------------------

		/** 部屋退室。
		*/
		public override void OnLeftRoom()
		{
			Tool.Log("AutoJoinRandomRoom","OnLeftRoom");
		}

		//--------------------------------------

		/** OnMasterClientSwitched
		*/
		public override void OnMasterClientSwitched(Photon.Realtime.Player a_new_master)
		{
			Tool.Log("AutoJoinRandomRoom","OnMasterClientSwitched : " + a_new_master.NickName);
		}

		/** OnJoinedLobby
		*/
		public override void OnJoinedLobby()
		{
			Tool.Log("AutoJoinRandomRoom","OnJoinedLobby");
		}

		/** OnLeftLobby
		*/
		public override void OnLeftLobby()
		{
			Tool.Log("AutoJoinRandomRoom","OnLeftLobby");
		}

		/** OnRegionListReceived
		*/
		public override void OnRegionListReceived(Photon.Realtime.RegionHandler a_region_handler)
		{
			Tool.Log("AutoJoinRandomRoom","OnRegionListReceived");

			if(a_region_handler.EnabledRegions != null){
				for(int ii=0;ii<a_region_handler.EnabledRegions.Count;ii++){
					Tool.Log("AutoJoinRandomRoom","OnRegionListReceived : " + a_region_handler.EnabledRegions[ii].ToString());
				}
			}
		}

		/** OnRoomListUpdate
		*/
		public override void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> a_roomlist)
		{
			Tool.Log("AutoJoinRandomRoom","OnRoomListUpdate");			

			if(a_roomlist != null){
				for(int ii=0;ii<a_roomlist.Count;ii++){
					Tool.Log("AutoJoinRandomRoom","OnRoomListUpdate : " + a_roomlist[ii].Name);
				}
			}
		}

		/** OnPlayerEnteredRoom
		*/
		public override void OnPlayerEnteredRoom(Photon.Realtime.Player a_new_player)
		{
			Tool.Log("AutoJoinRandomRoom","OnPlayerEnteredRoom : " + a_new_player.NickName);
		}

		/** OnPlayerLeftRoom
		*/
		public override void OnPlayerLeftRoom(Photon.Realtime.Player a_left_player)
		{
			Tool.Log("AutoJoinRandomRoom","OnPlayerLeftRoom : " + a_left_player.NickName);
		}

		/** ルームのカスタムプロパティが変更された。
		*/
		public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable a_properties)
		{
			Tool.Log("AutoJoinRandomRoom","OnRoomPropertiesUpdate");
		}

		/** プレイヤープロパティが変更された。
		*/
		public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player a_player,ExitGames.Client.Photon.Hashtable a_properties)
		{
			Tool.Log("AutoJoinRandomRoom","OnPlayerPropertiesUpdate : " + a_player.NickName);
		}

		/** FindFriendsの結果。
		*/
		public override void OnFriendListUpdate(List<Photon.Realtime.FriendInfo> a_friend_list)
		{
			Tool.Log("AutoJoinRandomRoom","OnFriendListUpdate");

			if(a_friend_list != null){
				for(int ii=0;ii<a_friend_list.Count;ii++){
					Tool.Log("AutoJoinRandomRoom","OnFriendListUpdate : " + a_friend_list[ii].UserId);
				}
			}
		}

		/** Custom Authentication serviceからの結果。
		*/
		public override void OnCustomAuthenticationResponse(Dictionary<string,object> data)
		{
			Tool.Log("AutoJoinRandomRoom","OnCustomAuthenticationResponse");
		}
	}
}
#endif

