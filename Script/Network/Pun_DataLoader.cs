

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
			where T : NetworkObject_Player_MonoBehaviour_Base
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
					UnityEngine.GameObject.DontDestroyOnLoad(t_gameobject);
					{
						Sync t_sync = new Sync(t_gameobject);

						if(this.player_component_type != null){
							Fee.Network.NetworkObject_Player_MonoBehaviour_Base t_networkobject = (Fee.Network.NetworkObject_Player_MonoBehaviour_Base)t_gameobject.AddComponent(this.player_component_type);
							if(t_networkobject != null){
								t_sync.SetNetworkObject(t_networkobject);
								t_networkobject.SetSync(t_sync);
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

