

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。ダミー。
*/


/** Fee.Network
*/
#if(!USE_DEF_FEE_PUN)
namespace Fee.Network
{
	namespace ExitGames.Client.Photon
	{
		public class Hashtable
		{
			public void Add(params System.Object[] a_param){}
		}
		public class OperationResponse{}
	}
	namespace Photon
	{
		namespace Pun
		{
			public abstract class MonoBehaviourPunCallbacks : UnityEngine.MonoBehaviour
			{
				public abstract void OnRegionListReceived(Photon.Realtime.RegionHandler a_region_handler);
				public abstract void OnConnected();
				public abstract void OnConnectedToMaster();
				public abstract void OnDisconnected(Photon.Realtime.DisconnectCause a_cause);
				public abstract void OnJoinedLobby();
				public abstract void OnLeftLobby();
				public abstract void OnRoomListUpdate(System.Collections.Generic.List<Photon.Realtime.RoomInfo> a_room_list);
				public abstract void OnCreatedRoom();
				public abstract void OnCreateRoomFailed(short a_return_code,string a_message);
				public abstract void OnJoinedRoom();
				public abstract void OnJoinRoomFailed(short a_return_code,string a_message);
				public abstract void OnJoinRandomFailed(short a_return_code,string a_message);
				public abstract void OnLeftRoom();
				public abstract void OnCustomAuthenticationResponse(System.Collections.Generic.Dictionary<string,object> a_data);
				public abstract void OnCustomAuthenticationFailed(string a_message);
				public abstract void OnLobbyStatisticsUpdate(System.Collections.Generic.List<Photon.Realtime.TypedLobbyInfo> a_lobby_statistics);
				public abstract void OnPlayerEnteredRoom(Photon.Realtime.Player a_player);
				public abstract void OnPlayerLeftRoom(Photon.Realtime.Player a_player);
				public abstract void OnMasterClientSwitched(Photon.Realtime.Player a_player);
				public abstract void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable a_properties_that_changed);
				public abstract void OnPlayerPropertiesUpdate(Photon.Realtime.Player a_player,ExitGames.Client.Photon.Hashtable a_changed_props);
				public abstract void OnFriendListUpdate(System.Collections.Generic.List<Photon.Realtime.FriendInfo> a_friend_list);
				public abstract void OnWebRpcResponse(ExitGames.Client.Photon.OperationResponse a_response);
			}
			public class PhotonNetwork
			{
				public static bool IsConnected;
				public static bool InLobby;
				public static bool InRoom;
				public static bool AutomaticallySyncScene;
				public static string NickName;
				public static string GameVersion;
				public static bool ConnectUsingSettings(){return true;}
				public static bool Disconnect(){return true;}
				public static bool JoinLobby(){return true;}
				public static bool LeaveLobby(){return true;}
				public static bool JoinOrCreateRoom(params System.Object[] a_param){return true;}
				public static bool LeaveRoom(){return true;}
				public static void Instantiate(params System.Object[] a_param){}
			}
			public interface IPunObservable
			{
			}
			public class PhotonStream
			{
			}
			public class PhotonMessageInfo
			{
			}
		}
		namespace Realtime
		{
			public class TypedLobby
			{
				public static TypedLobby Default;
			}
			public class RegionHandler
			{
				public System.Collections.Generic.List<Photon.Realtime.Region> EnabledRegions;
			}
			public class Player{}
			public class TypedLobbyInfo{}
			public class FriendInfo{}
			public class Region
			{
				public string Code;
				public string HostAndPort;
				public string Cluster;
				public string WasPinged;
				public string Ping;
			}
			public class RoomInfo
			{
				public bool RemovedFromList;
				public string Name;
				public int PlayerCount;
				public System.Collections.Generic.List<System.Collections.DictionaryEntry> CustomProperties;
			}
			public class RoomOptions
			{
				public int MaxPlayers;
				public bool IsVisible;
				public bool IsOpen;
				public ExitGames.Client.Photon.Hashtable CustomRoomProperties;
				public string[] CustomRoomPropertiesForLobby;
			}
			public enum DisconnectCause
			{
				ExceptionOnConnect,
				Exception,
				ServerTimeout,
				ClientTimeout,
				DisconnectByServerLogic,
				DisconnectByServerReasonUnknown,
				InvalidAuthentication,
				CustomAuthenticationFailed,
				AuthenticationTicketExpired,
				MaxCcuReached,
				InvalidRegion,
				OperationNotAllowedInCurrentState,
				DisconnectByClientLogic,
			}
		}
	}
}
#endif

