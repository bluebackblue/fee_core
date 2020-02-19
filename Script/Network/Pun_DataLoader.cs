

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ネットワーク。データローダー。
*/


/** Fee.Network
*/
#if(USE_DEF_FEE_PUN)
namespace Fee.Network
{
	/** Pun_DataLoader
	*/
	public class Pun_DataLoader : Photon.Pun.IPunPrefabPool
	{
		/** ID_NETWORKOBJECT_PLAYER
		*/
		public const string ID_NETWORKOBJECT_PLAYER = "network_player";

		/** プレイヤータイプ。
		*/
		private System.Type player_component_type;

		/** constructor
		*/
		public Pun_DataLoader()
		{
			this.player_component_type = null;
		}

		/** プレイヤータイプ。設定。
		*/
		public void SetPlayerComponent<T>()
			where T : NetworkObject_Player_Base
		{
			this.player_component_type = typeof(T);
		}

		/** プレイヤータイプ。解除。
		*/
		public void UnSetPlayerComponent()
		{
			this.player_component_type = null;
		}

		/** インスタンス。作成。
		*/
		public UnityEngine.GameObject Instantiate(string a_id,UnityEngine.Vector3 a_position,UnityEngine.Quaternion a_rotation)
		{
			UnityEngine.GameObject t_gameobject = null;

			switch(a_id){
			case Pun_DataLoader.ID_NETWORKOBJECT_PLAYER:
				{
					t_gameobject = new UnityEngine.GameObject(Pun_DataLoader.ID_NETWORKOBJECT_PLAYER);
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

						if(this.player_component_type != null){
							Fee.Network.NetworkObject_Player_Base t_networkobject = (Fee.Network.NetworkObject_Player_Base)t_gameobject.AddComponent(this.player_component_type);
							if(t_networkobject != null){
								t_sync_player.networkobject = t_networkobject;
								t_sync_status.networkobject = t_networkobject;
								t_networkobject.sync_player = t_sync_player;
								t_networkobject.sync_status = t_sync_status;
							}
						}
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
#endif

