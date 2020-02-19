

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。
*/


/** Fee.Network
*/
namespace Fee.Network
{
	public class Creator_Base
	{
	}


	public class Pun_DataLoader : Photon.Pun.IPunPrefabPool
	{
		/** network_player_type
		*/
		public System.Type network_player_type;

		/** インスタンス。作成。
		*/
		public UnityEngine.GameObject Instantiate(string a_id,UnityEngine.Vector3 a_position,UnityEngine.Quaternion a_rotation)
		{
			UnityEngine.GameObject t_gameobject = null;

			switch(a_id){
			case "network_player":
				{
					t_gameobject = new UnityEngine.GameObject("network_player");
					t_gameobject.SetActive(false);
					{
						Pun_Sync_Player t_sync_player = t_gameobject.AddComponent<Pun_Sync_Player>();
						Pun_Sync_Status t_sync_status = t_gameobject.AddComponent<Pun_Sync_Status>();

						Photon.Pun.PhotonView t_view_player = t_gameobject.AddComponent<Photon.Pun.PhotonView>();
						Photon.Pun.PhotonView t_view_status = t_gameobject.AddComponent<Photon.Pun.PhotonView>();

						t_view_player.ObservedComponents = new System.Collections.Generic.List<UnityEngine.Component>();
						t_view_player.ObservedComponents.Add(t_sync_player);
						t_view_status.ObservedComponents = new System.Collections.Generic.List<UnityEngine.Component>();
						t_view_status.ObservedComponents.Add(t_sync_status);

						t_sync_player.view = t_view_player;
						t_sync_status.view = t_view_status;
					}

					Fee.Network.NetworkObject_Player_Base t_networkobject = (Fee.Network.NetworkObject_Player_Base)t_gameobject.AddComponent(this.network_player_type);
					{
						t_networkobject.sync_player = t_gameobject.GetComponent<Fee.Network.Pun_Sync_Player>();
						t_networkobject.sync_status = t_gameobject.GetComponent<Fee.Network.Pun_Sync_Status>();
						t_networkobject.sync_player.networkobject = t_networkobject;
						t_networkobject.sync_status.networkobject = t_networkobject;
					}
				}break;
			default:
				{
					UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>(a_id);
					t_prefab.SetActive(false);
					t_gameobject = UnityEngine.GameObject.Instantiate<UnityEngine.GameObject>(t_prefab,a_position,a_rotation);
				}break;
			}

			return t_gameobject;
		}

		/** インスタンス。削除。
		*/
		public void Destroy(UnityEngine.GameObject a_gameobject)
		{
			UnityEngine.GameObject.Destroy(a_gameobject);
		}
	}
}

